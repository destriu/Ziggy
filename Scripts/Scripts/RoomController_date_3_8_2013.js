//This script handles all "behind the scenes" actions for 
//menu screens, level progression.

var timer : float;
var titleScreenMusic : AudioClip;
var titleScreenObj : GameObject;
var titleScreenWithEnterObj : GameObject;

function Start () 
{
	titleScreenWithEnterObj.guiTexture.enabled = true;
	audio.PlayOneShot(titleScreenMusic);

}

function Update () 
{
	if(Application.loadedLevelName != "Motion Test")
	{
		//Add one to the timer per cycle.
		timer += 1;
		
		//If it's been one second.
		if(timer > 60.0)
		{
			//Disable one Gui Texture, and enable the other one.
			titleScreenObj.guiTexture.enabled = !(titleScreenObj.guiTexture.enabled);
			titleScreenWithEnterObj.guiTexture.enabled = !(titleScreenWithEnterObj.guiTexture.enabled);
			timer = 0.0; //Reset the timer.
		}//end if
		
		//If the player has hit enter and they are at the main menu scene.
		if(isPressingEnter() && Application.loadedLevelName == "TitleScreenWithEnter")
		{
			Application.LoadLevel("Motion_Test"); //Go to level one.
		}//end if
		
		//If the player is at the credits and they press enter...
		else if(isPressingEnter() && Application.loadedLevelName == "Credits")
		{
			Application.LoadLevel("TitleScreenWithEnter"); //Go to the main menu.
		}//end if
	}

}//end function Update

//The purpose of this function is to check to see if the 
//player is pressing either "enter" key on the keyboard.
function isPressingEnter()
{
	//If the player is pressing "Return" or enter on the number pad...
	if(Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter))
	{
		return true;
	}//end if
	else
	{
		return false;
	}//end else
}//End method boolean isPressingEnter

//The purpose of this script is to handle the menu screen and buttons.

//@script RequireComponent(AudioSource)

