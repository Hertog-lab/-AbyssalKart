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
    //Drifting Relaited
    [SerializeField] private float rotationStrenghtModifier;
    public bool p_drifting = false;
    private float minRotateStrenght = 2.5f; 
    
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
            if (direction.x > direction.z)
            { direction.x += speedIncrease * Time.deltaTime; } 
            else if (direction.x < direction.z) 
            { direction.z += speedIncrease * Time.deltaTime; }
        }
        if      (UnityEngine.Input.GetAxisRaw("Horizontal") > 0f) {Drifting(false); p_drifting = true;}
        else if (UnityEngine.Input.GetAxisRaw("Horizontal") < 0f) {Drifting(true ); p_drifting = true;}
        else    {rotationStrenghtModifier = minRotateStrenght;  p_drifting = false;}
    }

    private void Drifting(bool left)
    {
        minRotateStrenght = 2.5f * (acceleration.magnitude * 0.10f);
        rotationStrenghtModifier = Mathf.Clamp(rotationStrenghtModifier += rotationStrenghtModifier * Time.deltaTime,minRotateStrenght,10f);
        direction = (left == false) 
            ? direction = new Vector3(direction.x += direction.z * rotationStrenghtModifier * Time.deltaTime, direction.y,direction.z -= direction.x * rotationStrenghtModifier * Time.deltaTime) 
            : direction = new Vector3(direction.x -= direction.z * rotationStrenghtModifier * Time.deltaTime, direction.y,direction.z += direction.x * rotationStrenghtModifier * Time.deltaTime);
    }
    
    private void MovementApplicance()
    {
        acceleration = direction;
        p_acceleration = acceleration.magnitude;
        float x = transform.rotation.x;
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.position += acceleration * Time.deltaTime;
    }
}