var timer : float;
var titleScreenMusic : AudioClip;

function Start () 
{
	
	audio.PlayOneShot(titleScreenMusic);

}

function Update () 
{
	
	
	if(Input.GetKey(KeyCode.KeypadEnter))
	{
		Application.LoadLevel("Motion_Test");
	}

}

//The purpose of this script is to handle the menu screen and buttons.

//@script RequireComponent(AudioSource)

