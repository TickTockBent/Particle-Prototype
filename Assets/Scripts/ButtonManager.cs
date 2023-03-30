using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    private bool clicked;

    public void Quit()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }

    public void Restart()
    {
        if (!clicked)
        {
            if (Data.Wingame)
            {
                Data.Respawns++;
            }
            clicked = !clicked;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
