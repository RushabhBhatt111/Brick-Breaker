using UnityEngine;

public class paddle : MonoBehaviour
{
    public Vector2 direction { get; private set; }
    public float speed = 30f;

    public float max_bounce_angle = 75f;
    public new Rigidbody2D rigidbody { get; private set; }
    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            this.direction = Vector2.left;  
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            this.direction = Vector2.right;
        }
        else 
        { 
            this.direction = Vector2.zero;
        }
    }
    private void FixedUpdate()
    {
        if(this.direction != Vector2.zero)
        {
            this.rigidbody.AddForce (this.direction * this.speed);
        }
    }

    public void ResetPaddle()
    {
        this.transform.position = new Vector2(0f, this.transform.position.y);
        this.rigidbody.velocity = Vector2.zero; 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ball ball = collision.gameObject.GetComponent<ball>();

        if (ball != null)
        {
            Vector3 paddlePosition = this.transform.position;
            Vector2 ContactPoint = collision.GetContact(0).point;

            float offet = paddlePosition.x - ContactPoint.x;
            float width = collision.otherCollider.bounds.size.x / 2; // collision takes ball collision data and otherCollider takes paddle collision data 
            
            float current_angle = Vector2.SignedAngle(Vector2.up, ball.rigidbody.velocity);
            float bounce_angle = (offet / width) * this.max_bounce_angle;
            float new_angle = Mathf.Clamp(current_angle + bounce_angle, -this.max_bounce_angle, this.max_bounce_angle);

            Quaternion rotations = Quaternion.AngleAxis(new_angle, Vector3.forward);
            ball.rigidbody.velocity = rotations * Vector2.up * ball.rigidbody.velocity.magnitude;

        }
    }
}
