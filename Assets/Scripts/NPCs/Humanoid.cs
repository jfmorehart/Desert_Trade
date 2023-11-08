using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Humanoid : MonoBehaviour, IDamageable
{
	[Header("Humanoid")]
	public Team team;
    public HP hp;

	protected Rigidbody2D rb;
	public bool isFacingRight = true;
    public bool isMoving;
	public bool movement_locked;



	protected virtual void Awake()
	{
		hp.SetOwner(this as IDamageable);
		rb = GetComponent<Rigidbody2D>();
	}

	protected virtual void Update()
	{
		if (!movement_locked){
			MovementUpdate();
		}
	}

	protected virtual void MovementUpdate() { 
		
    }

	//Will be used for swapping animations across humanoids
	public void FaceDir(bool isRight) {
        isFacingRight = isRight;
        if (isRight) {
            transform.localScale = new Vector3(1, 1, 1);
	    }
        else {
			transform.localScale = new Vector3(-1, 1, 1);
		}
    }

	//In case animation needs these functions later
	public virtual void StartMoving() {
		isMoving = true;
    }
	public virtual void StopMoving() {
		isMoving = false;
    }

    public virtual void Hit(int dmg, Vector2 thru, Team responsible) {
		hp.Damage(dmg);
		rb.AddForce(5000 * dmg * thru);
	}
	public virtual void Kill()
	{
		Destroy(gameObject);
	}
}
