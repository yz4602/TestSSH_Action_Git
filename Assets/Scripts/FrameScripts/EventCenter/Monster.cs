using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
	private void Start() 
	{
		Dead();	
	}
	
	// Start is called before the first frame update
	void Dead()
	{
		EventCenter.Instance.EventTrigger("MonsterDie", this);
	}
}
