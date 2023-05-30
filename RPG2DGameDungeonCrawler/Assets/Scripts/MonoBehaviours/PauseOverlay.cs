using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseOverlay : MonoBehaviour
{
    public bool IsShowing;
    void Start()
    {
        IsShowing = true;
    }

    void Update()
    {
        gameObject.SetActive(IsShowing);
    }
}
