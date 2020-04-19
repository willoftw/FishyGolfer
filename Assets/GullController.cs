using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GullController : MonoBehaviour
{
    public bool yAxis = false;
    public float movement = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (movement == 0)
            return;
        if (yAxis)
            this.transform.position = new Vector3(this.transform.position.x,this.transform.position.y+ Mathf.Sin(Time.time)/movement, this.transform.position.z);
        else
            this.transform.position = new Vector3(this.transform.position.x+Mathf.Sin(Time.time)/movement, this.transform.position.y, this.transform.position.z);
    }
}


