using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : Singleton<Pointer>
{
    public Rigidbody cylinderPrefab; //assumed to be 1m x 1m x 2m default unity cylinder to make calculations easy
    Rigidbody cylinder;

    // Start is called before the first frame update
    void Start()
    {
        //CreateCylinderBetweenPoints(Vector3.zero, new Vector3(10, 10, 10), 0.5f);
    }

    public void CreateCylinderBetweenPoints(Vector3 start, Vector3 end, float width)
    {
        var offset = end - start;
        var scale = new Vector3(width, offset.magnitude / 2.0f, width);
        var position = start + (offset / 2.0f);

        if (cylinder == null)
            cylinder = Instantiate(cylinderPrefab, position, Quaternion.identity);
        cylinder.transform.up = offset;
        cylinder.transform.localScale = scale;
    }

    // Update is called once per frame
    void Update()
    {

    }
}