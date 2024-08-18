using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public GameObject pausePanel;
	public bool isPaused = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
			if (!isPaused)
			{
				isPaused = true;
				Pause();
			}
			else
			{
				isPaused = false;
				Continue();
			}
		}
		if (Input.GetKeyDown(KeyCode.Q))
		{
			if (isPaused)
			{
				Quit();
			}
		}
	}

    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }
	public void Continue()
	{
		pausePanel.SetActive(false);
		Time.timeScale = 1;
	}
	public void Quit()
	{
		Application.Quit();
	}
}
