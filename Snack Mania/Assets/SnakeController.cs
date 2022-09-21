using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    public float Speed;
    [SerializeField] private GameObject AliveEyes;
    [SerializeField] private GameObject DeadEyes;
    [SerializeField] private List<Color> SkinColor = new List<Color>();


    private float OriginalSpeed;

    private Vector2 Direction = Vector2.up;
    private bool goingUp;
    private bool goingDown;
    private bool goingLeft;
    private bool goingRight;

    private Vector3 tempPos;

    [SerializeField] Transform bonePrefab;
    public List<Transform> Bone = new List<Transform>();
    private void Start()
    {
        GameManager.isGameRunning = true;
        tempPos = transform.position;
        SetSpeed(Speed);
        AliveEyes.SetActive(true);
        DeadEyes.SetActive(false);
        ChangeSkinColor(0);
    }
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

            for (int i = Bone.Count - 1; i > 0; i--)
            {
                Bone[i].position = Bone[i - 1].position;
            }
            transform.position = new Vector3(
                Mathf.Round(this.transform.position.x) + Direction.x,
                Mathf.Round(this.transform.position.y) + Direction.y);
        }
    }

    public void SetSpeed(float speed)
    {
        this.Speed = speed;
        Time.timeScale = Speed / 10;
    }

    private void Grow()
    {
        Transform newBone = Instantiate(this.bonePrefab);
        newBone.position = Bone[Bone.Count - 1].position;
        Bone.Add(newBone);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Food"))
        {
            other.GetComponent<Food>().RePosition();
            Grow();
        }
        if(other.CompareTag("Obstacle"))
        {
            Debug.Log("GameOver");
            GameManager.isGameRunning = false;
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
}
