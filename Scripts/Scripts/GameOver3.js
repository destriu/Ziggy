var timer : float;
var titleScreenMusic : AudioClip;

function Start () 
{
	
	audio.PlayOneShot(titleScreenMusic);

}

function Update () 
{
	
	
	if(Input.GetKey("enter"))
	{
		Application.LoadLevel("TitleScreenWithEnter");
	}

}
