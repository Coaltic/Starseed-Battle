using UnityEngine;

public class MoveableTile : MonoBehaviour
{
    public bool taken;

    public Transform tileLocation;

    private void Awake()
    {
        tileLocation = this.transform;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
