using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 自機 : MonoBehaviour 
{
	public MoveControler controler;
	
	void Update () 
	{
		controler.Accell = Input.GetButton("Fire1");
		controler.Horizontal = Input.GetAxisRaw("Horizontal");
		controler.Vertical = Input.GetAxisRaw("Vertical");
	}
}
