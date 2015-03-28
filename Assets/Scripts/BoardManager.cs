using UnityEngine;
using System.Collections;

public class BoardManager : MonoBehaviour {


	public GameObject iaBase;
	public GameObject playerBase;

	public GameObject player;
	public GameObject ia;

	public int iaLife;
	public int playerLife;

	private Transform _basePlayerTransform;
	private Transform _baseIaTransform;

	private const float BASE_SIZE = 0.91f;


	public int GetIaLife()
	{
		return iaLife;
	}

	public int GetPlayerLife()
	{
		return playerLife;
	}

	// Use this for initialization
	private void Start () 
	{
		if( iaLife == 0 ) iaLife = 8;
		if( playerLife == 0 ) playerLife = 8;

		_basePlayerTransform = GameObject.Find("BasePlayer").transform;
		_baseIaTransform = GameObject.Find("BaseIA").transform;

		_calculateAndDrawBases();
		_renderPlayers();
	}
	
	// Update is called once per frame
	private void Update () 
	{
		iaLife = _baseIaTransform.childCount;
		playerLife = _basePlayerTransform.childCount;
	}

	/// <summary>
	/// Calculate and draw the bases.
	/// </summary>
	private void _calculateAndDrawBases()
	{
		Camera camera = Camera.main;

		for( int i = 0; i < playerLife; i++ )
		{
			Vector2 spanwPosition = camera.ViewportToWorldPoint( new Vector2( 0.05f, ( i * ( BASE_SIZE / playerLife ) + 0.10f ) ) );

			GameObject instantiate = Instantiate( playerBase, spanwPosition, Quaternion.identity ) as GameObject;
			GameObject iaInstantiate = Instantiate( iaBase, spanwPosition * -1.0f, Quaternion.identity ) as GameObject;
			instantiate.tag = "Base";
			iaInstantiate.tag = "Base";
			instantiate.transform.SetParent( _basePlayerTransform );
			iaInstantiate.transform.SetParent( _baseIaTransform );
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
		GameObject iaInstantiate = Instantiate( ia, spawnPosition * -1.0f, ia.transform.rotation ) as GameObject;

		playerInstantiate.transform.SetParent( playersTranform );
		iaInstantiate.transform.SetParent( playersTranform );
	}
}
