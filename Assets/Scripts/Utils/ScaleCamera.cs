using UnityEngine;
using System.Collections;

public class ScaleCamera : MonoBehaviour
{
	#region Public properties
	public float ortographicSize = 5;
	public float aspect = 1.33333f;
	#endregion

	private void Start()
	{
		Camera camera = Camera.main;
		Camera.main.projectionMatrix = Matrix4x4.Ortho(
			-ortographicSize * aspect,
			ortographicSize * aspect,
			-ortographicSize, 
			ortographicSize,
			camera.nearClipPlane, camera.farClipPlane
			);

	}
}
