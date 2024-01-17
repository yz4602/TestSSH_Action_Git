using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
	CharacterController cc;
	
	private void Awake() 
	{
		cc = GetComponent<CharacterController>();
	}
	
	void Update()
	{
		Move();      
	}
   
	void Move()
	{
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		Vector3 dir = new Vector3(-h, 0, -v);
		cc.SimpleMove(dir);
	}
}
