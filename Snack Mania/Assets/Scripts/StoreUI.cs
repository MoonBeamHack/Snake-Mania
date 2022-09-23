using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreUI : MonoBehaviour
{

    public List<GameObject> DemoSnake = new List<GameObject>();
    public List<Color> demoColor = new List<Color>();
    public void SkinDemo(int newColor)
    {
        foreach (var item in DemoSnake)
        {
            item.GetComponent<Image>().color = demoColor[newColor];
        }
    }
}
