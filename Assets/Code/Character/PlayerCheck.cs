using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheck : MonoBehaviour
{

    public GameObject m_roofBlackout;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("hi");
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("lol");
        if(other.transform.gameObject.tag == "Player")
        {
            m_roofBlackout.SetActive(false);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.transform.gameObject.tag == "Player")
        {
            m_roofBlackout.SetActive(true);
        }
    }
}
