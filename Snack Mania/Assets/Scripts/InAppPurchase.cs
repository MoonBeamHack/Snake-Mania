using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAppPurchase : MonoBehaviour
{
    private void OnEnable()
    {
        if (StoreUI.insta)
        {
            StoreUI.insta.SetBalanceText();
        }
    }

    public void BuyCoins(int Index)
    {
        EvmosManager.Instance.CoinBuyOnSendContract(Index);
    }
}
