using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{
    Vector3 VScreen = new Vector3();
    Vector3 VWorld = new Vector3();
    Vector3 target = new Vector3();
    Vector3 HitVector = Vector3.zero;

    Vector3 ballStartPosition;

    private Rigidbody2D rb;
    public AnimationCurve velocityCurve;

    // Time when the movement started.
    private float startTime;
    // Total distance between the markers.
    private float journeyLength;

    private float moveDuration = 0f;  // time since moving forward started

    public float speed = 0.1F;

    bool moving = false;
    private float maxSpeed;
    public float dragFactor=1000;

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
        Debug.DrawLine(this.transform.position, target, Color.green);
        if (rb.velocity == Vector2.zero)
            moving = false;

       /* if (moving)
        {
            moveDuration += Time.deltaTime;
            
           // Debug.Log(Mathf.Lerp(1.0f, 0.1f, distCovered));
            // Distance moved equals elapsed time times speed..
            float distCovered = (Time.time - startTime) * speed;


           // distCovered = Mathf.LerpUnclamped(maxSpeed, 0.1f, (Time.time - startTime));
            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / journeyLength;

            // distCovered -= Mathf.Abs(speed * (maxSpeed/dragFactor));
            //Debug.Log(speed);

            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(ballStartPosition, target, fractionOfJourney);
            Debug.DrawLine(ballStartPosition, target, Color.blue);
            if (Vector3.Distance(target, this.transform.position) < 0.01f)
            {
                rb.velocity = Vector3.zero;
                moving = false;
                Debug.Log("Reached Target");
                target = Vector3.zero;
            }
        }*/
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
        HitVector = VWorld - this.transform.position;

        target = (ballStartPosition + -HitVector);//*10;
        //target.Scale(new Vector3(2.0f, 2.0f, 2.0f));

        // Calculate the journey length.
        journeyLength = Vector3.Distance(target, this.transform.position);
        speed = journeyLength/2;
        maxSpeed = speed;
        Debug.LogFormat("speed: {0} length: {1}", speed, journeyLength);


        this.GetComponent<Rigidbody2D>().AddForce(target * journeyLength * 10);
        moving = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D contact = collision.GetContact(0);
        this.transform.position = new Vector3(contact.point.x,contact.point.y,0);
        Debug.Log("Fish Collided");
    }

}
