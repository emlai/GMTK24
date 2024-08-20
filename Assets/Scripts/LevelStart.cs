using UnityEngine;

public class LevelStart : MonoBehaviour
{
    public PauseMenu pauseMenu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void SlowDown()
    {
        Time.timeScale = 0.2f;
    }

	void StopTIme()
	{
		Time.timeScale = 0f;
	}
}
