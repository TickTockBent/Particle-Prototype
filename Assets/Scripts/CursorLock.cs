﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorLock : MonoBehaviour
{
    
	// Use this for initialization
	void Start ()
	{
	    
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (Input.GetMouseButtonDown(0))
	    {
	        Cursor.lockState = CursorLockMode.Confined;
	    }
	}
}
