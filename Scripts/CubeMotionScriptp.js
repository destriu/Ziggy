#pragma strict
var Xmovement : float;
var Zmovement : float;
var test : GameObject;
var matArray : Material[];
var timer : float;
var left1 :int;
var left2 : int;
var right1 : int;
var right2 : int;
var furnaceLeft : int;
var furnaceRight : int;
var atFurnace : boolean;

function Start () 
{
	renderer.material = matArray[0];
	atFurnace = false;
}

function Update () 
{
	//If the user presses the left arrow and the player is not touching the furnace.
	if(Input.GetKey("left") && atFurnace == false)
	{
		
		renderer.material = matArray[left1];
		timer += Time.deltaTime;
		transform.position += (Vector3(0, 0, (Xmovement * -1)));
		if (timer > 0.1)
		{
			renderer.material = matArray[left2];
			if(timer > 0.2)
			{
				renderer.material = matArray[left1];
				timer = 0.0;
			}
		}
			
	}
	else if(Input.GetKey("left") && atFurnace == true)
	{
		renderer.material = matArray[furnaceLeft];
		transform.position += (Vector3(0, 0, (Xmovement * -1)));
		
	}
	//If the player is pressing the right arrow and the player is not touching the furnace.
	if(Input.GetKey("right") && atFurnace == false)
	{
		timer += Time.deltaTime;
		renderer.material = matArray[right1];
		transform.position += (Vector3(0, 0, Xmovement));
		if (timer > 0.1)
		{
			renderer.material = matArray[right2];
			if(timer > 0.2)
			{
				renderer.material = matArray[right1];
				timer = 0.0;
			}
		}
	}
	else if(Input.GetKey("right") && atFurnace == true)
	{
		renderer.material = matArray[furnaceRight];
		transform.position += (Vector3(0, 0, Xmovement));
	}
	//If the player is pressing the up arrow.
	if(Input.GetKey("up"))
	{
		transform.position += (Vector3((Zmovement *-1), 0, 0));
	}
	//If the player is pressing the down arrow.
	if(Input.GetKey("down"))
	{
		transform.position += (Vector3(Zmovement, 0, 0));
	}
	
	
}


function OnCollisionEnter(theObject : Collision) 
//Function to handle Furnace collision.
{
	if(atFurnace == false && theObject.gameObject.tag=="Furnace")
 //If the IceCube is hit by a fireball...
	{
		atFurnace = true;
	}
	else if (theObject.gameObject.tag =="Room")
	{
		atFurnace = false;
	}
}
