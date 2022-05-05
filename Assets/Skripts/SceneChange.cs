using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
	public void NextLevel(int _sceneNumber)
	{
		SceneManager.LoadScene(_sceneNumber);
	}
}
