using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class GameData : MonoBehaviour
{
    public Entity playerEntity => data.playerEntity;
    public List<Entity> enemyEntities => data.enemyEntities;

    public static GameData Instance { get; private set; }

    public List<IPersistedData> persistedDatas = new List<IPersistedData>();

    private string filePath => Application.persistentDataPath + "/GameData";
    private Data data = new Data();

    public void Initalize()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }

        if (File.Exists(filePath))
        {
            var gameData = JsonConvert.DeserializeObject<Data>(File.ReadAllText(filePath));
            data = gameData;
        }
        else
        {
            Clear();
        }
    }

    private void Start()
    {
        Load();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            Save();
        }
    }

#if UNITY_EDITOR
    private void OnApplicationQuit()
    {
        Save();
    }
#endif

    public void SetPlayerData(Entity entity)
    {
        data.playerEntity = entity;
    }

    public void Clear()
    {
        data.playerEntity = null;
        enemyEntities.Clear();

        WriteJson();
    }

    public void Save()
    {
        foreach (var item in persistedDatas)
        {
            item.Save();
        }

        WriteJson();
    }

    public void Load()
    {
        if (File.Exists(filePath))
        {
            var gameData = JsonConvert.DeserializeObject<Data>(File.ReadAllText(filePath));
            data = gameData;
        }
        else
        {
            Clear();
        }

        foreach (var item in persistedDatas)
        {
            item.Load();
        }
    }

    private void WriteJson()
    {
        var json = JsonConvert.SerializeObject(data, Formatting.Indented);

        File.WriteAllText(filePath, json);
    }

    [System.Serializable]
    public class Data
    {
        public Entity playerEntity = new Entity();
        public List<Entity> enemyEntities = new List<Entity>();
    }
}

public interface IPersistedData
{
    public abstract void Save();
    public abstract void Load();
}
