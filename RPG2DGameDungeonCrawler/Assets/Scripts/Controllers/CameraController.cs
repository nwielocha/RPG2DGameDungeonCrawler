using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Character PlayerController;
    private Vector3 _targetCameraPos, _initPos;
    private float _cameraNewX, _cameraNewY, _playerNewX, _playerNewY, _elapsed, _duration;
    private bool _active;

    void Start()
    {
        _active = false;
        _elapsed = 0;
        _duration = 1f;
    }

    void Update()
    {   
        if(_active)
        {
            _elapsed += Time.deltaTime;
            float completion = _elapsed / _duration;
            transform.position = Vector3.Lerp(_initPos, _targetCameraPos, completion);
            if(transform.position == _targetCameraPos){
                _active = false;
                _elapsed = 0;
                player.transform.position = new Vector3(
                    _playerNewX,
                    _playerNewY,
                    player.transform.position.z
                );
                PlayerController.LockControlls = false;
            } 
        }
    }

    public void Translate(Directions direction, Vector3 init)
    {
        _active = true;
        _initPos = init;

        switch(direction){
            case Directions.Up:
                _cameraNewX = init.x;
                _cameraNewY = init.y + (float) RoomComponent.Height;
                _playerNewX = player.transform.position.x;
                _playerNewY = player.transform.position.y + 4;
            break;
            case Directions.Down:
                _cameraNewX = init.x;
                _cameraNewY = init.y - (float) RoomComponent.Height;
                _playerNewX = player.transform.position.x;
                _playerNewY = player.transform.position.y - 4;
            break;
            case Directions.Right:
                _cameraNewX = init.x + (float) RoomComponent.Width;
                _cameraNewY = init.y;
                _playerNewX = player.transform.position.x + 3;
                _playerNewY = player.transform.position.y;
            break;
            case Directions.Left:
                _cameraNewX = init.x - (float) RoomComponent.Width;
                _cameraNewY = init.y;
                _playerNewX = player.transform.position.x - 3;
                _playerNewY = player.transform.position.y;
            break;
        }

        _targetCameraPos = new Vector3(
            _cameraNewX,
            _cameraNewY,
            init.z
        );
    }
}
