using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	private BoardManager _boardManager;
	private UIManager _uiManager;

	// Use this for initialization
	private void Start () 
	{
		_boardManager = this.transform.GetComponent<BoardManager>();
		_uiManager = this.transform.GetComponent<UIManager>();
	}
	
	// Update is called once per frame
	private void Update () 
	{
		_endGame();
	}

	private void _endGame()
	{
		int playerLife = _boardManager.GetPlayerLife();
		int iaLife = _boardManager.GetIaLife();

		if( playerLife == 0 || iaLife == 0 )
		{
			float totalTime = _uiManager.GetCurrentTime();

			SaveManager.SaveGame( totalTime, ( iaLife == 0 ) );

			Application.LoadLevel( "GameEnd" );
		}
	}


}
