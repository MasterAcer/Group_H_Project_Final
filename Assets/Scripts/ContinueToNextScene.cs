using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class ContinueToNextScene : MonoBehaviour
{

    public string nextLevel;
    private string[] levelNames;
    private GameData gameData;

    private void Start()
    {
        gameData = GameObject.FindGameObjectWithTag("Game Data").GetComponent<GameData>();
        levelNames = new string[] { "Level 1", "Level 2", "Level 3", "Level 4", "Level 5", "Level 6", "Level 7" };
    }

    public void LoadNextScene()
    {
        gameData.currentLevelIndex += 1;
        saveGame();
        SceneManager.LoadScene(nextLevel);

    }

    public void LoadMainMenu()
    {
        saveGame();
        SceneManager.LoadScene("Main Menu");
    }

    public void LoadLoadedGameScene()
    {
        string sceneName = levelNames[gameData.currentLevelIndex];
        SceneManager.LoadScene(sceneName);
    }

    public void saveGame()
    {

        SaveData data = new SaveData(gameData.playerType, gameData.currentLevelIndex, gameData.upgradesUsed);
        SaveSystem.SaveGame(data);
    }

    public void loadGame()
    {
        SaveData data = SaveSystem.LoadGame();
        if (data != null)
        {
            gameData.load = true;
            gameData.currentLevelIndex = data.currentLevelIndex;
            gameData.playerType = (PlayerType) Enum.Parse(typeof(PlayerType), data.playerType);
            gameData.upgradesUsed = data.upgradesUsed;
            LoadLoadedGameScene();
        }
        else
        {
            print("No save file found");
        }
    }
    
    public void exitGame()
    {
        Application.Quit();
    }

}
