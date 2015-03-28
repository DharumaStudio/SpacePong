using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	#region public variables
	public enum Direction { LEFT, RIGHT }

	public float speed = 2.5f;
	public float frequency = 5.0f;  // Speed of sine movement
	public float magnitude = 2.5f;   // Size of sine movement

	public int life;

	public Direction direction = Direction.LEFT;
	#endregion

	#region private variables
	private Vector3 _axis;
	private Vector3 _pos;
	#endregion

	/// <summary>
	/// Gets the life.
	/// </summary>
	/// <returns>The life.</returns>
	public int getLife()
	{
		return life;
	}

	/// <summary>
	/// Deducts the life.
	/// </summary>
	/// <param name="deduct">Deduct.</param>
	public void deductLife( int deduct )
	{
		life -= deduct;
	}

	/// <summary>
	/// Set params by default.
	/// </summary>
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

	// Use this for initialization
	private void Start()
	{
		if( life == 0 ) life = 1;
	}

	// Update is called once per frame 
	private void FixedUpdate () {
		_pos += transform.up * Time.deltaTime * speed;
		transform.position = _pos + _axis * Mathf.Sin ( Time.time * frequency ) * magnitude;
	}

	/// <summary>
	/// Raises the became invisible event.
	/// </summary>
	private void OnBecameInvisible()
	{
		Destroy ( this.gameObject );
	}

	/// <summary>
	/// Raises the collision enter2 d event.
	/// </summary>
	/// <param name="other">Other.</param>
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

	// Update is called once per frame
	private void Update()
	{
		if( life <= 0 )
		{
			//Pending animation explosion. ;)
			Destroy ( this.gameObject );
		}

	}
}