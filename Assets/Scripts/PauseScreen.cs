using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class PauseScreen : MonoBehaviour
{

    public string levelSelect;
    public string mainMenu;

    private LevelManager theLevelManager;

    public GameObject thePauseScreen;

    private PlayerController thePlayer;

    // Use this for initialization
    void Start()
    {
        theLevelManager = FindObjectOfType<LevelManager>();
        thePlayer = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        if (Input.GetButtonDown("Pause"))
        {
            if (Time.timeScale != 0)
            {
                PauseGame();
            }
            else {
                ResumeGame();
            }
        }
    }
    public void PauseGame()
    {
        thePauseScreen.SetActive(true);
        Time.timeScale = 0;
        thePlayer.canmove = false;
        theLevelManager.levelMusic.Pause();
    }


    public void ResumeGame()
    {
        Time.timeScale = 1;
        thePlayer.canmove = true;
        theLevelManager.levelMusic.Play();
        thePauseScreen.SetActive(false);
    }

    public void LevelSelect()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetInt("PlayerLives", theLevelManager.currentLives);
        PlayerPrefs.SetInt("CoinCount", theLevelManager.coinCount);
        SceneManager.LoadScene(levelSelect);
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(mainMenu);
    }




}
