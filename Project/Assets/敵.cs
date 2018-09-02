using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 敵 : MonoBehaviour 
{
	/// <summary>
	/// 移動処理をするコントローラー (自機と共通)
	/// </summary>
	public MoveControler controler;
	/// <summary>
	/// アセットの参照。
	/// </summary>
	public GameObject	 RingPrefab;
	/// <summary>
	/// 待ち時間最小 (インスペクタから設定する)
	/// </summary>
	public float waitTimeMin;
	/// <summary>
	/// 待ち時間最大 (インスペクタから設定する)
	/// </summary>
	public float waitTimeMax;
	/// <summary>
	/// 移動時間最小（インスペクタから設定する）
	/// </summary>
	public float moveTimeMin;
	/// <summary>
	/// 移動時間最大（インスペクタから設定する）
	/// </summary>
	public float moveTimeMax;
	/// <summary>
	/// 移動⇒待ち の後に横にどれだけ回転するか(最小)
	/// インスペクタから設定する
	/// </summary>
	public float addRotXMin;
	/// <summary>
	/// 移動⇒待ち の後に横にどれだけ回転するか(最大)
	/// インスペクタから設定する
	/// </summary>
	public float addRotXMax;
		/// <summary>
	/// 移動⇒待ち の後に上にどれだけ回転するか(最大)
	/// インスペクタから設定する
	/// </summary>
	public float addRotUpMin;
	/// <summary>
	/// 移動⇒待ち の後に上にどれだけ回転するか(最大)
	/// インスペクタから設定する
	/// </summary>
	public float addRotUpMax;
	/// <summary>
	/// Ringを作るスパン
	/// </summary>
	public float createRingSpan;
	/// <summary>
	/// 状態ごとでUpdateの内容を変えるためのもの
	/// </summary>
	private System.Action _action;
	/// <summary>
	/// 何秒移動する、とか何秒待つ、とかの汎用的な時間処理用のタイマー
	/// </summary>
	private float _timer;

	/// <summary>
	/// このタイマーがゼロになるとRingを作る
	/// </summary>
	private float _createRingTimer;

	void Awake()
	{
		_action = Act_Move_Init; 
	}

	/// <summary>
	/// 処理の一番最初
	/// </summary>
	void Act_Move_Init()
	{
		// どのくらい移動をしてるか時間を決める
		_timer = Random.Range( moveTimeMin, moveTimeMax );

		// アクセルを踏む
		controler.Accell = true;
		
		// 移動処理へ
		_action = Act_Acc;
	}

	/// <summary>
	/// 加速
	/// </summary>
	void Act_Acc()
	{
		// 時間を減らす
		_timer -= Time.deltaTime;
		if ( _timer <= 0.0f )
		{
			// アクセルを離す
			controler.Accell = false;
			// 減速処理へ
			_action = Act_Dcc;
		}
	}

	/// <summary>
	/// 減速が終わるまで待機する処理
	/// </summary>
	void Act_Dcc()
	{
		if ( controler.currentSpeed <= 0 )
		{
			// 減速し終わってたら次へ
			_action = Act_Wait_Init;
		}
	}

	/// <summary>
	/// どのくらい待機するか時間を決める
	/// </summary>
	void Act_Wait_Init()
	{
		// どのくらい待機するか時間を決める
		_timer = Random.Range( waitTimeMin, waitTimeMax );
		_action = Act_Wait;
	}

	/// <summary>
	/// 待機処理
	/// </summary>
	void Act_Wait()
	{
		// 時間を減らす
		_timer -= Time.deltaTime;
		if ( _timer <= 0.0f )
		{
			_action = Act_Rotate;
		}
	}

	/// <summary>
	/// 回転設定
	/// </summary>
	void Act_Rotate()
	{
		// ランダムに少し回転
		controler.AddRotate(
			Random.Range( addRotUpMin, addRotUpMax ),
			Random.Range( addRotXMin, addRotXMax ) );
		// 移動の開始に戻る
		_action = Act_Move_Init;
	}

	/// <summary>
	/// 毎フレームカウントして、
	/// タイマーが0になったらRingを生成する
	/// </summary>
	void Update_Ring()
	{
		if ( _action == Act_Wait )
		{
			//待機中はタイマーを更新しない
			return;
		}

		_createRingTimer -= Time.deltaTime;

		if ( _createRingTimer <= 0 )
		{
			var ring = Instantiate(RingPrefab);
			ring.transform.position = transform.position;
			ring.transform.rotation = transform.rotation;
			_createRingTimer = createRingSpan;
		}
	}
	void Update () 
	{
		// 毎フレーム状態を実行する
		if ( _action != null )
		{
			_action();
		}

		// リング生成のカウント
		Update_Ring();
	}
}
