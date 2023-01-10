using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{

    Rigidbody rb;
    AudioSource audioSource;

    [SerializeField]float mainThrust = 100f;
    [SerializeField]float rotationThrust = 100f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = 0.25f;

        audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space)) 
        {
            //Debug.Log("Pressing SPACE - Thrusting");
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
			if (!audioSource.isPlaying)
			{
                audioSource.Play();
			}
			else { 
            }
        }
        else
		{
            audioSource.Stop();
		}
	}

    void ProcessRotation()
	{
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
		{
			ApplyRotation(rotationThrust);
		}
		else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationThrust);
        }
    }
    
	private void ApplyRotation(float rotationThisFrame)
	{
        
        rb.freezeRotation = true; // freezing so we cant rotate manually
		transform.Rotate(Vector3.forward * Time.deltaTime * rotationThisFrame);
        rb.freezeRotation = false; // unfreezing so we can rotate manyally

	}
}
