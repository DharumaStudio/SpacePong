using UnityEngine;
using System.Collections;

public class IA : Entity {


	#region public variables
		private float timeToAct;
	#endregion

	#region private variables
		private float _elapsedTime;
		private bool _isMoving;
		private GameObject _closest;
		private int _targetY;
	#endregion

	/// <summary>
	/// Initialize parameters.
	/// </summary>
	protected override void Start ()
	{
		base.Start ();

		if( rechargeTimeStandard == 0 ) rechargeTimeStandard = 1f;
		if( rechargeTimePower == 0 ) rechargeTimePower = 2f;
		if( timeToAct == 0 ) timeToAct = 3f;

		myTransform = this.transform;

		_elapsedTime = 0f;
		_isMoving = false;
	}

	/// <summary>
	/// Spawns the space ship.
	/// </summary>
	public override void SpawnSpaceShip()
	{
		GameObject iaInstance = this.gameObject;
		currentRechargeTime += Time.deltaTime;
		_elapsedTime += Time.deltaTime;

		if (_isMoving) {
			float step = speed * Time.deltaTime;
			
			if ((iaInstance != null && _closest != null)) {
				
				Vector3 pos = iaInstance.transform.position;
				Vector3 towards = Vector3.MoveTowards (pos, _closest.transform.position, step);
				
				iaInstance.transform.position = new Vector3 (pos.x, towards.y, 0);

				if ( (_targetY == (int)iaInstance.transform.position.y )|| (_elapsedTime >= (timeToAct*1.5f))){
					_isMoving = false;
					fireShip ();
					_elapsedTime = 0f;
				}

			}
			else
			{
				_isMoving = false;
				_elapsedTime = 0f;
			}
		}
	}

	/// <summary>
	/// Movements the IA battle ship.
	/// </summary>
	public override void MovementBattleShip()
	{
		if (_elapsedTime >= timeToAct && !_isMoving) 
		{
			_closest = _getClosestObject (this.gameObject, TagAux.PLAYER_BULLET_TAG);
			if(_closest){
				_targetY = (int)_closest.transform.position.y;
				_isMoving = true;
			}
			_elapsedTime = 0f;
		}

		limitMovement();
	}

	/// <summary>
	/// Calculate the closest enemy spaceship
	/// </summary>
	/// <returns>The closest object.</returns>
	/// <param name="observer">Ia Object</param>
	/// <param name="tag">Tag to find the closest object</param>
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
			//Compares distances
			if( Vector3.Distance( observer.transform.position, obj.transform.position ) <= 
			   Vector3.Distance( observer.transform.position, closestObject.transform.position ) )
			{
				closestObject = obj;
			}

		}
		
		return closestObject;
	}
}