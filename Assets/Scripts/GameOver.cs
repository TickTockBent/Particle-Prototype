using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject menuGameObject;
    private TextMeshProUGUI menuText;
    [SerializeField] private GameObject otherGameObject;
    private TextMeshProUGUI otherText;
    [SerializeField] private GameObject buttGameObject;
    private TextMeshProUGUI buttonText;

	// Use this for initialization
	void Start ()
	{
	    menuText = menuGameObject.GetComponent<TextMeshProUGUI>();
	    otherText = otherGameObject.GetComponent<TextMeshProUGUI>();
	    buttonText = buttGameObject.GetComponent<TextMeshProUGUI>();
	}

    void OnEnable()
    {

        menuText = menuGameObject.GetComponent<TextMeshProUGUI>();
        otherText = otherGameObject.GetComponent<TextMeshProUGUI>();
        buttonText = buttGameObject.GetComponent<TextMeshProUGUI>();

        if (Data.Pausegame)
        {
            menuText.SetText("Game Paused");
            otherText.SetText("Press ESC to resume");
            buttonText.SetText("Restart Level");
        }

        if (!Data.Pausegame)
        {
            switch (Data.Wingame)
            {
                case true:
                    menuText.SetText("You Won!");
                    otherText.SetText("Each level gets harder as you progress");
                    buttonText.SetText("Next Level");
                    break;
                case false:
                    menuText.SetText("You Lost!");
                    otherText.SetText("Running out of shots ends the level");
                    buttonText.SetText("Restart Level");
                    break;
            }
        }
    }
}
