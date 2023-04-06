using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveData
{
    public static void SaveGameData()
    {
        GameData data = new GameData(DataManager.GetCurrentProfileId());
        data.FillGameData();

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/gamedata" + data.GetProfileId() + ".lol";

        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void LoadGameData(int profileId)
    {
        GameData data;

        string path = Application.persistentDataPath + "/gamedata" + profileId + ".lol";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            data = formatter.Deserialize(stream) as GameData;
            DataManager.FillData(data);
            stream.Close();
        }
        else
        {
            data = new GameData(profileId);
            DataManager.FillData(data);
        }
    }
}
