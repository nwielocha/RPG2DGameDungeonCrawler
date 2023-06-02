using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsController : MonoBehaviour
{
    private float _jumpValue = 1;
    public int CurrentIndex { get; private set; }
    public GameObject SelectionRect;
    public List<GameObject> Buttons;

    void Start()
    {
        for(int i = 0; i<Buttons.Count; i++)
        {  
           if(i > 0)
           {
                Buttons[i].transform.position = Buttons[i - 1].transform.position + new Vector3(0, -_jumpValue, 0);
           } 
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("w") && CurrentIndex > 0)
        {
            SelectionRect.transform.position += new Vector3(0, _jumpValue, 0); 
            CurrentIndex--;
        }

        if (Input.GetKeyDown("s") && CurrentIndex < Buttons.Count - 1)
        {
            SelectionRect.transform.position += new Vector3(0, -_jumpValue, 0); 
            CurrentIndex++;
        }
    }
}
