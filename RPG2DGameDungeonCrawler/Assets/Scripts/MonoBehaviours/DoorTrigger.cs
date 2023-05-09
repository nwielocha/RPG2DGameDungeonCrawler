using System.Collections;
using System.Collections.Generic;
using UnityEngine;
  
public class DoorTrigger : MonoBehaviour
{
    private bool triggerActive = false;
    public bool isUnlocked;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            triggerActive = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            triggerActive = false;
        }
    }

    private void Update()
    {
        if (triggerActive && isUnlocked && Input.GetKeyDown(KeyCode.E))
        {
            var transitionScript = gameObject.GetComponent<DoorTransition>();
            var doorScript = gameObject.GetComponent<DoorScript>();
            Directions direction = doorScript.direction;
            transitionScript.Action(direction);
        }
    }
}