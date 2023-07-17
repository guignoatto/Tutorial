using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffStats : MonoBehaviour
{
    public static BuffStats Instance { get; private set; }
    [Range(0, 5)]
    public static float bulletSpeedBuff = 1;
    [Range(0, 5)]
    public static float playerSpeedBuff = 1;

    private void Awake()
    {

        if (Instance == null) 
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
