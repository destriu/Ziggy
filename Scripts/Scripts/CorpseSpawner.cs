using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CorpseSpawner : MonoBehaviour 
{
	//Public Variables
	GameObject Cube_Java;//Cube gameobject with Java Script attached
	public List<GameObject> Corpse_List;//List of all Corpses in the scene
	public GameObject Corpse;//The intial corpse GameObject
	public GameObject Corpse_Spawn;//The spawned corpse(cloned Corpse Cube)
	public Vector3 StartPos;//Starting position of Corpse Cube
	public Rigidbody Corpse_Spawn_Rigibody;//Rigibody that is to be added to the Corpse Spawn
	public int Corpse_Count;//Number of corpses spawned(intialized at zero)
	public int Corpse_Spawn_Cap;//Maxium number of corpses that can be spawned at a time
	public int Corpse_Spawn_Timer_Per_Spawn;//Amount o time between corpse spawns
	public int Corpse_Fall_Trajectory;//The degrees or angle in which the corspes will fall(Decided by random number)
	public float Rand_Degree;//Degree the Corpse Spawn will rotate when falling
	public AudioSource Burning;
	public bool FirstWave = true;
	public bool SecondWave = false;
	public bool ThirdWave = false;
	//Private Variables
	MeshRenderer Corpse_Renderer;
	MeshRenderer Corpse_Spawn_Renderer;
	
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

	// Use this for initialization
	void Awake () 
	{			
		//Intialized variables 
		Cube_Java = (GameObject)GameObject.Find ("Ziggy");
		Corpse = this.gameObject;//Set Corpse to Corpse Cube
		Corpse_Renderer = Corpse.GetComponent ("MeshRenderer")as MeshRenderer;
		Corpse_Renderer.enabled = false;//Make Corpse Cube invisible
		Corpse.rigidbody.isKinematic = true;//Make it so Corpse Cube won't move
		StartPos = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y - .3f, gameObject.transform.position.z);//Sets the starting position of Corpse Cube to StartPos
		Corpse_Count = 0;
		Corpse_Spawn_Cap = 10;
	}

	int Trajectory_Randomizer()
	{
		return  Corpse_Fall_Trajectory = Random.Range (0,3);
	}
	
	float Trajectory_Degree()
	{
	 	return Rand_Degree = Random.Range (30f,120f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		Corpse_Spawn_Timer_Per_Spawn++;
		if (Corpse_Spawn_Timer_Per_Spawn == 120 & Corpse_Count != 10 && FirstWave)//Spawn Corpses or Clone Corpse Cube
		{
			Corpse_Count++;
			Corpse_Spawn = (GameObject)Instantiate(Corpse,StartPos,Quaternion.identity);
			Corpse_Spawn.name = "Corpse " + Corpse_Count;
			Corpse_Spawn_Renderer = Corpse_Spawn.GetComponent ("MeshRenderer")as MeshRenderer;
			Corpse_Spawn_Renderer.enabled = true;
			Corpse_Spawn.rigidbody.isKinematic = false;
			Corpse_Spawn.transform.rotation = new Quaternion(Trajectory_Degree(),0f,0f,0f);
			Corpse_Spawn.transform.Rotate(Trajectory_Degree(),Trajectory_Degree(),Trajectory_Degree());
			Corpse_Spawn.AddComponent ("ZiggieCorspeCollide");
			Destroy (Corpse_Spawn.GetComponent("CorpseSpawner"));
			Corpse_List.Add (Corpse_Spawn);
			Corpse_Spawn_Timer_Per_Spawn = 0;
		}
		
		if(SecondWave)
		{
			if (Corpse_Spawn_Timer_Per_Spawn == 90 & Corpse_Count != 25)//Spawn Corpses or Clone Corpse Cube
			{
				Corpse_Count++;
				Corpse_Spawn = (GameObject)Instantiate(Corpse,StartPos,Quaternion.identity);
				Corpse_Spawn.name = "Corpse " + Corpse_Count;
				Corpse_Spawn_Renderer = Corpse_Spawn.GetComponent ("MeshRenderer")as MeshRenderer;
				Corpse_Spawn_Renderer.enabled = true;
				Corpse_Spawn.rigidbody.isKinematic = false;
				Corpse_Spawn.transform.rotation = new Quaternion(Trajectory_Degree(),0f,0f,0f);
				Corpse_Spawn.transform.Rotate(Trajectory_Degree(),Trajectory_Degree(),Trajectory_Degree());
				Corpse_Spawn.AddComponent ("ZiggieCorspeCollide");
				Destroy (Corpse_Spawn.GetComponent("CorpseSpawner"));
				Corpse_List.Add (Corpse_Spawn);
				Corpse_Spawn_Timer_Per_Spawn = 0;
			}
		}
			
		if(ThirdWave)
		{
			if (Corpse_Spawn_Timer_Per_Spawn == 50 & Corpse_Count != 50)//Spawn Corpses or Clone Corpse Cube
			{
				Corpse_Count++;
				Corpse_Spawn = (GameObject)Instantiate(Corpse,StartPos,Quaternion.identity);
				Corpse_Spawn.name = "Corpse " + Corpse_Count;
				Corpse_Spawn_Renderer = Corpse_Spawn.GetComponent ("MeshRenderer")as MeshRenderer;
				Corpse_Spawn_Renderer.enabled = true;
				Corpse_Spawn.rigidbody.isKinematic = false;
				Corpse_Spawn.transform.rotation = new Quaternion(Trajectory_Degree(),0f,0f,0f);
				Corpse_Spawn.transform.Rotate(Trajectory_Degree(),Trajectory_Degree(),Trajectory_Degree());
				Corpse_Spawn.AddComponent ("ZiggieCorspeCollide");
				Destroy (Corpse_Spawn.GetComponent("CorpseSpawner"));
				Corpse_List.Add (Corpse_Spawn);
				Corpse_Spawn_Timer_Per_Spawn = 0;
			}
			
		}
	
	
	
	
	
	
	
	
	
	
	}
	
	
	
	
	
	
}