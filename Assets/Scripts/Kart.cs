using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kart : MonoBehaviour
{
    public bool freeze = false;
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
    
    [Header("Visuals")]
    Transform kartmodel;
    [SerializeField] float kartAngle;
    
    // Start is called before the first frame update
    void Start()
    {
        kartmodel = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!freeze) {
            input = new Vector2Int(
            ((Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) ? 0 : 
                ((Input.GetKey(KeyCode.A)) ? -1 : 
                ((Input.GetKey(KeyCode.D)) ? 1 : 0))), 
            //If both are pressed, do nothing. If left is held, input.x is -1, if right is held input.x is 1.
            ((Input.GetKey(KeyCode.W)) ? 1 : 0));
            //If up is held, input.y is 1. No brakes :)

            //Accelerate towards target acceleration.
            acceleration = Mathf.Clamp(Mathf.MoveTowards(acceleration, acceleration+passiveAcceleration, (Time.deltaTime*passiveAcceleration) * ((passiveAccelerate ? 1 : 0)+input.y)), -targetAcceleration, targetAcceleration);
            accelerationBoost = Mathf.Clamp(Mathf.MoveTowards(accelerationBoost, 0, Time.deltaTime*passiveAcceleration), 0, Mathf.Infinity);

            //Apply drifting
            if (Input.GetKeyDown(KeyCode.Space) && (drifting != true))
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
            
            if (Input.GetKey(KeyCode.Space))
            {
                driftPower += Time.deltaTime*2;
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
                    steerDir = Mathf.Clamp(Mathf.MoveTowards(steerDir, (steerBounds*(input.x+1.5f))/1.5f, Time.deltaTime*(smoothSteer*10)), -steerBounds*2*(acceleration/targetAcceleration), steerBounds*2*(acceleration/targetAcceleration));
                }
                else if (driftDirection == 1) {
                    //Drifting to the right
                    steerDir = Mathf.Clamp(Mathf.MoveTowards(steerDir, (steerBounds*(input.x-1.5f))/1.5f, Time.deltaTime*(smoothSteer*10)), -steerBounds*2*(acceleration/targetAcceleration), steerBounds*2*(acceleration/targetAcceleration));
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
}
