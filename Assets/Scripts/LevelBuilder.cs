using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    public GameObject platformPrefab;
    public GameObject flag;
    public GameObject coinPrefab;
    public Vector2[] platformPositions =
    {
        new Vector2( 5f, -1f),
        new Vector2( 0f,  1f),
        new Vector2(-5f,  3f),
        new Vector2( 2.5f, 4f)
    };
    

    public Vector2[] coinPositions =
    {
    new Vector2( 5f,  0.2f),
    new Vector2( 0f,  2.2f),
    new Vector2(-5f,  4.2f),
    new Vector2(-3f, -1.7f)
};
    void Start()
    {
        for(int i=0;i< platformPositions.Length; i++)
        {
            Instantiate(platformPrefab, platformPositions[i], Quaternion.identity);
        }
        flag.SetActive(true);
        for(int i= 0; i< coinPositions.Length; i++)
        {
            Instantiate(coinPrefab, coinPositions[i], Quaternion.identity);
        }
    }
}