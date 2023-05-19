using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISubject
{
    void NotifyObservers();
    void Attach(IObserver observer);
    void Detach(IObserver observer);
}
