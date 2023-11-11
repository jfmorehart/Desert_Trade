using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : Humanoid
{
	[Header("Keybinds")]
	public KeyCode dash;

	[Header("General Movement")]
	public float mov_accel;
	public float mov_maxSpeed;
	public float mov_decay;

	//Dashing (direct jump between positions)
	[Header("Dashing")]
	public float dash_dist;
	public float dash_cooldown;
	float dash_last; //used for timing

	//Sprinting (increased movespeed post dash)
	[Header("Sprinting")]
	public float sprint_duration;
	public float mov_sprintMax;
	public float mov_accel_sprint;
	bool sprinting;


	[Header("Enter Town")]
	public KeyCode enterKey;
	Transform icontrigger;



    protected override void Awake()
	{

		base.Awake();

		if (ScenesStatic.OnMap()) {
			GameObject[] spawns = GameObject.FindGameObjectsWithTag("MapTown");
			foreach(GameObject spawn in spawns) { 
				if(spawn.GetComponent<IconTrigger>().myTown == PlayerStatic.lastVisited) {
					Debug.Log("found");
					transform.position = spawn.transform.position;
					break;
				}
			}
		}
	}

	protected override void MovementUpdate()
	{
		Vector2 mvm;

		//Get player wasd input
		mvm.x = Input.GetAxisRaw("Horizontal");
		mvm.y = Input.GetAxisRaw("Vertical");

		if(mvm.magnitude > 0.1) {
			StartMoving();
		}
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

		//Town Trigger Logic
		if (Input.GetKeyDown(enterKey) && icontrigger != null)
		{
			icontrigger.GetComponent<IconTrigger>().Load();
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

		if (Mathf.Abs(mx) < 0.1f)
		{
			StopMoving();
			return;
		}
		if(mx > 0) {
			if (!isFacingRight) {
				FaceDir(true);
			}
		}
		else 
		{
			if (isFacingRight) {
				FaceDir(false);
			}
		}
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("MapTown")){
			icontrigger = collision.transform;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("MapTown"))
		{
			icontrigger = null;
		}
	}


	public override void Kill()
	{
		PlayerStatic.PlayerDeath();
	}
}

