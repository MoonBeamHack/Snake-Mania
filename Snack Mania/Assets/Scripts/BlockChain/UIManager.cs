using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager insta;

    [Header("GameplayMenu")]
    public GameObject StartUI;    

    [SerializeField] GameObject LoadingPanel;   

    public TMP_Text usernameText;

    
    

    [SerializeField] TMP_Text statusText;

    

    // fight manager
    [SerializeField] GameObject FightRequestUI;
    [SerializeField] TMP_Text fightRequestText;

    [Header("No Coins Info")]
    [SerializeField] GameObject NoCoinsUI;    

    [Header("GameplayMenu")]
    public GameObject GameplayUI;
   [SerializeField] TMP_Text scoreTxt;  

  


    [Header("VoiceChat")]
    [SerializeField] Image recorderImg;
    [SerializeField] Image listenerImg;
    [SerializeField] Sprite[] recorderSprites; //0 on 1 off
    [SerializeField] Sprite[] listenerSprites; //0 on 1 off


    [Header("StoreAndCollection")]
    [SerializeField] GameObject myCollectionUI;
    [SerializeField] TMP_Text TxtHeaderCollection;
    [SerializeField] GameObject LoadingMyCollection;
    [SerializeField] GameObject MyCollectionObject;



    [Header("Result")]
    [SerializeField] Image resultImg;
    [SerializeField] Sprite[] resultprites; //0 win 1 lose 2 tie

    [Header("Tutorial")]
    [SerializeField] GameObject TutorialUI;
    [SerializeField] int currentTutorial = 0;
    [SerializeField] GameObject[] tutorialObjects;

    [Header("GameOverUI")]
    [SerializeField] GameObject GameOverUI;
    [SerializeField] TMP_Text Score;
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
       

            scoreTxt.text = data.coins.ToString();
          

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
       
        NoCoinsUI.SetActive(true);
        LeanTween.scale(NoCoinsUI.transform.GetChild(0).gameObject, Vector3.one, 0.5f).setEase(LeanTweenType.easeInQuad);

    }
    public void CloseNoCoinsPopup()
    {

       
        LeanTween.scale(NoCoinsUI.transform.GetChild(0).gameObject, Vector3.zero, 0.5f).setEase(LeanTweenType.easeInQuad).setOnComplete(() => {
            NoCoinsUI.SetActive(false);
        });

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
        yield return new WaitForSeconds(3f);
        txt_information.transform.parent.gameObject.SetActive(false);
    }


    public void OpenGameOverPanel()
    {
        GameOverUI.SetActive(true);
        GamePlayUI.SetActive(false);
        Score.text = "Your Score : " + GameManager.Instance.Score.ToString();
        CoinsAward.text = "Your Reward : " + ((int)(GameManager.Instance.Score /2)).ToString();
        GameManager.Instance.CoinAmount += (int)(GameManager.Instance.Score / 2);
        CoinAmount.text = GameManager.Instance.CoinAmount.ToString();
       //int highscore = DatabaseManager.Instance.GetLocalData().highscore;
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
