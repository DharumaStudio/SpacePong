using UnityEngine;
using System.Collections;

public class BoardManager : MonoBehaviour {


	public GameObject iaBase;
	public GameObject playerBase;

	public GameObject player;
	public GameObject ia;

	public GameObject playerBullet;
	public GameObject iaBullet;

	public int iaLife;
	public int playerLife;

	private const float BASE_SIZE = 0.91f;

	// Use this for initialization
	private void Start () 
	{
		if( iaLife == 0 ) iaLife = 8;
		if( playerLife == 0 ) playerLife = 8;

		_calculateAndDrawBases();
		_renderPlayers();
	}
	
	// Update is called once per frame
	private void Update () 
	{
		// extract in another scripts!!!!!! just testing!!!!
		if( Input.GetKeyDown( "space" ) )
		{
			_fireShip( GameObject.FindGameObjectWithTag( "Player" ), playerBullet );
		}

		GameObject iaInstance = GameObject.FindGameObjectWithTag( "IA" );

		GameObject closest = _getClosestObject( iaInstance, "PlayerBullet" );

		float step = 50f * Time.deltaTime;

		if ( iaInstance != null && closest != null )
		{
			Vector3 pos = iaInstance.transform.position;
			Vector3 towards = Vector3.MoveTowards( pos, closest.transform.position, step);

			iaInstance.transform.position = new Vector3( pos.x, towards.y, 0 );
		}
	}

	/// <summary>
	/// Calculate and draw the bases.
	/// </summary>
	private void _calculateAndDrawBases()
	{
		Camera camera = Camera.main;
		
		Transform basePlayerTransform = GameObject.Find("BasePlayer").transform;
		Transform baseIaTransform = GameObject.Find("BaseIA").transform;

		for( int i = 0; i < playerLife; i++ )
		{
			Vector2 spanwPosition = camera.ViewportToWorldPoint( new Vector2( 0.05f, ( i * ( BASE_SIZE / playerLife ) + 0.10f ) ) );

			GameObject instantiate = Instantiate( playerBase, spanwPosition, Quaternion.identity ) as GameObject;
			GameObject iaInstantiate = Instantiate( iaBase, spanwPosition * -1.0f, Quaternion.identity ) as GameObject;

			instantiate.transform.SetParent( basePlayerTransform );
			iaInstantiate.transform.SetParent( baseIaTransform );
		}
	}

	/// <summary>
	/// Render the players in the scene.
	/// </summary>
	private void _renderPlayers()
	{
		Camera camera = Camera.main;

		Transform playersTranform = GameObject.Find("Players").transform;

		Vector2 spawnPosition = camera.ViewportToWorldPoint( new Vector2( 0.15f, 0.5f ) );
		GameObject playerInstantiate = Instantiate( player, spawnPosition, player.transform.rotation ) as GameObject;
		playerInstantiate.tag = "Player";
		GameObject iaInstantiate = Instantiate( ia, spawnPosition * -1.0f, ia.transform.rotation ) as GameObject;
		iaInstantiate.tag = "IA";

		playerInstantiate.transform.SetParent( playersTranform );
		iaInstantiate.transform.SetParent( playersTranform );
	}

	/// <summary>
	/// Fires a player bullet.
	/// </summary>
	private void _fireShip( GameObject shooter, GameObject bullet )
	{
		Vector3 shooterPos = shooter.transform.position;

		Instantiate ( bullet, shooterPos, bullet.transform.rotation );
	}

	private GameObject _getClosestObject( GameObject observer, string tag )
	{
		GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag( tag );
		GameObject closestObject = null;
		foreach( var obj in objectsWithTag )
		{
			if( !closestObject )
 			{
				closestObject = obj;
			}
			//compares distances
			if( Vector3.Distance( observer.transform.position, obj.transform.position ) <= 
			    Vector3.Distance( observer.transform.position, closestObject.transform.position ) )
			{
				closestObject = obj;
			}
		}

		return closestObject;
	}
}
