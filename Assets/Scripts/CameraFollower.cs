using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private float speed = 8;
    public Transform target;
    public bool anticipateMovement;
    [SerializeField] private float anticipateAmount;
    
    private Vector3 prevPos, curPos;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = target.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (anticipateMovement)
        {
            curPos = target.position;
            Vector3 diff = (curPos - prevPos)/Time.deltaTime*anticipateAmount;
            
            transform.position = Vector3.Lerp(transform.position, target.position + diff, Time.deltaTime*speed);
            
            prevPos = curPos;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime*speed);
        }
    }
}
