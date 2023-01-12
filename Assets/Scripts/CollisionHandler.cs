
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

	AudioSource audioSouce;

	[SerializeField]float delay = 1f;
	[SerializeField] AudioClip crashSound;
	[SerializeField] AudioClip successSound;

	bool isTransitioning = false;

	private void Start()
	{
		audioSouce = GetComponent<AudioSource>();
	}



	private void OnCollisionEnter(Collision collision)
	{
		if (isTransitioning) return;

		switch(collision.gameObject.tag){
			case "Friendly":
				Debug.Log("This thing is friendly");
				break;

			case "Finish":
				Debug.Log("This is the check-point");
				successSequence();
				break;

			/*case "Obstacle":
				Debug.Log("This is a obstacle");
				break;*/

			case "Fuel":
				Debug.Log("This is a fuel");
				break;

			default:

				crashSequence();
				//Debug.Log("Game Over");
				break;

		}
	}


	void crashSequence()
	{
		audioSouce.PlayOneShot(crashSound);
		isTransitioning = true;
		GetComponent<Mover>().enabled = false;
		Invoke("reloadLevel", delay);
		//audioSouce.Stop();
	}

	void successSequence()
	{

		audioSouce.PlayOneShot(successSound);
		isTransitioning = true;
		GetComponent<Mover>().enabled = false;
		Invoke("loadNextLevel", delay);
		//audioSouce.Stop();
	}
	void reloadLevel()
	{
		int currentSceneindex = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene(currentSceneindex);
	}

	void loadNextLevel()
	{
		int totalScenes = SceneManager.sceneCountInBuildSettings;
		int currentSceneindex = SceneManager.GetActiveScene().buildIndex;
		int nextSceneIndex = currentSceneindex + 1;
		SceneManager.LoadScene(nextSceneIndex % totalScenes);
	}
}
