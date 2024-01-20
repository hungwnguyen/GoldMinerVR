using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace yuki
{
    public class IO
    {
        private string _pathToPlayerAsset = "Assets/Data/PlayerData.asset";
        private string _pathToPlayerData = Path.Combine(Application.persistentDataPath, "playerData.json");

        public void SavePlayerData()
        {
            PlayerData playerData = AssetDatabase.LoadAssetAtPath<PlayerData>(_pathToPlayerAsset);

            if (playerData != null)
            {
                string json = JsonUtility.ToJson(playerData);
                File.WriteAllText(_pathToPlayerData, json);
            }
        }

        public void LoadPlayerData()
        {
            if (File.Exists(_pathToPlayerData))
            {
                string json = File.ReadAllText(_pathToPlayerData);

                PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);
                // TODO: Load player data
            }
            else
            {
                Debug.LogWarning("File not found. No data to load.");
            }
        }


    }
}
