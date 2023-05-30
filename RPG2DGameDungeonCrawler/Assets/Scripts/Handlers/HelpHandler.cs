using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HelpHandler : ButtonsHandler
{
    public override void Handle(int index)
    {
        switch(index){
        case 0:
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        break;
        }
    }
}
