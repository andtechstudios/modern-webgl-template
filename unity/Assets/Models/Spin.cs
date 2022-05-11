using UnityEngine;


public class Spin : MonoBehaviour
{
	public Transform yawAnchor;
	public Transform pitchAnchor;
	public Vector3 angularVelocity;

	public float maxPitch = 30.0f;
	public float speed = 1.0f;

	void Update()
	{
		var pitch = Mathf.Sin(Time.time * 2.0f * Mathf.PI * speed) * maxPitch;
		pitchAnchor.localEulerAngles = new Vector3(pitch, 0.0f, 0.0f);
		yawAnchor.Rotate(0.0f, angularVelocity.y * Time.deltaTime, 0.0f);
	}
}
