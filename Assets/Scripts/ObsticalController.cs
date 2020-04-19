using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsticalController : MonoBehaviour
{
    //public static 
    public float enterDragChange=0.0f;
    public float exitDragChange = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.attachedRigidbody.drag += enterDragChange;
        Debug.Log("Triggered: "+ other.tag);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        other.attachedRigidbody.drag += exitDragChange;
    }

}
