﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPipe : MonoBehaviour
{

    public Sprite emptySprite;
    public Sprite fullSprite;
    public float maxFishVelocity = 10.5f;//0.5

    private SpriteRenderer sr;

    private bool tooFast = false;
    // Start is called before the first frame update
    void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
        sr.sprite = emptySprite;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("HOLE: " + collider.GetComponent<Rigidbody2D>().velocity.magnitude);
        if (collider.GetComponent<Rigidbody2D>().velocity.magnitude > maxFishVelocity)
        {
            tooFast = true;
            return;
        }
        else
        {
            tooFast = false;
        }
        sr.sprite = fullSprite;
        StartCoroutine(hideAnimation(3));
        //Destroy(collider.gameObject);

        //TODO: Make this lerp towards top, it is effectively the "cutscene"
        //GameObject.FindGameObjectWithTag("MiniMapCamera").SetActive(false);
        Camera.main.orthographicSize = 7;
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (tooFast)
            return;
        if (collider.tag == "Fish")
        {
            Scored(collider);
        }
    }



    void Scored(Collider2D collider)
    {
  
        Debug.Log("Scored");
        StatusTracker.Instance.Pause();
        //StatusTracker.Instance.Pause();
        sr.sprite = fullSprite;
        StartCoroutine(hideAnimation(1));
        collider.gameObject.SetActive(false);
    }

    private IEnumerator hideAnimation(float time)
    {
        yield return new WaitForSeconds(time);
        sr.sprite = emptySprite;
        GameManager.Instance.Win();
        
    }

}
