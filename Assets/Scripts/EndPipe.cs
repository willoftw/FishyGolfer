using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPipe : MonoBehaviour
{

    public Sprite emptySprite;
    public Sprite fullSprite;

    private SpriteRenderer sr;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("HOLE");
        sr.sprite = fullSprite;
        Destroy(collision.gameObject);

        //TODO: Make this lerp towards top, it is effectively the "cutscene"
        GameObject.FindGameObjectWithTag("MiniMapCamera").SetActive(false);
        Camera.main.orthographicSize = 7;
    }
}
