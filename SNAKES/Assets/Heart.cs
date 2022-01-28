using UnityEngine;

public class Heart : MonoBehaviour
{
    public Collider2D Heartspawn;

    private void Start()
    {
        RandomizePosition();//starts the spawn on a randomized coordiantes
    }

    public void RandomizePosition()
    {
        Bounds bounds = Heartspawn.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);//generates random coordinates within set grid
        x = Mathf.Round(x);
        y = Mathf.Round(y);//ensures the hearts spawn within a set grid by rounding off the coordinates

        transform.position = new Vector2(x, y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        RandomizePosition();
    }

}
