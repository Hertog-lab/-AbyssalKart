using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaArm : MonoBehaviour
{
    [SerializeField] private Vector2 scaleRange = new Vector2(0.8f, 1.1f), speedRange = new Vector2(0.6f, 1f);
    [SerializeField] private float privatescale, privatespeed;
    private Vector3 rotOffset = new Vector3(-90, 0, 0);
    private Vector3 scaleOffset = new Vector3(8,8,8);
    [SerializeField] private Transform arm, player;
    [SerializeField] private float turnSpeed = 6;
    public float range = 8;
    private float meshRange = 5.33f;
    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = transform.GetChild(0).gameObject.GetComponent<Animator>();
        privatespeed = Random.Range(speedRange.x, speedRange.y);
        privatescale = Random.Range(scaleRange.x, scaleRange.y);
        transform.localScale = new Vector3(privatescale, privatescale, privatescale);
        anim.SetFloat("randomSpeed", privatespeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Vector3 direction = player.position - transform.position;
            anim.SetBool("Animate", (direction.magnitude < range));
            anim.SetFloat("randomSpeed", privatespeed);

            if (direction.magnitude < range)
            {

                //Quaternion toRotation = Quaternion.LookRotation(direction);
                //arm.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.time*turnSpeed);
                arm.rotation = Quaternion.LookRotation(Vector3.RotateTowards(arm.forward, direction, Time.deltaTime*turnSpeed, 0.0f));

                arm.localScale = Vector3.Lerp(arm.localScale, new Vector3(scaleOffset.x, scaleOffset.y, scaleOffset.z*(direction.magnitude/(meshRange* privatescale))), Time.deltaTime*(turnSpeed*2));
            }
            else
            {

                arm.rotation = Quaternion.LookRotation(Vector3.RotateTowards(arm.forward, (transform.position+new Vector3(0.1f, 5f, 0f)) - transform.position, Time.deltaTime*turnSpeed, 0.0f));

                arm.localScale = Vector3.Lerp(arm.localScale, new Vector3(scaleOffset.x, scaleOffset.y, scaleOffset.z), Time.deltaTime*(turnSpeed*2));
            }
        }
    }
}
