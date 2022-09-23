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
        UIManager.insta.OpenGameOverPanel();
    }
}
