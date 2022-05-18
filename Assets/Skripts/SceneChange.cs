using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
	public void NextLevel(int _sceneNumber)
	{
		SceneManager.LoadScene(_sceneNumber);
	}

	public void NextLevel(string name)
	{
		SceneManager.LoadScene(name);
	}

	public void ReloadScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
