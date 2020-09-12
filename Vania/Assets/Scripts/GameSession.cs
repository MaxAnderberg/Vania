using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameSession : MonoBehaviour
{
    // config
    [SerializeField] TextMeshProUGUI playerLivesTMP;
    [SerializeField] TextMeshProUGUI playerScoreTMP;
    [SerializeField] int playerLifes = 3;
    [SerializeField] int playerScore = 0;

    private void Awake()
    {
        int numberOfGameSession = FindObjectsOfType<GameSession>().Length;
        if (numberOfGameSession > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerLivesTMP.text = playerLifes.ToString();
        playerScoreTMP.text = playerScore.ToString();
    }

    public void AddToScore(int amount)
    {
        playerScore += amount;
        playerScoreTMP.text = playerScore.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if (playerLifes>1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    private void TakeLife()
    {
        playerLifes--;
        playerLivesTMP.text = playerLifes.ToString();

        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
