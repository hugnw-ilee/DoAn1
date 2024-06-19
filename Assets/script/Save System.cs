using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;


public class SaveSystem : MonoBehaviour
{
    private List<ISavable> dataSavables;
    private static SaveSystem instance;

    private SaveSystem() {}

    public static SaveSystem Instance
    {
        get
        {
            if (instance == null)
            {
                // Tạo một GameObject mới nếu instance chưa tồn tại
                GameObject singletonObject = new ("SaveSystem");
                instance = singletonObject.AddComponent<SaveSystem>();
                DontDestroyOnLoad(singletonObject);
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    private string GetFilePath(string fileName)
    {
        return Path.Combine(Application.persistentDataPath, fileName);
    }

    private List<ISavable> FindAllSavableObject(){
        IEnumerable<ISavable> savables = FindObjectsOfType<MonoBehaviour>().OfType<ISavable>();
        return new List<ISavable>(savables);
    }

    public void SaveData(string fileName)
    {
        var dataSavables = FindAllSavableObject();
        Dictionary<string, object> dict = new();

        foreach (var savable in dataSavables)
        {
            dict[savable.GetID()] = savable.ToData();
        }

        var json = JsonConvert.SerializeObject(dict, Formatting.Indented);
        string path = GetFilePath(fileName);
        File.WriteAllText(path, json);
    }

    public void LoadData(string fileName)
    {
        string path = GetFilePath(fileName);
        string json = File.ReadAllText(path);
        var dict = JsonConvert.DeserializeObject<Dictionary<string, JObject>>(json);

        dataSavables = FindAllSavableObject();

        foreach (var c in dataSavables){
            if (dict.TryGetValue(c.GetID(),out JObject Data)){
                switch(c){
                    case Player:
                        PlayerData player = Data.ToObject<PlayerData>();
                        c.FromData(player);
                        break;
                    case Enemy:
                        EnemyData enemy = Data.ToObject<EnemyData>();
                        c.FromData(enemy);
                        break;
                }
            }

        }
    }
}
