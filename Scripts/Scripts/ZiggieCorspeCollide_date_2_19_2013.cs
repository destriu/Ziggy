using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class ZiggieCorspeCollide : MonoBehaviour 
{
	//Public Variables
	public GameObject Corpse;
	public GameObject Cube;
	public GameObject Carrying;// Holds the GameObject that is being carried by the player
	public GameObject fireExplosion;
	public float Direction;
	public int Burn_Timer;
	public bool IsCarrying;// Is set to true if the player is carrying a object
	public bool StopMovement;
	public bool AtFurnace = false;// Set
	public bool Go_InTo_Furnace;
	public bool play = false;
	public Collider[] Floor_Collision;
	public AudioSource Play;
	public AudioSource[] Sounds;
	
	
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
	void Get_IsCarrying(bool True_Or_False)//Gets whether the cube is carrying a corpse
	{
		IsCarrying = True_Or_False;
	}
	
	void Get_Carrying(GameObject Attached)// reciver function to get the object the player is carrying and assign it to Carrying
	{
		Carrying = Attached;
	}
	
	void Floor()
	{
		Cube.SendMessage("Get_Stop_Movement", true,SendMessageOptions.DontRequireReceiver);
	}
	
	void Null_Collide()
	{
		Cube.SendMessage("Get_Stop_Movement", false,SendMessageOptions.DontRequireReceiver);
	}
	
	void Get_AtFurnace(bool At_f)// reciver function to get weither AtFurnace is true or not
	{
		AtFurnace = At_f;
	}
	
	void Get_Into_Furnace(bool Into_f)
	{
		Go_InTo_Furnace = Into_f;
	}
	
	// Use this for initialization
	void Awake () 
	{
		//Intialize Variables
		Cube = (GameObject)GameObject.Find ("Ziggy");// Sets Cube equal to the GameObject Ziggy
		Corpse = (GameObject)GameObject.Find ("Corspe_Cube");// Sets Corpse equal to the GameObject Corpse_Cube
		fireExplosion = GameObject.FindGameObjectWithTag("Exploder");// Sets fireExplosion equal to the GameObject Exploder
	}
	
	// Update is called once per frame
	void Update ()//Updates the corpses position if the cube isn't carrying anything
	{
		if(Carrying != null && Carrying == this.gameObject)// Goes off if Carrying doesnt equal nothing and the object being carried is this object
		{
			Physics.IgnoreCollision (collider, Cube.collider);// Turns off collision between the player and this object
			Physics.IgnoreCollision (collider, Corpse.collider);// Turns off collision between the player and this object(not a redundant call both colliders must be turned of for the other)
			rigidbody.isKinematic = true;// Makes it so this object can't move unless it is programmed to do so
		}
		
		if(Carrying != null && Carrying == this.gameObject && Input.GetKey ("right")&& !AtFurnace)// Goes off if Carrying doesnt equal nothing and the object being carried is this object
		{
			Direction = -.25f;
			rigidbody.isKinematic = true;// Makes it so this object can't move unless it is programmed to do so
			transform.position = new Vector3(Cube.transform.position.x, Cube.transform.position.y - .08f, Cube.transform.position.z + Direction);// modifies this GameObjects position to be at the end of the players Pich Fork when facing right
		}
		
		if(Carrying != null && Carrying == this.gameObject && Input.GetKey ("left") && !AtFurnace)// Goes off if  the object being carried is this object and the left arrow key is being pressed
		{
			Direction = .25f;
			rigidbody.isKinematic = true;// Makes it so this object can't move unless it is programmed to do so
			transform.position = new Vector3(Cube.transform.position.x, Cube.transform.position.y - .08f, Cube.transform.position.z + Direction);// modifies this GameObjects position to be at the end of the players Pich Fork when facing left
		}
		else if(Input.GetKey ("left") && AtFurnace && Carrying != null)// Goes off if  the object being carried is this object and the left arrow key is being pressed and the player is at the furnace
		{
			rigidbody.isKinematic = true;// Makes it so this object can't move unless it is programmed to do so
			transform.position = new Vector3(Cube.transform.position.x, Cube.transform.position.y + .3f, Cube.transform.position.z + Direction);// modifies this GameObjects position to be at the end of the players Pich Fork when facing left and at the furnace
		}
		
		if(Carrying != null && Carrying == this.gameObject && Input.GetKey ("down") && !AtFurnace)// Goes off if  the object being carried is this object and the down arrow key is being pressed
		{
			rigidbody.isKinematic = true;// Makes it so this object can't move unless it is programmed to do so
			transform.position = new Vector3(Cube.transform.position.x, Cube.transform.position.y - .08f, transform.position.z);// modifies this GameObjects position to stay at the end of the Pich Fork while moving down
		}	
		
		if(Carrying != null && Carrying == this.gameObject && Input.GetKey ("up") && !AtFurnace)// Goes off if  the object being carried is this object and the up arrow key is being pressed
		{
			rigidbody.isKinematic = true;// Makes it so this object can't move unless it is programmed to do so
			transform.position = new Vector3(Cube.transform.position.x, Cube.transform.position.y - .08f, transform.position.z);// modifies this GameObjects position to stay at the end of the Pich Fork while moving up
		}	
		
		if(Carrying != null && Carrying == this.gameObject && Input.GetKey(KeyCode.None) && !AtFurnace)// Goes off if  the object being carried is this object and the no arrow key is being pressed
		{
			rigidbody.isKinematic = true;// Makes it so this object can't move unless it is programmed to do so
			transform.position = new Vector3(Cube.transform.position.x,Cube.transform.position.y, transform.position.z);// modifies this GameObjects position to stay at the end of the Pich Fork while not moving
		}	
		
		Floor_Collision = Physics.OverlapSphere (new Vector3(transform.position.x,transform.position.y,transform.position.z),.1f);
		
		if(Carrying == this.gameObject)
		{
			foreach(Collider Obj in Floor_Collision)
			{
				if(Obj.tag == "Room" || Obj.name.Contains ("Corpse"))
				{
					if(Obj.name != this.gameObject.name)
					{
						Floor();
					}
					else if(Floor_Collision[0].name != "Room")
					{
						Null_Collide();
					}
				}
				else if(Floor_Collision[0].name != "Room")
				{
					Null_Collide();
				}
			}
		}
		
		if(Carrying != this.gameObject)
		{
			rigidbody.detectCollisions = true;
		}
		
		if(IsCarrying && AtFurnace)
		{
			if(Go_InTo_Furnace)
			{
				if(play != true)
				{
					audio.Play ();
					play = true;
				}
				transform.position = new Vector3(GameObject.Find("Furnace").transform.position.x + .1f,GameObject.Find("Furnace").transform.position.y - .5f,GameObject.Find("Furnace").transform.position.z);	
				Burn_Timer++;
				var CorpseSpawner_Script = Corpse.GetComponent("CorpseSpawner")as CorpseSpawner;
				CorpseSpawner_Script.Corpse_List.Remove (this.gameObject);// Removes this object from the Corpse_List on the script CorpseSpawner
				if(Burn_Timer == 120)
				{
					Cube.SendMessage("Is_Carrying", false, SendMessageOptions.DontRequireReceiver);// Sends a message to the Function Is_Carrying
					Cube.SendMessage("Get_Into_Furnace", false, SendMessageOptions.DontRequireReceiver);// Sends a message to the Function Get_Into_Furnace
					Destroy (this.gameObject);// destroies this GameObject
				}
			}
		}
	}
	
	void OnCollisionEnter(Collision collision)//Intializes the Corpses position if the Cube isnt carrying anything
	{
		Sounds = GetComponents<AudioSource>();// Sets the all AudioSources to Sounds
		if(collision.contacts[0].otherCollider.gameObject.tag == "Room" || collision.contacts[0].otherCollider.gameObject.name.Contains ("Corpse"))
		{
			Sounds[1].Play ();// Plays the second audio clip in sounds
		}
		Play.Play ();// Plays a audio clip
		if(!IsCarrying && collision.contacts[0].otherCollider.gameObject.name == "Cube")// goes off if IsCarrrying is false and the first collision it has is with the GameObject Cube
		{	
			rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;// Changes the collision detection mode of the object
			transform.rotation = transform.rotation;// Sets the Objects rotation equal to itself
		}
	}
}