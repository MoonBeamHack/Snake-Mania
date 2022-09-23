using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreUI : MonoBehaviour
{
    public static StoreUI insta;
    public List<GameObject> DemoSnake = new List<GameObject>();
    public List<Sprite> demoColor = new List<Sprite>();

    public GameObject[] nft_themes;

    private void Awake()
    {
        
    }
    private void OnEnable()
    {
        
    }
    public void SkinDemo(int newColor)
    {
        foreach (var item in DemoSnake)
        {
            item.GetComponent<Image>().sprite = demoColor[newColor];
        }
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
                        UIManager.insta.ShowNoCoinsPopup();
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
                        UIManager.insta.ShowNoCoinsPopup();
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
                        UIManager.insta.ShowNoCoinsPopup();
                        return;
                    }
                    break;
                }
        }
        EvmosManager.Instance.purchaseItem(index, false);
    }
}
