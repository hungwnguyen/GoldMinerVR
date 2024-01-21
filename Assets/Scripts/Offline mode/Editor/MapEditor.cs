using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Linq;

[System.Serializable]
public class PrefabData
{
    public GameObject go;
    public string prefabPath;
    public Vector3 position;

    public PrefabData(GameObject go, string prefabPath, Vector3 position)
    {
        this.go = go;
        this.prefabPath = prefabPath;
        this.position = position;
    }
}

[System.Serializable]
public class LevelData
{
    public int level;
    public string id;
    public List<PrefabData> prefabDataList = new List<PrefabData>();
}

public class MapEditor : EditorWindow
{
    private string prefabPath = "Assets/Prefabs/Offline/Rod";
    private List<PrefabData> prefabList = new List<PrefabData>();
    private Dictionary<GameObject, int> spawnCounts = new Dictionary<GameObject, int>();
    private List<PrefabData> spawnPrefab = new List<PrefabData>();
    private int currentLevel = 1;
    private int loadLevel = 1;
    private string levelFolderPath = "Assets/Map/Level";
    private List<string> levelFiles = new List<string>();
    private List<int> availableLevels = new List<int>();
    private int selectedLevelFileIndex = -1;

    [MenuItem("Window/Create Map Tool")]
    public static void ShowWindow()
    {
        MapEditor window = EditorWindow.GetWindow<MapEditor>();
        window.minSize = new Vector2(800, 600);
        window.LoadPrefabs();
        window.Show();
    }

    private void OnEnable()
    {
        LoadAvailableLevels();
    }

    private void OnGUI()
    {
        GUILayout.Label("Random Prefab Spawner", EditorStyles.boldLabel);

        prefabPath = EditorGUILayout.TextField("Prefab Path", prefabPath);

        if (GUILayout.Button("Load Prefabs"))
        {
            LoadPrefabs();
        }

        EditorGUILayout.LabelField("Prefab List");
        EditorGUILayout.BeginVertical();
        EditorGUI.indentLevel++;

        if (prefabList != null)
        {
            for (int i = 0; i < prefabList.Count; i++)
            {
                GameObject prefab = prefabList[i].go;

                EditorGUILayout.BeginHorizontal();

                prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", prefab, typeof(GameObject), false);

                int count;
                if (spawnCounts.TryGetValue(prefab, out count))
                {
                    int newCount = EditorGUILayout.IntSlider("Spawn Count", count, 0, 100);

                    if (newCount != count)
                    {
                        newCount = Mathf.Clamp(newCount, 0, 100);
                        spawnCounts[prefab] = newCount;
                    }

                    if (GUILayout.Button("+", GUILayout.Width(25)))
                    {
                        count = Mathf.Min(count + 1, 100);
                        spawnCounts[prefab] = count;
                    }

                    if (GUILayout.Button("-", GUILayout.Width(25)))
                    {
                        count = Mathf.Max(count - 1, 0);
                        spawnCounts[prefab] = count;
                    }
                }
                else
                {
                    int newCount = EditorGUILayout.IntSlider("Spawn Count", 0, 0, 100);
                    if (newCount != 0)
                    {
                        newCount = Mathf.Clamp(newCount, 0, 100);
                        spawnCounts.Add(prefab, newCount);
                    }

                    if (GUILayout.Button("+", GUILayout.Width(25)))
                    {
                        newCount = Mathf.Min(newCount + 1, 100);
                        spawnCounts[prefab] = newCount;
                    }

                    if (GUILayout.Button("-", GUILayout.Width(25)))
                    {
                        newCount = Mathf.Max(newCount - 1, 0);
                        spawnCounts[prefab] = newCount;
                    }
                }

                EditorGUILayout.EndHorizontal();
            }
        }

        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical();

        if (GUILayout.Button("Spawn"))
        {
            SpawnPrefabs();
            foreach (var prefab in prefabList)
            {
                spawnCounts[prefab.go] = 0;
            }
        }

        EditorGUILayout.BeginHorizontal();
        currentLevel = EditorGUILayout.IntField("Level", currentLevel);

        bool isLevelValid = (currentLevel > 0);

        EditorGUI.BeginDisabledGroup(!isLevelValid);
        if (GUILayout.Button("Save To JSON"))
        {
            SaveToJson();
        }
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        loadLevel = EditorGUILayout.IntPopup("Level To Load", loadLevel, availableLevels.Select(x => x.ToString()).ToArray(), availableLevels.ToArray());
        if (GUILayout.Button("Load Levels"))
        {
            LoadLevelFiles();
        }
        EditorGUILayout.EndHorizontal();

        GUILayout.Label("Level Files");
        if (levelFiles.Count > 0)
        {
            for (int i = 0; i < levelFiles.Count; i++)
            {
                bool isSelected = (selectedLevelFileIndex == i);
                if (GUILayout.Button(Path.GetFileName(levelFiles[i]), GUILayout.ExpandWidth(true)))
                {
                    selectedLevelFileIndex = isSelected ? -1 : i;
                    LoadSelectedLevel();
                }
            }
        }
        else
        {
            GUILayout.Label("No level files found.");
        }

        if (GUILayout.Button("Clear All"))
        {
            ClearAllPrefabs();
        }
    }

