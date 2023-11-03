using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatUtils : MonoBehaviour
{
	public static CombatUtils ins; // short for instance

	[HideInInspector]
	public PlayerMovement player;

	private void Awake()
	{
		ins = this;
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
	}


	/// <summary>
	/// This group of functions iteratively solves the target leading problem
	/// fuck calculus, we use the for loop
	/// </summary>
	/// <param name="iterations"> number of times to bracket the target </param>
	/// <param name="spos"> This is the inital position of the seeker</param>
	/// <param name="speed"> speed of seeker </param>
	/// <param name="tpos"> This is the initial position of the target </param>
	/// <param name="tvel"> This is the current velocity of the target </param>
	/// <returns> Returns bracketed target position </returns>
	public Vector3 LeadTarget(int iterations, Vector3 spos, float speed, Vector3 tpos, Vector2 tvel)
	{
		Vector2 pos = tpos;
		float time = Intercept_Time(spos, pos, speed);

		for (int i = 0; i < iterations; i++)
		{
			pos = Intercept_Pos(time, pos, tvel);
			time = Intercept_Time(spos, pos, speed);
		}

		return pos;
	}

	// Calculates intercept position given a time to target
	// uses initial tpos
	Vector2 Intercept_Pos(float timeToHit, Vector2 tpos, Vector2 tvel)
	{
		return (tpos + tvel * timeToHit);
	}

	//Calculates time it takes to get to a given position
	//uses predicted tpos
	float Intercept_Time(Vector2 spos, Vector2 tpos, float speed)
	{
		return Vector2.Distance(spos, tpos) / speed;

	}
}
