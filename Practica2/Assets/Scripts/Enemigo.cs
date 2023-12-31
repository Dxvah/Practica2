using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public float rangoVision;
    public LayerMask capaPlayer;
    public bool isWarning;
    public Transform player;
    public float speed;
    public int health = 100;
    void Start()
    {
        isWarning = false;
    }

    
    void Update()
    {
       isWarning = Physics.CheckSphere(transform.position,rangoVision, capaPlayer);
       if(isWarning == true)
       {
            
            transform.LookAt(new Vector3(player.position.x,transform.position.y, player.position.z));
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, player.position.y, player.position.z), speed * Time.deltaTime);
       }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangoVision);
    }
   void OnCollisionEnter(Collision col)
   {
        if (col.gameObject.CompareTag("Bullet"))
        {
            health -= 20; 
            if (health <= 0)
            {
                
                Destroy(gameObject);
            }
        }
   }
}
