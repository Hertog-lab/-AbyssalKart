using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleAnimReciever : MonoBehaviour
{
    public Tentacle tent;
    
    public void FinishSlam()
    {
        tent.anim.SetBool("Slam", false);
        tent.slammin = false;
    }
}
