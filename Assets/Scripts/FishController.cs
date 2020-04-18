using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{
    Vector3 VWorld = new Vector3();
    Vector3 target = new Vector3();
    Vector3 HitVector = Vector3.zero;
    Vector3 ballStartPosition;

    private Ray ray = new Ray();

    private Rigidbody2D rb;

    private Animator animator;


    // Time when the movement started.
    private float startTime;
    // Total distance between the markers.
    private float journeyLength;


    public float speed = 0.1F;

    bool moving = false;
    private float maxSpeed;
    public float dragFactor=1000;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        VWorld.z = 0;
        target = (ballStartPosition + -HitVector);//*10;
 
        Debug.DrawLine(this.transform.position, target, Color.green);
        //Debug.Log(rb.velocity.magnitude);
        animator.SetFloat("speed", rb.velocity.magnitude);
        if (moving && rb.velocity.magnitude < 0.01f)
        {
            rb.angularVelocity = 0.0f;
            moving = false;
            
            Debug.Log("stopped");
        }

        /* ray = Camera.main.ScreenPointToRay(Input.mousePosition);
         Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

         Debug.DrawLine(this.transform.position, this.transform.position+ray.direction, Color.blue);
 */
        // Cast a ray from screen point
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
        // Save the info
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawLine(transform.position, hit.point,Color.cyan);
            HitVector = hit.point - this.transform.position;

        }
        

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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
        // Save the info
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            ballStartPosition = this.transform.position;
            Debug.DrawLine(transform.position, hit.point, Color.cyan);
            HitVector = hit.point - this.transform.position;

        }


    }

    private void OnMouseUp()
    {
        if (moving)
            return;
        /*        startTime = Time.time;
                ballStartPosition = this.transform.position;
                HitVector = VWorld - this.transform.position;

        target = (ballStartPosition + -HitVector);//*10;
                //target.Scale(new Vector3(2.0f, 2.0f, 2.0f));

                // Calculate the journey length.
                journeyLength = Vector3.Distance(target, this.transform.position);
                speed = journeyLength/2;
                maxSpeed = speed;
                Debug.LogFormat("speed: {0} length: {1}", speed, journeyLength);

        */
        target = (ballStartPosition + -HitVector);//*10;
        journeyLength = Vector3.Distance(target, this.transform.position);
        this.GetComponent<Rigidbody2D>().AddForce(((target - this.transform.position)*journeyLength)*100);
        moving = true;
        animator.SetFloat("speed", 1.0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D contact = collision.GetContact(0);
        this.transform.position = new Vector3(contact.point.x,contact.point.y,0);
        Debug.Log("Fish Collided");
    }

}
