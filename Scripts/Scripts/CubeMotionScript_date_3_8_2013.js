#pragma strict
var Xmovement : float;
var Zmovement : float;
var test : GameObject;
var matArray : Material[];
var timer : float;

enum texturePositions {left1, left2, right1, right2, furnaceLeft,
					   furnaceRight, Drag_Left1, Drag_Left2, Drag_Right1,
					   Drag_Right2, Stab_Left, Stab_Right}

var atFurnace : boolean;
var IsCarrying : boolean = false;
var StopMovement : boolean;
var Stop_Right : boolean = false;
var Stop_Left : boolean = false;
var Carrying : GameObject;
var Carried_Corpses : GameObject[];
var Facing : String;
var Button_Tap : int;
var Button_Cap : int;
var Into_Furnace : boolean = false;
var Distance : float;
var Corpse_Distance : float;
var Stab_Timer : int = 0;


/***************************************************************************
	 * Use SendMessage to acces JavaScript attached to other Objects
	 * If JavaScript script isn't on the same object make a variable to reference the object
	 * The same goes for JavaScript
	 * Example C#
	 * {
	 * 		GameObject Test;
	 * 		Test = (GameObject)GameObject.Find("AnyObject");
	 * 		Test.SendMessage("Name_Of_Function", Parameter_To_Send,SendMessageOption.DontRequireReciever);
	 * 
	 * }
	 * 
	 * Example JavaScript
	 * {
	 * 		var Test : GameObject;
	 * 		Test = GameObject.Find("AnyObject");
	 * 		Test.SendMessage("Name_Of_Function", Parameter_To_Send,SendMessageOption.DontRequireReciever);
	 * 
	 * 
	 * }
	
	******************************************************************************/
	
function Start () 
{
	renderer.material = matArray[0];
	atFurnace = false;
}

function Get_Stop_Movement(Stop : boolean)
{
	StopMovement = Stop;
}

function Get_Carried_Corpses(Corpse : GameObject)
{
	//Carried_Corpses[].
}

function Get_IsCarrying(Is_C : boolean)
{
	IsCarrying = Is_C;
	if(Is_C)
	{
		Carrying = null;
	}
}

function Get_Into_Furnace(Into_F : boolean)
{
	Into_Furnace = Into_F;
}

