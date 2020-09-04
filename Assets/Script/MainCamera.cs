using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public float distanceAway = 1.7f;
    public float distanceUp = 1.3f;
    public float smooth = 2f;				// how smooth the camera movement is
    public bool isFollow = true;    //  是否可以跟随

    private Vector3 m_TargetPosition;		// the position the camera is trying to be in)

    public Transform follow;
    // Start is called before the first frame update
    void Start()
    {
        //follow = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // transform.LookAt(follow);
        // transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        if(isFollow)
        {
            // setting the target position to be the correct offset from the 
            m_TargetPosition = follow.position + Vector3.up * distanceUp - follow.forward * distanceAway;

            // making a smooth transition between it's current position and the position it wants to be in
            transform.position = Vector3.Lerp(transform.position, m_TargetPosition, Time.deltaTime * smooth);
        }
        
    }
}
