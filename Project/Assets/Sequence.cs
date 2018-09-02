using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sequence : MonoBehaviour 
{
	public Text CountText;
	public Text NumText;
	public bool enablePlayer{ private set; get; }
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
	}





}
