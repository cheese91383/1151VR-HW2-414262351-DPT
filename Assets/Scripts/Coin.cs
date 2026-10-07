using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public Sprite[] frames;
    public float riseHeight = 1.5f;
    public float duration = 0.5f;
    public float frameTime = 0.08f;

    bool collected;
    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !collected)
        {
            collected = true;

            GetComponent<Collider2D>().enabled = false;
            StartCoroutine(Pop());
        }
    }

    IEnumerator Pop()
    {
        Vector3 start = transform.position;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;

            float v = Mathf.Sin(t / duration * Mathf.PI);
            transform.position =  start + Vector3.up * riseHeight * v; 

            int index = (int)(t / frameTime) % frames.Length;
            sr.sprite = frames[index];

            yield return null;
        }

        Destroy(gameObject);
    }
}