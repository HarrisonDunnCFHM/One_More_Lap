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
    [SerializeField] float crashVolume = 0.4f;
    [SerializeField] GameObject crashParticles;

    //ref params
    [SerializeField] bool checkpointA = true; //serialized for debugging
    [SerializeField] bool checkpointB = true; //serialized for debugging
    [SerializeField] bool isMuted = false;
    [SerializeField] float currentVolume;

    //cache
    Rigidbody2D myRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = startSpeed;
        myRigidBody = GetComponent<Rigidbody2D>();
        checkpointA = true;
        checkpointB = true;
        currentVolume = crashVolume;
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
        var kartRotation = controlDirection * -turnSpeed * Time.deltaTime;
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
        AudioSource.PlayClipAtPoint(crashNoise, Camera.main.transform.position, currentVolume);
        CrashSparks();
    }

    private void CrashSparks()
    {
        GameObject explosion = Instantiate(crashParticles, transform.position, transform.rotation);
        Destroy(explosion, 1f);
    }

    private void OnTriggerEnter2D(Collider2D checkpoint)
    {
        if(checkpoint.gameObject.tag == "Checkpoint A")
        {
            checkpointA = true;
        }
        else if(checkpoint.gameObject.tag == "Checkpoint B")
        {
            checkpointB = true;
        }
        else
        { 
            return;
        }
    }

    public bool CheckCheckpoints()
    {
        if(checkpointA && checkpointB)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ResetCheckpoints()
    {
        checkpointA = false;
        checkpointB = false;
    }
    public void ToggleMute()
    {
        if (!isMuted)
        {
            currentVolume = 0;
            isMuted = true;
        }
        else if (isMuted)
        {
            currentVolume = crashVolume;
            isMuted = false;
        }
    }
}
