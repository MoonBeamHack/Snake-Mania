using System.Collections;
using TMPro;
using UnityEngine;

public class MessaeBox : MonoBehaviour
{
    public static MessaeBox insta;
    [SerializeField] GameObject msgBoxUI;
    [SerializeField] GameObject okBtn;
    [SerializeField] TMP_Text msgText;



    private void Awake()
    {
        insta = this;
        msgBoxUI.SetActive(false);
    }
    public void showMsg(string _msg, bool showBtn)
    {
        StopAllCoroutines();

        msgBoxUI.SetActive(true);
        if (showBtn) okBtn.SetActive(true);
        else okBtn.SetActive(false);

        msgText.text = _msg;

        StartCoroutine(WaitToShowOk());
    }


    int temp_id = 0;
    [SerializeField] GameObject retryBoxUI;
    public void ShowRetryPopup(int _id)
    {
        retryBoxUI.SetActive(true);
        temp_id = _id;
    }
    public void RetryClaim()
    {
        retryBoxUI.SetActive(false);
        EvmosManager.Instance.purchaseItem(temp_id, false);

    }
    public void CancelClaim()
    {
        //BlockChainManager.Instance.purchaseItem(temp_id, true);
        retryBoxUI.SetActive(false);
    }
    IEnumerator WaitToShowOk()
    {
        if (!okBtn.activeSelf)
        {
            yield return new WaitForSeconds(40);
            okBtn.SetActive(true);
        }

    }

    public void OkButton()
    {
        StopAllCoroutines();
        msgBoxUI.SetActive(false);
    }
}
