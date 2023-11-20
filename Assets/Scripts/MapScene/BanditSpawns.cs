using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditSpawns : MonoBehaviour
{
	public static BanditSpawns ins;
	public GameObject banditPrefab;

	public int maxBandits;
	public int minBanditsPerSpot;
	public int maxBanditsPerSpot;

	private void Awake()
	{
		ins = this;

		Transform[] kiddos = GetAllChildren();
		System.Array.Sort(RandomArr(kiddos.Length), kiddos);
		maxBandits = Mathf.Min(kiddos.Length, maxBandits);
		int b = 0;
		for(int i =0; i < maxBandits; i++) {
			kiddos[i].GetComponent<BSpawn>().enabled = false;
			b++;
			if(b >= maxBandits) {
				break;
			}
		}
	}

	public void SpawnMe(Transform tsp) {
		int n = Random.Range(minBanditsPerSpot, maxBanditsPerSpot + 1);
		for (int i = 0; i < n; i++)
		{
			Vector3 off = Random.insideUnitCircle * 2;
			Instantiate(banditPrefab, tsp.position + off, Quaternion.identity);
		}
	}


	int[] RandomArr(int len) {
		int[] ran = new int[len];
		for(int i = 0; i < len; i++) {
			ran[i] = Random.Range(0, len * len);
		}
		return ran;
    }

	Transform[] GetAllChildren() {
		Transform[] kiddos = new Transform[transform.childCount];
		for(int i = 0; i < kiddos.Length; i++) {
			kiddos[i] = transform.GetChild(i);
		}
		return kiddos;
    }

}
