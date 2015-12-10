using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{

    public static GameManager manager;
    public EventSystem eventSystem;
    public Terrain terrain;

    //REMOVE ME PLEASE KEVIN

    public GameObject winScreen;
    public GameObject loseScreen;
    public GameObject pauseMenu;
    public MonoBehaviour firstPersonScript;
    public Player playerScript;
    /////////////

    //Build Phase
    public List<SpawnPoint> spawnPoints;

    //Build Mode
    public TowerBuilder towerBuilder;
    public GameObject buildModeText;

    //Wave Phase
    public int enemyCount;
    private float respawnTime = 10.0f;
    private float respawnTimer = 0.0f;

    //Player Camera
    public GameObject firstPersonCam;

    //Topdown Camera
    public GameObject upperCam;

    //First Person UI
    public GameObject firstPersonViewUI;
    public Image playerHealthBar;
    public Text playerHealthText;
    public Image castleHealthBar;
    public Text castleHealthText;

    //Strategic View UI
    public GameObject strategicViewUI;
    public Text shardAmount;

    public Unit endPoint;
    public Unit player;
    private Vector3 playerSpawnPoint;
    public bool isBuildPhase = true;
    public int shard = 0;
    public int levelNo;
    public int waveNo = 0;

    //Enemy
    public GameObject[] enemies;

    void Awake ()
    {
        manager = this;
        playerSpawnPoint = player.transform.position;
        StartBuildPhase();
        //RemoveMePleaseKevin
        AddShards(0);
        ///////////
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !towerBuilder.isBuildMode)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0.0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            AddShards(100);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            SwitchView();
        }

        if (isBuildPhase)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                StartWavePhase();
            }
        }
        else
        {
            if (enemyCount == 0)
            {
                StartBuildPhase();
            }
        }
        if(!player.gameObject.activeSelf)
        {
            respawnTimer += Time.deltaTime;
            if(respawnTimer >= respawnTime)
            {
                RespawnPlayer();
            }
        }
    }

    void StartBuildPhase ()
    {
        if (!player.gameObject.activeSelf)
        {
            RespawnPlayer();
        }
        else
        {
            player.health = player.maxHealth;
            //REMOVE ME PLEASE KEVIN
            player.AliveCheck();
            ////////////
        }
        isBuildPhase = true;
        towerBuilder.enabled = true;
        SwitchToStrategic(false);
        ++waveNo;
        towerBuilder.recentlyBuiltTowers = new List<Tower>();
        foreach(SpawnPoint tempSpawnPoint in spawnPoints)
        {
            tempSpawnPoint.enabled = false;
            tempSpawnPoint.LoadSpawnInfo();
        }
    }

    void StartWavePhase ()
    {
        isBuildPhase = false;
        towerBuilder.enabled = false;
        SwitchToFirstPerson();
        foreach (SpawnPoint tempSpawnPoint in spawnPoints)
        {
            tempSpawnPoint.enabled = true;
        }
    }

    public void AddShards (int amount)
    {
        shard += amount;
        shardAmount.text = shard.ToString();
    }

    void SwitchView()
    {
        if (upperCam.activeSelf)
        {
            SwitchToFirstPerson();
        }
        else
        {
            SwitchToStrategic(!isBuildPhase);
        }
    }

    public void SwitchToStrategic(bool isSlowDown)
    {
        firstPersonCam.SetActive(false);
        firstPersonViewUI.SetActive(false);
        upperCam.SetActive(true);
        strategicViewUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (isSlowDown)
        {
            Time.timeScale = 0.2f;
        }
        firstPersonScript.enabled = false;
        playerScript.enabled = false;
    }

    public void SwitchToFirstPerson()
    {
        if (player.gameObject.activeSelf)
        {
            towerBuilder.EndBuildMode();
            firstPersonCam.SetActive(true);
            firstPersonViewUI.SetActive(true);
            upperCam.SetActive(false);
            strategicViewUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1.0f;
        }
        firstPersonScript.enabled = true;
        playerScript.enabled = true;
    }

    public void RespawnPlayer ()
    {
        respawnTimer = 0.0f;
        player.gameObject.SetActive(true);
        player.health = player.maxHealth;
        player.transform.position = playerSpawnPoint;
        //REMOVE ME PLEASE KEVIN
        player.AliveCheck();
        ////////////
    }

    public void LoseGame()
    {
        loseScreen.SetActive(true);
    }

    public void WinGame()
    {
        winScreen.SetActive(true);
    }

    public void BackToMainMenu()
    {
        Application.LoadLevel(0);
    }

    public void Unpause ()
    {
        Time.timeScale = 1.0f;
        if (firstPersonCam.activeSelf)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
