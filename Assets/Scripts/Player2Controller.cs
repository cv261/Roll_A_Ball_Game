using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player2Controller : MonoBehaviour
{
    private Rigidbody rb;
     public float thrust;
     public float jumpPower;
     public int score;
     public Text countText;
     public Text winText;
     public int NumberOfCubes;
     public bool GameOver;
     public GameObject GameManager;
    public GameObject Player;
    public Text collisionText;
    public PlayerController playerScript;
    public  bool usingMouse;
    public int mouseThrust;
    private Vector3 targetPosition;
    


   




    private void Start()
    {
         rb = GetComponent<Rigidbody>();          
         score = 0;
         setCountText();
         winText.gameObject.SetActive(false);
         GameOver = false;
        targetPosition = transform.position;


    }
    void update()
    {
        

    }

    void FixedUpdate()
    {
        if (GameOver == false& usingMouse != true)
        {
            
            if (Input.GetKey(KeyCode.LeftArrow))
            {
               rb.AddForce ((-1 * thrust * Time.deltaTime), 0, 0);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                rb.AddForce((1 * thrust * Time.deltaTime), 0, 0);
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rb.AddForce(0, 0, (1 * thrust * Time.deltaTime));
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                rb.AddForce(0, 0, (-1 * thrust * Time.deltaTime));
            }
            if (Input.GetKey(KeyCode.RightShift) && transform.position.y == 0.5f)
            {
                
                rb.AddForce(0, jumpPower, 0);
            }

        }
        
        
        if (GameOver == false & usingMouse != false)
        {
          
            
                var horMove = Input.GetAxis("Mouse X");
                var vertMove = Input.GetAxis("Mouse Y");
                rb.AddForce(new Vector3(horMove*mouseThrust, 0, vertMove*mouseThrust));
            if (Input.GetMouseButton(0) && transform.position.y == 0.5f)
            {
                
                rb.AddForce(0, jumpPower, 0); 
                    
                
            }


        }
            if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene("MinigameArea");
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup") && GameOver != true)
        {
            other.gameObject.SetActive(false);
            score++;
           ;
            setCountText();
            GameManager.gameObject.GetComponent<GameManager>().cubeCount--;
            
        }
        if (other.gameObject.CompareTag("wall")) {
            score--;
           


            if (GameOver != true)
            {
                setCountText();
            }
        }
        if (other.gameObject.CompareTag("player"))
        {
            Player.gameObject.GetComponent<PlayerController>().score--;
            playerScript.setCountText();
            collisionText.text = "Player 2 Hits!!!";
            
            collisionText.gameObject.SetActive(true);
            StartCoroutine(wait3Seconds());
        }
    }
    public void setCountText()
    {
        if(GameOver != true)
        countText.text = "Score:  " + score.ToString();
    }
    void addThrust(Vector3 movement)
    {
        rb.AddForce(movement * thrust);
    }
    IEnumerator wait3Seconds()
    {
        yield return new WaitForSeconds(3);
        collisionText.gameObject.SetActive(false);
    }
   
    

}
