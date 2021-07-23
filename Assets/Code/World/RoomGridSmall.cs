using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGridSmall : MonoBehaviour
{
    //1 Room
    public List<Room> m_options;

    public Room m_room;

    public bool m_isEnd;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn(bool m_forceSpawn)
    {
        int randomRoom;
        if (GameObject.FindObjectOfType<WorldEnd>() == null)
        {
            randomRoom = Random.Range(0, m_options.Count - 1);
        }
        else
        {

            Debug.Log("foundExisting game end");
            Debug.Log(m_forceSpawn);
            randomRoom = Random.Range(0, m_options.Count - 2);
        }


        if (m_forceSpawn)
            randomRoom = 10;
        if (randomRoom == 10)
            m_isEnd = true;
        Instantiate(m_options[randomRoom],this.transform.position - new Vector3(7.5f, 0, 7.5f), Quaternion.identity);

        Destroy(this.GetComponent<MeshRenderer>());
    }


}
