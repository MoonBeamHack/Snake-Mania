using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        isGameRunning = false;
    }
    #endregion

    public static bool isGameRunning;
    public int Score;
    public int CoinAmount;
    public float speed;

   

    public void GameOver()
    {
        Debug.Log("GameOver");
        isGameRunning = false;
        CoinAmount += (int)( Score);
        UIManager.insta.OpenGameOverPanel();
        UIManager.insta.snakePlayer.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
