using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public GameObject prefab;
    public List<Color> FruitType = new List<Color>();
    public Bounds SpawnArea;
    public int currentColor;
    public List<FoodGrid> grid = new List<FoodGrid>();
    List<Vector2> discardedCell = new List<Vector2>();

    private void Awake()
    {
        PopulateGrid(false);
    }
    void PopulateGrid(bool rePopulate)
    {
        if (!rePopulate)
        {
            for (int i = (int)SpawnArea.min.x; i < SpawnArea.max.x; i++)
            {
                for (int j = (int)SpawnArea.min.y; j < SpawnArea.max.y; j++)
                {
                    FoodGrid newGrid = new FoodGrid(new Vector3(i , j , 0), false);
                    grid.Add(newGrid);
                    //grid.Add(new Vector2(i, j));
                    /*if (i == 5 && j == 0)
                        continue;
                    Instantiate(prefab, new Vector3(i, j, 0), Quaternion.identity);*/
                }
            }
        }
        /*else
        {
            for (int i = 0; i < discardedCell.Count; i++)
            {
                //grid.Add(discardedCell[i]);
            }
            discardedCell.Clear();
        }*/
    }

    public void RePosition()
    {

        float randomX = (int)Random.Range(SpawnArea.min.x, SpawnArea.max.x);
        float randomY = (int)Random.Range(SpawnArea.min.y, SpawnArea.max.y);
       /* if (grid.Count == 0)
        { PopulateGrid(true); return; }
        Vector2 randomPos = grid[Random.Range(0, grid.Count)];

        if(Physics2D.Raycast(randomPos , Vector2.zero))
        {
            Debug.Log("alreadyOccupied");
            grid.Remove(randomPos);
            discardedCell.Add(randomPos);
            RePosition();
            return;
        }
        transform.position = randomPos;*/

        transform.position = new Vector3(randomX, randomY);
        
        currentColor = (int)Random.Range(0, FruitType.Count);
        GetComponent<SpriteRenderer>().color = FruitType[currentColor];

        PopulateGrid(true);
    }
}
[System.Serializable]
public class FoodGrid
{
    public Vector3 position;
    public bool Occupied;
    public FoodGrid(Vector2 pos, bool Occ)
    {
        position = pos;
        Occupied = Occ;
    }
}
