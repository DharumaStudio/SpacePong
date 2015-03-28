using UnityEngine;
using System.Collections;

public class Meteor : MonoBehaviour
{
	public enum Edge
	{
		LEFT,
		RIGHT,
		TOP,
		BOTTOM
	} 

	// Use this for initialization
	private void Start ()
	{
		//Print meteors in the Start and End Screen randomize.
		Edge edge = (Edge)Random.Range( (int)Edge.LEFT, (int)Edge.BOTTOM );
		Vector2 force = Vector2.right;

		switch( edge ) {
		case Edge.TOP:
			force = Vector2.up;
			break;

		case Edge.LEFT:
			force = Vector2.right * -1.0f;
			break;

		case Edge.RIGHT:
			force = Vector2.right;
			break;

		case Edge.BOTTOM:
			force = Vector2.up * -1.0f;
			break;


		}

		this.transform.GetComponent<Rigidbody2D>().AddForce( force * 20.0f * Time.deltaTime, ForceMode2D.Impulse );
	
	}

	/// <summary>
	/// Raises the became invisible event.
	/// </summary>
	private void OnBecameInvisible ()
	{
		Destroy ( this.gameObject );
	}
}
