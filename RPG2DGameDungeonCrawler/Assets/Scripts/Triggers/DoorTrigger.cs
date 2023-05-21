using System.Collections;
using System.Collections.Generic;
using UnityEngine;
  
public class DoorTrigger : MonoBehaviour
{
    private bool triggerActive = false;
    public bool isUnlocked;
    private Character _playerController;

    void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        _playerController = playerObject.GetComponent<Character>();
    }

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

        if (triggerActive && isUnlocked && Input.GetKeyDown(KeyCode.E) && !_playerController.LockControlls)
        {
            _playerController.LockControlls = true;
            DoorTransition transitionScript = gameObject.GetComponent<DoorTransition>();
            DoorController doorController = gameObject.GetComponent<DoorController>();
            Directions direction = doorController.Direction;
            transitionScript.Action(direction);
        }
    }
}