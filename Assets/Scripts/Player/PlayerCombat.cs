using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
	[Header("Melee")]
	public KeyCode meleeKey; // rebindable
	public Melee weapon;

	[Header("Throwing")]
	public Transform throwPoint; // place where the projectile should spawn
	public KeyCode throwKey; // rebindable
	public GameObject throwable; // todo replace with better system
	public float throwReload; // time between throws
	public float throwDelay; // for timing with animation
	float lastThrowTime; // for use in timers

	PlayerMovement pmov;

	public Animator anim;

	bool canthrow = true;
	bool canswing = true;

	private void Awake()
	{
		pmov = GetComponent<PlayerMovement>();
	}

	private void Update()
	{
		if (Input.GetKeyDown(throwKey) && canswing) {
			TryThrow();
		}

		if (Input.GetKeyDown(meleeKey) && canthrow)
		{
			weapon.TryStab();
			if (anim != null)
			{
				canswing = false;
				Invoke(nameof(EndStab), weapon.attackDuration + weapon.killDelay);
				anim.SetBool("swinging", true);
			}
		}
	}
	void EndThrow() {
		if (anim != null)
		{
			anim.SetBool("throwing", false);
			canthrow = true;
		}
	}

	public void EndStab() {
		if (anim != null)
		{
			anim.SetBool("swinging", false);
			canswing = true;
		}
	}


	void TryThrow() {

		if (Time.time - lastThrowTime > throwReload + throwDelay)
		{
			if (anim != null)
			{	
				anim.SetBool("throwing", true);
			}

			lastThrowTime = Time.time;
			Invoke(nameof(Chuck), throwDelay);
			Invoke(nameof(EndThrow), throwReload);
			canthrow = false;
		}
		else return;

    }


	void Chuck()
	{

		//Figure out direction
		int dir = pmov.isFacingRight ? 1 : -1;

		Vector2 mvm;

		//Get player wasd input
		mvm.x = Input.GetAxisRaw("Horizontal");
		mvm.y = Input.GetAxisRaw("Vertical");

		// Flip throw vector as required
		Vector3 throwOff = new Vector2(throwPoint.localPosition.x * dir, throwPoint.localPosition.y);

		//Instantiate throwable
		GameObject g = Instantiate(throwable,
		transform.position + throwOff,
		Quaternion.identity);

		if (mvm.magnitude < 0.01f)
		{
			mvm = Vector3.right * dir;
		}

		// Tell the sword which way to go
		g.GetComponent<Throwable>().Throw(mvm.normalized, pmov.team);

	}
}
