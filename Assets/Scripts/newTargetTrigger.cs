using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newTargetTrigger : MonoBehaviour
{
    public IEnemy enemy;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            IEnemy otherEnemy = other.gameObject.GetComponent<IEnemy>();
            if (otherEnemy != null)
            {
                enemy = otherEnemy;
                enemy = null;
            }
        }
    }
}