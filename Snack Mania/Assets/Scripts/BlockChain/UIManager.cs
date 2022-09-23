using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager insta;

    

    
    



     

 

  



    [Header("Tutorial")]
    [SerializeField] GameObject TutorialUI;
    [SerializeField] int currentTutorial = 0;
    [SerializeField] GameObject[] tutorialObjects;

    [Header("GameOverUI")]
    [SerializeField] GameObject GameOverUI;
    [SerializeField] TMP_Text Score;
    [SerializeField] TMP_Text HighScore_Txt;
    [SerializeField] TMP_Text CoinsAward;

    [Header("GamePlay UI")]
    [SerializeField] GameObject GamePlayUI;
    [SerializeField] TMP_Text CurrentScore;
    [SerializeField] GameObject PauseUI;

    [SerializeField] TMP_Text CoinAmount;

    
    private void Awake()
    {
        insta = this;

    }
    // Start is called before the first frame update
    void Start()
    {

        UpdatePlayerUIData(DatabaseManager.Instance.GetLocalData());
    }


  
    #region Tutorial
    public void ShowTutorial()
    {
        TutorialUI.SetActive(true);
        for (int i = 0; i < tutorialObjects.Length; i++)
        {
            tutorialObjects[i].SetActive(false);
        }
        tutorialObjects[currentTutorial].SetActive(true);
    }
    public void NextTutorial()
    {
        tutorialObjects[currentTutorial].SetActive(false);
        currentTutorial++;
        if (currentTutorial >= tutorialObjects.Length)
        {
            SkipTutorial();
            return;
        }
        tutorialObjects[currentTutorial].SetActive(true);
    }
    public void SkipTutorial()
    {
        PlayerPrefs.SetInt("tutorial", 1);
        TutorialUI.SetActive(false);
        
    }


    #endregion

    public void UpdatePlayerUIData(LocalData data)
    {
        CoinAmount.text = data.coins.ToString();
    }
  


    public TMP_Text txt_information;
   

    public void DeductCoins(int _no)
    {
        LocalData data = DatabaseManager.Instance.GetLocalData();
        data.coins -= 50;
        DatabaseManager.Instance.UpdateData(data);
        UIManager.insta.UpdatePlayerUIData(data);
    }

    public void ShowNoCoinsPopup()
    {

        MessaeBox.insta.showMsg("Not Enough Coins!", true);

    }   
    Coroutine coroutine;
    public void ShowInfoMsg(string info)
    {
        txt_information.transform.parent.gameObject.SetActive(true);

        txt_information.text = info;

        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        
        coroutine=StartCoroutine(disableTextInfo());
    }
    IEnumerator disableTextInfo()
    {
        yield return new WaitForSecondsRealtime(2f);
        txt_information.transform.parent.gameObject.SetActive(false);
    }


    public void OpenGameOverPanel()
    {
        GameOverUI.SetActive(true);
        GamePlayUI.SetActive(false);
        Score.text = "Your Score : " + GameManager.Instance.Score.ToString();
        CoinsAward.text = "Your Reward : " + ((int)(GameManager.Instance.Score)).ToString();
        
        CoinAmount.text = GameManager.Instance.CoinAmount.ToString();
        int highscore = DatabaseManager.Instance.GetLocalData().highscore;
        LocalData data = DatabaseManager.Instance.GetLocalData();
        data.coins += GameManager.Instance.CoinAmount;

        if (GameManager.Instance.Score > highscore)
        {
                    
            highscore = GameManager.Instance.Score;
            data.highscore = highscore;
          
        }

        DatabaseManager.Instance.UpdateData(data);
        UpdatePlayerUIData(data);
        HighScore_Txt.text = "High Score : " + highscore;

    }

    public void PauseGame(bool pause)
    {
        PauseUI.SetActive(pause);
        if (pause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = GameManager.Instance.speed;
        }
    }

    public void UpdateScore()
    {
        CurrentScore.text = "Score : " + GameManager.Instance.Score.ToString();
    }





}