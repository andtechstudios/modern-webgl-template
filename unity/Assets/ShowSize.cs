using TMPro;
using UnityEngine;


public class ShowSize : MonoBehaviour
{
	[SerializeField]
	TMP_Text text;

	void FixedUpdate()
	{
		text.text = $"Size: ({Screen.width}, {Screen.height})";
	}
}
