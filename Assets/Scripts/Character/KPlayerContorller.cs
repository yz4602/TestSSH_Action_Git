using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KPlayerContorller : MonoBehaviour
{
	public float standardSpeed;
	public float maxSpeed;
	private float currentSpeed;
	
	private Rigidbody playerRB;
	public float JumpForce;
	//private float currentSpeed;

	private float vertical;
	private float horizontal;
	private Vector3 moveDir;
	private Vector3 moveAmount;
	private Animator anim;

	void Awake()
	{
		playerRB = GetComponent<Rigidbody>();
		anim = GetComponent<Animator>();
	}

	void Update()
	{
		GetMoveInput();
		JumpKRigidbody();
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		MoveKRigidbody();
	}

	private void GetMoveInput()
	{
		vertical = Input.GetAxis("Vertical");
		horizontal = 0; //Input.GetAxis("Horizontal");
		
	}

	void MoveKRigidbody()
	{
		//Player move

		if(vertical != 0 || horizontal != 0)
		{
			moveDir = vertical * transform.forward.normalized;
			moveDir += horizontal * transform.right.normalized;
			if(Input.GetAxis("Accelerate") > 0 && vertical > 0)
			{
				Debug.Log(Input.GetAxis("Accelerate"));
				playerRB.MovePosition(playerRB.position + moveDir * maxSpeed * 0.1f);
				anim.SetFloat("BlendSpeed", vertical * standardSpeed + vertical * Input.GetAxis("Accelerate") * (maxSpeed - standardSpeed));
			}	
			else
			{
				playerRB.MovePosition(playerRB.position + moveDir * standardSpeed * 0.1f);
				anim.SetFloat("BlendSpeed", vertical * standardSpeed);
			}
			
			//If player's orientation does not have to equal camera's direction, delete following block 
			if(vertical != 0)
			{
				var faceRotation = Camera.main.transform.position - this.transform.position;
				faceRotation.x = -faceRotation.x;
				faceRotation.y = 0;
				faceRotation.z = -faceRotation.z;
				this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(faceRotation), 0.2f);
			}
		}
	}

	void JumpKRigidbody()
	{
		Debug.Log(anim.GetCurrentAnimatorStateInfo(0).IsName("Jump"));
		if (Input.GetButtonDown("Jump") && isOnGround() && !anim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
		{
			playerRB.AddForce(transform.up.normalized * JumpForce, ForceMode.Impulse);
			anim.SetTrigger("Jump");
		}
	}
	
	AnimationClip GetAnimationClip(string clipName)
	{
		foreach (AnimationClip clip in anim.runtimeAnimatorController.animationClips)
		{
			if (clip.name == clipName)
				return clip;
		}
		return null; // If not found
	}

	bool  isOnGround()
	{
		float height = Mathf.Infinity;
		//Ground Layer is 6
		var hits = Physics.RaycastAll(transform.position + new Vector3(0,0.5f,0), transform.up * -1, 0.7f, 1 << LayerMask.NameToLayer("Ground"));
		Debug.DrawRay(transform.position, transform.up * (-50f), Color.red);

		foreach(var hit in hits)
		{
			if (hit.distance < height)
				height = hit.distance;
		}

		if(height < 1.1f)
		{
			return true;
		}

		return false;
	}
}
