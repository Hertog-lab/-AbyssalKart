using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Kart : MonoBehaviour
{
    [SerializeField] private float speedIncrease;
    
    public float p_acceleration;
    public Vector3 direction = new Vector3(0f,0f,1f);
    private Vector3 acceleration;
    private Vector3 oldDirection;
    //Drifting Relaited
    [SerializeField] private float rotationStrenghtModifier;
    public bool p_drifting = false;
    private float minRotateStrenght = 3;

    private void Update()
    {
        Input();
        //Drifting(); Input runs here and gets direction from Input
        MovementApplicance();
    }

    private void Input()
    {
        if (UnityEngine.Input.GetAxisRaw("Vertical") > 0f)
        {
            direction.x += direction.x * speedIncrease * 0.1f * Time.deltaTime;
            direction.z += direction.z * speedIncrease * 0.1f * Time.deltaTime;
        }
        if      (UnityEngine.Input.GetAxisRaw("Horizontal") > 0f) {Drifting(false); p_drifting = true;}
        else if (UnityEngine.Input.GetAxisRaw("Horizontal") < 0f) {Drifting(true ); p_drifting = true;}
        else    
        {
            rotationStrenghtModifier = minRotateStrenght;
            p_drifting = false;
        }
    }

    private void Drifting(bool left)
    {
        float driftStrength = (p_acceleration < 5f) ? (direction.x + direction.z) * 7.5f : 0f;
        rotationStrenghtModifier = Mathf.Clamp(rotationStrenghtModifier += rotationStrenghtModifier * Time.deltaTime,minRotateStrenght,10f);
        direction = (left == false) 
            ? direction = new Vector3(direction.x += driftStrength + (direction.z * rotationStrenghtModifier) * Time.deltaTime, direction.y,direction.z -= (direction.x * rotationStrenghtModifier) * Time.deltaTime) 
            : direction = new Vector3(direction.x -= driftStrength + (direction.z * rotationStrenghtModifier) * Time.deltaTime, direction.y,direction.z += (direction.x * rotationStrenghtModifier) * Time.deltaTime);
        direction = (left == false) 
            ? direction = new Vector3(direction.x -= oldDirection.z * Time.deltaTime, direction.y,direction.z += oldDirection.x * Time.deltaTime) 
            : direction = new Vector3(direction.x += oldDirection.z * Time.deltaTime, direction.y,direction.z -= oldDirection.x * Time.deltaTime);

    }
    
    private void MovementApplicance()
    {
        acceleration = direction;
        p_acceleration = acceleration.magnitude;
        oldDirection = direction;
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.position += acceleration * Time.deltaTime;
    }
}