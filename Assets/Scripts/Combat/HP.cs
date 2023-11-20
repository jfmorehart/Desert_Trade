using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
	public float hp_max;
	public float hp;

	public Transform backbar;
	public Transform hpbar;
	Renderer hren;

	bool owned;
	IDamageable owner;

	private void Awake()
	{
		hren = hpbar.GetComponent<Renderer>();
	}

	public void Damage(float dmg) {

		float newhp = hp - dmg;
		if (newhp > hp_max) newhp = hp_max;
		if(newhp <= 0) {
			newhp = 0;
			
			//Kill base
			if (owned) {
				owner.Kill();
			}
			Destroy(gameObject);

		}
		SetHP(newhp);
	}

	public void SetHP(float newhp) {
		float oldHP = hp;
		hp = newhp;
		StartCoroutine(HPLerp(oldHP)); // send with old to have lerp amt
    }


	IEnumerator HPLerp(float lerpHP) {
		//Lerps from starting HP to Current HP
		float stime = Time.time;

		float lerpLengthSeconds = 0.3f;
		float hpScale;
		float deltaHP = (hp - lerpHP) * (1 / lerpLengthSeconds);

		for (int i = 0; i < 500; i++)
		{
			hpScale = lerpHP / hp_max;
			lerpHP += deltaHP * Time.deltaTime;
		
			hpbar.localScale = new Vector3(hpScale, 1, 1);

			if(Time.time - stime > lerpLengthSeconds) {
				LerpExit();
				yield break;
			}
			yield return new WaitForEndOfFrame();
		
		}
		LerpExit();  
    }

	void LerpExit() { 
		//cleans up loose ends
		if(hp <= 0) {
			hren.enabled = false;
		}
    }

	public void SetOwner(IDamageable id) {
		owned = true;
		owner = id;
	}
}
