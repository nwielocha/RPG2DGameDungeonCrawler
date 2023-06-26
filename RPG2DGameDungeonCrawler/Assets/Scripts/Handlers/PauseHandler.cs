using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseHandler : ButtonsHandler
{
    private GameObject _helpCanvas;
    public bool IsHelpActive = false;

    void Awake()
    {
        _helpCanvas = GameObject.Find("HelpCanvas");
        _helpCanvas.SetActive(false);
    }

    public override void Handle(int index)
    {
        if (!IsHelpActive)
        {
            switch (index)
            {
                case 0:
                    LevelController.UnPause();
                    GameObject playerObject = GameObject.FindWithTag("Player");
                    Character playerController = playerObject.GetComponent<Character>();
                    playerController.LockControlls = false;
                    break;
                case 1:
                    LevelController.UnPause();
                    SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
                    break;
                case 2:
                    _helpCanvas.SetActive(true);
                    IsHelpActive = true;
                    break;
                case 3:
                    Application.Quit();
                    break;
            }
        }
    }
}
