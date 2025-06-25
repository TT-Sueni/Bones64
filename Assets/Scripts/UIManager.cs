using UnityEngine;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
            
    }

    public void PlayGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Debug.Log("play");
       UnityEngine.SceneManagement.SceneManager.LoadScene("Gameplay");
        
        Time.timeScale = 1;
    }
    public void Credits()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Debug.Log("credits" );
        UnityEngine.SceneManagement.SceneManager.LoadScene("Credits");

    }
    public void BacktoMainMenu()
    {
        Debug.Log("main menu");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");

    }

    public void QuitGame()
    {
        Debug.Log("quiteaste");
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        Debug.Log("play");
        pausePanel.SetActive(false);
    }
}
