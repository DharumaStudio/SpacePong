using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public enum Direction { LEFT, RIGHT }

	public float speed = 2.5f;
	public float frequency = 5.0f;  // Speed of sine movement
	public float magnitude = 2.5f;   // Size of sine movement

	public Direction direction = Direction.LEFT;

	private Vector3 _axis;
	private Vector3 _pos;
	
	void Awake () {
		_pos = transform.position;

		if ( direction == Direction.LEFT )
		{
			_axis = transform.right; 
		}
		else
		{
			_axis = transform.right * -1;
		}
	}
	
	void FixedUpdate () {
		_pos += transform.up * Time.deltaTime * speed;
		transform.position = _pos + _axis * Mathf.Sin ( Time.time * frequency ) * magnitude;
	}
}