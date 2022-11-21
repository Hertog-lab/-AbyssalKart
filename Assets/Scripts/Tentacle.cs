using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : MonoBehaviour
{
    private Manager manager;

    private void Start()
    {
        manager = FindObjectOfType<Manager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Kart kart = other.GetComponent<Kart>();
        if(kart.drifting == true)
        {
            DestroyTentacle();
            Debug.LogError("DriftKill");
        }
        else
        {
            manager.GameOver();
        }
    }

    private void DestroyTentacle()
    {

    }
}
