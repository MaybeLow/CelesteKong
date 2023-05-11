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
    
    // Initially, the class was supposed to read a json file.
    // However, the Unity build for this game did not read the json file.
    // Some parts of the method were commented out to keep the same functionality
    private void ReadAchievementData()
    {
        // Read the entire file into a string
        //string json = File.ReadAllText("Assets/StreamingAssets/Achievements.json");

        // Deserialize the JSON data into an instance of the MyData class
        //ParsedAchievements data = JsonConvert.DeserializeObject<ParsedAchievements>(json);

        int i = 0;
        foreach (GameObject achievementBlock in achievementBlocks)
        {
            TMP_Text text = achievementBlock.GetComponentInChildren<TMP_Text>();
            AchievementBlock block = achievementBlock.GetComponent<AchievementBlock>();
            //text.text = data.achievements[i].description;
            text.text = block.Description;
            if (!DataManager.UnlockedAchievements.Contains(i))
            {
                text.text += "\nLOCKED!";
            } else
            {
                text.text += "\nUNLOCKED!";
            }

            i++;
            if (i >= 3)
            {
                break;
            }
        }

        
    }
}
