using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;

public class ProfileButton : MonoBehaviour
{
    [SerializeField] int profileId;

    private void OnEnable()
    {
        LoadGameData(profileId);
    }

    public void LoadGameData(int profileId)
    {
        GameData data;
        TMP_Text text = GetComponentInChildren<TMP_Text>();

        string path = Application.persistentDataPath + "/gamedata" + profileId + ".lol";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            data = formatter.Deserialize(stream) as GameData;
            text.text = "Finished Levels: " + (data.FinishedLevels.Count).ToString();
            stream.Close();
        }
        else
        {
            text.text = "Finished Levels: 0";
        }
    }
}
