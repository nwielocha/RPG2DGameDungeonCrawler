using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour, ISubject, IObserver
{
    public int LevelNumber {get; set;} 
    private List<IObserver> _observers = new List<IObserver>();
    public static GameObject currentGameObject;
    private void NextLevel(){
        this.LevelNumber++;
        NotifyObservers();
    }

    public void NotifyObservers(){
        foreach(IObserver observer in _observers){
            observer.UpdateObserver(this);
        }
    }

    public void Attach(IObserver observer){
        this._observers.Add(observer);
    }

    public void Detach(IObserver observer){
        this._observers.Remove(observer);
    }

    public void UpdateObserver(ISubject subject){
        NextLevel();
    }

    public void Start()
    {
        this.LevelNumber = 1;
        currentGameObject = gameObject;
        Dungeon dungeon = new Dungeon();
        Attach(dungeon);
    }
}
