using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Stats : MonoBehaviour
{
    public int playerLife;

    // Start is called before the first frame update
    void Start()
    {
        playerLife = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerLife <= 0)
        {
            print("A player have died");
        }
    }
}
