using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Snake : MonoBehaviour
{
    private List<Transform> segments = new List<Transform>();//creates the list for the segments
    public Transform segmentPrefab;
    public Vector2 direction = Vector2.right;
    public int initialSize = 8;//initializes the snake saize to 8

    private void Start()//Starts the game.
    {
        ResetState();//respawns the snake.
    }

    private void Update()
    {
        
        if (direction.x != 0f)//handles the movement when the snake is on x axis and allows only up and down movement
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
                direction = Vector2.up;//moves the snake up
            } else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
                direction = Vector2.down;//moves the snake down
            }
        }
        
        else if (direction.y != 0f)//handles the movement when the snake is on x axis and allows only right and left movement
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
                direction = Vector2.right;//moves the snake right
            } else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
                direction = Vector2.left;//moves the snake left
            }
        }
    }

    private void FixedUpdate()
    {
        
        for (int i = segments.Count - 1; i > 0; i--) {
            segments[i].position = segments[i - 1].position;//makes the segments always follow the segment before it or the one after it
        }

        float x = Mathf.Round(transform.position.x) + direction.x;
        float y = Mathf.Round(transform.position.y) + direction.y;//ensures that the snake moves within a set grid by rounding off the coordinates

        transform.position = new Vector2(x, y);//Makes the snake move in the direction it is facing
    }

    public void Grow()//handles when the snake grows a segment
    {
        Transform segment = Instantiate(segmentPrefab);//uses the prefab as the template for the segment
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);//adds a segment to the snake
    }

    public void ResetState()//resets the game
    {
        direction = Vector2.right;
        transform.position = Vector3.zero;
        for (int i = 1; i < segments.Count; i++) {
            Destroy(segments[i].gameObject);
        }
        segments.Clear();
        segments.Add(transform);

        for (int i = 0; i < initialSize - 1; i++) {
            Grow();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)//handles when the snake hits another game objects collider box
    {
        if (other.gameObject.CompareTag("Heart")) {//activates growth of segment when the snake "eats" a Heart
            Grow();
        } else if (other.gameObject.CompareTag("Wall")) { 
            //activates Resetstate to simulate death when the snake "hits" any gameobject tagged Wall (walls and the snakes body itself)
            ResetState();
        }
    }

}
