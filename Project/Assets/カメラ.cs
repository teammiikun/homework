using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class カメラ : MonoBehaviour 
{
	/// <summary>
	/// 探すのめんどくさいから、インスペクタから直接突っ込む
	/// </summary>
	public Transform target;
	/// <summary>
	/// カメラのターゲットからのオフセット位置
	/// </summary>
	public Vector3 offsetPosition;
	/// <summary>
	/// 追従の抵抗(回転)
	/// </summary>
	public float lerpSpeedRot;
	/// <summary>
	/// 追従の抵抗(位置)
	/// </summary>
	public float lerpSpeedPos;

	/// <summary>
	/// ターゲットの前の姿勢
	/// </summary>
	private Quaternion prevRotation;
	/// <summary>
	/// 前回の位置
	/// </summary>
	private Vector3	   prevPosition;

	void Awake()
	{
		_UpdatePositionAndRotation( 1.0f, 1.0f );
	}


	void LateUpdate () 
	{
		_UpdatePositionAndRotation( Time.deltaTime * lerpSpeedPos, Time.deltaTime * lerpSpeedRot);

	}


	/// <summary>
	/// 前回の位置と回転から、lerp分だけ現在の位置と回転に変更して、
	/// 位置と回転を記録する
	/// </summary>
	/// <param name="lerpPosition"></param>
	/// <param name="lerpRotation"></param>
	private void _UpdatePositionAndRotation( float lerpPosition, float lerpRotation )
	{
		// 位置を箱に追従させる
		var pos = target.localToWorldMatrix.MultiplyPoint( offsetPosition );
		transform.position = Vector3.Lerp( prevPosition, pos, lerpPosition );
		prevPosition = transform.position;

		// 回転を箱に追従
		transform.rotation = Quaternion.Lerp( prevRotation, target.transform.rotation, lerpRotation );
		prevRotation = transform.rotation;

	}
}
