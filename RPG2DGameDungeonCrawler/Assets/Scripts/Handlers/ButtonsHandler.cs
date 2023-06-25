using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ButtonsHandler : MonoBehaviour
{
    private ButtonsController _buttonsController;

    void Start()
    {
        _buttonsController = gameObject.GetComponent<ButtonsController>();
    }

    public void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            Handle(_buttonsController.CurrentIndex);
        }
    }

    public abstract void Handle(int index);
}
