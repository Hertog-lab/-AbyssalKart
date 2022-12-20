using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : MonoBehaviour
{
    private Kart target;
    public Transform t;
    Animator anim;
    public bool slammin;
    public float range;

    private void Start()
    {
        target = FindObjectOfType<Kart>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector3 targetpos = target.transform.position;
        targetpos.y = transform.position.y;
        if (!slammin)
        {
            t.LookAt(targetpos);
            
            float dist = (targetpos - transform.position).magnitude;
            if (dist < range)
            {
                slammin = true;
                t.LookAt(target.transform.position + target.p_direction);
                anim.SetBool("Slam", true);
            }
        }
    }
    
    public void FinishSlam()
    {
        anim.SetBool("Slam", false);
        slammin = false;
    }
}
