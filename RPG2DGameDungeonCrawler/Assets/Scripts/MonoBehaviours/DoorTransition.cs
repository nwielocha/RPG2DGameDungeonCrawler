using System.Collections;
using System.Collections.Generic;
using UnityEngine;
  
  public class DoorTransition : MonoBehaviour
    {

        private GameObject _mainCamera;
        private CameraController _cameraController;
        public void Action(Directions direction)
        {
          _mainCamera = GameObject.FindWithTag("MainCamera");
          if(_mainCamera != null){
            _cameraController = _mainCamera.GetComponent<CameraController>();
            _cameraController.Translate(direction, _mainCamera.transform.position);
          }
        }
    }