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
    public float jumpForce = 100f;
    public AudioSource colisionSonido;
    private bool puedeSaltar = true;
    


    void Start()
    {
        Player = GetComponent<Rigidbody>();
        Score = 0;
        audioSource = GetComponent<AudioSource>();
        colisionSonido = GetComponent<AudioSource>(); 
    }
    void FixedUpdate()
    {
       
            direction.x = Input.GetAxis("Horizontal") * Time.deltaTime * m_Speed;
            direction.z = Input.GetAxis("Vertical")  * Time.deltaTime * m_Speed;
            Player.AddForce(direction, ForceMode.Impulse);
            
            if (puedeSaltar && Input.GetKeyDown(KeyCode.Space))
            {
                Saltar();
            }
    }
    void Update()
    {
        

        if (Score == 5)
        {
                
                audioSource.Pause();
        }  
    }
    void Saltar()
    {
        Player.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        puedeSaltar = false;
    }
    void OnCollisionEnter (Collision col)
    {
    
    
      if (col.gameObject.tag == "Friend")
      {
        GameObject particulasNuevas = Instantiate(particulas, col.transform.position, col.transform.rotation);
        colisionSonido.Play();
        Score++;
        ScoreText.text = " " + Score;
        Destroy(col.gameObject); 
      }
      else  if (col.gameObject.CompareTag("Suelo"))
      {
         puedeSaltar = true;
      }
    }
}
        
 

