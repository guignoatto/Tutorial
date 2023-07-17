using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffTestController : MonoBehaviour
{
    [Range(0.1f, 5)]
    public float _bulletSpeedBuff = 1;

    [Range(0.1f, 5)]
    public float _playerSpeedBuff = 1;

    private void Update()
    {
        BuffStats.bulletSpeedBuff = _bulletSpeedBuff;
        BuffStats.playerSpeedBuff = _playerSpeedBuff;
    }
}
