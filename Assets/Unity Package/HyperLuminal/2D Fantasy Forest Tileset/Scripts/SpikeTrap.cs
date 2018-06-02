using UnityEngine;
using System.Collections;

public class SpikeTrap : MonoBehaviour 
{
	#region Member Variables
	/// <summary>
	/// The sprite representing the trap
	/// </summary>
	private SpriteRenderer sprite;

	/// <summary>
	/// The sprite used for the on toggle setting
	/// </summary>
	public Sprite OnSprite; 

	/// <summary>
	/// The sprite used for the off toggle setting
	/// </summary>
	public Sprite OffSprite;
	
	/// <summary>
	/// The time between animation changes
	/// </summary>
	private float Timer = 0.0f;
	
	/// <summary>
	/// A toggle for turning this tiles functionality on or off
	/// </summary>
	// 
	public enum TOGGLE
	{
		ON = 0,
		OFF = 1,
	}
	public TOGGLE Toggle;

	/// <summary>
	/// The time the trap takes to activate
	/// </summary>
	public float TrapTime = 2.0f;
	#endregion

	void Start () 
	{
		// use the initial inspector setting to determine the starting phase of this object
		sprite = gameObject.GetComponent<SpriteRenderer>();
		
		if(Toggle == TOGGLE.OFF)
		{
			sprite.sprite = OffSprite;
			gameObject.GetComponent<Collider2D>().enabled = false;
		}
		else if(Toggle == TOGGLE.ON)
		{
			sprite.sprite = OnSprite;
			gameObject.GetComponent<Collider2D>().enabled = true;
		}
	}

	void Update()
	{
		// Update the timer with the elapsed time
		Timer += Time.deltaTime;

		// Check if the timer has finished
		if(Timer > TrapTime)
		{
			Timer = 0.0f;
			ToggleObject();
		}
	}

	/// <summary>
	/// Used to toggle between object states 
	/// </summary>
	public void ToggleObject()
	{
		if(Toggle == TOGGLE.OFF)
		{
			// turn it on
			Toggle = TOGGLE.ON;
			
			// change the sprite to an on trigger
			sprite.sprite = OnSprite;
			gameObject.GetComponent<Collider2D>().enabled = true;
		}
		else if(Toggle == TOGGLE.ON)
		{
			// turn it off
			Toggle = TOGGLE.OFF;
			
			// change the sprite to an off trigger
			sprite.sprite = OffSprite;	
			gameObject.GetComponent<Collider2D>().enabled = false;
		}
	}
}
