using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
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
    public GameObject Player2;
    public Player2Controller player2Script;
    public Text collisionText;
    
    


    private void Start()
    {
        rb = GetComponent<Rigidbody>();       
        score = 0;
        setCountText();
        winText.gameObject.SetActive(false);
        GameOver = false;
        collisionText.gameObject.SetActive(false);
            
    }
    void update()
    {
        
    }

    void FixedUpdate()
    {
        if (GameOver == false)
        {
            
            if (Input.GetKey(KeyCode.A))
            {
                rb.AddForce((-1 * thrust * Time.deltaTime), 0, 0);
            }
            if (Input.GetKey(KeyCode.D))
            {
                rb.AddForce((1 * thrust * Time.deltaTime), 0, 0);
            }
            if (Input.GetKey(KeyCode.W))
            {
                rb.AddForce(0, 0, (1 * thrust * Time.deltaTime));
            }
            if (Input.GetKey(KeyCode.S))
            {
                rb.AddForce(0, 0, (-1 * thrust * Time.deltaTime));
            }
            if (Input.GetKeyDown("space") && transform.position.y == 0.5f)
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
       if (other.gameObject.CompareTag("Pickup")&&GameOver != true)
        {
            other.gameObject.SetActive(false);
            score ++;
            
            setCountText();
            GameManager.gameObject.GetComponent<GameManager>().cubeCount--; ;
            
        }
       if (other.gameObject.CompareTag("wall")){
            score--;
            


            if (GameOver != true)
            {
                setCountText();
            }
       }
        if (other.gameObject.CompareTag("player2"))
        {
            Player2.gameObject.GetComponent<Player2Controller>().score--;
            player2Script.setCountText();
            collisionText.text = "Player 1 Hits!!!";            
            collisionText.gameObject.SetActive(true);
            StartCoroutine(wait3Seconds());
        }
      
            
    }
    public void setCountText()
    {
        if (GameOver != true)
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
