using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject m_worldGenPrefab;
    public WorldGen m_worldGen;
    public bool m_tutorial;

    public int m_level;

    public float m_playerHealth;
    public float m_playerEnergy;
    private void Awake()
    {
        GameManager[] objs = GameObject.FindObjectsOfType<GameManager>();

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

    }

    private void Start()
    {
        m_tutorial = true;
        if (m_tutorial)
            StartCoroutine("LoadTutorial");
        else
            NextLevel();
    }
    public void NextLevel()
    {

        m_playerHealth = GameObject.FindObjectOfType<Player>().m_health;
        StartCoroutine("LoadGame");

    }

    private bool levelWasLoaded = false;
    private void OnLevelWasLoaded(int iLevel)
    {
        levelWasLoaded = true;
    }

    private IEnumerator LoadTutorial()
    {
        m_level++;
        // Start loading the scene
        AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync("Tutorial", LoadSceneMode.Single);
        // Wait until the level finish loading
        while (!asyncLoadLevel.isDone)
            yield return null;
        // Wait a frame so every Awake and Start method is called
        yield return new WaitForEndOfFrame();
        m_worldGen = GameObject.FindObjectOfType<WorldGen>();
        m_worldGen.PlayerSpawn();
        m_playerHealth = 100;
        m_playerEnergy = 100;
        GameObject.FindObjectOfType<Player>().m_health = m_playerHealth;
        GameObject.FindObjectOfType<Player>().m_energy = m_playerEnergy;

        GameObject.Find("Current Level").GetComponent<Text>().text = "Tutorial level";
    }


    private IEnumerator LoadGame()
    {
        m_level++;
        // Start loading the scene
        AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync("SampleScene", LoadSceneMode.Single);
        // Wait until the level finish loading
        while (!asyncLoadLevel.isDone)
            yield return null;
        // Wait a frame so every Awake and Start method is called
        yield return new WaitForEndOfFrame();
        m_worldGen = GameObject.FindObjectOfType<WorldGen>();
        m_worldGen.m_dimensions = m_level;
        m_worldGen.StartWorldGen(m_level);
        GameObject.FindObjectOfType<Player>().m_health = m_playerHealth;
        GameObject.Find("Current Level").GetComponent<Text>().text = "Level " + m_level.ToString();
    }

    private IEnumerator EndGame()
    {
        // Start loading the scene
        AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync("EndGame", LoadSceneMode.Single);
        // Wait until the level finish loading
        while (!asyncLoadLevel.isDone)
            yield return null;
        // Wait a frame so every Awake and Start method is called
        yield return new WaitForEndOfFrame();
        GameObject.FindObjectOfType<Text>().text = GameObject.FindObjectOfType<Text>().text + m_level;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameEnd()
    {
       StartCoroutine("EndGame");
    }
}
