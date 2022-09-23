using Defective.JSON;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
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




    
    private void Awake()
    {
        insta = this;

    }
    // Start is called before the first frame update
    void Start()
    {
        GameplayUI.SetActive(false);
        resultImg.gameObject.SetActive(false);
        StartUI.SetActive(true);
       
        statusText.gameObject.SetActive(true);
        statusText.text = "";
      

        //UpdatePlayerUIData(true, true);
        //UpdateUserName(DatabaseManager.Instance.GetLocalData().name, SingletonDataManager.userethAdd);

      

      
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

    public void UpdatePlayerUIData(bool _show,LocalData data, bool _init = false)
    {
        if (_show)
        {
            if (_init)
            {
               // SelectGender(data.characterNo);
            }

            scoreTxt.text = data.coins.ToString();
          

           // if (PhotonNetwork.LocalPlayer.CustomProperties["health"] != null) healthSlider.value = float.Parse(PhotonNetwork.LocalPlayer.CustomProperties["health"].ToString());
        }
        else
        {
            GameplayUI.SetActive(false);
        }
    }
    public void UpdatePlayerUIData(bool _show, bool _init = false)
    {
        if (_show)
        {           
            //if (PhotonNetwork.LocalPlayer.CustomProperties["health"] != null) healthSlider.value = float.Parse(PhotonNetwork.LocalPlayer.CustomProperties["health"].ToString());
        }
        else
        {
            GameplayUI.SetActive(false);
        }
    }


    public TMP_Text txt_information;
    public void ShowBurnableNFTConfimation(int _id, string status)
    {
        txt_information.transform.parent.gameObject.SetActive(true);
        if (status.Equals("success"))
        {
            txt_information.text = "Coin Purchase of " + status + " successful";
        }
        else
        {
            txt_information.text = "Coin Purchase of " + status + " Failed";
        }

        StartCoroutine(disableTextInfo());
    }
    public void ShowCoinPurchaseStatus(TranscationInfo info)
    {
        txt_information.transform.parent.gameObject.SetActive(true);
        if (info.transactionStatus.Equals("success"))
        {
            txt_information.text = "Coin Purchase of " + info.coinAmount + " successful";
        }
        else
        {
            txt_information.text = "Coin Purchase of " + info.coinAmount + " Failed";
        }
        StartCoroutine(disableTextInfo());
    }
   


 
    public void UpdateUserName(string _ethad = null)
    {
        if (_ethad != null)
        {
            usernameText.text = "Hi, " + "\n Your crypto address is : " + _ethad;          
        }
       
    }

    public void UpdateStatus(string _msg)
    {
        statusText.text = _msg;
        StartCoroutine(ResetUpdateText());
    }

    IEnumerator ResetUpdateText()
    {
        yield return new WaitForSeconds(2);
        statusText.text = "";
    }


    
  
   

  



 

  
    public void BuyThemeFromShop(int index)
    {
        LocalData data = DatabaseManager.Instance.GetLocalData();
        
        switch (index)
        {
            case 1:
                {
                    if (data.coins >= 200)
                    {
                        /*data.coins -= 200;
                        DatabaseManager.Instance.UpdateData(data);
                        UpdatePlayerUIData(true, data);*/
                    }
                    else
                    {
                        ShowNoCoinsPopup();
                        return;
                    }
                    break;
                }
            case 2:
                {
                    if (data.coins >= 200)
                    {
                        /*data.coins -= 200;
                        DatabaseManager.Instance.UpdateData(data);
                        UpdatePlayerUIData(true, data);*/
                    }
                    else
                    {
                        ShowNoCoinsPopup();
                        return;
                    }
                    break;
                }
            case 3:
                {
                    if (data.coins >= 200)
                    {
                        /*data.coins -= 200;
                        DatabaseManager.Instance.UpdateData(data);
                        UpdatePlayerUIData(true, data);*/
                    }
                    else
                    {
                        ShowNoCoinsPopup();
                        return;
                    }
                    break;
                }
        }
        EvmosManager.Instance.purchaseItem(index, false);
    }


   

    public void DeductCoins(int _no)
    {
        LocalData data = DatabaseManager.Instance.GetLocalData();
        data.coins -= DatabaseManager.Instance.allMetaDataServer[_no].cost;
        DatabaseManager.Instance.UpdateData(data);
        UIManager.insta.UpdatePlayerUIData(true, data);
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


   
   


}
