using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;


public class SaveManager : MonoBehaviour
{

    public static SaveManager instance;

    public SaveData activeSave;

    public bool hasLoaded;

    private void Awake()
    {
        instance = this;

        Load();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Save();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            DeleteSaveData();
        }
    }

    public void Save()
    {
        string dataPath = Application.persistentDataPath;

        var serializer = new XmlSerializer(typeof(SaveData));
        var stream = new FileStream(dataPath + "/" + activeSave.saveName + ".save", FileMode.Create);
        serializer.Serialize(stream, activeSave);
        stream.Close();

    }

    public void Load()
    {
        string dataPath = Application.persistentDataPath;

        if(System.IO.File.Exists(dataPath + "/" + activeSave.saveName + ".save"))
        {
            var serializer = new XmlSerializer(typeof(SaveData));
            var stream = new FileStream(dataPath + "/" + activeSave.saveName + ".save", FileMode.Open);
            activeSave = serializer.Deserialize(stream) as SaveData;
            stream.Close();

            hasLoaded = true;
        }
    }

    public void DeleteSaveData()
    {
        string dataPath = Application.persistentDataPath;

        if(System.IO.File.Exists(dataPath + "/" + activeSave.saveName + ".save"))
        {
            File.Delete(dataPath + "/" + activeSave.saveName + ".save");
        }
    }
}

[System.Serializable]
public class SaveData
{
    public string saveName;

    public Vector3 lastCheckPointPos;

    public int health;

    public int circleShards;
    public int squareShards;
    public int frogShards;

    public int gold;
    public int key;
    public int demonsHorn;

    public int readLetter;
    public int talkedAboutLetter;

    public bool enableWalljump = false;
    public bool gotHorn = false;

    public bool tpToCaveForest;
}
