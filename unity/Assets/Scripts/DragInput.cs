using UnityEngine;

public class DragInput : MonoBehaviour
{
	[SerializeField]
	private Transform target;
	[SerializeField]
	private Vector3 restingAngularVelocity;
	[SerializeField]
	private float degreePerCm = 90.0f;

	private Vector3 targetEulerAngles;
	private Vector3 currentEulerAngles;
	private Vector3 itpVelocity;
	private float itpSmoothTime = 0.3f;
	private Vector3 lastMousePosition;
	private bool requireReset = true;

	private const float CmToInch = 0.393701f;

	void Update()
	{
		if (Input.GetMouseButton(0))
		{
			if (requireReset)
			{
				lastMousePosition = Input.mousePosition;

				requireReset = false;
			}

			var delta = Input.mousePosition - lastMousePosition;
			var offset = delta * (1.0f / Screen.dpi) * CmToInch * degreePerCm;

			targetEulerAngles.x += -offset.y;
			targetEulerAngles.y += offset.x;
		}
		else
		{
			targetEulerAngles += restingAngularVelocity * Time.deltaTime;
			requireReset = true;
		}
		targetEulerAngles.x = Mathf.Clamp(targetEulerAngles.x, 0.0f, 80.0f);

		// Interpolate
		currentEulerAngles.x = Mathf.SmoothDamp(currentEulerAngles.x, targetEulerAngles.x, ref itpVelocity.x, itpSmoothTime);
		currentEulerAngles.y = Mathf.SmoothDamp(currentEulerAngles.y, targetEulerAngles.y, ref itpVelocity.y, itpSmoothTime);

		// Apply
		target.eulerAngles = currentEulerAngles;

		// Finalize
		lastMousePosition = Input.mousePosition;
	}
}
