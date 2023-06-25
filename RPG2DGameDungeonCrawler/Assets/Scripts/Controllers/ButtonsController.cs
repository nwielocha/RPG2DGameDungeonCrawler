using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsController : MonoBehaviour
{
    public int CurrentIndex { get; private set; } = 0;
    public float JumpValue = 1;
    public GameObject SelectionRect;
    public List<GameObject> Buttons;

    void Start()
    {
        AdjustButtons();
    }

    void Update()
    {
        HandleUserInput();
    }

    private void AdjustButtons()
    {
        for (int i = 0; i < Buttons.Count; i++)
        {
            if (i > 0)
            {
                Buttons[i].transform.position =
                    Buttons[i - 1].transform.position + new Vector3(0, -JumpValue, 0);
            }
        }
    }

    private void HandleUserInput()
    {
        if (Input.GetKeyDown("w") && CurrentIndex > 0)
        {
            SelectionRect.transform.position += new Vector3(0, JumpValue, 0);
            CurrentIndex--;
        }

        if (Input.GetKeyDown("s") && CurrentIndex < Buttons.Count - 1)
        {
            SelectionRect.transform.position += new Vector3(0, -JumpValue, 0);
            CurrentIndex++;
        }
    }
}
