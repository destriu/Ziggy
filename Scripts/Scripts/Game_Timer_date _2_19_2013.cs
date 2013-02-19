using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Game_Timer : MonoBehaviour 
{
	public int G_Timer = 0;// Timer for the game
	public int Seconds = 0;// Hold the amount of seconds that have passed
	public string Wave;// WIll hold text saying what the current wave is and the time limit
	public bool Game_Over;// Is set to true if the player hasn't destroyed all the bodies
	public bool Win_Or_Lose;// checks to see if the player has won (true) or lost(false)
	public GameObject Spawner;// 
	public string Corpse_Count = "0";// holds the current number of corpses in the room
	public List<GameObject> Corpse_List;// holds the current number of bodies in the room
	
	// Use this for initialization
	void Start () 
	{
		Spawner = GameObject.Find ("Corspe_Cube");// Sets Spawner equal to the GAmeObject with the name Corpse_Cube
	}
	
	void OnGUI()
	{
		  GUI.skin.box.fontSize = 16;// Sets the font size for the GUI boxs to 16
		  GUI.Box(new Rect(Screen.width/2 -45, 5f-2, 100f-20, 50f-20), Seconds.ToString ());// creates a GUI Box at the desired location and displays Seconds in string form
		  GUI.Box(new Rect(10f, 5f, 140f, 25f), "Corpse Count: " + Corpse_Count);// creates a GUI Box at the desired location and diplays Corpse_Count
		  GUI.Box(new Rect(575f, 5f, 190f, 25f), Wave);// creates a GUI Box at the desired location and diplays Wave
		
		
		//The line of code below displays lives, but lives have not been implemented yet.  
		//GUI.Box(new Rect(575f, 31f, 190f, 25f), "Lives: ");
	}
	
	// Update is called once per frame
	void Update () 
	{		
		
		Corpse_Count = Corpse_List.Count.ToString ();// sets Corpse_Count to equal the Count of Corpse_List
		var CorpseSpawner_Script = Spawner.GetComponent ("CorpseSpawner")as CorpseSpawner;// Temp variable to hold the Corpse_Spawner script
		Corpse_List = CorpseSpawner_Script.Corpse_List;// Sets Corpse_List equal to the Corpse_List in the script Corpse_Spawner
		G_Timer++;// increments G_Timer
		if(G_Timer == 60)// if G_Timer equals 60 it is resets and Seconds is incremented by 1
		{
			Seconds++;
			G_Timer = 0;
		}
		
		if(CorpseSpawner_Script.FirstWave)// Checks to see if FirstWave is set to true on the Corpse_Spawner script
		{
			Wave = "Wave 1   Time Limit: 35";// Sets wave equal to the following text "Wave 1   Time Limit: 35"
		}
		
		if(CorpseSpawner_Script.SecondWave)// Checks to see if SecondWave is set to true on the Corpse_Spawner script
		{
			Wave = "Wave 2	  Time Limit: 60";// Sets wave equal to the following text "Wave 2	  Time Limit: 60"
		}
		
		if(CorpseSpawner_Script.ThirdWave)// Checks to see if ThirdWave is set to true on the Corpse_Spawner script
		{
			Wave = "Wave 3    Time Limit: 120";// Sets wave equal to the following text "Wave 3    Time Limit: 120"
		}
		
		if(Seconds == 23)//Checks to see if Seconds equals 23 
		{
			audio.Play ();// Plays a audio clip(Clock)
		}
		
		if(Seconds == 35 && CorpseSpawner_Script.FirstWave)// )// Checks to see if FirstWave is equal to true on the Corpse_Spawner script and thet Win_Or_Lose equals true and Starts the next wave
		{
			Win_Or_Lose = (Corpse_List.Count == 0);// Check to see if the player has destroyed all the bodies and is set to true if the player has
		
			if(CorpseSpawner_Script.FirstWave && Win_Or_Lose)// Checks to see if FirstWave is equal to true on the Corpse_Spawner script and thet Win_Or_Lose equals true
			{
				CorpseSpawner_Script.Corpse_Spawn_Timer_Per_Spawn =0;// Resets Corpse_Spawn_Timer_Per_Spawn
				CorpseSpawner_Script.FirstWave = false;
				CorpseSpawner_Script.SecondWave = true;
			}
			
			if(!Win_Or_Lose)// Ends the game if Win_Or_Lose if equal to false
			{
				Game_Over = true;
			}
		}
		
		if(Seconds == 83)// plays a audio clip(Clock) if Seconds equals 83
		{
			audio.Play ();
		}
		
		if(Seconds == 95 && CorpseSpawner_Script.SecondWave)// Checks to see if Seconds equals 95 and SecondWave on CorpseSpawner is equal to true
		{
			Win_Or_Lose = (Corpse_List.Count == 0);
		
			if(CorpseSpawner_Script.SecondWave && Win_Or_Lose)// Checks to see if FirstWave is equal to true on the Corpse_Spawner script and thet Win_Or_Lose equals true and Starts the next wave
			{
				CorpseSpawner_Script.Corpse_Spawn_Timer_Per_Spawn = 0;// Resets Corpse_Spawn_Timer_Per_Spawn
				CorpseSpawner_Script.SecondWave = false;
				CorpseSpawner_Script.ThirdWave = true;
			}
			
			if(!Win_Or_Lose)// Ends the game if Win_Or_Lose if equal to false
			{
				Game_Over = true;
			}
		}
		
		if(Seconds == 143)// Plays a audio clip(Clock) if Seconds equals 143
		{
			audio.Play ();
		}
		
		if(Seconds == 165 && CorpseSpawner_Script.ThirdWave)// Checks to see if ThirdWave is set to true on the script CorspeSpawner and that Seconds is equal to 165
		{
			Win_Or_Lose = (Corpse_List.Count == 0);
		
			if(CorpseSpawner_Script.ThirdWave && Win_Or_Lose)// Loads the Winners screen(Congrats scene) if the Player destroyed all corpses and ThirdWave on the Corpse_Spawner script is equal to true
			{
				Application.LoadLevel ("Congrats");
			}
			
			if(!Win_Or_Lose)// Ends the game if Win_Or_Lose if equal to false
			{
				Game_Over = true;
			}
		}
		
		if(Game_Over)// Brings up the GameOver scene
		{
			Application.LoadLevel("GameOver");
		}
	}
}
