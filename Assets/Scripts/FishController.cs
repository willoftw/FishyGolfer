using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{
    Vector3 target = new Vector3();
    Vector3 HitVector = Vector3.zero;
    Vector3 ballStartPosition;

    private Rigidbody2D rb;

    private Animator animator;

    private LineRenderer lr;


    // Time when the movement started.
    private float startTime;
    // Total distance between the markers.
    private float journeyLength;


    public float speed = 0.1F;

    public bool moving = false;
    private float maxSpeed;
    public float dragFactor=1000;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        lr = this.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        target = (ballStartPosition + -HitVector);//*10;
        Debug.DrawLine(this.transform.position, target, Color.green);

        //control animation state
        //Debug.Log(rb.velocity.magnitude);
        animator.SetFloat("speed", rb.velocity.magnitude);

        if (moving && rb.velocity.magnitude < 0.05f)
        {
            rb.angularVelocity = 0.0f;
            moving = false;
            
            Debug.Log("stopped");
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawLine(transform.position, hit.point,Color.cyan);
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
            HitVector = hit.point - this.transform.position;
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
        ballStartPosition = this.transform.position;

        target = (ballStartPosition + -HitVector);//*10;
        journeyLength = Vector3.Distance(target, this.transform.position);
        DrawLine(this.transform.position, target,Color.blue, 0.2f);
    }

    void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f)
    {
        lr.sortingOrder = 98;
        lr.material = new Material(Shader.Find("UI/Default"));
        lr.SetColors(color, color);
        lr.SetWidth(0.1f, 0.1f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }

    private void OnMouseUp()
    {
        if (moving)
            return;

        //Remove the pointer
        lr.SetPosition(0, Vector3.zero);
        lr.SetPosition(1, Vector3.zero);

        this.GetComponent<Rigidbody2D>().AddForce(((target - this.transform.position)*journeyLength)*100);
        moving = true;

        //force animation to start rather than waiting for velocity to climb (therfore being framerate dependant)
        animator.SetFloat("speed", 1.0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Fish Collided with: " + collision.gameObject.tag);
        ContactPoint2D contact = collision.GetContact(0);
        this.transform.position = new Vector3(contact.point.x, contact.point.y, 0);

    }

    private void OnTriggerEnter(Collider other)
    {

    }

}
