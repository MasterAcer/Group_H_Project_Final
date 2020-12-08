using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    // Spawn point of player
    private Transform spawnPoint;

    // Character movement script of player
    private CharacterController player;

    // Prefabs of ranger and knight sprites
    public GameObject rangerCharacter;
    public GameObject knightCharacter;
    public GameObject rogueCharacter;
    public GameObject wizardCharacter;

    // Player type select and upgrade selection menu game objects
    public GameObject playerSelectMenu;
    public GameObject selectUpgradeMenu;

    // Type of player
    private PlayerType playerType;

    // Arrays of upgrades
    public Upgrade[] knightUpgrades;
    public Upgrade[] wizardUpgrades;
    public Upgrade[] rogueUpgrades;
    public Upgrade[] rangerUpgrades;
    
    // Array of upgrade buttons
    public GameObject[] upgradeButton;

    // For Saving
    private GameData gameData;
    
    //Name of current scene and others
    private string currentSceneName;

    // Start is called before the first frame update
    void Start()
    {
        createUpgrades();
        currentSceneName = SceneManager.GetActiveScene().name;
        gameData = GameObject.FindGameObjectWithTag("Game Data").GetComponent<GameData>();
        assignSpawnPointObject();
        
        if (currentSceneName.Equals("Level 1"))
        {
            playerSelectMenu = GameObject.Find("Select Player Type Menu");
        }
        else
        {
            playerSelectMenu = new GameObject();
        }

        if (gameData.load)
        {
            if(gameData.playerType == PlayerType.RANGER)
            {
                onRangerButtonClick();
                applyUpgrade();
            }
            else if (gameData.playerType == PlayerType.KNIGHT)
            {
                onKnightButtonClick();
                applyUpgrade();
            }
            else if (gameData.playerType == PlayerType.ROGUE)
            {
                onRogueButtonClick();
                applyUpgrade();
            }
            else if (gameData.playerType == PlayerType.WIZARD)
            {
                onWizardButtonClick();
                applyUpgrade();
            }
            gameData.load = false;
        }
    }   

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // Apply upgrades on load that have been previously selected
    public void applyUpgrade()
    {
        Upgrade[] upgrades = selectUpgradeArray();
        for (int i = 0; i < gameData.upgradesUsed.Count; i++)
        {
            print(i);
            print(gameData.upgradesUsed[i]);
            upgrades[gameData.upgradesUsed[i]].applyUpgrade(ref player);
            //setDisabledColour(upgradeButton[gameData.upgradesUsed[i]].GetComponent<Button>());
            //disableButton(gameData.upgradesUsed[i]);
        }
    }
    
    private void assignSpawnPointObject()
    {
        spawnPoint = GameObject.FindGameObjectWithTag("Spawn Point").GetComponent<Transform>();
    }
    
    // Respawn mechanics
    public void Respawn()
    {
        gameData.load = true;
        LoadLevel(currentSceneName);
    }
    
    public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    // Create the 8 upgrades that will be used and populates arrays of upgrades for all characters with appropriate upgrades
    // Note some some upgrades appear twice when populating array since they will be offered twice as two separate upgrades
    private void createUpgrades()
    {
        Upgrade health = new Upgrade("Health +30%", "maxHealth", 0.3f);
        Upgrade speed = new Upgrade("Speed +10%", "Speed",0.1f);
        Upgrade damage = new Upgrade("Damage +30%", "DMG",0.3f);
        Upgrade fireRate = new Upgrade("Fire Rate +20%", "Fire Rate",-0.2f);
        Upgrade jump = new Upgrade("Wizard Jumps Higher", "Jump",0.5f);
        
        knightUpgrades = new Upgrade[] {health, speed, damage, health, damage, speed, damage, health};
        wizardUpgrades = new Upgrade[] {health, speed, damage, jump, damage, fireRate, speed, damage};
        rogueUpgrades = new Upgrade[] {health, speed, damage, damage, health, speed, health, speed};
        rangerUpgrades = new Upgrade[] {health, speed, damage, speed, fireRate, damage, speed, fireRate};
    }

    // Ranger character type selection handler
    public void onRangerButtonClick()
    {
        // Instantiate ranger, disable player type selection menu and set player type
        Instantiate(rangerCharacter, spawnPoint.position, spawnPoint.rotation);
        disablePlayerSelectMenu();
        setPlayerType(PlayerType.RANGER);
        
        // Set player object to relavent scripts
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<RangerMovement>();
        setPlayerObjectInCamera(GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>());
        setPlayerObjectInEnemy(GameObject.FindGameObjectWithTag("Player"));
        gameData.setPlayerType(playerType);
    }

    // Knight character type selection handler
    public void onKnightButtonClick()
    {
        // Instantiate knight, disable player type selection menu and set player type
        Instantiate(knightCharacter, spawnPoint.position, spawnPoint.rotation);
        disablePlayerSelectMenu();
        setPlayerType(PlayerType.KNIGHT);

        // Set player object to relavent scripts
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<KnightMovement>();
        setPlayerObjectInCamera(GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>());
        setPlayerObjectInEnemy(GameObject.FindGameObjectWithTag("Player"));
        gameData.setPlayerType(playerType);
    }

    public void onRogueButtonClick()
    {
        // Instantiate rogue, disable player type selection menu and set player type
        Instantiate(rogueCharacter, spawnPoint.position, spawnPoint.rotation);
        disablePlayerSelectMenu();
        setPlayerType(PlayerType.ROGUE);

        // Set player object to relavent scripts
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        setPlayerObjectInCamera(GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>());
        setPlayerObjectInEnemy(GameObject.FindGameObjectWithTag("Player"));
        gameData.setPlayerType(playerType);
    }

    public void onWizardButtonClick()
    {
        // Instantiate wizard, disable player type selection menu and set player type
        Instantiate(wizardCharacter, spawnPoint.position, spawnPoint.rotation);
        disablePlayerSelectMenu();
        setPlayerType(PlayerType.WIZARD);

        // Set player object to relavent scripts
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<WizardMovement>();
        setPlayerObjectInCamera(GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>());
        setPlayerObjectInEnemy(GameObject.FindGameObjectWithTag("Player"));
        gameData.setPlayerType(playerType);
    }

    // Set player object in camera follow script
    private void setPlayerObjectInCamera(Transform newPlayer)
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().Player = newPlayer;
    }
    
    // Set player object in enemy controller script
    private void setPlayerObjectInEnemy(GameObject newPlayer)
    {
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject Enemy in Enemies)
        {
            Enemy.GetComponent<EnemyController>().setPlayerComponents(newPlayer);
        }
    }

    // Disable player select menu
    private void disablePlayerSelectMenu()
    {
        playerSelectMenu.SetActive(false);
    }

    // Enable player upgrade menu
    public void enableUpgradeMenu()
    {
        selectUpgradeMenu.SetActive(true); 
        Upgrade[] upgrades = selectUpgradeArray();
        populateUpgradesMenu(upgrades);
        disableUsedButtons();
    }

    public void disableUsedButtons()
    {
        for (int i = 0; i < gameData.upgradesUsed.Count; i++)
        {
            setDisabledColour(upgradeButton[gameData.upgradesUsed[i]].GetComponent<Button>());
            disableButton(gameData.upgradesUsed[i]);
        }
        
    }

    // Set player type
    private void setPlayerType(PlayerType type)
    {
        playerType = type;
    }

    // Selects correct array based on what character was chosen
    private Upgrade[] selectUpgradeArray()
    {

        if (playerType == PlayerType.KNIGHT)
        {
            return knightUpgrades;
        }
        else if(playerType == PlayerType.RANGER)
        {
            return rangerUpgrades;
        }
        else if(playerType == PlayerType.ROGUE)
        {
            return rogueUpgrades;
        }
        else
        {
            return wizardUpgrades;
        }
        
    }
    
    // Populate the buttons with corresponding upgrades' description
    private void populateUpgradesMenu(Upgrade[] upgrade)
    {
        upgradeButton = GameObject.FindGameObjectsWithTag("UpgradeButton");

        for (int i = 0; i < upgrade.Length; i++)
        {
            upgradeButton[i].GetComponent<Button>().GetComponentInChildren<Text>().text = upgrade[i].description;
        }
    }

    public void upgradeSelected(int index)
    {
        Upgrade[] upgrades = selectUpgradeArray();
        CharacterController playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        upgrades[index].applyUpgrade(ref playerScript);
        populateGameData(index);
        disableUpgradeButtons();
        setDisabledColour(upgradeButton[index].GetComponent<Button>());
    }

    public void populateGameData(int index)
    {
        gameData.upgradesUsed.Add(index);
        gameData.playerType = playerType;
        gameData.load = true;
    }

    // Set interactable of all upgrade buttons to false after upgrade is selected.
    private void disableUpgradeButtons()
    {
        for(int i = 0; i < upgradeButton.Length; i++)
        {
            upgradeButton[i].GetComponent<Button>().interactable = false;
        }
    }

    
    // disables just one button form array of buttons
    private void disableButton(int index)
    {
        upgradeButton[index].GetComponent<Button>().interactable = false;
    }

    // Set disabled colour of selected upgrade to green
    private void setDisabledColour(Button button)
    {

        ColorBlock cb = button.colors;
        cb.disabledColor = Color.green;
        button.colors = cb;
    }    
    
    public void restartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
