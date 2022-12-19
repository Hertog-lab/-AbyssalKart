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
    public float rotationStrenghtModifier;
    public bool p_drifting = false;
    public float minRotateStrenght = 3;

    private bool playerControl = true;
    
    //camera stuff
    //[SerializeField] private GameObject camera1;
    //[SerializeField] private GameObject camera2;

    private void Update()
    {
        if (playerControl == true) { Input(); }
        SpeedControll();
        MovementApplicance();
    }

    private void Input()
    {
        //if (UnityEngine.Input.GetKey(KeyCode.F)) { camera1.GetComponent<Camera>().enabled = false; camera2.GetComponent<Camera>().enabled = true; }
      //  else { camera1.GetComponent<Camera>().enabled = true; camera2.GetComponent<Camera>().enabled = false; }
        
        if (UnityEngine.Input.GetAxisRaw("Vertical") > 0f)
        {
            direction += direction * (speedIncrease * 0.1f * Time.deltaTime);
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

    private void SpeedControll()
    {
        if (p_acceleration < 5.3f)
        {
            direction += direction * (speedIncrease * 0.1f * Time.deltaTime);
        }
    }
    
    private void MovementApplicance()
    {
        acceleration = new Vector3(direction.x, 0f, direction.z);
        p_acceleration = acceleration.magnitude;
        oldDirection = direction;
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.position += acceleration * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject);
        
        if (collision.transform.GetComponent<Wall>() == true)
        {
            direction = Vector3.Reflect(direction, collision.GetContact(0).normal);
        }
        if (collision.transform.GetComponent<MouthHazard>() == true)
        {
            playerControl = false;
            Debug.Log("gobble gobble");
            direction = new Vector3(direction.x += direction.z * Time.deltaTime, direction.y,direction.z -= direction.x * Time.deltaTime);
        }
        
    }
}