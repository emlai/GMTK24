using UnityEngine;

public class DeathPanel : MonoBehaviour
{

	public GameObject pausePanel;
	public GameObject pauseView;
	public GameObject settingsView;
	public bool isPaused = false;

	// Update is called once per frame
	float elapsed = 0f;
	float lastTime = 0f;
	private void FixedUpdate()
	{
		if (lastTime == 0)
		{
			lastTime = Time.realtimeSinceStartup;
		}
		else
		{
			elapsed += Time.realtimeSinceStartup - lastTime;
			lastTime = Time.realtimeSinceStartup;
			Time.timeScale = Mathf.Lerp(1f, 0f, elapsed);
		}
	}
}
