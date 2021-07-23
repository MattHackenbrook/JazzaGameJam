using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject m_player;
    public List<AudioClip> m_soundFiles;
    public AudioSource m_source;
    public float m_soundReset;


    public LineRenderer m_lr;
    public Material m_electricLineMaterial;
    public List<Texture> m_lightningTextures;
    // Start is called before the first frame update
    void Start()
    {
        m_lr = GetComponentInChildren<LineRenderer>();
        m_player = FindObjectOfType<Player>().gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        m_soundReset -= Time.deltaTime;
        transform.LookAt(m_player.transform);

        //shootray
        RaycastHit rayHit;
        Ray rayTest = new Ray(transform.position, m_player.transform.position-transform.position);
        Debug.DrawRay(transform.position, m_player.transform.position - transform.position, Color.blue);

        if (Physics.Raycast(rayTest, out rayHit))
        {
            if (rayHit.collider.gameObject.tag == "Player")
            {
                if(m_soundReset < 0)
                {
                    m_source.clip = m_soundFiles[Random.Range(0, m_soundFiles.Count)];
                    if(!m_source.isPlaying)
                        m_source.Play();
                    m_soundReset = 10;
                }
                if (Vector3.Distance(m_player.transform.position, this.transform.position) < 3.5f)
                {
                    m_lr.enabled = true;
                    m_lr.SetPosition(0, m_lr.transform.position);
                    m_lr.SetPosition(1, m_player.transform.position);
                    m_lr.transform.rotation = Random.rotation;
                    m_electricLineMaterial.mainTexture = m_lightningTextures[Random.Range(0, m_lightningTextures.Count)];
                    Debug.DrawRay(transform.position, m_player.transform.position - transform.position, Color.green);
                    m_player.GetComponent<Player>().m_health -= Time.deltaTime * GameObject.FindObjectOfType<GameManager>().m_level;
                    //need particle effect
                    // Debug.Log("Youlose");
                }
                else
                {
                    this.transform.position += (m_player.transform.position - transform.position).normalized * Time.deltaTime * 3;
                    m_lr.enabled = false;
                }
                // Debug.Log("Nowall");
            }
            else
            {
                //Debug.Log("wall");
            }
        }
    }


}
