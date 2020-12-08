using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData {



    public string playerType;
    public int currentLevelIndex;
    public List<int> upgradesUsed;
    public int numOfLives;

    public SaveData(PlayerType type, int index, List<int> used)
    {
        playerType = type.ToString();
        Debug.Log(playerType); 
        currentLevelIndex = index;
        upgradesUsed = used;
    }
}
