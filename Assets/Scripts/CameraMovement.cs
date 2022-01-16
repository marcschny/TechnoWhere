using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform player;

    public float smoothSpeed = 0.15f;

    public Vector3 offset;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void FixedUpdate(){ //run right after

        //stop camera movement when game over
        if(GameManager.GameIsOver){
            this.enabled = false;
            return;
        }

        transform.position = player.position+offset;
    }
    
    
}
