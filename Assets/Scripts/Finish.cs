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
            canFinish = false;
            currentCheckpoint = 0;
        }
        else if (chechpoint.number == currentCheckpoint)
        {
            Debug.Log("checkPoint : " + currentCheckpoint);
            currentCheckpoint++;
            if(currentCheckpoint == Checkpoints.Length)
            {
                Debug.Log("Finish");
                canFinish = true;
            }
        }
    }

    private void Update()
    {
        Debug.Log(currentCheckpoint + " : checkpointcurrent");
        Debug.Log(Checkpoints.Length + " : checkpoint lenght");
    }
}
