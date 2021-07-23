using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldEnd : MonoBehaviour
{
    // Start is called before the first frame update
    public int m_keyRequirement;
    Player player;
    BoxCollider endCollider;

    void Start()
    {
        endCollider = gameObject.GetComponent<BoxCollider>();
        try { endCollider.isTrigger = false;} catch (NullReferenceException e) { }
        try { player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>(); } catch (NullReferenceException e) { }
    }

    // Update is called once per frame
    void Update()
    {
        if (endCollider == null)
        {
            endCollider = gameObject.GetComponent<BoxCollider>();
            endCollider.isTrigger = false;
        }
        if(player == null)
        {
            try
            { player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>(); }
            catch (NullReferenceException e) { Debug.Log("Cant find player"); }
        }
        if(player != null)
        {
            if (player.m_keys >= m_keyRequirement)
            {
                endCollider.isTrigger = true;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
            if(other.GetComponent<Player>().m_keys == m_keyRequirement)
                GameObject.FindObjectOfType<GameManager>().NextLevel();
    }
}
