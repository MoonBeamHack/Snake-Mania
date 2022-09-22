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
    public List<FoodGrid> EmptyCells = new List<FoodGrid>();

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
                }
            }
        }
    }

    public void RePosition()
    {

        float randomX = (int)Random.Range(SpawnArea.min.x, SpawnArea.max.x);
        float randomY = (int)Random.Range(SpawnArea.min.y, SpawnArea.max.y);

        Debug.Log("Going to new pos");
        var emptyCells = grid.FindAll(x => x.Occupied == false);
        if (emptyCells.Count == 0) return;
        foreach (var item in emptyCells)
        {
            EmptyCells.Add(item);
        }
        Vector3 randomPos = EmptyCells[Random.Range(0, EmptyCells.Count)].position;

        transform.position = randomPos;
        //transform.position = new Vector3(randomX, randomY);
        
        currentColor = (int)Random.Range(0, FruitType.Count);
        GetComponent<SpriteRenderer>().color = FruitType[currentColor];
        EmptyCells.Clear();
       // PopulateGrid(true);
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
