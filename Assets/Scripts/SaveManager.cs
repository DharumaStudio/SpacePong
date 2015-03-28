using UnityEngine;
using System.Collections;

public class SaveManager : MonoBehaviour {

	#region Consts
	private const string BEST_TIME = "BestTime";
	private const string PLAYER_WON = "PlayerWon";
	private const string CURRENT_TIME = "CurrentTime";
	#endregion
	
	/// <summary>
	/// Saves the player total time only if it is less than saved.
	/// </summary>
	/// <param name="totalTime">Total Time.</param>
	public static void SaveGame( float currentTotalTime, bool playerWon )
	{
		if( playerWon )
		{
			float betterTime = GetPlayerBetterTime();

			if( currentTotalTime < betterTime || betterTime == 0 )
			{
				PlayerPrefs.SetFloat( BEST_TIME, currentTotalTime );
			}

			PlayerPrefs.SetFloat( CURRENT_TIME, currentTotalTime );
		}
		PlayerPrefs.SetInt( PLAYER_WON, _boolToInt( playerWon ) );
	}
	
	
	/// <summary>
	/// Gets the player time.
	/// </summary>
	/// <returns>The player score.</returns>
	public static float GetPlayerBetterTime()
	{
		return PlayerPrefs.GetFloat( BEST_TIME, 0 );
	}
	
	/// <summary>
	/// Gets the player result.
	/// </summary>
	/// <returns>The player result 1 if he won, 0 if he lost.</returns>
	public static bool GetPlayerWon()
	{
		int playerWon = PlayerPrefs.GetInt( PLAYER_WON, 0 );
		PlayerPrefs.DeleteKey( PLAYER_WON );
		
		return _intToBool( playerWon );
		
	}

	/// <summary>
	/// Gets the player current time.
	/// </summary>
	/// <returns>The player current time.</returns>
	public static float GetPlayerCurrentTime()
	{
		float playerTime = PlayerPrefs.GetFloat( CURRENT_TIME, 0 );
		PlayerPrefs.DeleteKey( CURRENT_TIME );
		
		return playerTime;		
	}

	/// <summary>
	/// Aux function to convert Boolean to integer
	/// </summary>
	/// <returns>The to int.</returns>
	/// <param name="value">If set to <c>true</c> value.</param>
	private static int _boolToInt( bool value )
	{
		return ( value ) ? 1 : 0;
	}

	/// <summary>
	/// Aux function to convert integer to boolean
	/// </summary>
	/// <returns><c>true</c>, if to bool was _inted, <c>false</c> otherwise.</returns>
	/// <param name="value">Value.</param>
	private static bool _intToBool( int value )
	{
		return ( value == 0 ) ? false : true;
	}
}
