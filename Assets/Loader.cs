using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GameManager gm = GameManager.Instance;
        DontDestroyOnLoad(gm);
        DialogController dc = DialogController.Instance;
        StatusTracker st = StatusTracker.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
