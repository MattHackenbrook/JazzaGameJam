using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGridLarge : MonoBehaviour
{
    //4x4 Room
    public List<GameObject> m_options;

    public bool m_split;
    public List<GameObject> m_rooms;
    public GameObject m_room;
    public GameObject m_prefab;
    public bool m_hasSplit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn(bool m_forceSplit)
    {


        float x = Random.value;
        if (m_forceSplit)
            x = 1;
        if (x >= 0.5f)
        {
            m_hasSplit = true;
            int m_roomCount = 0;
            bool m_hasSplitChildren = false;


            m_rooms = new List<GameObject>();

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    m_rooms.Add(Instantiate(m_prefab, new Vector3((i * 30)+this.transform.position.x - 15, 0, (j * 30) + this.transform.position.z-15), this.transform.rotation));
                }

            }

            //split
            foreach (GameObject f_rg in m_rooms)
            {
                if (!m_hasSplitChildren && m_roomCount == m_rooms.Count - 1)
                {
                    f_rg.GetComponent<RoomGridMedium>().Spawn(true);
                }
                else
                    f_rg.GetComponent<RoomGridMedium>().Spawn(false);

                if (f_rg.GetComponent<RoomGridMedium>().m_hasSplit)
                    m_hasSplitChildren = true;

                m_roomCount++;
            }
            Destroy(this.GetComponent<MeshRenderer>());
        }
        else
        {
            m_room = Instantiate(m_options[Random.Range(0, m_options.Count - 1)], this.transform.position - new Vector3(30,0,30), Quaternion.identity);

            Destroy(this.GetComponent<MeshRenderer>());
        }
    
    }
}
