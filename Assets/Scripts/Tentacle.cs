using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : MonoBehaviour
{
    public Kart target;
    private KartPassTrough kartfinder;
    public Transform t;
    public Animator anim;
    public bool slammin;
    public float range;
    private GameObject player;
    private Manager manager;

    private void Start()
    {
        kartfinder = GetComponentInParent<KartPassTrough>();
        anim = transform.parent.parent.parent.gameObject.GetComponent<Animator>();
        manager = FindObjectOfType<Manager>();
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

    private void OnTriggerEnter(Collider other)
    {
        if(other == target.GetComponent<Collider>())
        {
            manager.GameOver();
        }
    }
}
