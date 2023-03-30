using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{

    [SerializeField] private GameObject menuPanel;

	// Use this for initialization
	void Start ()
	{
		menuPanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (Input.GetKeyDown(KeyCode.Escape))
	    {
	        if (!menuPanel.activeInHierarchy)
	        {
	            PauseGame();
	            return;
	        }

	        if (menuPanel.activeInHierarchy)
	        {
	            ContinueGame();
	            return;
	        }
	    }
	}

    private void PauseGame()
    {
        Debug.Log("Tried to pause");
        Time.timeScale = 0;
        Data.Pausegame = true;
        menuPanel.SetActive(true);
    }

    private void ContinueGame()
    {
        Debug.Log("Tried to pause");
        Data.Pausegame = false;
        Time.timeScale = 1;
        menuPanel.SetActive(false);
    }
}
