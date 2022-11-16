using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kart : MonoBehaviour
{
    public Vector2Int input = new Vector2Int();
    
    [Header("Linear movement")]
    public float targetAcceleration = 10; //Update this every round
    [SerializeField] private float passiveAcceleration = 0.5f; //Divided by deltatime
    public float acceleration;
    public float accelerationBoost;
    public bool passiveAccelerate = true;
    float driftPower = 0;
    [Header("Angular movement")]
    public float smoothSteer = 4;
    public float steerBounds = 20;
    public float steerDir;
    [Space(4)]
    public bool drifting;
    [Range(-1,1)] public int driftDirection; //-1 = left, 1 = right
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        input = new Vector2Int(
        ((Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) ? 0 : 
            ((Input.GetKey(KeyCode.A)) ? -1 : 
            ((Input.GetKey(KeyCode.D)) ? 1 : 0))), 
        //If both are pressed, do nothing. If left is held, input.x is -1, if right is held input.x is 1.
        ((Input.GetKey(KeyCode.W)) ? 1 : 0));
        //If up is held, input.y is 1. No brakes :)
        
        //Accelerate towards target acceleration.
        acceleration = Mathf.Clamp(Mathf.MoveTowards(acceleration, acceleration+passiveAcceleration, (Time.deltaTime*passiveAcceleration) * ((passiveAccelerate ? 1 : 0)+input.y)), -targetAcceleration, targetAcceleration);
        accelerationBoost = Mathf.Clamp(Mathf.MoveTowards(accelerationBoost, 0, Time.deltaTime*passiveAcceleration*2), 0, Mathf.Infinity);
        
        //Apply drifting
        if (Input.GetKey(KeyCode.Space))
        {
            if (drifting != true)
            {
                if (steerDir > (steerBounds/2))
                {
                    driftDirection = -1;
                    drifting = true;
                }
                if (steerDir < -(steerBounds/2))
                {
                    driftDirection = 1;
                    drifting = true;
                }
            }
            else
            {
                driftPower += Time.deltaTime;
            }
        }
        else
        {
            if (drifting == true)
            {
                accelerationBoost += Mathf.Clamp(driftPower, 0, targetAcceleration*2);
                driftDirection = 0;
                driftPower = 0;
                drifting = false;
            }
        }
        
        //Steering (clamped between steerBounds)
        if (drifting)
        {
            if (driftDirection == -1) {   
                //Drifting to the left
                steerDir = steerBounds/2 + Mathf.Clamp(Mathf.MoveTowards(steerDir, steerBounds*input.x, Time.deltaTime*(smoothSteer*10)), -steerBounds*(acceleration/targetAcceleration), steerBounds*(acceleration/targetAcceleration));
            }
            else if (driftDirection == 1) {
                //Drifting to the right
                steerDir = -steerBounds/2 + Mathf.Clamp(Mathf.MoveTowards(steerDir, steerBounds*input.x, Time.deltaTime*(smoothSteer*10)), -steerBounds*(acceleration/targetAcceleration), steerBounds*(acceleration/targetAcceleration));
            }
            else {drifting = false;}
        }
        else {
        
            //Normal steering
            steerDir = Mathf.Clamp(Mathf.MoveTowards(steerDir, steerBounds*input.x, Time.deltaTime*(smoothSteer*10)), -steerBounds*(acceleration/targetAcceleration), steerBounds*(acceleration/targetAcceleration));
        }
        
        //Apply acceleration & steering
        transform.position += transform.forward * (acceleration+accelerationBoost) * Time.deltaTime;
        transform.Rotate(0, Time.deltaTime*(steerDir*10) ,0);
    }
}
