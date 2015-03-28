using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public enum Direction { LEFT, RIGHT }

	public float speed = 2.5f;
	public float frequency = 5.0f;  // Speed of sine movement
	public float magnitude = 2.5f;   // Size of sine movement

	public int life;

	public Direction direction = Direction.LEFT;

	private Vector3 _axis;
	private Vector3 _pos;


	public int getLife()
	{
		return life;
	}

	public void deductLife( int deduct )
	{
		life -= deduct;
	}
	
	private void Awake () {
		_pos = transform.position;

		if ( direction == Direction.LEFT )
		{
			_axis = transform.right;
			_pos.x -= 1.5f;
		}
		else
		{
			_axis = transform.right * -1;
			_pos.x += 1.5f;
		}
	}

	private void Start()
	{
		if( life == 0 ) life = 1;
	}
	
	private void FixedUpdate () {
		_pos += transform.up * Time.deltaTime * speed;
		transform.position = _pos + _axis * Mathf.Sin ( Time.time * frequency ) * magnitude;
	}

	private void OnBecameInvisible()
	{
		Destroy ( this.gameObject );
	}

	private void OnCollisionEnter2D( Collision2D other )
	{
		int layer = other.gameObject.layer;
		int currentLife = life;

		//Only the enemy bullets has the Mask.
		if( layer == LayerMask.NameToLayer( LayerAux.IA_SPACESHIP_LAYER ) )
		{
			Bullet colliderBullet = other.transform.GetComponent<Bullet>();
			int otherLife = colliderBullet.getLife();

			if( life == otherLife )
			{
				Destroy( this.gameObject );
				Destroy( other.gameObject );
			}
			else
			{
				life -= colliderBullet.getLife();
				colliderBullet.deductLife( currentLife );
			}
		}
		else
		{
			string tag = other.transform.tag;
			if( tag == TagAux.BASE_TAG || tag == TagAux.PLAYER_TAG  || 
			    tag == TagAux.PLAYER_IA_TAG )
			{
				life = 0;
			}
			else if( tag == TagAux.PLAYER_SHIELD_TAG ){
				life = 1;
				Destroy ( other.gameObject );
			}
		}
	}

	private void Update()
	{
		if( life <= 0 )
		{
			//Pending animation explosion. ;)
			Destroy ( this.gameObject );
		}

	}
}