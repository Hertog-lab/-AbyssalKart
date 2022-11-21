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

    private void OnCollisionEnter(Collision other)
    {
        Kart kart = other.gameObject.GetComponent<Kart>();
        if(kart.drifting == true)
        {
            DestroyTentacle();
            Debug.Log("DriftKill");
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
