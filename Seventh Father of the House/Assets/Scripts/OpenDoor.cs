using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;
using System;
/*
* This class is attached to a door handle. The door handle is child of a door.
*/
public class OpenDoor : MonoBehaviour
 {
    public SteamVR_Action_Boolean booleanAction;
    public SteamVR_Input_Sources inputSource;
    public Rigidbody _doorHinge;
    public Transform _pivot;

    private Vector3 force;
    private Vector3 cross;

    private bool holdingHandle;
    private float angle;
    private const float forceMultiplier = 150f;

    

    private bool _isGrabbing = false;

    private void OnEnable()
    {
        booleanAction[inputSource].onStateDown += OnGrabStateDown;
        booleanAction[inputSource].onStateUp += OnGrabStateUp;
    }

    private void OnDisable()
    {
        booleanAction[inputSource].onStateDown -= OnGrabStateDown;
        booleanAction[inputSource].onStateDown -= OnGrabStateUp;
    }

    private void OnGrabStateDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        _isGrabbing = true;
    }

    private void OnGrabStateUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        _isGrabbing = false;
    }

    private void HandHoverUpdate(Hand hand)
     {
        if (_isGrabbing)
        {
            holdingHandle = true;

            // Direction vector from the door's pivot point to the hand's current position
            Vector3 doorPivotToHand = hand.transform.position - transform.parent.position;

            // Ignore the y axis of the direction vector
            doorPivotToHand.y = 0;

            // Direction vector from door handle to hand's current position
            force = hand.transform.position - transform.position;

            // Cross product between force and direction. 
            cross = Vector3.Cross(doorPivotToHand, force);
            angle = Vector3.Angle(doorPivotToHand, force);
        }
        else
        {
            holdingHandle = false;
        }
    }

     void Update()
     {
         if (holdingHandle)
         {
             // Apply cross product and calculated angle to
             _doorHinge.angularVelocity = cross * angle * forceMultiplier;
         }
     }
 
     private void OnHandHoverEnd()
     {

         // Set angular velocity to zero if the hand stops hovering
         if(!_isGrabbing)
            _doorHinge.angularVelocity = Vector3.zero;
     }
 }