using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    public float spinSpeed;
    public float throwVel;

    Rigidbody2D rb;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}
	public void Throw(Vector2 dir) {
		rb.velocity = throwVel * dir;
		rb.angularVelocity = spinSpeed;
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
		// dumb, probably slow might just switch to an inheritance structure
		MonoBehaviour[] mbs = collision.gameObject.GetComponents<MonoBehaviour>();
		foreach (MonoBehaviour m in mbs)
		{
			if (m is IDamageable idintr)
			{
				idintr.Hit(3, collision.transform.position - transform.position);
				break;
			}
		}

		Destroy(gameObject);
	}
}
