using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public GameObject boxFragment;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Instantiate(boxFragment, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
