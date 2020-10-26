using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    public Text winText;
    public Text livesText;
    private int scoreValue = 0;
    private int lives = 3;
    Animator anim;
    private bool facingRight = true;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;



    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        SetScoreText();
        SetLivesText();
        winText.text = "";
        anim = GetComponent<Animator>();

        musicSource.clip = musicClipOne;
        musicSource.Play();
        musicSource.loop = true;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));

        if (scoreValue >= 4)
        {
            winText.text = "You Win! Game created by Fernando D Villegas";
            musicSource.clip = musicClipTwo;
            musicSource.Play();
            musicSource.Stop();
        }

        if (lives <= 0)
        {
            winText.text = "You Lose!";
            GameObject.Find("Player").GetComponent<PlayerScript>().enabled = false;
        }

        //if(scoreValue == 4)
       // {
          //  transform.position = new Vector2(31.0f, 0.0f);
      //  }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetInteger("State", 2);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetInteger("State", 0);
        }

       
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            SetScoreText();
            Destroy(collision.collider.gameObject);
        }
        if(collision.collider.tag == "Enemy")
        {
            lives -= 1;
            SetLivesText();
            Destroy(collision.collider.gameObject);
        }
    }



    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            if(Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }
    void SetScoreText ()
    {
        score.text = "Score: " + scoreValue.ToString();
    }

    void SetLivesText ()
    {
        livesText.text = "Lives: " + lives.ToString();
    }

}
