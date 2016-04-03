using UnityEngine;
using System.Collections;

public class Turtle : MonoBehaviour {
    public float moveSpeed = 0.1f;


	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x * -moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
    }

    // check collision
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "Animal")
        {
            Physics2D.IgnoreCollision(this.GetComponent<BoxCollider2D>(), coll.gameObject.GetComponent<BoxCollider2D>());
        }

        if (coll.gameObject.tag != "Respawn" && coll.gameObject.tag != "Player" && coll.gameObject.tag != "Animal")
        {
            Destroy(this.gameObject);
        }
    }

    void OnDestroy()
    {
        // Get the parent object script
        GameObject thePlayer = GameObject.Find("Playerz");
        Player playerScript = thePlayer.GetComponent<Player>();
        playerScript.score += 10.0f;

        if (playerScript.gameOver == false)
        {
            if (moveSpeed == 0.4f)
            {
                playerScript.score += 10.0f;
            }
            else if (moveSpeed == 0.8f)
            {
                playerScript.score += 30.0f;
            }
            else
            {
                playerScript.score += 50.0f;
            }
        }
    }
}