    private void LoadPrefabs()
    {
        if (!string.IsNullOrEmpty(prefabPath))
        {
            string[] prefabGUIDs = AssetDatabase.FindAssets("t:Prefab", new string[] { prefabPath });

            prefabList.Clear();
            spawnCounts.Clear();
            foreach (string prefabGUID in prefabGUIDs)
            {
                string prefabPath = AssetDatabase.GUIDToAssetPath(prefabGUID);
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
                if (prefab != null)
                {
                    prefabList.Add(new PrefabData(prefab, prefabPath, Vector3.zero));
                    spawnCounts.Add(prefab, 0);
                }
            }
        }
        else
        {
            Debug.LogError("Prefab Path is empty!");
        }
    }

    private void SpawnPrefabs()
    {
        ClearAllPrefabs();

        if (prefabList != null && prefabList.Count > 0)
        {
            foreach (var prefab in prefabList)
            {
                int count;
                if (spawnCounts.TryGetValue(prefab.go, out count))
                {
                    for (int i = 0; i < count; i++)
                    {
                        GameObject spawnedPrefab = (GameObject)PrefabUtility.InstantiatePrefab(prefab.go);
                        spawnedPrefab.transform.position = Vector3.zero;
                        spawnPrefab.Add(new PrefabData(spawnedPrefab, prefab.prefabPath, prefab.position));
                    }
                }
            }
        }
        else
        {
            Debug.LogError("Prefab List is empty!");
        }
    }

    private void ClearAllPrefabs()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Rod");
        foreach (GameObject gameObject in gameObjects)
        {
            DestroyImmediate(gameObject);
        }
        spawnPrefab.Clear();
        selectedLevelFileIndex = -1;
    }

    private void LoadLevelFiles()
    {
        levelFolderPath = "Assets/Map/Level" + loadLevel;

        string[] files = Directory.GetFiles(levelFolderPath, "*.json");
        levelFiles.Clear();
        levelFiles.AddRange(files);
    }

    private void LoadSelectedLevel()
    {
        if (selectedLevelFileIndex >= 0 && selectedLevelFileIndex < levelFiles.Count)
        {
            string selectedFilePath = levelFiles[selectedLevelFileIndex];
            string jsonData = File.ReadAllText(selectedFilePath);

            LevelData levelData = JsonUtility.FromJson<LevelData>(jsonData);

            currentLevel = levelData.level;

            ClearAllPrefabs();

            foreach (var prefabData in levelData.prefabDataList)
            {
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabData.prefabPath);
                if (prefab != null)
                {
                    GameObject spawnedPrefab = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
                    spawnedPrefab.transform.position = prefabData.position;
                    spawnPrefab.Add(new PrefabData(spawnedPrefab, prefabData.prefabPath, prefabData.position));
                }
            }
        }
    }

    private void SaveToJson()
    {
        string mapFolderPath = "Assets/Map";
        if (!AssetDatabase.IsValidFolder(mapFolderPath))
        {
            AssetDatabase.CreateFolder("Assets", "Map");
        }

        string levelFolderPath = Path.Combine(mapFolderPath, "Level" + currentLevel.ToString());
        if (!AssetDatabase.IsValidFolder(levelFolderPath))
        {
            AssetDatabase.CreateFolder(mapFolderPath, "Level" + currentLevel.ToString());
        }

        string id = Guid.NewGuid().ToString();
        string jsonFileName = "Level" + currentLevel + "_" + id + ".json";
        string jsonFilePath = Path.Combine(levelFolderPath, jsonFileName);

        LevelData levelData = new LevelData();
        levelData.level = currentLevel;
        levelData.id = id;

        foreach (var prefab in spawnPrefab)
        {
            prefab.position = prefab.go.transform.position;
            levelData.prefabDataList.Add(prefab);
        }

        float progress = 0.0f;
        float progressStep = 1.0f / levelData.prefabDataList.Count;

        EditorUtility.DisplayProgressBar("Saving JSON", "Saving...", progress);

        using (StreamWriter streamWriter = new StreamWriter(jsonFilePath, false, System.Text.Encoding.UTF8))
        {
            string jsonData = JsonUtility.ToJson(levelData, true);

            streamWriter.Write(jsonData);

            progress += progressStep;
            EditorUtility.DisplayProgressBar("Saving JSON", "Saving...", progress);
        }

        EditorUtility.ClearProgressBar();

        if (!availableLevels.Contains(currentLevel))
        {
            availableLevels.Add(currentLevel);
            availableLevels.Sort();
        }

        Debug.Log("Level data saved to: " + jsonFilePath);
    }

    private void LoadAvailableLevels()
    {
        string mapFolderPath = "Assets/Map";
        if (AssetDatabase.IsValidFolder(mapFolderPath))
        {
            string[] levelDirectories = Directory.GetDirectories(mapFolderPath, "Level*");

            availableLevels.Clear();

            foreach (string directoryPath in levelDirectories)
            {
                string directoryName = Path.GetFileName(directoryPath);
                if (int.TryParse(directoryName.Replace("Level", ""), out int level))
                {
                    availableLevels.Add(level);
                }
            }
            availableLevels.Sort();
        }
    }

}
