using UnityEngine;
using System.Collections;

public class ButtonActions : MonoBehaviour {

	/// <summary>
	/// Call the game Scene
	/// </summary>
	public void StartGame()
	{
		Application.LoadLevel( "Game" );
	}

	/// <summary>
	/// Call the game Scene
	/// </summary>
	public void RestartGame()
	{
		Application.LoadLevel( "Game" );
	}
}
