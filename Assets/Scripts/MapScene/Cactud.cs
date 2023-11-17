using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cactud : MonoBehaviour, IDamageable
{
    public int hp = 2;

	public void Hit(int dmg, Vector2 thru, Team t) {
        hp -= dmg;
        if(hp < 1) {
            Kill();
	    }
    }

    public void Kill() {
        DropManager.ins.DropOnMe(4, DropManager.Drop.CactusChunk, transform.position);
        Destroy(gameObject);
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.gameObject.CompareTag("MapTown") || collision.gameObject.CompareTag("NoCactus")) {
            Destroy(gameObject);
	    }
	}
}
