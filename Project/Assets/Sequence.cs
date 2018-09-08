using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Sequence : MonoBehaviour 
{
	public Canvas ResultCanvasPrefab;
	public Text CountText;
	public Text NumText;
	public Text Info;
	public Text Info2;
	public bool enablePlayer{ private set; get; }
	public float playerSpeedPerSecond{ set; get;}
	public bool gameOver{ set; get; }
	private int num;
	private float timer{ set; get; }

	void Awake()
	{
		enablePlayer = false;
		Info2.enabled = false;

		StartCoroutine(GameLoop());
	}

	void Update()
	{

		Info.text = 
string.Format(
@"{0}Mm/h
{1}:{2:00}", 
(int)(playerSpeedPerSecond * 60.0f * 60.0f / 1000.0f),
(int)timer,
(int)(timer * 100.0f)%100
 );
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
			timer += Time.deltaTime;
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

		yield return new WaitForSeconds(1.0f);

		Info2.enabled = true;

		while( !Input.GetButtonDown("Fire1"))
		{
			yield return null;
		}

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

	public void ShowResult( bool OK )
	{
		var canvas = Instantiate( ResultCanvasPrefab ).GetComponent<Canvas>();
		if ( !OK )
		{
			canvas.GetComponentInChildren<Text>().text = "Failed!";
			canvas.GetComponentInChildren<Text>().color = Color.blue;
		}

		Destroy( canvas.gameObject, 3.0f );
	}






}
