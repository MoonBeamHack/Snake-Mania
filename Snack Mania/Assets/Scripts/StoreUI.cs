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

    [SerializeField] SnakeController sController;

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
        Debug.Log("trying skin");
        sController.ChangeSkinColor(newColor);
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
        int selectedTheme = DatabaseManager.Instance.GetLocalData().selectedTheme;
        SkinDemo(selectedTheme);
        SetBalanceText();
        DisableOwnedItems(selectedTheme);
        
    }   

    async public void DisableOwnedItems(int selectedTheme)
    {
        loadingUI.SetActive(true);
        mainShopUI.SetActive(false);

        List<string> result = await MoonbeamManager.Instance.GetNFTList();
        List<int> purchasedItems = new List<int>();
        purchasedItems.Add(0);
        if (result.Count > 0)
        {
            Debug.Log(result);

            //JSONObject jsonObject = new JSONObject(result);

            for (int i = 0; i < result.Count; i++)
            {

                Debug.Log(result[i]);

                if (result[i].StartsWith("70") && result[i].Length == 3)
                { 
                    int id = Int32.Parse(result[i]) - 699;

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
                nft_themes_buttons[temp].transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => {  BuyThemeFromShop(temp-1); });
                nft_themes_buttons[temp].transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = "Buy";
                nft_themes_buttons[temp].transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
            }
        }
        nft_themes_buttons[selectedTheme].transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = "Selected";
        loadingUI.SetActive(false);
        mainShopUI.SetActive(true);
    }
    public int lastBoughtSkin=-1;
    public void BuyThemeFromShop(int index)
    {
        LocalData data = DatabaseManager.Instance.GetLocalData();
        if (data.coins < 50)
        {
            UIManager.insta.ShowNoCoinsPopup();
            return;
        }
        lastBoughtSkin = index+1;
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
        for (int i = 0; i < nft_themes_buttons.Length; i++)
        {
           var childText = nft_themes_buttons[i].transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
            if (childText.text == "Selected") childText.text = "Select";
            else if (index == i) childText.text = "Selected";

            Debug.Log(childText + " " + i);
        }
        data.selectedTheme = index;
        DatabaseManager.Instance.UpdateData(data);
        SkinDemo(data.selectedTheme);
        UIManager.insta.ShowInfoMsg("Changed Theme");
    }
}
