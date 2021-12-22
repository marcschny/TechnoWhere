using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GlowStickScript : MonoBehaviour
{
    private Light2D light;
    public IEnemy enemy;


    // Start is called before the first frame update
    void Start()
    {
        light = GetComponentInChildren<Light2D>();
        light.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy != null)
        {
            enemy.onTriggerGlowStick(this.gameObject);
            enemy = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            IEnemy otherEnemy = other.gameObject.GetComponent<IEnemy>();
            if (otherEnemy != null)
            {
                enemy = otherEnemy;
            }
        }
    }
}