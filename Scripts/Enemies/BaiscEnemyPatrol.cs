using UnityEngine;

public class BaiscEnemyPatrol : MonoBehaviour
{

    [SerializeField]
    private float leftXPatrol;
    [SerializeField]
    private float rightXPatrol;
    private int direction = 1;
    [SerializeField]
    private float speed;

    [SerializeField]
    private Rigidbody2D rb2d;

    // Start is called before the first frame update

    private void Start()
    {
        if(leftXPatrol == 0) //if the left or right variables are left blank, change them to the game objects current transform
        {
            leftXPatrol = transform.position.x;
        }
        if(rightXPatrol == 0)
        {
            rightXPatrol = transform.position.x;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < leftXPatrol) //if transform is too far left
        {
            direction = 0; //change direction variable
        }
        if(transform.position.x > rightXPatrol) //if transform is too far right
        {
            direction = 1; //change direction variable
        }

        if (direction == 0) //if direction variable is 0, move right
        {
            rb2d.velocity = new Vector2(2 * (speed+1), 0);
        }
        else 
        {
            rb2d.velocity = new Vector2(-2 * (speed + 1), 0); //else move left
        }

    }
}
