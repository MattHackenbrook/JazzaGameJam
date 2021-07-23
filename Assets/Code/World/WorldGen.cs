using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldGen : MonoBehaviour
{
    public List<GameObject> m_rooms;
    public Room m_room;
    public int m_dimensions;
    public GameObject m_prefab;
    

    public GameObject m_edgePrefab;
    public GameObject m_cornerPrefab;
    public GameObject m_playerPrefab;
    public GameObject m_chestPrefab;
    public GameObject m_endPrefab;
    public GameObject m_enemyPrefab;

    // Start is called before the first frame update

    private void Awake()
    {
       
    }
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        
    }



    //load level

    public void StartWorldGen(int f_dimensions)
    {
        m_rooms = new List<GameObject>();

        for(int i = 0; i < f_dimensions; i++)
        {
            for (int j = 0; j < f_dimensions; j++)
            {
                m_rooms.Add(Instantiate(m_prefab, new Vector3(i * 60, 0, j * 60), this.transform.rotation));
            }

        }
        bool m_hasSplit = false;
        int m_roomCount = 0;
       foreach (GameObject f_rgls in m_rooms)
       {
            if (!m_hasSplit && m_roomCount == m_rooms.Count-1)
            {
                f_rgls.GetComponent<RoomGridLarge>().Spawn(true);
            }
            else
                f_rgls.GetComponent<RoomGridLarge>().Spawn(false);

            if (f_rgls.GetComponent<RoomGridLarge>().m_hasSplit)
                m_hasSplit = true;

            m_roomCount++;
       }
        EdgeCreation(f_dimensions);

    }

    void EdgeCreation(int f_dimensions)
    {
        for (int i = 0; i < f_dimensions; i++)
        {
            Instantiate(m_edgePrefab, new Vector3((i * 60)-30, 0, -40), Quaternion.identity);
            Instantiate(m_edgePrefab, new Vector3(-40f,0,(i * 60)+30f), Quaternion.Euler(new Vector3(0,90,0)));
            Instantiate(m_edgePrefab, new Vector3((i * 60) + 30f, 0, (f_dimensions * 60)-20f), Quaternion.Euler(new Vector3(0, 180, 0)));
            Instantiate(m_edgePrefab, new Vector3((f_dimensions * 60) - 20.25f, 0, (i*60)-30), Quaternion.Euler(new Vector3(0, 270, 0)));


        }

        //corners
        Instantiate(m_cornerPrefab, new Vector3(-40f,0,-40f), Quaternion.identity);
        Instantiate(m_cornerPrefab, new Vector3(-40f, 0, (f_dimensions * 60) -20f), Quaternion.Euler(new Vector3(0, 90, 0)));
        Instantiate(m_cornerPrefab, new Vector3((f_dimensions * 60) - 20, 0, (f_dimensions * 60) - 20), Quaternion.Euler(new Vector3(0, 180, 0)));
        Instantiate(m_cornerPrefab, new Vector3((f_dimensions * 60) - 20f, 0, -40f), Quaternion.Euler(new Vector3(0, 270, 0)));

        PlayerSpawn();
    }

    public void PlayerSpawn()
    {
        List<CharacterSpawn> m_spawns = new List<CharacterSpawn>();
        m_spawns.AddRange(FindObjectsOfType<CharacterSpawn>());

        Instantiate(m_playerPrefab, m_spawns[Random.Range(0, m_spawns.Count-1)].transform.position + new Vector3(0,5,0), Quaternion.identity);
        ChestSpawn();
    }

    void ChestSpawn()
    {
        for(int i = 0; i < m_dimensions * m_dimensions; i++)
        {
            List<ChestSpawn> m_spawns = new List<ChestSpawn>();
            m_spawns.AddRange(FindObjectsOfType<ChestSpawn>());
            int temp = Random.Range(0, m_spawns.Count - 1);
            Instantiate(m_chestPrefab, m_spawns[temp].transform.position + new Vector3(0, .5f, 0), Quaternion.identity);
            m_spawns.RemoveAt(temp);
        }
        EndSpawn();
    }

    void EndSpawn()
    {
       // List<EndSpawn> m_spawns = new List<EndSpawn>();
       // m_spawns.AddRange(FindObjectsOfType<EndSpawn>());
       // int temp = Random.Range(0, m_spawns.Count - 1);
       // Instantiate(m_endPrefab, m_spawns[temp].transform.position + new Vector3(0, .5f, 0), Quaternion.identity);
       // m_spawns.RemoveAt(temp);
        GameObject.FindObjectOfType<WorldEnd>().m_keyRequirement = m_dimensions * m_dimensions;
       //
        EnemySpawn();
    }

    void EnemySpawn()
    {
        List<EnemySpawn> m_spawns = new List<EnemySpawn>();
        m_spawns.AddRange(FindObjectsOfType<EnemySpawn>());

        int m_enemycap;

        if (m_spawns.Count < m_dimensions * m_dimensions * 4)
            m_enemycap = m_spawns.Count;
        else
            m_enemycap = m_dimensions * m_dimensions * 4;

            for (int i = 0; i < m_enemycap; i++)
        {
            int temp = Random.Range(0, m_spawns.Count - 1);
            Instantiate(m_enemyPrefab, m_spawns[temp].transform.position + new Vector3(0, .5f, 0), Quaternion.identity);
            m_spawns.RemoveAt(temp);
        }

    }
}
