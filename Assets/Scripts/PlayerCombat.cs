using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
	[Header("Throwing")]
	public KeyCode throwKey; // rebindable
	public GameObject throwable; // todo replace with better system
	public float throwReload; // time between throws
	float lastThrowTime; // for use in timers

	PlayerMovement pmov;

	private void Awake()
	{
		pmov = GetComponent<PlayerMovement>();
	}

	private void Update()
	{
		if (Input.GetKeyDown(throwKey)) {
			TryThrow();
		}
	}


	void TryThrow() {

		if (Time.time - lastThrowTime > throwReload)
		{
			lastThrowTime = Time.time;
		}
		else return;

		Vector3 throwOff = pmov.isFacingRight? Vector3.right : -Vector3.right;
		GameObject g = Instantiate(throwable,
		transform.position + throwOff,
		Quaternion.identity);

		g.GetComponent<Throwable>().Throw(throwOff);
	
    }
}
