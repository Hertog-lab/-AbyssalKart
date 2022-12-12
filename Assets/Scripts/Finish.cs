using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private checkpoint[] Checkpoints;
    private int currentCheckpoint = 0;
    private bool canFinish;

    private void OnTriggerEnter(Collider collision)
    {
        Manager manager = FindObjectOfType<Manager>();

        checkpoint chechpoint = collision.gameObject.GetComponent<checkpoint>();
        if(chechpoint == null)
        {
            return;
        }
        Debug.Log("IDK");
        if(chechpoint.finish == true && canFinish == true)
        {
            manager.Lap();
        }
        else if (chechpoint.number == currentCheckpoint + 1)
        {
            Debug.Log("checkPoint : " + currentCheckpoint);
            if(currentCheckpoint == Checkpoints.Length)
            {
                canFinish = true;
                currentCheckpoint = 0;
            }
            currentCheckpoint++;
        }
    }
}
