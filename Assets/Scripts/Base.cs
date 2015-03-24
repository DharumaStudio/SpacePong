using UnityEngine;
using System.Collections;

public class Base : MonoBehaviour {

	private void OnCollisionEnter2D( Collision2D other )
	{
		Destroy( this.gameObject );
	}
}
