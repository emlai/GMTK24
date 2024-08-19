using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadWin : MonoBehaviour
{
	// Start is called once before the first execution of Update after the MonoBehaviour is created


	public void LoadWinScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
