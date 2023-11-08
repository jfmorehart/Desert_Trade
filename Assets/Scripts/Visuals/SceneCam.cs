using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneCam : MonoBehaviour
{
	public Transform target;
	public float followDist;

	public float accel;
	public float drag;

	[HideInInspector]
	public Vector2 velocity;

	public ComputeShader Rasterize;
	public float ras_scale;

	public bool followY;
	public float stationaryDragMult = 1;

	public bool lockTo;

	private void Update()
	{
		if (target == null) return;

		Vector2 delta = target.position - transform.position;


		if (lockTo) {
			transform.position = new Vector3(target.position.x, target.position.y, -10);
		}

		if (!followY) {
			delta.y = 0;
		}

		if(delta.magnitude > followDist) { 
			velocity += accel * Time.deltaTime * delta.normalized;
		}
		else {
			velocity *= 0.98f - Time.deltaTime * drag * stationaryDragMult;
		}
		velocity *= 1 - Time.deltaTime * drag;

		transform.Translate(velocity * Time.deltaTime);
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		Debug.Log("rendering image");
		destination = source;
		destination.enableRandomWrite = true;
		Rasterize.SetFloat("combine", ras_scale);
		Rasterize.SetFloat("invcombine", 1 / ras_scale);
		Rasterize.SetTexture(0, "Result", destination);
		Rasterize.Dispatch(0, Screen.width / 32, Screen.height / 32, 1);
	}

}
