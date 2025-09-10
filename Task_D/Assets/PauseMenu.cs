using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pelinPauseMenu;
    private bool isPaused;

    void Start()
    {
        pelinPauseMenu.SetActive(false); // Disable Canvas PelinPauseMenu at startup
        isPaused = true;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause(); // The function is also executed when the continue_button is pressed
        }
    }
    
    // I didn't display the code in Update(), but put it in a separate function to add functionality to the continue_button
    public void Pause()
    {
        if (isPaused) // If paused, then exit pause - all false, start time
        {
            pelinPauseMenu.SetActive(false);
            isPaused = false;
            Time.timeScale = 1f;
        }
        else // If in the game, then go into pause - all true, stop time
        {
            pelinPauseMenu.SetActive(true);
            isPaused = true;
            Time.timeScale = 0f;
        }
    }
}
