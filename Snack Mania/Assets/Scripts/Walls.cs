using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DefaultExecutionOrder(1)]
public class Walls : MonoBehaviour
{
    [SerializeField] BoxCollider2D bc2d;
    [SerializeField] Food food;

    private void OnEnable()
    {
       
        foreach (var item in food.grid)
        {

            if (item.position.x < bc2d.bounds.max.x &&
                item.position.x > bc2d.bounds.min.x &&
                item.position.y > bc2d.bounds.min.y &&
                item.position.y < bc2d.bounds.max.y)
            {
                food.EmptyCells.Remove(item);                
            }
        }
    }
    private void OnDisable()
    {
        foreach (var item in food.grid)
        {
            if (item.position.x < bc2d.bounds.max.x &&
                item.position.x > bc2d.bounds.min.x &&
                item.position.y > bc2d.bounds.min.y &&
                item.position.y < bc2d.bounds.max.y)
            {
                food.EmptyCells.Add(item);
               
            }

        }
    }

    public void tempmethod()
    {

    }
}
