using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : IObserver, ISubject
{
    public int LevelNumber {get; set;}
    private List<IObserver> _observers = new List<IObserver>(); 
    private List<Room> _rooms = new List<Room>();
    private DungeonGenerator _generator;

    public Dungeon(){
        _generator = new DungeonGenerator();
        Attach(_generator);
        NotifyObservers();
    }

   public void Attach(IObserver observer){
        this._observers.Add(observer);
    }

    public void Detach(IObserver observer){
        this._observers.Remove(observer);
    }

    public void NotifyObservers(){
        foreach (IObserver observer in _observers){
            observer.UpdateObserver(this);
        }
    }
    
    public void AddRoom(Room room){
        this._rooms.Add(room);
    }

    public void RemoveRoom(Room room){
        this._rooms.Remove(room);
    }

    public void UpdateObserver(ISubject subject){
        if((subject is Level)) 
        {
            this.LevelNumber = (subject as Level).LevelNumber;
            NotifyObservers();
        }
    }
}
