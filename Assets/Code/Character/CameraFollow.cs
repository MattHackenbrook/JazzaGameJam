using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Player m_player;
    
    // Start is called before the first frame update
    void Start()
    {
        m_player = GameObject.FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_player != null)
            this.transform.position = m_player.transform.position + new Vector3(-10, 15, -10);
        else
            m_player = GameObject.FindObjectOfType<Player>();
    }
}
