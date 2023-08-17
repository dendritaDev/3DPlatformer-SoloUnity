﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivator : MonoBehaviour
{

    public static BossActivator instance;

    private void Awake()
    {

        instance = this; 
    }

    public GameObject entrance, theBoss;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            entrance.SetActive(false);
            theBoss.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
