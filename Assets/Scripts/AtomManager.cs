using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Analytics;
using Random = UnityEngine.Random;

public class AtomManager : MonoBehaviour
{
    private GameObject uiCanvas;
    [SerializeField] private GameObject restartMenu;
    private static TextMeshProUGUI uiText;
    private GameObject[] atoms;
    private static List<GameObject> objList;
    private AudioSource atomManagerAudioSource;
    [SerializeField] private AudioClip distortionAudioClip;
    private GameObject atom;
    private static int score;
    //private int rightClickShots = 3;

    //private float waitForSpawn = 5f;
    private float theCountdown = 10f;

    private float xMin = -6f;
    private float xMax = 6f;

    private float yMin = -4f;
    private float yMax = 4f;
    
    // Use this for initialization
	void Start ()
	{
	    atom = (GameObject)Resources.Load("Atom Template");

	    atomManagerAudioSource = GetComponent<AudioSource>();

	    Data.Endgame = false;
	    Data.Wingame = false;
        
        objList = new List<GameObject>();
        
	    for (int i = 0; i < LevelObjectives.InitialSpawn; i++)
	    {
	        SpawnAtom();
	    }

	    atoms = GameObject.FindGameObjectsWithTag("Atom");
	    Debug.Log("Found " + atoms.Length + " Atoms.");

        foreach (GameObject o in atoms)
	    {
	        objList.Add(o);
            Debug.Log("Added atom to list.");
	    }

        uiCanvas = GameObject.Find("UI Template");
	    uiText = uiCanvas.transform.Find("AtomPanel").Find("AtomText").gameObject.GetComponent<TextMeshProUGUI>();
	    UpdateCount();
	}
	
	// Update is called once per frame
	void LateUpdate ()
	{
	    if (objList.Count == 0)
	    {
	        Time.timeScale = 0;
	        Data.Endgame = true;
	        Data.Wingame = true;
	        GameOver();
	    }

	    /*if (theCountdown <= 0f && atomCount <= LevelObjectives.RespawnNumber && LevelObjectives.RespawningAtoms == true)
	    {
	        SpawnAtom();
	        theCountdown = waitForSpawn;
	    }*/

	    if (Input.GetMouseButtonDown(1))
	    {
	        Distortion();
	    }

	    theCountdown -= Time.deltaTime;
	}

    void Distortion()
    {
        atomManagerAudioSource.clip = distortionAudioClip;
        atomManagerAudioSource.Play();

        Vector3 clickPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D[] hitCollider2Ds = Physics2D.OverlapCircleAll(clickPoint, 5);
        foreach (Collider2D c in hitCollider2Ds)
        {
            if (c.transform.tag == "Atom")
            {
                LevelObjectives.Score(1);
            }

            Vector2 directionToClickPoint = clickPoint - c.transform.position;
            c.gameObject.GetComponent<Rigidbody2D>().AddForce(directionToClickPoint.normalized * 155);
        }
    }

    void GameOver()
    {
        restartMenu.SetActive(true);
    }

    void SpawnAtom()
    {
        Vector2 pos = new Vector2(
            Random.Range(xMin, xMax),
            Random.Range(yMin, yMax));
        Instantiate(atom, pos, Quaternion.identity);
    }

    static void UpdateCount()
    {
        uiText.text = LevelObjectives.LeftText + objList.Count;
    }

    public static int Count()
    {
        return (objList.Count);
    }

    public static void Register (GameObject obj)
    {
        objList.Add(obj);
        Debug.Log("Atom added to list.");
        UpdateCount();
    }

    public static void UnRegister (GameObject obj)
    {
        objList.Remove(obj);
        Debug.Log("Atom removed from list.");
        UpdateCount();
    }

}
