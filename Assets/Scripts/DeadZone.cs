using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("!!!");
        if (collision.gameObject.name == "Ball")
        {
            FindAnyObjectByType<GameManager>().Miss();
            Debug.Log("HI");
        }
    }
}
