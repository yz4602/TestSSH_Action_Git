using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMgr : BaseManager<InputMgr>
{
	private bool isStart = false;
	public InputMgr()
	{
		MonoManager.Instance.AddUpdateListener(MyUpdate);
	}
	
	public void StartOrEndCheck(bool isOpen)
	{
		isStart = isOpen;
	}
	
	private void MyUpdate()
	{
		if(!isStart)
			return;
		CheckKeyCode(KeyCode.W);
		CheckKeyCode(KeyCode.A);
		CheckKeyCode(KeyCode.S);
		CheckKeyCode(KeyCode.D);
	}
	
	private void CheckKeyCode(KeyCode key)
	{
		if(Input.GetKeyDown(key))
			EventCenter.Instance.EventTrigger("KeyDown", key);
		if(Input.GetKeyUp(key))
			EventCenter.Instance.EventTrigger("KeyUp", key);
	}
}
