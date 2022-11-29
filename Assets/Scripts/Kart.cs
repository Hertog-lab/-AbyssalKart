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
    private Vector3 direction = new Vector3(0f,0f,1f);
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
        if (UnityEngine.Input.GetKey(KeyCode.W) == true)
        {
            if (direction.x > direction.z)
            { direction.x += speedIncrease * Time.deltaTime; } 
            else if (direction.x < direction.z) 
            { direction.z += speedIncrease * Time.deltaTime; }
        }
        if      (UnityEngine.Input.GetAxisRaw("Horizontal") > 0) {Drifting(false); p_drifting = true;}
        else if (UnityEngine.Input.GetAxisRaw("Horizontal") < 0) {Drifting(true ); p_drifting = true;}
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
        transform.position += acceleration * Time.deltaTime;
    }
    
    /*
    public float p_acceleration;
    public bool p_drifting = false;
    [SerializeField] private float speedIncreaseAmount = 0f;
    [SerializeField] private float driftSmoothing = 0f;
    private Vector3 currentDirection;
    private Vector3 targetDirection;
    private float inertiaModifier;
    private Rigidbody rbody;
    private Vector3 oldPosition;
    private Vector3 storedInput;
    
    private void Start()
    {
        rbody = GetComponent<Rigidbody>();
        inertiaModifier = 0f;
        p_acceleration = 0f;
    }
    private void DriftingMode(bool left)
    {
        p_drifting = true;
        Vector3 direction = (left == true) ? Vector3.left : Vector3.right;
        targetDirection += direction * (speedIncreaseAmount * Time.deltaTime);
    }

    private void SpeedAppliance()
    {
        storedInput = new Vector3(storedInput.x * speedIncreaseAmount, storedInput.y * speedIncreaseAmount, storedInput.z * speedIncreaseAmount);
        oldPosition = transform.position;
        currentDirection = storedInput * (speedIncreaseAmount * Time.deltaTime);
        storedInput = Vector3.zero;
        p_acceleration = Vector3.Distance(oldPosition, transform.position);
        transform.position += currentDirection + targetDirection;
    }

    private void Update()
    {
        float    inputFB =       Input.GetAxisRaw("Vertical");
        float    inputLR =       Input.GetAxisRaw("Horizontal");
        if      (inputFB >  0) { storedInput = Vector3.forward; }
        if      (inputLR <  0) { DriftingMode(true ); }
        else if (inputLR >  0) { DriftingMode(false); }
        else if (inputLR == 0) { p_drifting = false; }
        SpeedAppliance();
    }*/
}