function Update () 
{
	Distance = Vector3.Distance(Vector3(GameObject.Find("Furnace").transform.position.x,
				GameObject.Find("Furnace").transform.position.y,transform.position.z), 
				GameObject.Find("Furnace").transform.position);
	
	if(Carrying != null)
	{
		Corpse_Distance = Vector3.Distance(Carrying.transform.position, transform.position);
	}

	//If the user presses the left arrow and the player is not touching the furnace.
	if(Input.GetKey("left") && atFurnace == false && Stop_Left == false 
	   && renderer.material != matArray[texturePositions.Stab_Right] 
	   && renderer.material != matArray[texturePositions.Stab_Left])
	{
		Facing = "left";
			if(IsCarrying)
		{
			renderer.material = matArray[texturePositions.Drag_Left1];
		}
		else
		{
			renderer.material = matArray[texturePositions.left1];
		}
		timer += Time.deltaTime;
		transform.position += (Vector3(0, 0, (Xmovement * -1)));
		if (timer > 0.1 && !IsCarrying)
		{
			renderer.material = matArray[texturePositions.left2];
			if(timer > 0.2)
			{
				renderer.material = matArray[texturePositions.left1];
				timer = 0.0;
			}
		}
		else
		{
			if (timer > 0.1)
			{
				renderer.material = matArray[texturePositions.Drag_Left2];
				if(timer > 0.2)
				{
					renderer.material = matArray[texturePositions.Drag_Left1];
					timer = 0.0;
				}
			}
			
		}
			
	}
	else if(Input.GetKey("left") && atFurnace == true)
	{
		renderer.material = matArray[texturePositions.furnaceLeft];
		//transform.position += (Vector3(0, 0, (Xmovement * -1)));
		Carrying.transform.position = GameObject.Find("Furnace").transform.position;
		
	}
	//If the player is pressing the right arrow and the player is not touching the furnace.
	if(Input.GetKey("right") && atFurnace == false && Stop_Right == false 
	   && renderer.material != matArray[texturePositions.Stab_Right] 
	   && renderer.material != matArray[texturePositions.Stab_Left])
	{
		Facing = "right";
		timer += Time.deltaTime;
		if(IsCarrying)
		{
			renderer.material = matArray[texturePositions.Drag_Right1];
		}
		else
		{
			renderer.material = matArray[texturePositions.right1];
		}
		transform.position += (Vector3(0, 0, Xmovement));
		if (timer > 0.1 && !IsCarrying)
		{
			renderer.material = matArray[texturePositions.right2];
			if(timer > 0.2)
			{
				renderer.material = matArray[texturePositions.right1];
				timer = 0.0;
			}
		}
		else
		{
		
			if (timer > 0.1)
			{
				renderer.material = matArray[texturePositions.Drag_Right2];
				if(timer > 0.2)
				{
					renderer.material = matArray[texturePositions.Drag_Right1];
					timer = 0.0;
				}
			}
		}
	}
	
	//If the player is pressing the up arrow.
	if(Input.GetKey("up") && !atFurnace)
	{
		transform.position += (Vector3((Zmovement *-1), 0, 0));
	}
	//If the player is pressing the down arrow.
	if(Input.GetKey("down") && !atFurnace)
	{
		transform.position += (Vector3(Zmovement, 0, 0));
	}
	
	if(Input.GetKeyDown(KeyCode.Space) && Carrying != null && Corpse_Distance < .5f)
	{
		renderer.material = matArray[texturePositions.Stab_Right];
		IsCarrying = true;
		Carrying.SendMessage("Get_IsCarrying",IsCarrying,SendMessageOptions.DontRequireReceiver);
	}
	
	if(renderer.material == matArray[texturePositions.Stab_Right] || 
	   renderer.material == matArray[texturePositions.Stab_Left] )
	{
		Stab_Timer++;
	}
	
	if(Stab_Timer >= 90)
	{
		if(Facing == "right")
		{
			renderer.material = matArray[texturePositions.Drag_Right1];
		}
		else
		{
			renderer.material = matArray[texturePositions.Drag_Left1];
		}
	
		Stab_Timer = 0;
	}
	
	if(Facing == "right" && renderer.material != matArray[texturePositions.Stab_Right] 
	   && renderer.material != matArray[texturePositions.Stab_Left])
	{
	
		if(Input.GetKeyDown(KeyCode.Space) && IsCarrying )
		{
			Button_Tap++;
			Button_Cap = Button_Tap + 1;
			if(Button_Tap == 1)
			{
				audio.Play();
				Carrying.SendMessage("Get_Carrying",Carrying,SendMessageOptions.DontRequireReceiver);
			}
			
		}
	}
	else if(Facing == "left" && renderer.material != matArray[texturePositions.Stab_Right] 
		    && renderer.material != matArray[texturePositions.Stab_Left])
	{
		if(Input.GetKeyDown(KeyCode.Space) && IsCarrying)
		{
			Button_Tap++;
			Button_Cap = Button_Tap + 1;
			if(atFurnace && Button_Tap >= 1)
				{
					if(Button_Tap == (Button_Cap - 1))
					{
						Into_Furnace = true;
						atFurnace = false;
						IsCarrying = false;
						Carrying.SendMessage("Get_Into_Furnace", Into_Furnace, SendMessageOptions.DontRequireReceiver);
						Button_Tap = 0;
					}
				}
		}
	}
	
	if(atFurnace == true && Carrying == null)
	{
		atFurnace = false;
	}
	
}


function OnCollisionEnter(collision : Collision)
{

	if(!IsCarrying && collision.contacts[0].otherCollider.gameObject.name.Contains("Corpse"))
	{
		Carrying = collision.contacts[0].otherCollider.gameObject;
	}
	
	if(atFurnace == false && collision.gameObject.tag == "Furnace" && Carrying != null)
	{
		atFurnace = true;
		Carrying.SendMessage("Get_AtFurnace", atFurnace, SendMessageOptions.DontRequireReceiver);
	}
}