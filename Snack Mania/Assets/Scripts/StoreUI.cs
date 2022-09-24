using Defective.JSON;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreUI : MonoBehaviour
{
    public static StoreUI insta;
    public List<GameObject> DemoSnake = new List<GameObject>();
    public List<Sprite> demoColor = new List<Sprite>();

    public GameObject[] nft_themes_buttons;

    [SerializeField] TMP_Text[] balanceText;

    [SerializeField] GameObject mainShopUI;
    [SerializeField] GameObject loadingUI;

    private void Awake()
    {
        insta = this;
        this.gameObject.SetActive(false);
    }
    public void SkinDemo(int newColor)
    {
        foreach (var item in DemoSnake)
        {
            item.GetComponent<Image>().sprite = demoColor[newColor];
        }
        FindObjectOfType<SnakeController>().ChangeSkinColor(newColor);
    }

    public void SetBalanceText()
    {
        for (int i = 0; i < balanceText.Length; i++)
        {
            balanceText[i].text = "Balance : " + MoonbeamManager.userBalance.ToString();
        }

    }
    private void OnEnable()
    {

        SkinDemo(DatabaseManager.Instance.GetLocalData().selectedTheme);
        SetBalanceText();
        DisableOwnedItems();
    }   

    async public void DisableOwnedItems()
    {
        loadingUI.SetActive(true);
        mainShopUI.SetActive(false);

        string result = await MoonbeamManager.Instance.CheckNFTBalance();
        List<int> purchasedItems = new List<int>();
        purchasedItems.Add(0);
        if (!string.IsNullOrEmpty(result) && result != "[]")
        {
            Debug.Log(result);

            JSONObject jsonObject = new JSONObject(result);

            for (int i = 0; i < jsonObject.count; i++)
            {

                Debug.Log(jsonObject[i].GetField("tokenId"));

                if (jsonObject[i].GetField("tokenId").stringValue.StartsWith("50") && jsonObject[i].GetField("tokenId").stringValue.Length == 3)
                { 
                    int id = Int32.Parse(jsonObject[i].GetField("tokenId").stringValue) - 499;

                    if (id >= 0 && id < nft_themes_buttons.Length)
                    {
                        purchasedItems.Add(id);
                    }
                }
            }
        }

        for (int i = 0; i < nft_themes_buttons.Length; i++)
        {
            int temp = i;
            nft_themes_buttons[temp].transform.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
            if (purchasedItems.Contains(temp))
            {
                nft_themes_buttons[temp].transform.GetChild(0).GetComponent<Button>().onClick.AddListener(()=> { SelectTheme(temp); });
                nft_themes_buttons[temp].transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = "Select";
                nft_themes_buttons[temp].transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                nft_themes_buttons[temp].transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => {  BuyThemeFromShop(temp); });
                nft_themes_buttons[temp].transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = "Buy";
                nft_themes_buttons[temp].transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
            }
        }

        loadingUI.SetActive(false);
        mainShopUI.SetActive(true);
    }
    int lastBoughtSkin=-1;
    public void BuyThemeFromShop(int index)
    {
        LocalData data = DatabaseManager.Instance.GetLocalData();
        if (data.coins < 50)
        {
            UIManager.insta.ShowNoCoinsPopup();
            return;
        }
        lastBoughtSkin = index;
        MoonbeamManager.Instance.purchaseItem(index, false);
    }
    public void EnableNewItem()
    {
        if (lastBoughtSkin != -1)
        {
            nft_themes_buttons[lastBoughtSkin].transform.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
            nft_themes_buttons[lastBoughtSkin].transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => { SelectTheme(lastBoughtSkin); });
            nft_themes_buttons[lastBoughtSkin].transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = "Select";
            nft_themes_buttons[lastBoughtSkin].transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        }
    }
    public void SelectTheme(int index)
    {
        LocalData data = DatabaseManager.Instance.GetLocalData();
        data.selectedTheme = index;
        DatabaseManager.Instance.UpdateData(data);
        SkinDemo(data.selectedTheme);
        UIManager.insta.ShowInfoMsg("Changed Theme");
    }
}
