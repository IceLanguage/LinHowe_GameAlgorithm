using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{

	#region Member Variables
	/// <summary>
	/// The distance the player can move before the camera follows
	/// </summary>
	public Vector2 Margins;	

	/// <summary>
	/// The maximum x and y coordinates the camera can have
	/// </summary>
	public Vector2 MAXBounds;

	/// <summary>
	/// The minimum x and y coordinates the camera can have
	/// </summary>
	public Vector2 MINBounds;

	/// <summary>
	/// The player character
	/// </summary>
	public GameObject PlayerCharacter;

	/// <summary>
	///  Reference to the users current view transform.
	/// </summary>
	private Transform PlayerTransform;
	#endregion

	void Start()
	{
		// get the players transform
		PlayerTransform = PlayerCharacter.transform;
	}
	
	void Update ()
	{
		// By default the target x and y coordinates of the camera are it's current x and y coordinates.
		Vector2 target = new Vector2(transform.position.x, transform.position.y);
		
		// If the player has moved beyond the x margin
		if(CheckXMargin())
		{
			// the target X-coordinate should be a Lerp between the camera's current x position and the player's current x position.
			target.x = Mathf.Lerp(transform.position.x, PlayerTransform.position.x , 8.0f * Time.deltaTime);
		}
		
		// If the player has moved beyond the y margin
		if(CheckYMargin())
		{
			// The target y coordinate should be a Lerp between the camera's current y position and the player's current y position.
			target.y = Mathf.Lerp(transform.position.y, PlayerTransform.position.y , 8.0f * Time.deltaTime);
		}
		
		// Clamp the camera within the bounds
		target.x = Mathf.Clamp(target.x, MINBounds.x, MAXBounds.x);
		target.y = Mathf.Clamp(target.y, MINBounds.y, MAXBounds.y);

		// Set the camera's position to the target position with the same z component.
		transform.position = new Vector3(target.x, target.y, transform.position.z);
	}

	/// <summary>
	/// Checks if the distance between the camera and player (on X axis) is greater than X margin
	/// </summary>
	/// <returns><c>true</c>, is distance is greater, <c>false</c> otherwise.</returns>
	bool CheckXMargin()
	{
		// Returns true if the distance between the camera and the player in the x axis is greater than the x margin.
		return Mathf.Abs(transform.position.x - PlayerTransform.position.x) > Margins.x;
	}
	
	/// <summary>
	/// Checks if the distance between the camera and player (on Y axis) is greater than Y margin
	/// </summary>
	/// <returns><c>true</c>, is distance is greater, <c>false</c> otherwise.</returns>
	bool CheckYMargin()
	{
		// Returns true if the distance between the camera and the player in the y axis is greater than the y margin.
		return Mathf.Abs(transform.position.y - PlayerTransform.position.y) > Margins.y;
	}
}
