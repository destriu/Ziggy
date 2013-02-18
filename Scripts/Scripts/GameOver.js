#pragma strict
var gameOverMusic : AudioClip;


function Start () 
{
	audio.PlayOneShot(gameOverMusic);
}

function Update () 
{
	if(Input.GetKey("enter"))
	{
		Application.LoadLevel("TitleScreenWithEnter");
	}
}

@script RequireComponent(AudioSource)