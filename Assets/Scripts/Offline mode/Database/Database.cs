using System.Collections;
using Proyecto26;
using UnityEngine;
using UnityEngine.Networking;

public class Database : MonoBehaviour
{
    public string database_url = "";
    private User user;

    IEnumerator Start()
    {
        user = new User();
        user.timePlay = PlayerPrefs.GetInt("timePlay", 0);
        user.gobackCount = PlayerPrefs.GetInt("gobackCount", 0);
        user.gobackCount++;
        PlayerPrefs.SetInt("gobackCount", user.gobackCount);
        user.clickCount = PlayerPrefs.GetInt("clickCount", 0);
        user.maxScore = PlayerPrefs.GetInt("hight score", 0);
        user.maxLevel = PlayerPrefs.GetInt("maxLevel", 0);
        user.fxMusic = PlayerPrefs.GetFloat("fx", 1);
        user.bgMusic = PlayerPrefs.GetFloat("bg", 1);
        user.starCount = PlayerPrefs.GetInt("starCount", 0);
        DontDestroyOnLoad(this);
        user.ip = PlayerPrefs.GetString("ip", "");
        if (string.IsNullOrEmpty(user.ip))
        {
            user.ip = Random.Range(1, 10000) + ":" + Random.Range(1, 10000) + Random.Range(1, 10000) + ":" + Random.Range(1, 10000) + Random.Range(1, 10000) + ":" + Random.Range(1, 10000);
            while (true)
            {

                string url = database_url + user.ip + ".json";

                UnityWebRequest www = UnityWebRequest.Get(url);
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.Success)
                {
                    if (www.downloadHandler.text.Equals("null"))
                    {
                        PostData();
                        PlayerPrefs.SetString("ip", user.ip);
                        yield break;
                    }
                    else
                    {
                        user.ip = Random.Range(1, 10000) + ":" + Random.Range(1, 10000) + Random.Range(1, 10000) + ":" + Random.Range(1, 10000) + Random.Range(1, 10000) + ":" + Random.Range(1, 10000);
                        Debug.Log(www.downloadHandler.text);
                    }
                }
                else
                {
                    Debug.Log("Internet disconnected");
                }
            }
        }
        else
        {
            PostData();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            user.clickCount++;
            PlayerPrefs.SetInt("clickCount", user.clickCount);
            UpdateProperties("clickCount", user.clickCount);
        }
    }

    private void PostData()
    {
        RestClient.Put(database_url + user.ip + ".json", user).Then(res =>
        {
            StartCoroutine(UpdateTime());
        });
    }

    IEnumerator UpdateTime()
    {
        while (true)
        {
            user.timePlay++;
            yield return new WaitForSeconds(1);
            PlayerPrefs.SetInt("timePlay", user.timePlay);
            UpdateProperties("timePlay", user.timePlay);
        }
    }

    public void UpdateProperties(string key, float value)
    {
        if (string.IsNullOrEmpty(user.ip))
        {
            return;
        }
        try
        {
            RestClient.Put(database_url + user.ip + "/" + key + ".json",
            new Float(value));
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }
}