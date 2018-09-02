using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 障害物となるわっか
/// </summary>
public class Ring : MonoBehaviour
{
	/// <summary>
	/// 子供になるオブジェクト
	/// Assetフォルダ内に置いておいて複製する
	/// </summary>
	public GameObject childPrefab;
	/// <summary>
	/// 回るオブジェクトの数。
	/// インスペクタから設定する
	/// </summary>
	public int size;
	/// <summary>
	/// 初期の長さ
	/// </summary>
	public float initLength;
	/// <summary>
	/// 広がりきるまでの時間
	/// </summary>
	public float timeOpen;
	/// <summary>
	/// 回転速度
	/// </summary>
	public float moveSpeed;
	/// <summary>
	/// オブジェクトを入れる入れつ
	/// </summary>
	private Transform[] childTransformArray{ set; get; }
	/// <summary>
	/// 今どのくらい回転したか
	/// </summary>
	private float currentOffset{ set; get; }

	/// <summary>
	/// リングの半径
	/// </summary>
	private float length{ set; get; }

	/// <summary>
	/// 広がり切るまでの時間を計測したりするのに使う
	/// </summary>
	private float _timer;

	/// <summary>
	/// シーケンス用
	/// </summary>
	private System.Action _action;

	void Awake()
	{
		childTransformArray = new Transform[size];

		for ( int i = 0; i < childTransformArray.Length; i++ )
		{
			childTransformArray[i] = Instantiate(childPrefab).transform;
			childTransformArray[i].name = string.Format("{0}_{1}", name, i.ToString("00"));
			childTransformArray[i].parent = transform;
		}

		UpdateRing();

		_action = Act_Open_Init;
	}

	void Update()
	{
		if ( _action != null )
		{
			_action();
		}

		// リング毎フレーム処理
		UpdateRing();
	}

	void Act_Open_Init()
	{
		_timer = timeOpen;
		length = 0;
		_action = Act_Open;
	}

	void Act_Open()
	{
		_timer = Mathf.Max( _timer - Time.deltaTime, 0 );

		if ( timeOpen <= 0 ){Debug.LogError("timeOpen は 0 よりおおきく");  return; }

		// Sinカーブにすることで減速っぽく開く
		length = Mathf.Sin( ( 1.0f - _timer / timeOpen ) * Mathf.PI / 2.0f ) * initLength;

		if ( _timer == 0)
		{
			_action = Act_Idle;
		}
	}

	void Act_Idle()
	{

	}

	/// <summary>
	/// 子供たちをそれぞれ適切な座標位置に配置する
	/// </summary>
	void UpdateRing()
	{
		// 回転をアニメーションさせる
		currentOffset += moveSpeed * Time.deltaTime;
		for( int i = 0; i < childTransformArray.Length; i++ )
		{
			var rad = (float)i / (float)childTransformArray.Length * Mathf.PI * 2.0f;
			rad += currentOffset;
			var pos = new Vector3( Mathf.Cos(rad), Mathf.Sin(rad), 0.0f ) * length;
			childTransformArray[i].transform.localPosition = pos;
		}
	}
}
