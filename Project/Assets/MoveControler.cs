using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// キー入力で動かす
/// </summary>
public class MoveControler : MonoBehaviour 
{
	/// <summary>
	/// どんどん足してくスピード
	/// </summary>
	public float forwardF;
	/// <summary>
	/// ブレーキ量
	/// </summary>
	public float BrakeF;
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
	/// <summary>
	/// 現在のスピード
	/// </summary>
	public float currentSpeed{ private set; get; }

	/// <summary>
	/// 今y方向にどれだけ回転してるか
	/// </summary>
	public float currentRotUp{ private set; get; }
	/// <summary>
	/// 今右とか左にどれだけ回転してるか
	/// </summary>
	public float currentRotX{ private set; get; }

	/// <summary>
	/// アクセル
	/// </summary>
	public bool Accell{ set; get; }
	/// <summary>
	/// ブレーキ
	/// </summary>
	public bool Brake{ set; get; }
	/// <summary>
	/// 水平回転( -1 ~ 1 前提 )
	/// </summary>
	public float Horizontal{ set; get; }
	/// <summary>
	/// 垂直回転( -1 ~ 1 前提 )
	/// </summary>
	public float Vertical{ set; get; }

	void Update()
	{
		// 前進
		if ( Accell )
		{
			currentSpeed += Time.deltaTime * forwardF;
		}
		else
		{
			currentSpeed = Mathf.Max( currentSpeed - Time.deltaTime * decF, 0 );
		}

		// 減速
		if ( Brake )
		{
			currentSpeed = Mathf.Max( currentSpeed - BrakeF * Time.deltaTime, 0);
		}

		transform.localPosition += transform.forward * currentSpeed * Time.deltaTime;

		// 回転
		currentRotUp += Horizontal * Time.deltaTime * rotateSpeedX;
		currentRotX  -= Vertical * Time.deltaTime * rotateSpeedY;

		transform.rotation = Quaternion.Euler( currentRotX, currentRotUp, 0 );
	}

	/// <summary>
	/// 敵のための処理。
	/// 即時姿勢を変更する
	/// </summary>
	public void AddRotate( float Up, float X )
	{
		currentRotUp += Up;
		currentRotX += X;
	}

}
