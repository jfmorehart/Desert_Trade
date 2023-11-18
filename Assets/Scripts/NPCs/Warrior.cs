using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : NPC
{
	//This class expands the NPC class to 
	// include support for using weapons and chasing enemies

	[Header("Warrior")]
	public bool attacking;
	public Humanoid target;

	public float meleeDist;
	public Melee weapon;
	
	Vector2 start;
	float followDist = 12;
	protected override void Awake()
	{
		base.Awake();
		start = transform.position;
	}
	protected override void MovementUpdate()
	{
		base.MovementUpdate();

		if (!attacking) {
			return;
		}
		if(target == null) {
			attacking = false;
			return;
		}
		Vector2 dir = start - (Vector2)transform.position;
		Vector2 tdir = start - (Vector2)target.transform.position;
		if (dir.magnitude < followDist && tdir.magnitude < followDist)
		{
			SetDestination(target.transform.position);
			if (Vector2.Distance(target.transform.position, transform.position) < meleeDist)
			{
				weapon.TryStab();
				anim.SetBool("swinging", true);
				Invoke(nameof(EndStab), weapon.attackDuration + weapon.killDelay);
			}
		}
		else if(dir.magnitude > 2) {
			SetDestination(start);
		}
	}

	public void EndStab()
	{
		if (anim != null)
		{
			anim.SetBool("swinging", false);
		}
	}


	public void Attack(Humanoid newtarget) {
		attacking = true;
		target = newtarget; 
		SetDestination(newtarget.transform.position);
    }
}
