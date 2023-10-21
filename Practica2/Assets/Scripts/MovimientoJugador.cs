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
   public CinemachineVirtualCamera virtualCamera;
   public float thirdPersonFOV = 60f;
   public float firstPersonFOV = 0f;
   private bool isFirstPerson = false;
   public GameObject pauseMenu;
   public TextMeshProUGUI mensajeText; 
   private float mensajeDuracion = 10f; 
   private float tiempoMensaje = 0f; 
   private bool mensajeMostrado = false;
   private bool isPaused = false;

    void Start()
    {
        Player = GetComponent<Rigidbody>();
        Score = 0;
        audioSource = GetComponent<AudioSource>();
        SetThirdPersonView();
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

        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleCameraView();
        }

        if (Score == 5)
        {
            PauseGame();
            audioSource.Pause();
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
    }

    private void SetThirdPersonView()
    {
        virtualCamera.m_Lens.FieldOfView = thirdPersonFOV;
        isFirstPerson = false;
    }

    private void SetFirstPersonView()
    {
        virtualCamera.m_Lens.FieldOfView = firstPersonFOV;
        isFirstPerson = true;
    }

    private void ToggleCameraView()
    {
        if (isFirstPerson)
        {
            SetThirdPersonView();
        }
        else
        {
            SetFirstPersonView();
        }
    }

    void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
    }
}
        
 

