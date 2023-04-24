using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using UnityEngine;
using TMPro;

public class AchievementDisplay : MonoBehaviour
{
    [SerializeField] private List<GameObject> achievementBlocks;

    private void OnEnable()
    {
        ReadAchievementData();
    }
    
    private void ReadAchievementData()
    {
        // Read the entire file into a string
        string json = File.ReadAllText("Assets/Data/AchievementData/Achievements.json");

        // Deserialize the JSON data into an instance of the MyData class
        ParsedAchievements data = JsonConvert.DeserializeObject<ParsedAchievements>(json);

        int i = 0;
        foreach (GameObject achievementBlock in achievementBlocks)
        {
            TMP_Text text = achievementBlock.GetComponentInChildren<TMP_Text>();
            text.text = data.achievements[i].description;
            if (!DataManager.UnlockedAchievements.Contains(data.achievements[i].id))
            {
                text.text += "\nLOCKED!";
            }

            i++;
            if (i >= data.achievements.Length)
            {
                break;
            }
        }

        
    }
}
