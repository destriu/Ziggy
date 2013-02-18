var titleScreenMusic : AudioClip;


function Start () 
{
	//DontDestroyOnLoad(titleScreenMusic);
	audio.PlayOneShot(titleScreenMusic);
}

function Update () 
{

}

//The purpose of this script is to handle the menu screen and buttons.

@script RequireComponent(AudioSource)

