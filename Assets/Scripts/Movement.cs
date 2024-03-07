using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private Rigidbody myBody;
    [SerializeField] float thrustingSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] AudioClip thrusters;
    [SerializeField] ParticleSystem thrustersParticles;
    [SerializeField] ParticleSystem rightThrustersParticles;
    [SerializeField] ParticleSystem leftThrustersParticles;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust() {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void ProcessRotation() {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            StartRightRotation();
        }
        else
        {
            StopRightRotation();
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            StartLeftRotation();
        }
        else
        {
            StopLeftRotation();
        }
    }

    private void StartThrusting()
    {
        myBody.AddRelativeForce(Vector3.up * Time.deltaTime * thrustingSpeed);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(thrusters);
        }
        if (!thrustersParticles.isPlaying)
        {
            thrustersParticles.Play();
        }
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        thrustersParticles.Stop();
    }

    private void StartLeftRotation()
    {
        ApplyRotation(rotationSpeed);
        if (!rightThrustersParticles.isPlaying)
        {
            rightThrustersParticles.Play();
        }
    }
    private void StopLeftRotation()
    {
        rightThrustersParticles.Stop();
    }

    private void StartRightRotation()
    {
        ApplyRotation(-rotationSpeed);
        if (!leftThrustersParticles.isPlaying)
        {
            leftThrustersParticles.Play();
        }
    }
    
    private void StopRightRotation()
    {
        leftThrustersParticles.Stop();
    }

    private void ApplyRotation(float rotationSpeed)
    {
        myBody.freezeRotation = true;
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
        myBody.freezeRotation = false;
    }
}
