using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float Duration;
    public Transform player;
    public Character PlayerController;
    private Vector3 _targetCameraPos,
        _targetPlayerPos,
        _initPos;
    private float _elapsed;

    private bool _activeTransition;

    void Start()
    {
        _activeTransition = false;
        _elapsed = 0;
    }

    void Update()
    {
        if (player != null)
        {
            if (_activeTransition)
            {
                _elapsed += Time.deltaTime;
                transform.position = Vector3.Lerp(_initPos, _targetCameraPos, _elapsed / Duration);

                if (transform.position == _targetCameraPos)
                {
                    _activeTransition = false;
                    _elapsed = 0;

                    player.transform.position = _targetPlayerPos;
                    PlayerController.LockControlls = false;
                }
            }
        }
    }

    public void Translate(Directions direction, Vector3 init)
    {
        _activeTransition = true;
        _initPos = init;

        float cameraNewX = 0,
            cameraNewY = 0,
            playerNewX = 0,
            playerNewY = 0;

        switch (direction)
        {
            case Directions.Up:
                cameraNewX = init.x;
                cameraNewY = init.y + (float)RoomComponent.Height;
                playerNewX = player.transform.position.x;
                playerNewY = player.transform.position.y + 4;
                break;
            case Directions.Down:
                cameraNewX = init.x;
                cameraNewY = init.y - (float)RoomComponent.Height;
                playerNewX = player.transform.position.x;
                playerNewY = player.transform.position.y - 4;
                break;
            case Directions.Right:
                cameraNewX = init.x + (float)RoomComponent.Width;
                cameraNewY = init.y;
                playerNewX = player.transform.position.x + 3;
                playerNewY = player.transform.position.y;
                break;
            case Directions.Left:
                cameraNewX = init.x - (float)RoomComponent.Width;
                cameraNewY = init.y;
                playerNewX = player.transform.position.x - 3;
                playerNewY = player.transform.position.y;
                break;
        }

        _targetCameraPos = new Vector3(cameraNewX, cameraNewY, init.z);
        _targetPlayerPos = new Vector3(playerNewX, playerNewY, player.transform.position.z);
    }
}
