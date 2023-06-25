using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : ButtonsHandler
{
    public override void Handle(int index)
    {
        switch (index)
        {
            case 0:
                SceneManager.LoadScene("Level", LoadSceneMode.Single);
                break;
            case 1:
                SceneManager.LoadScene("HelpMenu", LoadSceneMode.Single);
                break;
            case 2:
                Application.Quit();
                break;
        }
    }
}
