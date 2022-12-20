using UnityEngine;

public class Obstaclegenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] TentacleLocations;
    [SerializeField] private GameObject[] MouthLocations;

    private void Start()
    {
        foreach (var Tentacle in TentacleLocations) Tentacle.SetActive(false);
        foreach (var Mouth in MouthLocations) Mouth.SetActive(false);           
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
}
