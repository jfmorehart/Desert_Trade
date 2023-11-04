using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
	public void Hit(int dmg, Vector2 through, Team responsible); // through points in the knockback dir
	public void Kill();
}
