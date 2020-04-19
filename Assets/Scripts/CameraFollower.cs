using UnityEngine;
using System.Collections;
using System;

public class CameraFollower : MonoBehaviour
{

    public GameObject player;        //Public variable to store a reference to the player game object


    public Vector3 offset;            //Private variable to store the offset distance between the player and camera

    // Use this for initialization
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        Debug.LogWarning(transform.position - player.transform.position);
    }

    public void CalculateOffset()
    {
        //offset = transform.position - player.transform.position;
    }
    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        try
        {
            Debug.Log(offset);
            //transform.position = player.transform.position + offset;
            StartCoroutine(LerpTo(player.transform.position + offset,1.0f));
        }
        catch (Exception e)
        {
            //its been destroyed, but i dont much mind.
        }
    }

    IEnumerator LerpFromTo(Vector3 pos1, Vector3 pos2, float duration)
    {
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            transform.position = Vector3.Lerp(pos1, pos2, t / duration);
            yield return 0;
        }
        transform.position = pos2;
    }

    IEnumerator LerpTo(Vector3 pos2, float duration)
    {
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            transform.position = Vector3.Lerp(this.transform.position, pos2, t / duration);
            yield return 0;
        }
        transform.position = pos2;
    }
}