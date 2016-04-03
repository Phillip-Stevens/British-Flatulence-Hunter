using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public bool facingRight = true;         // For determining which way the player is currently facing.
    [HideInInspector]
    public float moveForce = 0.4f;          // Amount of force added to move the player left and right.
    public float maxSpeed = 0.4f;             // The fastest the player can travel in the x axis.
    private Animator anim;                  // Reference to the player's animator component
    public float test = 0.0f;
    bool farting = false;

    // fart
    public GameObject fartCloud;
    public float maxgas = 100;
    public float gas = 100;
    public float fartCost = 10;
    public float timer = 0.0f;

    // Score
    public float score = 0;
    public float gameTime = 20; //seconds
    public bool gameOver = false;
 

    // Audio source
    AudioSource source;

    void Awake()
    {
        // Setting up references.
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }


    void Update()
    {
        if (gameTime >= 0)
        {
            gameTime -= Time.deltaTime;

            // User Input

            if (Input.GetKeyDown("escape"))
            {
                Application.LoadLevel("GameJamGame");
            }
            if (Input.GetKeyDown("space"))
            {
                if (farting == false && gas > 0)
                {
                    GameObject cloud = Instantiate(fartCloud, this.transform.position, this.transform.rotation) as GameObject;
                    gas -= fartCost;
                    print("space key was pressed");
                    source.pitch = Random.Range(0.5f, 1.5f);
                    source.Play();
                }
            }

            // Gas Regen
            if (gas < 100)
            {
                timer += 1.0f * Time.deltaTime;

                if (timer >= 4.0f)
                {
                    if (gas < 100)
                    {
                        // add ten to gas
                        gas += 10;
                    }
                    timer = 0;
                }
            }
        }
        else
        {
            gameOver = true;

            if (Input.GetKeyDown("escape"))
            {
                Application.LoadLevel("GameJamGame");
            }
        }
    }


    void FixedUpdate()
    {
        if(!gameOver)
        {
            // Cache the horizontal input.
            float h = Input.GetAxis("Horizontal");

            // If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
            if (h * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
                // ... add a force to the player.
                GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * moveForce);

            // If the player's horizontal velocity is greater than the maxSpeed...
            if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
                // ... set the player's velocity to the maxSpeed in the x axis.
                GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

            // If the input is moving the player right and the player is facing left...
            if (h > 0 && !facingRight)
                // ... flip the player.
                Flip();
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (h < 0 && facingRight)
                // ... flip the player.
                Flip();
        }
    }


    void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Fart")
        {
            Physics2D.IgnoreCollision(this.GetComponent<BoxCollider2D>(), coll.gameObject.GetComponent<BoxCollider2D>());
        }
    }

    void OnCollision2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Fart")
        {
            Physics2D.IgnoreCollision(this.GetComponent<BoxCollider2D>(), coll.gameObject.GetComponent<BoxCollider2D>());
        }
    }

    void OnGUI()
    { 
        GUIStyle Label2 = new GUIStyle(GUI.skin.GetStyle("label"));
        Label2.fontSize = 22;
        Label2.normal.textColor = Color.red ;

        // Display the amount of Gas
        GUI.Label(new Rect(0, 0, Screen.width / 2.5f, Screen.height / 2), "Gas: " + gas.ToString(), Label2);

        // Display timer for score
        if(gas < 100)
        {
            GUI.Label(new Rect(0, 30, (Screen.width / 2.5f), Screen.height / 2 + 60), "Regen Fart in: " + timer.ToString(), Label2);
        }

        // Display Score
        GUI.Label(new Rect(0, 60, (Screen.width / 2.5f), Screen.height / 2 + 60), "Score: " + score.ToString(), Label2);

        // Display Time
        GUI.Label(new Rect(0, 90, (Screen.width / 2.5f), Screen.height / 2 + 60), "Time: " + gameTime.ToString(), Label2);

        if(gameOver)
        {
            // Display Score
            Label2.fontSize = 32;
            Label2.normal.textColor = Color.red;
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, (Screen.width / 2.5f), Screen.height / 2 + 60), "Score: " + score.ToString(), Label2);
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 2 + 60, (Screen.width / 2.5f), Screen.height / 2 + 60), "Press escape to restart");
        }
    }
}
