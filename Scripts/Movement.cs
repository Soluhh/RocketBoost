using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrustSpeed = 2f;
    [SerializeField] float rotationSpeed = 2f;
    [SerializeField] AudioClip mainEngineSFX;
    [SerializeField] ParticleSystem mainEngineVFX;
    [SerializeField] ParticleSystem rightSideThruster;
    [SerializeField] ParticleSystem leftSideThruster;

    Rigidbody myRigidbody;
    AudioSource myAudioSource;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RocketMovement();
        RocketThrust();
    }

    void RocketMovement()
    {
        if(Input.GetKey(KeyCode.A))
        {
            RocketRotation(rotationSpeed);
            if(!rightSideThruster.isPlaying)
            {
                rightSideThruster.Play();
            }
        }
        else
        {
            rightSideThruster.Stop();
        }

        if(Input.GetKey(KeyCode.D))
        {
            RocketRotation(-rotationSpeed);
            if(!leftSideThruster.isPlaying)
            {
                leftSideThruster.Play();
            }
        }
        else
        {
            leftSideThruster.Stop();
        }
    }

    void RocketThrust()
    {
        if(Input.GetKey(KeyCode.W))
        {
            myRigidbody.AddRelativeForce(new Vector3(0, thrustSpeed, 0) * Time.deltaTime);
            if(!myAudioSource.isPlaying)
            {
                mainEngineVFX.Play();
                myAudioSource.PlayOneShot(mainEngineSFX);
            }
            if(!mainEngineVFX.isPlaying)
            {
                mainEngineVFX.Play();
            }
        }
        else
        {
            mainEngineVFX.Stop();
            myAudioSource.Stop();
        }
    }

    void RocketRotation(float rotationThisFrame)
    {
        myRigidbody.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        myRigidbody.freezeRotation = false;
    }
}
