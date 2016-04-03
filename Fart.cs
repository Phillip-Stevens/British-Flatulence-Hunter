using UnityEngine;
using System.Collections;

public class Fart : MonoBehaviour
{

    public float timer = 0;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Creates a timer
        timer += 1.0f * Time.deltaTime;

        if (timer >= 0.5f)
        {
            Destroy(this.gameObject);
        }
    }

    // check collision
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
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
}
