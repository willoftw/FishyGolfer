using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{
    Vector3 VScreen = new Vector3();
    Vector3 VWorld = new Vector3();

    // Time when the movement started.
    private float startTime;
    // Total distance between the markers.
    private float journeyLength;

    public float speed = 0.1F;

    bool moving = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        VWorld.z = 0;
        Debug.DrawLine(this.transform.position, VWorld);
        Debug.DrawLine(this.transform.position, -VWorld, Color.green);

        if (moving)
        {
            // Distance moved equals elapsed time times speed..
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(this.transform.position, -VWorld, fractionOfJourney);
            if (Vector3.Distance(-VWorld, this.transform.position) < 0.1f)
            {
                moving = false;
                Debug.Log("Reached Target");
            }
        }
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
        if (moving)
            return;
        startTime = Time.time;

        // Calculate the journey length.
        journeyLength = Vector3.Distance(VWorld, this.transform.position);
        speed = VWorld.magnitude;

        this.GetComponent<Rigidbody2D>().AddForce((VWorld.normalized*-1) * (Vector3.Distance(VWorld,this.transform.position)*10));
        moving = true;
    }
}
