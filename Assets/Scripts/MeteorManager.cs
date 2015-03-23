using UnityEngine;
using System.Collections;

public class MeteorManager : MonoBehaviour {


	#region Public variables
	public GameObject[] meteors;
	public int maxMeteorsOnScreen;
	#endregion

	#region Private variables
	private GameObject _meteorsHierarchy;
	#endregion

	// Use this for initialization
	private void Start () 
	{
		if( maxMeteorsOnScreen == 0 ) maxMeteorsOnScreen = 5;

		_meteorsHierarchy = GameObject.Find( "Meteors" );
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( _meteorsHierarchy.transform.childCount < maxMeteorsOnScreen )
		{
			_generateMeteors();
		}
	
	}

	private void _generateMeteors()
	{
		GameObject meteor = meteors[Random.Range( 0, meteors.Length )];
		Camera camera = Camera.main;

		Vector2 randomSpawn = camera.ScreenToWorldPoint( new Vector2( Random.Range( 0,Screen.width ), Random.Range(0,Screen.height ) ) );

		GameObject meteorToInstantiate = Instantiate( meteor, randomSpawn, Quaternion.identity ) as GameObject;

		meteorToInstantiate.transform.SetParent( _meteorsHierarchy.transform );
	}
}
