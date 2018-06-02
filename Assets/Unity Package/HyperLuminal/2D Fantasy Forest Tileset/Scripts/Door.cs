using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour 
{
	#region Member Variables
	/// <summary>
	/// The sprite for the door
	/// </summary>
	private SpriteRenderer spriteRenderer;

	/// <summary>
	/// The sprite used for the on toggle setting
	/// </summary>
	public Sprite OpenSprite;    

	/// <summary>
	/// The sprite used for the off toggle setting
	/// </summary>
	public Sprite ClosedSprite;  

	/// <summary>
	/// Do we alter this objects collision or not?
	/// </summary>
	public bool CollisionToggle; 

	/// <summary>
	/// A toggle for turning this tiles functionality on or off
	/// </summary>
	public enum TOGGLE
	{
		OPEN = 0,
		CLOSED = 1,
	}
	public TOGGLE Toggle;
	#endregion

	void Start () 
	{
		// use the initial inspector setting to determine the starting phase of this object
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

		if(Toggle == TOGGLE.CLOSED)
		{
			spriteRenderer.sprite = ClosedSprite;
			if(CollisionToggle){gameObject.GetComponent<Collider2D>().enabled = true;}
		}
		else if(Toggle == TOGGLE.OPEN)
		{
			spriteRenderer.sprite = OpenSprite;
			if(CollisionToggle){gameObject.GetComponent<Collider2D>().enabled = false;}
		}

	}

	/// <summary>
	/// Used to toggle between object states
	/// </summary>
	public void ToggleObject()
	{
		if(Toggle == TOGGLE.OPEN)
		{
			// make it closed
			Toggle = TOGGLE.CLOSED;
			
			// enable this objects collider and change the sprite to a closed door
			spriteRenderer.sprite = ClosedSprite;
			if(CollisionToggle){gameObject.GetComponent<Collider2D>().enabled = true;}
		}
		else if(Toggle == TOGGLE.CLOSED)
		{
			// make it open
			Toggle = TOGGLE.OPEN;
			
			// remove this objects collider and change the sprite to an open door
			spriteRenderer.sprite = OpenSprite;
			if(CollisionToggle){gameObject.GetComponent<Collider2D>().enabled = false;}
		}
	}
}
