using System;
using UnityEngine;

namespace scripts2
{
    public class ItemScript : MonoBehaviour
    {
        private GameObject player;
        private PlayerScript playerScript;

        public float rotSpeed = 2;

        private void Start()
        {
            player = GameObject.FindWithTag("Player");
            playerScript = player.GetComponent<PlayerScript>();
        }

        private void Update()
        {
            transform.Rotate(0,0,100*rotSpeed*Time.deltaTime);
        }
    }
}