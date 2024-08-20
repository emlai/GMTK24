using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenControls : MonoBehaviour
{
	public Animator fadeOut;
	public float transitionTime = 1f;

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.R))
		{
			fadeOut.SetTrigger("Start");
		}

		if (Input.GetKeyDown(KeyCode.Q))
		{
			Quit();
		}
	}
	public void Quit()
	{
		Application.Quit();
	}

	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
		//StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex - 1));
	}


	//IEnumerator LoadLevel(int levelIndex)
	//{
	//	fadeOut.SetTrigger("Start");

	//	yield return new WaitForSeconds(transitionTime);

	//	SceneManager.LoadScene(levelIndex);
	//}

}
