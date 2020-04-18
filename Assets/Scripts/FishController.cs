using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{
    Vector3 VScreen = new Vector3();
    Vector3 VWorld = new Vector3();
    Vector3 HitVector = Vector3.zero;

    Vector3 ballStartPosition;

    private Rigidbody2D rb;

    // Time when the movement started.
    private float startTime;
    // Total distance between the markers.
    private float journeyLength;

    public float speed = 0.1F;

    bool moving = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        VWorld.z = 0;
        Debug.DrawLine(this.transform.position, VWorld);
        Debug.DrawLine(this.transform.position, this.transform.position + -HitVector, Color.green);

        if (moving)
        {
            // Distance moved equals elapsed time times speed..
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / journeyLength;
            //Debug.Log(fractionOfJourney);

            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(ballStartPosition, ballStartPosition + -HitVector, fractionOfJourney);
            Debug.DrawLine(ballStartPosition, ballStartPosition + -HitVector, Color.blue);
            if (Vector3.Distance(ballStartPosition + -HitVector, this.transform.position) < 0.01f)
            {
                rb.velocity = Vector3.zero;
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
        if (moving)
            return;
        VScreen.x = Input.mousePosition.x;
        VScreen.y = Input.mousePosition.y;
        VScreen.z = Camera.main.transform.position.z;
        VWorld = Camera.main.ScreenToWorldPoint(VScreen);

        HitVector = VWorld - this.transform.position;
    }

    private void OnMouseUp()
    {
        if (moving)
            return;
        startTime = Time.time;
        ballStartPosition = this.transform.position;
        
        // Calculate the journey length.
        journeyLength = Vector3.Distance(VWorld, this.transform.position);
        speed = journeyLength;
        Debug.LogFormat("speed: {0} length: {1}", speed, journeyLength);

        HitVector = VWorld - this.transform.position;

        //this.GetComponent<Rigidbody2D>().AddForce((this.transform.position + -HitVector * -1) * (Vector3.Distance(VWorld,this.transform.position)*10));
        moving = true;
    }
}
