using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour 
{
	#region Member Variables
	/// <summary>
	/// The player transform.
	/// </summary>
	private Transform playerTransform;

	/// <summary>
	/// The sprite renderer.
	/// </summary>
	private SpriteRenderer spriteRenderer;
	#endregion

	// Use this for initialization
	void Start () 
	{
		// obtain the local references
		playerTransform = GameObject.Find("PlayerCharacter").transform;
		spriteRenderer = this.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () 
	{

		// to ensure correct positioning of the environment around the player (3D Depth Effect)
		// we need to make the tiles below the player higher than the player in the render layering
		// and the ones above the player be lower than the player in the render layering
		if(this.tag == "LargeTile")
		{
			if((playerTransform.position.y - 1.28f) > this.transform.position.y)
			{
				// make all the tiles lower than the player higher than them on render layer 
				spriteRenderer.sortingLayerName = "TreeLayer";
			}
			else
			{
				// give this tile a normal tile set render order
				spriteRenderer.sortingLayerName = "Tileset";
			}
		}
	}
}
