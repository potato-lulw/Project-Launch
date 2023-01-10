
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

	[SerializeField]float delay = 1f;
	
	private void OnCollisionEnter(Collision collision)
	{
		switch(collision.gameObject.tag){
			case "Friendly":
				Debug.Log("This thing is friendly");
				break;

			case "Finish":
				Debug.Log("This is the check-point");
				loadNextLevel();
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
		GetComponent<Mover>().enabled = false;
		Invoke("reloadLevel", delay);
	}

	void successSequence()
	{
		GetComponent<Mover>().enabled = false;
		Invoke("loadNextLevel", delay);
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
