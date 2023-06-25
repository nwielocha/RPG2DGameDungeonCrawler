using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseHandler : ButtonsHandler
{
    private GameObject _helpCanvas;

    void Awake()
    {
        _helpCanvas = GameObject.Find("HelpCanvas");
        _helpCanvas.SetActive(false);
    }

    public override void Handle(int index)
    {
        switch (index)
        {
            case 0:
                LevelController.UnPause();
                break;
            case 1:
                SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
                break;
            case 2:
                _helpCanvas.SetActive(true);
                break;
            case 3:
                Application.Quit();
                break;
        }
    }
}
