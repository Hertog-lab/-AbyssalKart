using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstaclegenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] TentacleLocations;
    [SerializeField] private GameObject[] MouthLocations;


    private void Update()
    {
        
    }

    private void Start()
    {
        foreach (var Tentacle in TentacleLocations)
        {
            Tentacle.SetActive(false);
        }
        
        foreach (var Mouth in MouthLocations)
        {
            Mouth.SetActive(false);
        }

        int random;
        random = Random.Range(0, TentacleLocations.Length);
        SpawnObstacle(random, true);
    }

    private void SpawnObstacle(int obstacleNumber, bool IsTentacle)
    {
        if (IsTentacle)
        {
            TentacleLocations[obstacleNumber].SetActive(true);
        }
        else
        {
            MouthLocations[obstacleNumber].SetActive(true);
        }
    }
}
