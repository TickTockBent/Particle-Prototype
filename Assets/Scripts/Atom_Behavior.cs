using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

[RequireComponent(typeof(AudioSource))]

public class Atom_Behavior : MonoBehaviour
{

    [SerializeField] private bool canSplit;
    [SerializeField] private bool canAbsorb;
    [SerializeField] private int numFragments;
    [SerializeField] private int numParticles;
    [SerializeField] private bool hasAbsorbedParticle;
    [SerializeField] private GameObject fissionProduct;
    [SerializeField] private float fissionPower = 10;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private int numImpacts = 2;
    [SerializeField] private AudioClip splitAudioClip;

    private Rigidbody2D rb;
    private AudioSource splitAudioSource;

	// Use this for initialization
	void Start ()
	{
	    rb = GetComponent<Rigidbody2D>();
	    splitAudioSource = GetComponent<AudioSource>();
	    numParticles = Random.Range(1, 3);
	    splitAudioSource.clip = splitAudioClip;
	}
	
	// Update is called once per frame
	void Update ()
	{
		Fission();

	    if (rb.velocity.magnitude > maxSpeed)
	    {
	        rb.velocity = rb.velocity.normalized * maxSpeed;
	    }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "Particle")
        {
            ParticleCollision(col);
        }
    }

    void ParticleCollision(Collision2D col)
    {
        if (!hasAbsorbedParticle && canAbsorb)
        {
            Destroy(col.gameObject);
            hasAbsorbedParticle = true;
        }

        if (!canAbsorb)
        {
            numImpacts--;
            if (numImpacts <= 0)
            {
                LevelObjectives.Score(10);
                Destroy(gameObject);
            }
        }
    }

    void SplitAudio()
    {
        Debug.Log("Playing Audio From " + splitAudioSource);
        Debug.Log("Audio Source Enabled: " + splitAudioSource.enabled);
        Debug.Log("Audio Clip Played: " + splitAudioSource.clip);
        splitAudioSource.Play();
    }

    void Fission()
    {
        if (!hasAbsorbedParticle)
        {
            return;
        }

        if (hasAbsorbedParticle && canSplit)
        {
            LevelObjectives.Score(5);

            SplitAudio();

            var atomPosition = transform.position;

            for (int i = 0; i < numFragments; i++)
            {
                var fragmentImpulse = new Vector2(Random.Range(-1, 1), Random.Range(-1,1));
                fragmentImpulse = fragmentImpulse.normalized;

                var newFragment = Instantiate(fissionProduct, transform.position, Quaternion.identity);
                
                
                newFragment.GetComponent<Rigidbody2D>().AddForce(fragmentImpulse*fissionPower);
                newFragment.transform.parent = transform.parent;
            }

            for (int i = 0; i < numParticles; i++)
            {
                Vector2 newImpulse = new Vector2(
                    Random.Range(-1,1),
                    Random.Range(-1,1));

                ParticleManager.NewParticle(transform.position, newImpulse);
            }

            hasAbsorbedParticle = false;
            AtomManager.UnRegister(gameObject);
            Destroy(gameObject);
            
        }
    }
}
