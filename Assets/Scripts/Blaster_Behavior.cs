using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Blaster_Behavior : MonoBehaviour
{

    [SerializeField] private float rotateSpeed = 1;
    private Vector2 particleOrigin;
    private Vector2 particleDirection;
    private GameObject blasterHead;
    private Vector2 targetDirection;
    private float targetAngle;
    private Quaternion rotation;
    public static int shotsRemaining;
    [SerializeField] private GameObject shotsGameObject;
    private TextMeshProUGUI shotsText;
    private AudioSource blasterAudioSource;

    [SerializeField] private AudioClip shootAudioClip;

    void Start()
    {
        blasterHead = gameObject.transform.Find("Blaster_Head").gameObject;
        shotsText = shotsGameObject.GetComponent<TextMeshProUGUI>();
        shotsRemaining = LevelObjectives.ShotsAllowed;
        blasterAudioSource = GetComponent<AudioSource>();
    }

    void FaceMouse()
    {
        targetDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        
        if (targetAngle <= -90f)
        {
            targetAngle = 170f;
        }

        if (targetAngle > -90f && targetAngle <= 0f)
        {
            targetAngle = 10f;
        }

        rotation = Quaternion.AngleAxis(targetAngle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);
    }
	
	void Update ()
	{
	    FaceMouse();

	    if (Input.GetMouseButtonDown(0) && shotsRemaining >= 1)
	    {
	        blasterAudioSource.clip = shootAudioClip;
            blasterAudioSource.Play();

	        particleOrigin = blasterHead.transform.position;
	        particleDirection = blasterHead.transform.up;

	        ParticleManager.NewParticle(particleOrigin, particleDirection);

	        shotsRemaining--;
            shotsText.SetText("Shots: " + shotsRemaining);

	        Debug.Log(shotsRemaining);
	    }
	}
}
