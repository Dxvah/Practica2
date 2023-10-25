using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SimpleShoot : MonoBehaviour
{
    
    public GameObject bulletPrefab;
    public Transform spawnPoint;
    public Animator gunAnimator;
    public float shotPower = 500f;
    public float shotRate = 0.5f;
    public float shotRateTime = 0;
    public AudioSource sonidoDisparo;


    void Update()
    {
        
        if (Input.GetButtonDown("Fire1"))
        {
            if(Time.time> shotRateTime)
            {
                GameObject newBullet;
                newBullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
                newBullet.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * shotPower);
                sonidoDisparo.Play();
                gunAnimator.SetTrigger("Fire");
                shotRateTime = Time.time + shotRate;
                Destroy(newBullet, 2);
            }
        }
    }
}
