using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Game_Timer : MonoBehaviour 
{
	public int G_Timer = 0;
	public int Seconds = 0;
	public string Timer;
	public string Wave;
	public bool Game_Over;
	public bool Win_Or_Lose;
	public GameObject Spawner;
	public string Corpse_Count = "0";
	public List<GameObject> Corpse_List;
	
	// Use this for initialization
	void Start () 
	{
		Spawner = GameObject.Find ("Corspe_Cube");
	}
	
	void OnGUI()
	{
		  GUI.skin.box.fontSize = 16;
		  GUI.Box(new Rect(Screen.width/2 -45, 5f-2, 100f-20, 50f-20), Seconds.ToString ());
		  GUI.Box(new Rect(10f, 5f, 140f, 25f), "Corpse Count: " + Corpse_Count);
		  GUI.Box(new Rect(575f, 5f, 190f, 25f), Wave);
		
		
		//The line of code below displays lives, but lives have not been implemented yet.  
		//GUI.Box(new Rect(575f, 31f, 190f, 25f), "Lives: ");
	}
	
	// Update is called once per frame
	void Update () 
	{		
		
		Corpse_Count = Corpse_List.Count.ToString ();
		var CorpseSpawner_Script = Spawner.GetComponent ("CorpseSpawner")as CorpseSpawner;
		Corpse_List = CorpseSpawner_Script.Corpse_List;
		G_Timer++;
		if(G_Timer == 60)
		{
			Seconds++;
			G_Timer = 0;
		}
		
		if(CorpseSpawner_Script.FirstWave)
		{
			Wave = "Wave 1   Time Limit: 35";
		}
		
		if(CorpseSpawner_Script.SecondWave)
		{
			Wave = "Wave 2	  Time Limit: 60";
		}
		
		if(CorpseSpawner_Script.ThirdWave)
		{
			Wave = "Wave 3    Time Limit: 120";
		}
		
		if(Seconds == 23)
		{
			audio.Play ();
		}
		
		if(Seconds == 35 && CorpseSpawner_Script.FirstWave)
		{
			Win_Or_Lose = (Corpse_List.Count == 0);
		
			if(CorpseSpawner_Script.FirstWave && Win_Or_Lose)
			{
				CorpseSpawner_Script.Corpse_Spawn_Timer_Per_Spawn =0;
				CorpseSpawner_Script.FirstWave = false;
				CorpseSpawner_Script.SecondWave = true;
			}
			
			if(!Win_Or_Lose)
			{
				Game_Over = true;
			}
		}
		
		if(Seconds == 83)
		{
			audio.Play ();
		}
		
		if(Seconds == 95 && CorpseSpawner_Script.SecondWave)
		{
			Win_Or_Lose = (Corpse_List.Count == 0);
		
			if(CorpseSpawner_Script.SecondWave && Win_Or_Lose)
			{
				CorpseSpawner_Script.Corpse_Spawn_Timer_Per_Spawn =0;
				CorpseSpawner_Script.SecondWave = false;
				CorpseSpawner_Script.ThirdWave = true;
			}
			
			if(!Win_Or_Lose)
			{
				Game_Over = true;
			}
		}
		
		if(Seconds == 143)
		{
			audio.Play ();
		}
		
		if(Seconds == 165 && CorpseSpawner_Script.ThirdWave)
		{
			Win_Or_Lose = (Corpse_List.Count == 0);
		
			if(CorpseSpawner_Script.ThirdWave && Win_Or_Lose)
			{
				Application.LoadLevel ("Congrats");
			}
			
			if(!Win_Or_Lose)
			{
				Game_Over = true;
			}
		}
		
		if(Game_Over)
		{
			Application.LoadLevel("GameOver");
		}
	}
}
