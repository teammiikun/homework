using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// キー入力で動かす
/// </summary>
public class 自機 : MonoBehaviour 
{
	/// <summary>
	/// どんどん足してくスピード
	/// </summary>
	public float forwardF;
	/// <summary>
	/// 入力が無い時に落ちてくスピード
	/// </summary>
	public float decF;
	/// <summary>
	/// 自分から見て水平方向に動く回転スピード
	/// </summary>
	public float rotateSpeedX;
	/// <summary>
	/// 上を向いたり、下を向いたりするスピード
	/// </summary>
	public float rotateSpeedY;

	private float currentSpeed{ set; get; }

	void Update()
	{
		// 前進
		if ( Input.GetButton("Fire1") )
		{
			currentSpeed += Time.deltaTime * forwardF;
		}
		else
		{
			currentSpeed = Mathf.Max( currentSpeed - Time.deltaTime * decF, 0 );
		}

		transform.localPosition += transform.forward * currentSpeed * Time.deltaTime;

		// 方向転換
		transform.Rotate( 
			-Input.GetAxisRaw("Vertical") * Time.deltaTime * rotateSpeedY,
			Input.GetAxisRaw("Horizontal") * Time.deltaTime * rotateSpeedX,
			 0 );
	}

}
