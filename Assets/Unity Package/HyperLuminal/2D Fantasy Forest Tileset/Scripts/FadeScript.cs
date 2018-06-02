using UnityEngine;
using System.Collections;

public class FadeScript : MonoBehaviour
{
	#region Member Variables
	/// <summary>
	/// The sprite that represents the fade on screen
	/// </summary>
	private SpriteRenderer spriteRenderer;

	/// <summary>
	/// The alpha value of the fade
	/// </summary>
	private float AlphaValue;
	
	/// <summary>
	/// A toggle for turning this tiles functionality on or off
	/// </summary>
	public enum FADETYPE
	{
		IN = 0,
		OUT = 1,
		NONE = 2,
		RESPAWN = 3,
	}
	public FADETYPE FadeType;
	#endregion

	// Use this for initialization
	void Start () 
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		FadeType = FADETYPE.IN;
		AlphaValue = 1.0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// fade in or fade out based on the objects state
		if(FadeType == FADETYPE.IN)
		{
			AlphaValue -= 0.25f * Time.deltaTime;
			// limit the possible alpha value
			if(AlphaValue < 0.0f)
			{
				AlphaValue = 0.0f;
				FadeType = FADETYPE.NONE;
			}
		}
		else if(FadeType == FADETYPE.OUT)
		{
			AlphaValue += Time.deltaTime;

			// limit the possible alpha value
			if(AlphaValue > 1.0f)
			{
				AlphaValue = 1.0f;
				FadeType = FADETYPE.NONE;
				ChangeLevel();
			}
		}
		else if(FadeType == FADETYPE.RESPAWN)
		{
			AlphaValue += (2.0f * Time.deltaTime);
			// limit the possible alpha value
			if(AlphaValue > 1.0f)
			{
				AlphaValue = 1.0f;
				FadeType = FADETYPE.IN;
				GameObject.Find("PlayerCharacter").GetComponent<PlayerMovement>().RespawnPlayerAtCheckpoint();
			}
		}

		// set the objects new colour
		spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, AlphaValue);
	}

	/// <summary>
	/// Set the fade out state	
	/// </summary>
	public void FadeOut()
	{
		FadeType = FADETYPE.OUT;
	}

	/// <summary>
	/// Respawns the fade
	/// </summary>
	public void RespawnFade()
	{
		// set the respawn state
		FadeType = FADETYPE.RESPAWN;
	}

	/// <summary>
	/// Changes the level to the next level in the list
	/// </summary>
	private void ChangeLevel()
	{
		int levelID = Application.loadedLevel + 1;
		if(levelID > Application.levelCount - 1){ levelID = 0; }
		Application.LoadLevel(levelID);
	}
}
