using UnityEngine;

public class PauseMenu : MonoBehaviour
{

	public GameObject pausePanel;
	public GameObject pauseView;
	public GameObject settingsView;
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
				if (settingsView.activeSelf)
				{
					settingsView.SetActive(false);
					pauseView.SetActive(true);
				}
				else
				{
					isPaused = false;
					Continue();
				}
			}
		}
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			if (isPaused)
			{
				settingsView.SetActive(true);
				pauseView.SetActive(false);
			}
		}
		if (Input.GetKeyDown(KeyCode.Q))
		{
			if (isPaused)
			{
				Quit();
			}
		}
		if (Input.GetKeyDown(KeyCode.S))
		{
			if (isPaused)
			{
				pauseView.SetActive(false);
				settingsView.SetActive(true);
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
