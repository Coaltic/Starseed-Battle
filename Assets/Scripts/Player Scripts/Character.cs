using UnityEngine;

public class Character : MonoBehaviour
{
    public MoveableTile currentLocationTile;
    public int turnOrder;

    public Vector2 standardScale;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        standardScale = new Vector2(transform.localScale.x, transform.localScale.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
