using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MovimientoJugador : MonoBehaviour
{
    
   
    
    AudioSource audioSource;
    public GameObject particulas;
    public float m_Speed = 20f;
    Rigidbody Player;
    Vector3 direction;
    private object TotalFriend;
    public int Score;
    public TextMeshProUGUI ScoreText;
    


    void Start()
    {
        Player = GetComponent<Rigidbody>();
        Score = 0;
        audioSource = GetComponent<AudioSource>();
    }



    void FixedUpdate()
    {
       
            direction.x = Input.GetAxis("Horizontal") * Time.deltaTime * m_Speed;
            direction.z = Input.GetAxis("Vertical")  * Time.deltaTime * m_Speed;
            Player.AddForce(direction, ForceMode.Impulse);
    }



    void Update()
    {
        

        if (Score == 8)
        {
                
                audioSource.Pause();
        }
    }
    void OnCollisionrEnter (Collider col)
    {
    
    
      if (col.gameObject.CompareTag("Friend"))
      {
        Instantiate(particulas, col.transform.position, col.transform.rotation);
        Score++;
        ScoreText.text = "Score = " + Score;
        Destroy(col.gameObject);
        
      }
    }
}
        
 

