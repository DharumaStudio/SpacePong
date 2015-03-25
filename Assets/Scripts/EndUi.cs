using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndUi : MonoBehaviour {


	public Text betterTimeText;
	public Text currentTimeText;
	public Text endText;

	// Use this for initialization
	void Start () {
		_printInterface();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void _printInterface()
	{
		bool playerWon = SaveManager.GetPlayerWon();

		if( playerWon )
		{
			endText.text = "You Win";
			endText.color = Color.cyan;
		}

		float betterTime = SaveManager.GetPlayerBetterTime();
		float currentTime = SaveManager.GetPlayerCurrentTime();


		betterTimeText.text = "Better Time: " + _stringfyTime( betterTime );

		if( currentTime == betterTime )
		{
			currentTimeText.color = Color.yellow;
			currentTimeText.text = "New Record: " + _stringfyTime( currentTime );
			betterTimeText.text = "Better Time: " + _stringfyTime( currentTime );
		}
		else
		{
			currentTimeText.text = "Current Time: " + _stringfyTime( currentTime );
		}


	}

	private string _stringfyTime( float timer )
	{
		if( timer > 0 )
		{
			int minutes =  (int)( timer / 60 );
			int seconds = (int)( timer % 60 );
			
			return ( (minutes < 10) ? "0" + minutes : minutes.ToString() ) + ":" + ( ( seconds < 10 ) ? "0" + seconds : seconds.ToString() );
		}

		return "00:00";
	}
}
