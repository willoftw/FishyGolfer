using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashController : MonoBehaviour
{
    float splashTime = 2.0f;
    public GameObject fader;
    Image fadeImage;
    // Start is called before the first frame update
    void Start()
    {
        fadeImage = fader.GetComponent<Image>();
        StartCoroutine(FadeToBlackCoroutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator StartGame(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(1);
    }

    private IEnumerator FadeToBlackCoroutine()
    {
        Debug.Log("Fading");
        float fade = 0.0f;
        fader.SetActive(true);
        do
        {
            Color c = fadeImage.color;
            c.a = Mathf.Lerp(0.0f, 1.0f, fade);
            fadeImage.color = c;
            fade += 0.5f * Time.deltaTime;
            yield return null;
        } while (fade <= 1.0f);
        SceneManager.LoadScene(1);
    }
}
