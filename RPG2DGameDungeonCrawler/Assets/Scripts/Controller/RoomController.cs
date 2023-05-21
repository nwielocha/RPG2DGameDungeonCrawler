using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public RoomComponent Room { get; set; }
    private float _timeToRespawn;
    private float _time;
    private bool _wasCleared = false;
    private bool _isPlayerPresent;
    private uint _livingEnemiesCount;
    private RoomGenerator _roomGenerator;


    void Start()
    {
        _roomGenerator = new RoomGenerator(this);
        _timeToRespawn = CalculateRespawnTime();
        _roomGenerator.Generate();

        // call room generator
        // call room initiator
    }

    void Update()
    {
        if(!_isPlayerPresent && _livingEnemiesCount <= 0)
        {
            _time += Time.deltaTime;
            if(_time >= _timeToRespawn)
            {
                // respawn enemies
                // set time to 0
            }
        }
    }

    private float CalculateRespawnTime()
    {
        uint levelNumber = LevelController.MainGameObject.GetComponent<LevelController>().LevelNumber;
        
        return (float) (180 + 30 * levelNumber);
    }
}
