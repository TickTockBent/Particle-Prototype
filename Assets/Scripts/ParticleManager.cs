using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class ParticleManager : MonoBehaviour {

    //private GameObject uiCanvas;
    private static TextMeshProUGUI uiText;
    [SerializeField] private TextMeshProUGUI setUIText;
    private GameObject[] particleGameObjects;
    private static GameObject particle;
    private static float speed = 100f;
    [SerializeField] private GameObject restartMenu;


    // Use this for initialization
    void Start()
    {
        particle = (GameObject) Resources.Load("Particle");
        
        //uiCanvas = GameObject.Find("UI Template");
        uiText = setUIText;
        
        if (uiText == null)
        {
            Debug.Log("uiText Null Reference");
        }
    }
    
    void LateUpdate()
    {
        particleGameObjects = GameObject.FindGameObjectsWithTag("Particle");
        if (particleGameObjects.Length == 0 && Blaster_Behavior.shotsRemaining == 0)
        {
            Time.timeScale = 0;
            Data.Endgame = true;
            Data.Wingame = false;
            GameOver();
        }
    }

    void GameOver()
    {
        restartMenu.SetActive(true);
    }

    public static void NewParticle(Vector2 location, Vector2 direction)
    {
        var newParticle = Instantiate(particle, location, Quaternion.identity);
        
        newParticle.transform.parent = GameObject.Find("Particles").transform;
        newParticle.GetComponent<Rigidbody2D>().AddForce(direction * speed);
    }
}
