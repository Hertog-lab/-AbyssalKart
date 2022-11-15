using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private float drivingSpeed;
    void Update()
    {
        Vector3 velocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical")).normalized * drivingSpeed;
        transform.position += velocity * Time.deltaTime;
        transform.LookAt(transform.position + velocity);
        Debug.DrawRay(transform.position, velocity, Color.red);
    }
}