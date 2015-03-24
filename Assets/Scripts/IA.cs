using UnityEngine;
using System.Collections;

public class IA : Entity {

	protected override void Start ()
	{
		base.Start ();
		myTransform = this.transform;
	}

	public override void SpawnSpaceShip()
	{
		//IA intelligence!
	}

	public override void MovementBattleShip()
	{
		GameObject iaInstance = this.gameObject;
		
		GameObject closest = _getClosestObject( iaInstance, "PlayerBullet" );
		
		float step = 50f * Time.deltaTime;
		
		if ( iaInstance != null && closest != null )
		{
			Vector3 pos = iaInstance.transform.position;
			Vector3 towards = Vector3.MoveTowards( pos, closest.transform.position, step);
			
			iaInstance.transform.position = new Vector3( pos.x, towards.y, 0 );
		}

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
