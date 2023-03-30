using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_Behavior : MonoBehaviour
{

    
    [SerializeField] private float minSpeed = 8f;

    private float timeToLive = 5f;
    private float timeElapsed = 0;
    private Vector2 newImpulse;
    private Rigidbody2D rb;
    private float nudgeFactor = 0.2f;

    private AudioSource bounceAudioSource;
    private AudioClip bounceClip;
    [SerializeField] private AudioClip[] bounceAudioClips;

	// Use this for initialization
	void Start ()
	{
	    bounceAudioSource = GetComponent<AudioSource>();
	}
    
   // Update is called once per frame
	void Update ()
	{

	    CheckVelocity();

        rb.velocity = 10f * (rb.velocity.normalized);
        
	    timeElapsed += Time.deltaTime;
	    timeToLive -= Time.deltaTime;

	    if (timeToLive <= 0f)
	    {
            LevelObjectives.Score(1);
	        Destroy(gameObject);
	    }

	}

    void CheckVelocity()
    {
        if (ParticleVelocity().x == 0 && ParticleVelocity().y == 0)
        {
            rb.AddForce(RandomVector2());
        }
    }

    Vector2 RandomVector2()
    {
        Vector2 randomVector = new Vector2(
            Random.Range(0.01f, nudgeFactor),
            Random.Range(0.01f, nudgeFactor));

        return randomVector;
    }

    Vector2 ParticleVelocity()
    {
        rb = GetComponent<Rigidbody2D>();
        return rb.velocity;
    }

    void NewImpulse()
    {
        newImpulse = new Vector2(Random.Range(-1, 2), Random.Range(-1, 1));

        while (newImpulse.x == 0 || newImpulse.y == 0)
        {
            newImpulse = new Vector2(Random.Range(-1, 2), Random.Range(-1, 1));
        }

        rb.AddForce(newImpulse.normalized * minSpeed);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        int index = Random.Range(0, bounceAudioClips.Length);
        bounceClip = bounceAudioClips[index];

        bounceAudioSource.clip = bounceClip;
        bounceAudioSource.Play();

        if (col.transform.tag == "Atom")
        {
            timeToLive = 5f;
        }

        /*if (col.transform.tag != "Atom")
        {
            CheckNudge();
        }*/
    }

    void CheckNudge()
    {
        Vector2 velocityCheck = rb.velocity.normalized;

        if (velocityCheck.y == 0f)
        {
            if (velocityCheck.x == 1f || velocityCheck.x == -1f)
            {
                Vector2 newVelocity = RandomVector2() + velocityCheck;
                rb.AddForce(newVelocity.normalized);
            }
        }

        if (velocityCheck.x == 0)
        {
            if (velocityCheck.y == 1f || velocityCheck.y == -1f)
            {
                Vector2 newVelocity = RandomVector2() + velocityCheck;
                rb.AddForce(newVelocity.normalized);
            }
        }
    }

}
