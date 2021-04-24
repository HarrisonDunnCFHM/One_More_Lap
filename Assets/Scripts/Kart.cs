using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Kart : MonoBehaviour
{
    //config params
    [SerializeField] float turnSpeed = 1f;
    [SerializeField] float startSpeed = 10f;

    [SerializeField] float maxSpeed; //serialized for debugging
    [SerializeField] AudioClip crashNoise;
    [SerializeField] float crashVolume = 0.8f;

    //cache
    Rigidbody2D myRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        maxSpeed = startSpeed;
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
        //Debug.Log("Your speed is " + kartSpeed);
        myRigidBody.velocity = transform.up * kartSpeed;
    }

    public void IncreaseMoveSpeed(float newSpeed)
    {
        maxSpeed += newSpeed;
    }

    public void IncreaseTurnSpeed(float newTurnSpeed)
    {
        turnSpeed += newTurnSpeed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioSource.PlayClipAtPoint(crashNoise, Camera.main.transform.position, crashVolume);
    }

}
