using UnityEngine;
using System.Collections;

public class IA : Entity {

	private float _elapsedTime;
	private float _timeToAct;
	private bool _isMoving;
	private GameObject _closest;
	private int _targetY;
	protected override void Start ()
	{
		base.Start ();
		if( rechargeTimeStandard == 0 ) rechargeTimeStandard = 1f;
		if( rechargeTimePower == 0 ) rechargeTimePower = 2f;
		myTransform = this.transform;
		_elapsedTime = 0f;
		_timeToAct = 1f;
		_isMoving = false;
	}

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

				if ( (_targetY == (int)iaInstance.transform.position.y )|| (_elapsedTime >= (_timeToAct*1.5f))){
					_isMoving = false;
					fireShip ();
					_elapsedTime = 0f;
				}
				Debug.Log ("===============");
			}
		}else if (_elapsedTime >= _timeToAct) {
			Debug.Log ("+++++++++++++++");
			_closest = _getClosestObject (this.gameObject, "PlayerBullet");
			if(_closest){
				_targetY = (int)_closest.transform.position.y;
				_isMoving = true;
			}
			_elapsedTime = 0f;
		}
		Debug.Log (_elapsedTime);
	}

	public override void MovementBattleShip()
	{
		limitMovement();
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