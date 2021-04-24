using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Kart : MonoBehaviour
{
    //config params
    [SerializeField] float turnSpeed = 1f;
    [SerializeField] float maxSpeed = 10f;

    //cache
    Rigidbody2D myRigidBody;
    
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerTurn();
        PlayerSpeed();
    }

    private void PlayerTurn()
    {
        float controlDirection = CrossPlatformInputManager.GetAxis("Horizontal"); //value between -1 to +1
        var kartRotation = controlDirection * -turnSpeed;
        myRigidBody.rotation += kartRotation;
    }

    private void PlayerSpeed()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Vertical"); //value between -1 to +1
        var kartSpeed = controlThrow * maxSpeed;
        Debug.Log("Your speed is " + kartSpeed);
        //Vector2 kartVelocity = new Vector2(myRigidBody.velocity.x, kartSpeed);
        myRigidBody.velocity = transform.up * kartSpeed;
    }
}
