using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public float speed = 8;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = target.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime*speed);
    }
}
