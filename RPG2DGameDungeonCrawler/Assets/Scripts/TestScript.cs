using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : IObserver
{
    public int LevelNumber {get; set;}
    public TestScript(){
        this.LevelNumber = 2;
    }

    public void UpdateObserver(ISubject subject){
        
    }
}
