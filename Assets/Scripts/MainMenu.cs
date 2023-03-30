using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject MenuPanel;
    [SerializeField] private GameObject HowToPlayPanel;
    [SerializeField] private GameObject CreditsPanel;

    public void HowToPlay()
    {
        HowToPlayPanel.SetActive(true);
        MenuPanel.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Credits()
    {
        CreditsPanel.SetActive(true);
        MenuPanel.SetActive(false);
    }

    public void NewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
