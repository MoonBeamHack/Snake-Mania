using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuUI : MonoBehaviour
{
    [SerializeField] GameObject loadingPanel;
    
    



  
    public void Play()
    {
        loadingPanel.SetActive(true);
        SceneManager.LoadScene("SampleScene");


    }
  
   

}
