using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatUtils : MonoBehaviour
{
	public static CombatUtils ins; // short for instance
	public PlayerMovement player;
	public bool playerisEvil;

	private void Awake()
	{
		ins = this;
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
	}

}
public enum Team
{
	Neutral,
	Player,
	Bandit,
	Villager
}
