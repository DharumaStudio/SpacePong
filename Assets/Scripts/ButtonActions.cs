using UnityEngine;
using System.Collections;

public class ButtonActions : MonoBehaviour {


	public void StartGame()
	{
		Application.LoadLevel( "Game" );
	}

	public void RestartGame()
	{
		Application.LoadLevel( "Game" );
	}
}
