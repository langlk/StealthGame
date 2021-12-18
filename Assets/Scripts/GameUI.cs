using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject gameWonScreen;
    bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        Guard.OnGuardAlerted += OnGameLost;
        FindObjectOfType<Player>().OnReachExit += OnGameWon;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver && Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.LoadScene(0);
        }
    }
    
    void OnGameLost() {
        gameOverScreen.SetActive(true);
        OnGameOver();
    }

    void OnGameWon() {
        gameWonScreen.SetActive(true);
        OnGameOver();
    }

    void OnGameOver() {
        gameOver = true;
        FindObjectOfType<Player>().OnReachExit -= OnGameWon;
        Guard.OnGuardAlerted -= OnGameLost;
    }
}
