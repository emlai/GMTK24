using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{

	public GameObject pausePanel;
	public GameObject pauseView;
	public GameObject settingsView;
	public GameObject deathPanel;
	public GameObject winPanel;
	public bool isPaused = false;
	public bool isGameEnd = false;
	public float mouseSensitivity;
	[SerializeField] Player player;
	

	[SerializeField]
	private TMP_Text sensitivityDisplay;

	private void Awake()
	{
		DontDestroyOnLoad(this);
	}

	private void Start()
	{
		sensitivityDisplay.text = mouseSensitivity.ToString();
	}
	void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (!isPaused && !winPanel.activeSelf && !deathPanel.activeSelf)
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
					player.rotationSpeed = mouseSensitivity;
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

		if (Input.GetKeyDown(KeyCode.R))
		{
			if (isPaused)
			{
				isPaused = false;
				Restart();
			}
			else {
				if (winPanel.activeSelf | deathPanel.activeSelf)
				{
					isPaused = false;
					Restart();
				}
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

		if (Input.GetKeyDown(KeyCode.S))
		{
			if (isPaused)
			{
				pauseView.SetActive(false);
				settingsView.SetActive(true);
			}
		}

		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			SensitivityUp();
		}

		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			SensitivityDown();
		}

		if (Input.GetKeyDown(KeyCode.L))
		{
			Die();
		}

		if (Input.GetKeyDown(KeyCode.V))
		{
			Win();
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

	public void Restart()
	{
		isPaused = false;
		Time.timeScale = 1;
		pausePanel.SetActive(false);
		deathPanel.SetActive(false);
		winPanel.SetActive(false);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void SensitivityDown()
	{
		if(mouseSensitivity > 50)
		{
			mouseSensitivity = mouseSensitivity - 50;
		}
		else
		{
			mouseSensitivity = 50;
		}
		sensitivityDisplay.text = mouseSensitivity.ToString();
	}

	public void SensitivityUp()
	{
		if (mouseSensitivity < 800)
		{
			mouseSensitivity = mouseSensitivity + 50;
		}
		else
		{
			mouseSensitivity = 800;
		}
		sensitivityDisplay.text = mouseSensitivity.ToString();
	}

	public void Die()
	{
		if (!isPaused)
		{
			deathPanel.SetActive(true);
		}
	}

	public void Win()
	{
		if (!isPaused)
		{
			winPanel.SetActive(true);
		}
	}
}
