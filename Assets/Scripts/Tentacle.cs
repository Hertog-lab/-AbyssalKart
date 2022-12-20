using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : MonoBehaviour
{
    private Kart kart;

    private void Start()
    {
        kart = FindObjectOfType<Kart>();
    }

    private void Update()
    {
        transform.LookAt(kart.transform);
    }
}
