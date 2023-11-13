using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
	Collider2D col;
	Renderer ren;
	Team team;


	//Maybe should be replaced with struct system
	[Header("Stats")]
	public int damage;
	public float attackDuration;
	public float attackCooldown;
	public float killDelay;
	float lastAttack; // for timer

	bool ownedByPlayer;

	private void Awake()
	{
		//Must be child of a humanoid
		team = transform.root.GetComponent<Humanoid>().team;

		col = GetComponent<Collider2D>();
		ren = GetComponent<Renderer>();
		ren.enabled = false;
		col.enabled = false;
	}

	//What we call when we want to attack
	public bool TryStab() { 

		if(Time.time - lastAttack > attackCooldown) {
			Invoke(nameof(Stab), killDelay);
			lastAttack = Time.time;
			return true;
		}
		return false;
    }

	//What executes the attack
	void Stab()
	{
		ren.enabled = false;
		col.enabled = true;

		Invoke(nameof(HideKillBox), attackDuration);
	}

	//What ends the attack
	void HideKillBox()
	{
		ren.enabled = false;
		col.enabled = false;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//Is it our friend?
		if (collision.TryGetComponent(out Humanoid h))
		{
			if (h.team == team) return;
		}

		//Nope, try to kill it
		IDamageable mbs = collision.gameObject.GetComponent<IDamageable>();
		if (mbs is IDamageable idintr)
		{
			idintr.Hit(damage, collision.transform.position - transform.position, team);
		}
	}
}
