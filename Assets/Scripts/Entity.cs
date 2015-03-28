using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Entity : MonoBehaviour {

	#region public variables
	public GameObject[] spaceShipsStandard;
	public GameObject[] spaceShipPower;

	public float rechargeTimeStandard;
	public float rechargeTimePower;
	public float speed;
	#endregion

	#region protected variables
	protected float currentRechargeTime;
	protected Transform myTransform;
	#endregion


	abstract public void SpawnSpaceShip();
	abstract public void MovementBattleShip();

	public enum SpaceShipType
	{
		NONE,
		STANDARD,
		POWER
	}

	// Use this for initialization
	protected virtual void Start () {

		if( rechargeTimeStandard == 0 ) rechargeTimeStandard = 1.5f;
		if( rechargeTimePower == 0 ) rechargeTimePower = 4f;
		if( speed == 0 ) speed = 10f;

	}
	
	// Update is called once per frame
	protected virtual void Update () 
	{
		MovementBattleShip();
		SpawnSpaceShip();
	}

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
			spaceShip = spaceShipsStandard[0];
			Instantiate ( spaceShip, shooterPos, spaceShip.transform.rotation );
			currentRechargeTime = 0;
			break;
		case SpaceShipType.POWER:
			spaceShip = spaceShipPower[0];
			Instantiate ( spaceShip, shooterPos, spaceShip.transform.rotation );
			currentRechargeTime = 0;
			break;
		}
		

	}

	/// <summary>
	/// Limits the movement of entities in the camera.
	/// </summary>
	protected virtual void limitMovement()
	{
		PolygonCollider2D collider = myTransform.GetComponent<PolygonCollider2D>();
		float cameraLimit = Camera.main.orthographicSize - collider.bounds.size.y;
		
		float limitY = Mathf.Clamp( transform.position.y, cameraLimit * -1.0f, cameraLimit );
		myTransform.position = new Vector2( transform.position.x, limitY );
	}

	/// <summary>
	/// Get the type of spaceship.
	/// </summary>
	/// <returns>The space ship type.</returns>
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

	protected void restartCooldown()
	{
		GameObject uicd = GameObject.FindGameObjectWithTag ("UICD");
		Animator auicd = uicd.GetComponent<Animator> ();
		uicd.SetActive(false);
		uicd.SetActive(true);
		auicd.Play ("ShipCD");
		GameObject uisd = GameObject.FindGameObjectWithTag ("UISD");
		Animator auisd = uisd.GetComponent<Animator> ();
		uisd.SetActive(false);
		uisd.SetActive(true);
		auisd.Play ("ShieldCD");

	}

}
