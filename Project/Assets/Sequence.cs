using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Sequence : MonoBehaviour 
{
	public Text CountText;
	public Text NumText;
	public bool enablePlayer{ private set; get; }
	public bool gameOver{ set; get; }
	private int num;
	void Awake()
	{
		enablePlayer = false;
		StartCoroutine(GameLoop());
	}

	public void AddNum()
	{
		num++;
		NumText.text = num.ToString();
	}

	public void DecNum()
	{
		num--;
		NumText.text = num.ToString();
	}


	private IEnumerator GameLoop()
	{
		for ( int count = 3; count > 0; count-- )
		{
			CountText.text = count.ToString();
			yield return new WaitForSeconds(1.0f);
		}

		CountText.enabled = false;
		enablePlayer = true;

		while ( true )
		{
			if ( num <= 0 )
			{
				yield return StartCoroutine( Clear() );
			}

			if ( gameOver )
			{
				yield return StartCoroutine( GameOver() );
			}
			yield return null;
		}
	}

	private IEnumerator Clear()
	{
		var enemy = FindObjectOfType<敵>();
		Destroy(enemy.gameObject);

		CountText.text = "CLEAR";
		CountText.enabled = true;

		yield return new WaitForSeconds(10.0f);

		SceneManager.LoadScene(0);

	}

	private IEnumerator GameOver()
	{
		var enemy = FindObjectOfType<敵>();
		Destroy(enemy.gameObject);

		CountText.text = "DEAD";
		CountText.enabled = true;

		yield return new WaitForSeconds(2.0f);

		SceneManager.LoadScene(0);

	}






}
