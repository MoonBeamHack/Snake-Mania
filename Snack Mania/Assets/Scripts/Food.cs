using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DefaultExecutionOrder(0)]
public class Food : MonoBehaviour
{
    public bool Initialize;
    public GameObject prefab;
    public List<Color> FruitType = new List<Color>();
    public Bounds SpawnArea;
    public int currentColor;
    public List<FoodGrid> grid = new List<FoodGrid>();
    public List<FoodGrid> EmptyCells = new List<FoodGrid>();

    #region initializeGrid

    private void Awake()
    {
       // PopulateGrid(Initialize);
    }

   
    public void PopulateGrid(bool FirstTime)
    {
        if (FirstTime)
        {
            for (int i = (int)SpawnArea.min.x; i < SpawnArea.max.x; i++)
            {
                for (int j = (int)SpawnArea.min.y; j < SpawnArea.max.y; j++)
                {
                    FoodGrid newGrid = new FoodGrid(new Vector3(i, j, 0), false);
                    grid.Add(newGrid);
                    EmptyCells.Add(newGrid);
                }
            }
        }
        else
        {
            var oldOccupied = grid.FindAll(x => x.Occupied == true);
            for (int i = 0; i < oldOccupied.Count; i++)
            {
                oldOccupied[i].Occupied = false;
                EmptyCells.Add(oldOccupied[i]);
            }
        }
        
    }
    #endregion
    #region reset Fruit
    public void RePosition()
    {
        Debug.Log("Going to new pos");

        #region finds Empty cell
        
        if (EmptyCells.Count == 0) return;

        Vector3 randomPos = EmptyCells[Random.Range(0, EmptyCells.Count)].position;
        #endregion
        transform.position = randomPos;
        
        currentColor = (int)Random.Range(0, FruitType.Count);
        GetComponent<SpriteRenderer>().color = FruitType[currentColor];
        //EmptyCells.Clear();
    }
    #endregion
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
