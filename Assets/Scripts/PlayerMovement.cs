using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[Header("Keybinds")]
	public KeyCode dash;
	public KeyCode block;

	[Header("General Movement")]
	public float mov_accel;
	public float mov_maxSpeed;
	public float mov_decay;
	Vector2 v; // velocity
	bool movement_locked;

	//Dashing (direct jump between positions)
	[Header("Dashing")]
	public float dash_dist;
	public float dash_cooldown;
	float dash_last; //used for timing

	[Header("Sprinting")]
	//Sprinting (increased movespeed post dash)
	public float sprint_duration;
	public float mov_sprintMax;
	public float mov_accel_sprint;
	float sprint_last; //used for timing
	bool sprinting;


	private void Update()
	{
		MovementUpdate();
    }

	void MovementUpdate()
	{
		if (movement_locked) return; // for cutscenes etc

		Vector2 mvm;

		//Get player wasd input
		mvm.x = Input.GetAxisRaw("Horizontal");
		mvm.y = Input.GetAxisRaw("Vertical");

		//Accelerate player in direction of wasd
		v += (sprinting ? mov_accel_sprint : mov_accel) * Time.time * mvm.normalized;

		//Update velocity
		v = Vector2.ClampMagnitude(v, sprinting ? mov_sprintMax : mov_maxSpeed);
		v *= 1 - (Time.deltaTime * mov_decay);

		//Actually move player
		transform.Translate(v * Time.deltaTime, Space.World);

		//Dashing Logic
		if (Input.GetKeyDown(dash))
		{
			Dash(mvm);
		}
	}

	//Jumps forwards
	void Dash(Vector2 mvm) {

		if(Time.time - dash_last > dash_cooldown) {
			dash_last = Time.time;
			sprinting = true;
			transform.Translate(mvm.normalized * dash_dist);
			Invoke(nameof(SprintOff), sprint_duration);
		}	
    }

	//Invoked after dash to end increased mobility
	void SprintOff()
	{
		sprinting = false;
	}
}

