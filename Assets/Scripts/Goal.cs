using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameObject diamondPrefab;
    bool reached;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && reached == false)
        {
            reached = true;
            GameObject d = Instantiate(diamondPrefab, transform.position + new Vector3(-0.8f, 1f, 0f), Quaternion.identity);
            d.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0f, 6f);
            //Destroy(d, 2f);

            FindAnyObjectByType<GameManager>().Win();
            Debug.Log("過關!!");

        }
    }
}
