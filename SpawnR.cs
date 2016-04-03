using UnityEngine;
using System.Collections;

public class SpawnR : MonoBehaviour
{

    // Variables to spawn things
    public float timer = 0.0f;
    public float cooldown = 0.0f;
    public GameObject[] animals;
    public int toSpawn = 0;
    public bool canSpawn = true;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Spawn Monster
        if (canSpawn == true)
        {

            toSpawn = Random.Range(0, 9);

            if (toSpawn >= 0 && toSpawn <= 3) // Squirrel
            {
                GameObject squirrel = Instantiate(animals[0], this.transform.position, this.transform.rotation) as GameObject;
                cooldown = Random.Range(1.0f, 3.0f);
                canSpawn = false;
            }
            else if (toSpawn == 5) // deer spawn
            {
                GameObject deer = Instantiate(animals[1], this.transform.position, this.transform.rotation) as GameObject;
                cooldown = Random.Range(2.0f, 6.0f);
                canSpawn = false;
            }
            else // Turtle
            {
                GameObject turtle = Instantiate(animals[2], this.transform.position, this.transform.rotation) as GameObject;
                cooldown = Random.Range(0.5f, 2.0f);
                canSpawn = false;
            }

        }
        else // can spawn must be false so start the timer
        {
            timer += 1.0f * Time.deltaTime;

            if (timer >= cooldown)
            {
                timer = 0;
                cooldown = 0;
                canSpawn = true;
            }
        }
    }
}
