using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class ZiggieCorspeCollide : MonoBehaviour 
{
	//Public Variables
	public GameObject Corpse;
	public GameObject Cube;
	public GameObject Carrying;
	public GameObject fireExplosion;
	public float Direction;
	public int Burn_Timer;
	public bool IsCarrying;
	public bool StopMovement;
	public bool AtFurnace = false;
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
	
	void Get_Carrying(GameObject Attached)
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
	
	void Get_AtFurnace(bool At_f)
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
		Cube = (GameObject)GameObject.Find ("Ziggy");
		Corpse = (GameObject)GameObject.Find ("Corspe_Cube");
		fireExplosion = GameObject.FindGameObjectWithTag("Exploder");
	}
	
	// Update is called once per frame
	void Update ()//Updates the corpses position if the cube isn't carrying anything
	{
		if(Carrying != null && Carrying == this.gameObject)
		{
			Physics.IgnoreCollision (collider, Cube.collider);
			Physics.IgnoreCollision (collider, Corpse.collider);
			rigidbody.isKinematic = true;
		}
		
		if(Carrying != null && Carrying == this.gameObject && Input.GetKey ("right")&& !AtFurnace)
		{
			Direction = -.25f;
			rigidbody.isKinematic = true;
			transform.position = new Vector3(Cube.transform.position.x, Cube.transform.position.y - .08f, Cube.transform.position.z + Direction);
		}
		
		if(Carrying != null && Carrying == this.gameObject && Input.GetKey ("left") && !AtFurnace)
		{
			Direction = .25f;
			rigidbody.isKinematic = true;
			transform.position = new Vector3(Cube.transform.position.x, Cube.transform.position.y - .08f, Cube.transform.position.z + Direction);
		}
		else if(Input.GetKey ("left") && AtFurnace && Carrying != null && !AtFurnace)
		{
			rigidbody.isKinematic = true;
			transform.position = new Vector3(Cube.transform.position.x, Cube.transform.position.y + .3f, Cube.transform.position.z + Direction);
		}
		
		if(Carrying != null && Carrying == this.gameObject && Input.GetKey ("down") && !AtFurnace)
		{
			rigidbody.isKinematic = true;
			transform.position = new Vector3(Cube.transform.position.x, Cube.transform.position.y - .08f, transform.position.z);
		}	
		
		if(Carrying != null && Carrying == this.gameObject && Input.GetKey ("up") && !AtFurnace)
		{
			rigidbody.isKinematic = true;
			transform.position = new Vector3(Cube.transform.position.x, Cube.transform.position.y - .08f, transform.position.z);
		}	
		
		if(Carrying != null && Carrying == this.gameObject && Input.GetKey(KeyCode.None) && !AtFurnace)
		{
			rigidbody.isKinematic = true;
			transform.position = new Vector3(Cube.transform.position.x,Cube.transform.position.y, transform.position.z);
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
				CorpseSpawner_Script.Corpse_List.Remove (this.gameObject);
				if(Burn_Timer == 120)
				{
					Cube.SendMessage("Is_Carrying", false, SendMessageOptions.DontRequireReceiver);
					Cube.SendMessage("Get_Into_Furnace", false, SendMessageOptions.DontRequireReceiver);
					Destroy (this.gameObject);
				}
			}
		}
	}
	
	void OnCollisionEnter(Collision collision)//Intializes the Corpses position if the Cube isnt carrying anything
	{
		Sounds = GetComponents<AudioSource>();
		if(collision.contacts[0].otherCollider.gameObject.tag == "Room" || collision.contacts[0].otherCollider.gameObject.name.Contains ("Corpse"))
		{
			Sounds[1].Play ();
		}
		Play.Play ();
		if(!IsCarrying && collision.contacts[0].otherCollider.gameObject.name == "Cube")
		{	
			rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
			transform.rotation = transform.rotation;
		}
	}
}