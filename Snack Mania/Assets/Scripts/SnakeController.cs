using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{

    public float Speed;
    [SerializeField] private int initialSize;
    [SerializeField] private GameObject AliveEyes;
    [SerializeField] private GameObject DeadEyes;
    [SerializeField] private List<Color> SkinColor = new List<Color>();
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Food food;


    private float OriginalSpeed;

    private Vector2 Direction = Vector2.up;
    private bool goingUp;
    private bool goingDown;
    private bool goingLeft;
    private bool goingRight;

    private Vector3 tempPos;

    [SerializeField] Transform bonePrefab;
    public List<Transform> Bone = new List<Transform>();
    [SerializeField] bool doInstantly;
    #region initialize snake
    public void StartGame()
    {
        tempPos = transform.position;
        ResetGame();
        GetComponent<SpriteRenderer>().color = bonePrefab.GetComponent<SpriteRenderer>().color;
        
    }
    private void ResetGame()
    {
        //food.PopulateGrid(false);
        GameManager.isGameRunning = true;
        SetSpeed(Speed);
        AliveEyes.SetActive(true);
        DeadEyes.SetActive(false);


        for (int i = 1; i < Bone.Count; i++)
        {
            DestroyImmediate(Bone[i].gameObject);
        }
        Bone.Clear();
        Bone.Add(this.transform);

        transform.position = tempPos;

        for (int i = 0; i < initialSize; i++)
        {
            Bone.Add(Instantiate(bonePrefab));
        }
        food.RePosition();
    }
    #endregion
    private void Update()
    {
        if (GameManager.isGameRunning)
        {
            #region direction
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (goingDown) return;
                Direction = new Vector2(0, 1);
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (goingUp) return;
                Direction = new Vector2(0, -1);
                transform.rotation = Quaternion.Euler(0 , 0 , 180);
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (goingLeft) return;
                Direction = new Vector2(1, 0);
                transform.rotation = Quaternion.Euler(0 ,0 , 270);
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (goingRight) return;
                Direction = new Vector2(-1, 0);
                transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            #endregion
            #region movement

            //tempPos += new Vector3(Direction.x, Direction.y) * Time.deltaTime * Speed;
            //transform.position = new Vector3(Mathf.Round(tempPos.x), Mathf.Round(tempPos.y));
            #endregion
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.R))
                ResetGame();
        }
    }

    private void FixedUpdate()
    {
      
        if (GameManager.isGameRunning)
        {
            #region direction
            if (Direction == Vector2.up)
            {
                goingUp = true;

                goingLeft = false;
                goingRight = false;
                goingDown = false;
            }
            else if (Direction == Vector2.down)
            {
                goingDown = true;

                goingLeft = false;
                goingRight = false;
                goingUp = false;
            }
            else if (Direction == Vector2.right)
            {

                goingRight = true;

                goingDown = false;
                goingLeft = false;
                goingUp = false;
            }
            else if (Direction == Vector2.left)
            {
                goingLeft = true;

                goingDown = false;
                goingRight = false;
                goingUp = false;
            }
            #endregion

            #region movement
            for (int i = Bone.Count - 1; i > 0; i--)
            {
                Bone[i].position = Bone[i - 1].position;
            }
            transform.position = new Vector3(
                Mathf.Round(this.transform.position.x) + Direction.x,
                Mathf.Round(this.transform.position.y) + Direction.y);

            StartCoroutine(GridUpdate());
            #endregion
        }
    }
    #region Gameplay
    private void Grow()
    {
        Transform newBone = Instantiate(this.bonePrefab);
        newBone.position = Bone[Bone.Count - 1].position;
        Bone.Add(newBone); 
        //food.EmptyCells.Remove(food.grid.Find(x => x.position == Bone[Bone.Count -2].transform.position));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Food"))
        {
            Food food = other.GetComponent<Food>();
            StartCoroutine(ChangeSkinColorCO(food.currentColor));

            food.RePosition();
            Grow();
        }
        if (other.CompareTag("Obstacle"))
        {
            GameManager.Instance.GameOver();
            DeadEyes.SetActive(true);
            AliveEyes.SetActive(false);
        }
    }

    public void ChangeSkinColor(int colorID)
    {
        for (int i = 0; i < Bone.Count; i++)
        {
            Bone[i].GetComponent<SpriteRenderer>().color = SkinColor[colorID];
        }
        bonePrefab.GetComponent<SpriteRenderer>().color = SkinColor[colorID];
    }

    IEnumerator ChangeSkinColorCO(int colorID)
    {
        Debug.Log("changing  color :" + colorID);
        for (int i = 0; i < Bone.Count; i++)
        {
            Bone[i].GetComponent<SpriteRenderer>().color = SkinColor[colorID];
            if (!doInstantly)
                yield return new WaitForFixedUpdate();
            else
                yield return new WaitForSeconds(0);
        }
        bonePrefab.GetComponent<SpriteRenderer>().color = SkinColor[colorID];
    }
    #endregion
    IEnumerator GridUpdate()
    {
        FoodGrid MouthInGrid = food.grid.Find(x => x.position == transform.position);
        FoodGrid TailInGrid = food.grid.Find(x => x.position == Bone[Bone.Count - 1].position);
        int index;
        if (MouthInGrid != null)
        {
            index = food.grid.IndexOf(MouthInGrid);
            MouthInGrid.Occupied = true;
            food.grid[index] = MouthInGrid;
            if(food.EmptyCells.Contains(MouthInGrid))
                food.EmptyCells.Remove(MouthInGrid);
        }
        else
        {
            Debug.Log("Mouth Grid not found");
        }
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        if (TailInGrid != null)
        {
            index = food.grid.IndexOf(TailInGrid);
            TailInGrid.Occupied = false;
            food.grid[index] = TailInGrid;
            if (!food.EmptyCells.Contains(TailInGrid))
                food.EmptyCells.Add(TailInGrid);
        }
        else
        {
            Debug.Log("Tail Grid not found");
        }

    }
    public void SetSpeed(float speed)
    {
        this.Speed = speed;
        Time.timeScale = Speed / 10;
    }
    public void TestingFullLength()
    {

    }
}
