using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Humanoid : MonoBehaviour, IDamageable
{
	[Header("HP")]
	public HP hp;

	[Header("AnimStuff")]
	public bool isFacingRight = true;
    public bool isMoving;
	public bool movement_locked;

	protected virtual void Awake()
	{
		hp.SetOwner(this as IDamageable);
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

    public virtual void Hit(int dmg, Vector2 thru) {
		hp.Damage(dmg);
	}
	public virtual void Kill()
	{
		Destroy(gameObject);
	}
}
