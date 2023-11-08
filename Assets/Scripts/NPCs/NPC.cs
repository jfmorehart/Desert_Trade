using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Humanoid
{
	// This class adds a bunch of useful behaviors
	// to the humanoid class for use with NPCs, like
	// detecting enemies and moving to positions

	[Header("NPC")]
	public float visionRadius;
	public float mov_speed;

    //[HideInInspector]
    public Vector3 dest;
    public float stopDist;

	float thinkDelay = 0.4f;
    float lastThink;


    protected override void Update() {
        base.Update();

		//distribute over frames
		lastThink = Random.Range(0, 1f);

        if(Time.time - lastThink > thinkDelay) {
            Think();
	    }
    }

    protected override void MovementUpdate()
    {
        base.MovementUpdate();
        if (!isMoving) return;

        Vector2 delta = dest - transform.position;
        if(delta.magnitude < stopDist) {
            StopMoving();
            return;
	    }

		rb.velocity = Vector2.ClampMagnitude(rb.velocity, mov_speed);
		rb.velocity += mov_speed * Time.deltaTime * delta.normalized;

		if (delta.x > 0)
		{
			if (!isFacingRight)
			{
				FaceDir(true);
			}
		}
		else
		{
			if (isFacingRight)
			{
				FaceDir(false);
			}
		}
	}

	public virtual void SetDestination(Vector3 target)
	{
		dest = target;
		StartMoving();
    }
	
	protected virtual void Think() {
		// Handles operations we only want done every 
		// once in a while, like circlecasts

		Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, visionRadius);
        foreach(Collider2D hi in hits) { 
	        if(hi.TryGetComponent(out Humanoid hu)){ 
	            if(hu.team != team) {
					// We need to figure out how to react
                    Encounter(hu);
		        }
	        }
	    }
	}

    protected virtual void Encounter(Humanoid hu) { 
        //  Inheritors will figure out to flee or chase or chill
    }
}
