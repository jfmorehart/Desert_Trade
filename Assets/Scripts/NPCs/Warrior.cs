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

		SetDestination(target.transform.position);
		if(Vector2.Distance(target.transform.position, transform.position) < meleeDist) {
			weapon.TryStab();
		}
	}

	public void Attack(Humanoid newtarget) {
		attacking = true;
		target = newtarget; 
		SetDestination(newtarget.transform.position);
    }
}
