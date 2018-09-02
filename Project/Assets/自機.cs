using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 自機 : MonoBehaviour 
{
	public MoveControler controler;
	public Sequence		 sequence;

	void Awake()
	{
		sequence = GameObject.FindGameObjectWithTag("Sequence").GetComponent<Sequence>();

	}
	
	void Update () 
	{
		if ( !sequence.enablePlayer ){ return; }
		controler.Accell = Input.GetButton("Fire1");
		controler.Brake = Input.GetButton("Fire2");
		controler.Horizontal = Input.GetAxisRaw("Horizontal");
		controler.Vertical = Input.GetAxisRaw("Vertical");
	}
}
