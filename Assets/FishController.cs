using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{
    Vector3 VScreen = new Vector3();
    Vector3 VWorld = new Vector3();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(VWorld);
        // Todo someting  
        VWorld.z = 0;
        Debug.DrawLine(this.transform.position, VWorld);
        Debug.DrawLine(this.transform.position, -VWorld, Color.green);
    }

    private void OnMouseDown()
    {
        Debug.Log("Mouse Down");
    }

    private void OnMouseDrag()
    {

        VScreen.x = Input.mousePosition.x;
        VScreen.y = Input.mousePosition.y;
        VScreen.z = Camera.main.transform.position.z;
        VWorld = Camera.main.ScreenToWorldPoint(VScreen);
    }

    private void OnMouseUp()
    {
        //this.GetComponent<Rigidbody2D>().AddForce((VWorld.normalized*-1) * (Vector3.Distance(VWorld,this.transform.position)*10));
    }
}
