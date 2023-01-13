using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{

	Rigidbody rb;
	AudioSource audioSource;

	[SerializeField] float mainThrust = 100f;
	[SerializeField] float rotationThrust = 100f;

	[SerializeField] AudioClip mainEngine;


	[SerializeField] ParticleSystem mainBoostpart;
	[SerializeField] ParticleSystem leftBoostpart;
	[SerializeField] ParticleSystem rightBoostpart;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		rb.drag = 0.25f;

		audioSource = GetComponent<AudioSource>();
		audioSource.Stop();
	}



	void Update()
	{
		ProcessThrust();
		ProcessRotation();
	}

	void ProcessThrust()
	{

		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space))
		{

			StartThrusting();
		}
		else
		{
			StopThrust();
		}
	}

	void ProcessRotation()
	{
		if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
		{
			RotateLeft();
		}
		else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
		{
			RotateRight();
		}
		else
		{
			StopRotating();
		}
	}

	private void StartThrusting()
	{
		rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
		if (!audioSource.isPlaying)
		{
			audioSource.PlayOneShot(mainEngine);

		}
		if (!mainBoostpart.isPlaying)
		{
			mainBoostpart.Play();
		}
	}

	private void StopThrust()
	{
		audioSource.Stop();
		mainBoostpart.Stop();
	}

	private void RotateRight()
	{
		ApplyRotation(-rotationThrust);

		if (!rightBoostpart.isPlaying)
		{
			rightBoostpart.Play();
		}
	}

	private void RotateLeft()
	{
		ApplyRotation(rotationThrust);
		if (!leftBoostpart.isPlaying)
		{
			leftBoostpart.Play();
		}
	}

	private void StopRotating()
	{
		leftBoostpart.Stop();
		rightBoostpart.Stop();
	}

	private void ApplyRotation(float rotationThisFrame)
	{

		rb.freezeRotation = true; // freezing so we cant rotate manually
		transform.Rotate(Vector3.forward * Time.deltaTime * rotationThisFrame);
		rb.freezeRotation = false; // unfreezing so we can rotate manyally

	}
}
