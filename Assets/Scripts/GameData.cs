using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{

    public bool load;

    public PlayerType playerType;
    public int currentLevelIndex;
    public List<int> upgradesUsed = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        load = false;
        currentLevelIndex = -1;
        
    }

    public void setPlayerType(PlayerType type)
    {
        playerType = type;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

   

}
