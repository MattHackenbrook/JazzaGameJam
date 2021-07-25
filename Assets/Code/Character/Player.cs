using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public int m_speed;
    public float m_health;
    public float m_energy;

    public string m_moveDir;

    public Sprite m_idleSprite;
    public List<Sprite> m_sprites;
    public List<AnimationClip> m_animsIdle;
    public int m_keys;

    public Animator m_anim;
    public string m_animIdle;
    int f_chestCount;

    Vector2 f_movement;
    bool up, down, left, right, sprint = false;

    // Start is called before the first frame update
    void Start()
    {
        //WebGLInput.captureAllKeyboardInput = true;
        m_health = GameObject.FindObjectOfType<GameManager>().m_playerHealth;
        m_energy = GameObject.FindObjectOfType<GameManager>().m_playerEnergy;

        f_chestCount = GameObject.FindObjectsOfType<Chest>().Length;
        GameObject.Find("KeyText").GetComponent<Text>().text = "x 0 / " + f_chestCount;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_health < 0)
            GameObject.FindObjectOfType<GameManager>().GameEnd();

        GameObject.Find("HP").GetComponent<Slider>().value = m_health / 100;
        GameObject.Find("HPText").GetComponent<Text>().text = m_health.ToString("##") + " / 100";

        print(m_energy);

        var energySlider = GameObject.Find("EG");
        energySlider.GetComponent<Slider>().value = m_energy / 100;
        var energyText = GameObject.Find("EnergyText");
        energyText.GetComponent<Text>().text = m_energy.ToString("##") + " / 100";

        keyboardControlls();


    }

    public void FixedUpdate()
    {

        var gamepad = Gamepad.current;
        if (gamepad != null)
        {
            f_movement = gamepad.leftStick.ReadValue();
            sprint = gamepad.leftTrigger.isPressed;
        }

        float angle = Mathf.Atan2(f_movement.x, f_movement.y) * Mathf.Rad2Deg;
        angle = Mathf.Round(angle / 45.0f) * 45.0f;

        playerAnitmations(angle);
        

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Chest")
        {
            var gamepad = Gamepad.current;
            if (gamepad == null)
            {
                if ( Keyboard.current.fKey.ReadValue() > 0 && other.GetComponent<Chest>().m_opened == false)
                {
                    other.GetComponent<Chest>().m_opened = true;
                    UnityEngine.Debug.Log("openchest");
                    m_keys++;
                    var maxEnergy = GameObject.FindObjectOfType<GameManager>().m_playerEnergy;
                    m_energy += 20f;
                    m_energy = Math.Min(m_energy, maxEnergy);

                    GameObject.Find("KeyText").GetComponent<Text>().text = "x " + m_keys.ToString() + " / " + f_chestCount;
                    Destroy(other.gameObject);
                }
            }
            else
            {

                if ((gamepad.aButton.isPressed || Keyboard.current.fKey.ReadValue() > 0) && other.GetComponent<Chest>().m_opened == false)
                {
                    other.GetComponent<Chest>().m_opened = true;
                    UnityEngine.Debug.Log("openchest");
                    m_keys++;

                    GameObject.Find("KeyText").GetComponent<Text>().text = "x " + m_keys.ToString() + " / " + f_chestCount;
                    Destroy(other.gameObject);
                }
            }
        }
    }

    void playerAnitmations(float angle)
    {
        if (f_movement != Vector2.zero)
        {
            float run = 0;
            if (sprint && m_energy > 0) { run = 1.5f; m_energy -= 0.2f; }
            else { run = 1f; m_energy = Math.Max(m_energy, 0); }
            //n
            if (angle == 0)
            {
                this.transform.position += ((Vector3.right + Vector3.forward) / 2) * Time.deltaTime * m_speed * 1.5f * run;
                m_anim.Play("N_Run");
                m_animIdle = "N_Idle";
            }

            //ne
            if (angle == 45)
            {
                this.transform.position += Vector3.right * Time.deltaTime * m_speed * run;
                m_anim.Play("NE_Run");
                m_animIdle = "NE_Idle";
                this.GetComponentInChildren<SpriteRenderer>().flipX = false;
            }

            //e
            if (angle == 90)
            {

                this.transform.position += ((Vector3.right + -Vector3.forward) / 2) * Time.deltaTime * m_speed * 1.5f * run;
                m_anim.Play("E_Run");
                m_animIdle = "E_Idle";
                this.GetComponentInChildren<SpriteRenderer>().flipX = false;
            }

            //se
            if (angle == 135)
            {
                this.transform.position += -Vector3.forward * Time.deltaTime * m_speed * run;
                m_anim.Play("SE_Run");
                m_animIdle = "SE_Idle";
                this.GetComponentInChildren<SpriteRenderer>().flipX = false;
            }

            //s
            if (angle == 180 || angle == -180)
            {
                this.transform.position += ((-Vector3.right + -Vector3.forward) / 2) * Time.deltaTime * m_speed * 1.5f * run;
                m_anim.Play("S_Run");
                m_animIdle = "S_Idle";
                this.GetComponentInChildren<SpriteRenderer>().flipX = false;
            }

            //sw
            if (angle == -135)
            {
                this.GetComponentInChildren<SpriteRenderer>().sprite = m_sprites[3];
                this.transform.position += -Vector3.right * Time.deltaTime * m_speed * run;

                m_anim.Play("SE_Run");
                m_animIdle = "SE_Idle";
                this.GetComponentInChildren<SpriteRenderer>().flipX = true;
            }


            //w
            if (angle == -90)
            {
                this.GetComponentInChildren<SpriteRenderer>().sprite = m_sprites[2];

                this.transform.position += ((-Vector3.right + Vector3.forward) / 2) * Time.deltaTime * m_speed * 1.5f * run;
                m_anim.Play("E_Run");
                m_animIdle = "E_Idle";

                this.GetComponentInChildren<SpriteRenderer>().flipX = true;
            }

            //nw
            if (angle == -45)
            {
                this.transform.position += Vector3.forward * Time.deltaTime * m_speed * run;
                this.GetComponentInChildren<SpriteRenderer>().sprite = m_sprites[1];

                m_anim.Play("NE_Run");
                m_animIdle = "NE_Idle";
                this.GetComponentInChildren<SpriteRenderer>().flipX = true;
            }
        }
        if (f_movement == new Vector2(0, 0))
        {
            m_anim.Play(m_animIdle);
        }
    }

    void keyboardControlls()
    {
        f_movement = Vector2.zero;
        //Detect movement commands
        if (Input.GetKeyDown(KeyCode.LeftShift)) { sprint = true; }
        if (Input.GetKeyDown(KeyCode.W)) { up = true; UnityEngine.Debug.Log("up"); }
        if (Input.GetKeyDown(KeyCode.A)) { left = true; UnityEngine.Debug.Log("left"); }
        if (Input.GetKeyDown(KeyCode.S)) { down = true; UnityEngine.Debug.Log("down"); }
        if (Input.GetKeyDown(KeyCode.D)) { right = true; UnityEngine.Debug.Log("right"); }

        //Detect halt movement commands
        if (Input.GetKeyUp(KeyCode.LeftShift)) { sprint = false; }
        if (Input.GetKeyUp(KeyCode.W)) up = false;
        if (Input.GetKeyUp(KeyCode.A)) left = false;
        if (Input.GetKeyUp(KeyCode.S)) down = false;
        if (Input.GetKeyUp(KeyCode.D)) right = false;

        if (up && !down) f_movement.y = 1;
        if (down && !up) f_movement.y = -1;
        if (right && !left) f_movement.x = 1;
        if (left && !right) f_movement.x = -1;
    }

}
