using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{

    [SerializeField] private GameObject mainMenu;
    
    // Update is called once per frame
	void Update ()
	{
	    if (Input.anyKeyDown)
	    {
            mainMenu.SetActive(true);
            gameObject.SetActive(false);
	    }
	}
}
