using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HelpOverlayHandler : ButtonsHandler
{
    public override void Handle(int index)
    {
        switch (index)
        {
            case 0:
                GameObject.Find("HelpCanvas").SetActive(false);
                GameObject.Find("PauseCanvas").GetComponent<PauseHandler>().IsHelpActive = false;
                break;
        }
    }
}
