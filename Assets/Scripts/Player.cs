using UnityEngine;
using System.Collections;

public class Player : Entity {

	/// <summary>
	/// Start this instance.
	/// </summary>
	protected override void Start ()
	{
		base.Start ();
		myTransform = this.transform;
	}

	/// <summary>
	/// Spawns the space ship.
	/// </summary>
	public override void SpawnSpaceShip()
	{
		currentRechargeTime += Time.deltaTime;

		#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
		if( Input.GetKeyDown( "space" ) && currentRechargeTime >= rechargeTimeStandard )
		{
			fireShip();
		}
		#endif
	}

	/// <summary>
	/// Movements the battle ship.
	/// </summary>
	public override void MovementBattleShip()
	{
		float vertical = 0.0f;

		#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
		vertical = Input.GetAxis("Vertical");
		myTransform.Translate( vertical * Time.deltaTime * speed, 0, myTransform.position.z );

		limitMovement();
		#endif
	}

}
