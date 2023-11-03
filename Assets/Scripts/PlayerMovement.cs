using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[Header("Keybinds")]
	public KeyCode dash;

	[Header("General Movement")]
	public float mov_accel;
	public float mov_maxSpeed;
	public float mov_decay;
	Rigidbody2D rb;
	bool movement_locked;
	public bool isFacingRight;


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

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

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

		DirectionalLogic(mvm.x);

		//Accelerate player in direction of wasd
		rb.velocity+= (sprinting ? mov_accel_sprint : mov_accel) * Time.time * mvm.normalized;

		//Update velocity
		rb.velocity = Vector2.ClampMagnitude(rb.velocity, sprinting ? mov_sprintMax : mov_maxSpeed);
		rb.velocity*= 1 - (Time.deltaTime * mov_decay);

		//Actually move player
		transform.Translate(rb.velocity* Time.deltaTime, Space.World);

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

	void DirectionalLogic(float mx) {

		//Handle direction stuff

		if (Mathf.Abs(mx) < 0.1f) return;

		if(mx > 0) {
			if (!isFacingRight) {
				isFacingRight = true;
			}
		}
		else 
		{
			if (isFacingRight) {
				isFacingRight = false;
			}
		}
    }
}

