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
    [SerializeField] float maxSpeed = 15f;
    [SerializeField] float maxTurn = 4f;

    [SerializeField] float currentSpeed; //serialized for debugging
    [SerializeField] AudioClip crashNoise;
    [SerializeField] float crashVolume = 0.8f;
    [SerializeField] GameObject crashParticles;

    //cache
    Rigidbody2D myRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = startSpeed;
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
        var kartSpeed = controlThrow * currentSpeed;
        //Debug.Log("Your speed is " + kartSpeed);
        myRigidBody.velocity = transform.up * kartSpeed;
    }

    public void IncreaseMoveSpeed(float newSpeed)
    {
        if (currentSpeed <= maxSpeed)
        {
            currentSpeed += newSpeed;
        }
    }

    public void IncreaseTurnSpeed(float newTurnSpeed)
    {
        if (turnSpeed <= maxTurn)
        { 
            turnSpeed += newTurnSpeed;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioSource.PlayClipAtPoint(crashNoise, Camera.main.transform.position, crashVolume);
        CrashSparks();
    }

    private void CrashSparks()
    {
        GameObject explosion = Instantiate(crashParticles, transform.position, transform.rotation);
        Destroy(explosion, 1f);
    }
}
