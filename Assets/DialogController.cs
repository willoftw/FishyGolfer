using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogController : MonoBehaviour
{
	public TextMeshProUGUI speechText;
	string fullText;

	void Start()
	{
		fullText = GameManager.Instance.getActiveCourseController().Dialog;
		speechText.text = "";

		//StartCoroutine(PlayDialog());
	}

	public void ReplayDialog()
	{
		fullText = GameManager.Instance.getActiveCourseController().Dialog;
		speechText.text = "";
		StartCoroutine(PlayDialog());
	}

	IEnumerator PlayDialog()
	{
		foreach (char c in fullText)
		{
			speechText.text += c;
			yield return new WaitForSeconds(0.100f);
		}
		if (GameManager.Instance.getActiveCourseController().isFinal)
		{
			//yield return new WaitForSeconds(1.000f);
			GameManager.Instance.Win(true);
			//do somthing like return to menu screen
		}
		else
		{
			GameManager.Instance.gameState = GameManager.GameState.ACTIVE;
			this.gameObject.SetActive(false);
		}
		
	}

}
