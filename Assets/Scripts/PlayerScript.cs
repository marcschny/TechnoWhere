using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;


[System.Serializable]
public class PlayerScript : MonoBehaviour

{
    private Vector3 target;

    //Walk speed that can be set in Inspector
    [SerializeField] private float moveSpeed = 5f;
    private float speed;


    public Rigidbody2D rb;
    public Camera cam;

    Vector2 movement;
    Vector2 mousePos;

    private float rotationSpeed = 300f;

    //boost
    private float startBoostTime = 0f;
    public static float defaultBoostTime;
    private float boostTime;
    public float influenceTime = 15f;

    private Animator anim;

    public IEnemy enemy;
    public bool caught;


    private bool partyFound;

    public static bool hasEnergyDrink;

    public GameObject lightStickPrefab;
    public static int glowStickCount;


    public static bool showProgress;

    private GameObject globalVolume;
    private DrunkScript drunkScript;

    private int collectables; //number of collectables


    //Use this for initialization
    void Start()
    {
        speed = moveSpeed;
        boostTime = startBoostTime;
        defaultBoostTime = 4f;
        showProgress = false;
        partyFound = false;
        hasEnergyDrink = false;
        collectables = 0;
        anim = GetComponent<Animator>();
        target = transform.position;
        glowStickCount = 3;
        globalVolume = GameObject.FindGameObjectWithTag("Global Volume");
        drunkScript = globalVolume.GetComponent<DrunkScript>();
    }

    void Update()
    {
        //Move Enemy
        MoveInput();


        if (enemy != null)
        {
            enemy.onTrigger();
            enemy = null;
        }

        if (boostTime > 0)
        {
            boostTime -= Time.deltaTime;
        }

        //activate boost
        if (Input.GetKeyDown("space") && hasEnergyDrink && boostTime == 0f)
        {
            StartCoroutine(SpeedBoost());
            StartCoroutine(Influence());

        }

        if (Input.GetButtonDown("Fire1") && glowStickCount > 0)
        {
            //only if mouse is not over a ui element (e.g pause menu)
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                ThrowLightStick();
            }

        }

        if (Input.GetButtonDown("Fire2"))
        {
            ToggleLight();
        }
    }


    private void FixedUpdate()
    {
        Move();
    }


    private void MoveInput()
    {
        //prevent ui overlay click through
        /*
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
*/
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (movement != Vector2.zero)
        {
            anim.enabled = true;
        }
        else
        {
            anim.enabled = false;
        }
    }


    private void Move()
    {
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

            Vector2 lookDir = mousePos - rb.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;
    }


    private void ThrowLightStick()
    {
        var lightStick = Instantiate(lightStickPrefab, transform.position, transform.rotation);

        //MoveGlowStick(lightStick.GetComponent<Rigidbody2D>());
        glowStickCount--;
    }

    private void ToggleLight()
    {
        GetComponentInChildren<Light2D>().enabled = !GetComponentInChildren<Light2D>().enabled;
    }

    private IEnumerator SpeedBoost()
    {
        speed = moveSpeed * 1.8f;
        boostTime = defaultBoostTime;
        showProgress = true;
        yield return new WaitForSeconds(boostTime);
        speed = moveSpeed;
        boostTime = startBoostTime;
        showProgress = false;
        hasEnergyDrink = false;
    }

    private IEnumerator Influence()
    {
        drunkScript.enabled = true;
        yield return new WaitForSeconds(influenceTime);
        drunkScript.enabled = false;
        drunkScript.SetToDefault();
    }


    /*
    private IEnumerator MoveGlowStick (Rigidbody2D rb)
    {
        float t = 0;
        Vector2 start = rb.transform.position;
        Vector2 target = rb.transform.position + transform.forward * 20;
 
        while (t <= 1)
        {
            t += Time.fixedDeltaTime / thrust;
            rb.MovePosition (Vector3.Lerp (start, target, t));
 
            yield return null;
        }
    }
*/
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            partyFound = true;
            Debug.Log("Party found.");
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            IEnemy otherEnemy = other.gameObject.GetComponent<IEnemy>();
            if (otherEnemy != null)
            {
                enemy = otherEnemy;
            }
        }

        if (other.gameObject.CompareTag("EnergyDrink"))
        {
            if (!hasEnergyDrink)
            {
                hasEnergyDrink = true;
                Destroy(other.gameObject);
            }
        }

        if (other.gameObject.CompareTag("Collectable"))
        {
            collectables++;
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Drug"))
        {
            StartCoroutine(Influence());
            Destroy(other.gameObject);
        }


        //pick up glowstick
        //if (other.gameObject.CompareTag("GlowStick_Game"))
        //{
        //    Debug.Log("on glowstick");
        //    if (glowStickCount < 3)
        //    {
        //        glowStickCount++;
        //        Destroy(other.gameObject);
        //    }
        //}
    }
}