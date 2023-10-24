using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;

public class MovimientoJugador : MonoBehaviour
{
    
   AudioSource audioSource;
   public AudioClip amigoSound;
   public GameObject particulas;
   public float m_Speed = 20f;
   Rigidbody Player;
   Vector3 direction;
   private object TotalFriend;
   public int Score;
   public TextMeshProUGUI ScoreText;
   public float jumpForce = 100f;
   private bool puedeSaltar = true;
   public GameObject virtualCamera;
   public GameObject FPCamera;
    private bool isFirstPerson = false;
   public GameObject pauseMenu;
   public TextMeshProUGUI mensajeText; 
   private float mensajeDuracion = 10f; 
   private float tiempoMensaje = 0f; 
   private bool mensajeMostrado = false;
   private bool isPaused = false;
   public GameObject canvas;
   public GameObject victoryCanva;
    



    void Start()
    {
        Player = GetComponent<Rigidbody>();
        Score = 0;
        audioSource = GetComponent<AudioSource>();
        canvas.SetActive(true);
    }

    void Update()
    {
        if (!mensajeMostrado)
        {
            tiempoMensaje += Time.deltaTime;

            
            if (tiempoMensaje < mensajeDuracion)
            {
                mensajeText.gameObject.SetActive(true);
            }
            else
            {
                mensajeText.gameObject.SetActive(false);
                mensajeMostrado = true;
            }
        }
        if(Input.GetKeyDown("c"))
        {
            isFirstPerson = true;
        }
        if (Input.GetKeyDown("x"))
        {
            isFirstPerson = false;
        }

            if (Score == 5)
        {
            Victory();
            audioSource.Pause();
        }
        if( isFirstPerson == true)
        {
            virtualCamera.SetActive(false);
            FPCamera.SetActive(true);
            
        }
        else
        {
            virtualCamera.SetActive(true);
            FPCamera.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        direction.x = Input.GetAxis("Horizontal") * Time.deltaTime * m_Speed;
        direction.z = Input.GetAxis("Vertical") * Time.deltaTime * m_Speed;
        Player.AddForce(direction, ForceMode.Impulse);

        if (puedeSaltar && Input.GetKeyDown(KeyCode.Space))
        {
            Saltar();
        }
    }

    void Saltar()
    {
        Player.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        puedeSaltar = false;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Friend"))
        {
            GameObject particulasNuevas = Instantiate(particulas, col.transform.position, col.transform.rotation);

            Score++;
            ScoreText.text = " " + Score;
            Destroy(col.gameObject);
            audioSource.PlayOneShot(amigoSound);
        }
        else if (col.gameObject.CompareTag("Suelo"))
        {
            puedeSaltar = true;
        }
        else if (col.gameObject.CompareTag("Water"))
        {
            PauseGame();
        }
        else if (col.gameObject.CompareTag("Enemy"))
        {
            PauseGame();
        }
        else if (col.gameObject.CompareTag("Tuerca"))
        {
            
        }
    }

    void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        canvas.SetActive(false);
    }

    void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
    }
    void Victory()
    {
        victoryCanva.SetActive(true);
        canvas.SetActive(false);
        Time.timeScale = 0f;
    }
}
        
 

