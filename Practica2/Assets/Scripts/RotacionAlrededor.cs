using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacionAlrededor : MonoBehaviour
{
    public GameObject box;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        transform.RotateAround(box.transform.position, Vector3.up, 50 * Time.deltaTime);
    }
}
