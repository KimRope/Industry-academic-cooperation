using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUIBtn : MonoBehaviour
{
    public Transform menuUI;
    public Transform helpUI;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0.0f;
    }

    public void GameStart()
    {
        Time.timeScale = 1.0f;
        menuUI.gameObject.SetActive(false);
    }

    public void HelpView()
    {
        menuUI.gameObject.SetActive(false);
        helpUI.gameObject.SetActive(true);
    }

    public void GameQuit()
    {
        Application.Quit();
    }
}
