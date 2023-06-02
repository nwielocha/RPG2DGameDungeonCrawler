using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : ButtonsHandler
{
    public override void Handle(int index)
    {
        switch(index){
        case 0:
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        break;
        case 1:
            SceneManager.LoadScene("LevelGeneratorScene", LoadSceneMode.Single);
        break;
        case 2:
            Application.Quit();
        break;
        }
    }
}
