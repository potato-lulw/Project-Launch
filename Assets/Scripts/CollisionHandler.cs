
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

	AudioSource audioSource;
	
	[SerializeField]float delay = 1f;
	[SerializeField] AudioClip crashSound;
	[SerializeField] AudioClip successSound;
	[SerializeField] ParticleSystem crashParticles;
	[SerializeField] ParticleSystem successParticles;


	

	bool isTransitioning = false;
	bool collisionDisabled = false;

	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}

	private void Update()
	{
		Cheats();
	}


	void Cheats()
	{
		if (Input.GetKeyDown(KeyCode.L))
		{
			LoadNextLevel();
		}

		else if (Input.GetKeyDown(KeyCode.C))
		{
			collisionDisabled = !collisionDisabled;
		}
	}


	private void OnCollisionEnter(Collision collision)
	{
		if (isTransitioning || collisionDisabled) return;

		switch(collision.gameObject.tag){
			case "Friendly":
				Debug.Log("This thing is friendly");
				break;

			case "Finish":
				Debug.Log("This is the check-point");
				SuccessSequence();
				break;

			/*case "Obstacle":
				Debug.Log("This is a obstacle");
				break;*/

			case "Fuel":
				Debug.Log("This is a fuel");
				break;

			default:

				CrashSequence();
				//Debug.Log("Game Over");
				break;

		}
	}


	void CrashSequence()
	{
		audioSource.volume = 0.2f;
		audioSource.PlayOneShot(crashSound);
		
		crashParticles.Play();
		isTransitioning = true;
		//gameObject.SetActive(false);
		//GetComponent<Renderer>().enabled = false;
		GetComponent<Mover>().enabled = false;
		//Invoke("bruh", 0.5f);
		Invoke("ReloadLevel", delay);
		//audioSouce.Stop();
	}

	void SuccessSequence()
	{
		audioSource.volume = 0.5f;
		audioSource.PlayOneShot(successSound);
		successParticles.Play();
		isTransitioning = true;
		GetComponent<Mover>().enabled = false;
		Invoke("LoadNextLevel", delay);
		//audioSouce.Stop();
	}
	void ReloadLevel()
	{
		
		int currentSceneindex = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene(currentSceneindex);
	}

	void LoadNextLevel()
	{
		int totalScenes = SceneManager.sceneCountInBuildSettings;
		int currentSceneindex = SceneManager.GetActiveScene().buildIndex;
		int nextSceneIndex = currentSceneindex + 1;
		SceneManager.LoadScene(nextSceneIndex % totalScenes);
	}
}
