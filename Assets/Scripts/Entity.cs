using UnityEngine;
using System.Collections;

public abstract class Entity : MonoBehaviour {


	public GameObject[] spaceShipsStandard;
	public GameObject[] spaceShipPower;

	public float rechargeTimeStandard;
	public float rechargeTimePower;
	public float speed;

	protected float currentRechargeTime;
	protected Transform myTransform;

	public enum SpaceShipType
	{
		NONE,
		STANDARD,
		POWER
	}

	// Use this for initialization
	protected virtual void Start () {

		if( rechargeTimeStandard == 0 ) rechargeTimeStandard = 5.0f;
		if( rechargeTimePower == 0 ) rechargeTimePower = 10.0f;
		if( speed == 0 ) speed = 10.0f;
	}
	
	// Update is called once per frame
	protected virtual void Update () 
	{
		MovementBattleShip();
		SpawnSpaceShip();
	}

	abstract public void SpawnSpaceShip();
	abstract public void MovementBattleShip();

	/// <summary>
	/// Fires a player bullet.
	/// </summary>
	protected virtual void fireShip()
	{
		Vector2 shooterPos = myTransform.position;
		GameObject spaceShip = null;
		
		SpaceShipType spaceShipType = _getSpaceShipType();
		
		switch( spaceShipType )
		{
		case SpaceShipType.STANDARD:
			spaceShip = spaceShipsStandard[Random.Range(0, spaceShipsStandard.Length)];
			break;
		case SpaceShipType.POWER:
			spaceShip = spaceShipPower[Random.Range(0, spaceShipPower.Length)];
			break;
			
		case SpaceShipType.NONE:
			Debug.LogError("Error fire ship");
			break;
		}
		
		Instantiate ( spaceShip, shooterPos, spaceShip.transform.rotation );
		
		currentRechargeTime = 0;
	}

	protected virtual void limitMovement()
	{
		PolygonCollider2D collider = myTransform.GetComponent<PolygonCollider2D>();
		float cameraLimit = Camera.main.orthographicSize - collider.bounds.size.y;
		
		float limitY = Mathf.Clamp( transform.position.y, cameraLimit * -1.0f, cameraLimit );
		myTransform.position = new Vector2( transform.position.x, limitY );
	}

	private SpaceShipType _getSpaceShipType()
	{
		SpaceShipType type = SpaceShipType.NONE;
		
		if( currentRechargeTime >= rechargeTimeStandard && currentRechargeTime < rechargeTimePower )
		{
			type = SpaceShipType.STANDARD;
		}
		else if( currentRechargeTime >= rechargeTimePower )
		{
			type = SpaceShipType.POWER;
		}
		
		return type;
	}

}
