using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Obstaclegenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] TentacleLocations;
    [SerializeField] private GameObject[] MouthLocations;
    [SerializeField] private int spawnDelay = 10;

    private void Start()
    {
        randomize();
        //foreach (var Tentacle in TentacleLocations) Tentacle.SetActive(false);
        foreach (var Mouth in MouthLocations) Mouth.SetActive(false);
        StartCoroutine(SpawnObstacle(spawnDelay));
    }
    /// <summary>
    /// activate an obstacle
    /// </summary>
    /// <param name="obstacleNumber">number of obstalce in array</param>
    /// <param name="Obstacle_ID">ID of the type of obstacle</param>
    public void SpawnObstacle(int obstacleNumber, int Obstacle_ID)
    {
        if (Obstacle_ID == 0) TentacleLocations[obstacleNumber].SetActive(true);
        else if(Obstacle_ID == 1) MouthLocations[obstacleNumber].SetActive(true);
    }

    IEnumerator SpawnObstacle(int time)
    {
        SpawnObstacle(Random.Range(0, TentacleLocations.Length), 0);
        SpawnObstacle(Random.Range(0, MouthLocations.Length), 1);
        yield return new WaitForSeconds(time);
        StartCoroutine(SpawnObstacle(time));
    }

    private void randomize()
    {
        foreach(var tentacle in TentacleLocations)
        {
            TentacleLock tent = tentacle.GetComponent<TentacleLock>();
            if(tent.Lock == false) tentacle.transform.position = new Vector3(Random.Range(-50, 100), tentacle.transform.position.y, Random.Range(-90, 60));
        }

        foreach(var mouth in MouthLocations)
        {
            mouth.transform.position = new Vector3(Random.Range(-50, 100), mouth.transform.position.y, Random.Range(-90, 60));
        }
    }
}
