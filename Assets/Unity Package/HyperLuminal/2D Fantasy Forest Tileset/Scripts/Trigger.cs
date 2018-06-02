using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Trigger : MonoBehaviour 
{
	#region Member Variables
	/// <summary>
	/// The sprite renderer
	/// </summary>
	private SpriteRenderer spriteRenderer;
	
	public Sprite OnSprite;    // the sprite used for the on toggle setting
	public Sprite OffSprite;  // the sprite used for the off toggle setting

	/// <summary>
	/// The list of affected objects by this trigger
	/// </summary>
	public List<GameObject> TriggeredObjects = new List<GameObject>();

	/// <summary>
	/// A toggle for turning this tiles functionality on or off
	/// </summary>
	public enum TOGGLE
	{
		ON = 0,
		OFF = 1,
	}
	public TOGGLE Toggle;
	#endregion


	void Start () 
	{
		// use the initial inspector setting to determine the starting phase of this object
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		
		if(Toggle == TOGGLE.OFF)
		{
			spriteRenderer.sprite = OffSprite;
		}
		else if(Toggle == TOGGLE.ON)
		{
			spriteRenderer.sprite = OnSprite;
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
			spriteRenderer.sprite = OnSprite;
		}
		else if(Toggle == TOGGLE.ON)
		{
			// turn it off
			Toggle = TOGGLE.OFF;
			
			// change the sprite to an off trigger
			spriteRenderer.sprite = OffSprite;		
		}
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.gameObject.name == "PlayerCharacter")
		{
			// toggle this object
			ToggleObject();

			// toggle each of the objects in the list
			foreach(GameObject obj in TriggeredObjects)
			{
				// check if its a door object, if it is then toggle it using its script
				if(obj.GetComponent<Door>())
				{
					obj.GetComponent<Door>().ToggleObject();
				}
			}
		}
	}
}
