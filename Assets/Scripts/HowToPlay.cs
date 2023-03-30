using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlay : MonoBehaviour
{

    [SerializeField] private GameObject mainMenu;
	
	void Update ()
	{
	    if (Input.anyKeyDown)
	    {
            mainMenu.SetActive(true);
            gameObject.SetActive(false);
	    }
	}
}
