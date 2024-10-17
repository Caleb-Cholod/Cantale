using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused;
    public GameObject pauseMenuUI;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused && Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
        else if (isPaused && Input.GetKeyDown(KeyCode.Escape))
        {
            Resume();
        }
    }
    public void Pause(){
        pauseMenuUI.SetActive(true);
        isPaused = true;
        Time.timeScale = 0;
    }
    public void Resume(){
        pauseMenuUI.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
    }
}
