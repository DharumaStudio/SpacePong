using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {


	public Text counterText;
	private float _currentTime;

	/// <summary>
	/// Gets the current time.
	/// </summary>
	/// <returns>The current time.</returns>
	public float GetCurrentTime()
	{
		return _currentTime;
	}


	// Update is called once per frame
	void Update () 
	{
		_updateCounter();
	}

	/// <summary>
	/// Print in the screen the actual time on game.
	/// </summary>
	private void _updateCounter()
	{
		_currentTime += Time.deltaTime;
		int minutes =  (int)( _currentTime / 60 );
		int seconds = (int)( _currentTime % 60 );

		counterText.text = ( (minutes < 10) ? "0" + minutes : minutes.ToString() ) + ":" + ( ( seconds < 10 ) ? "0" + seconds : seconds.ToString() );


	}
}
