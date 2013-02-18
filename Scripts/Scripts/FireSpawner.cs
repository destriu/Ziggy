using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireSpawner : MonoBehaviour {
	
	public GameObject Timer;
	public GameObject[] Floor_Fire;
	public int Fire_Timer;
	public bool Fire_On = false;
	public int Which_Fire;
	
	
	// Use this for initialization
	void Start () 
	{
		Timer = GameObject.Find ("Cube");
		
		Floor_Fire = GameObject.FindGameObjectsWithTag ("Floor Fire");
		
		foreach(GameObject Fire in Floor_Fire)
		{
			Fire.audio.enabled = false;
			Fire.renderer.enabled = false;
			Fire.collider.enabled = false;
		}
	}
	
	
	void Random_Fire()
	{
		Which_Fire = Random.Range (0,40);
	}
	
	// Update is called once per frame
	void Update () 
	{
		var Game_Timer_Script = Timer.GetComponent ("Game_Timer")as Game_Timer;
		if(Game_Timer_Script.Seconds >= 0)
		{
			Fire_Timer++;
			if(Fire_Timer >= 200)
			{
				if(Fire_On)
				{
					foreach(GameObject Fire in Floor_Fire)
					{
						if(Fire.collider.enabled)
						{
							Fire.collider.enabled = false;
							Fire.renderer.enabled = false;
						}
					}
				}
				Random_Fire ();
				Floor_Fire[Which_Fire].collider.enabled = true;
				Floor_Fire[Which_Fire].renderer.enabled = true;
				Random_Fire ();
				Floor_Fire[Which_Fire].collider.enabled = true;
				Floor_Fire[Which_Fire].renderer.enabled = true;
				Random_Fire ();
				Floor_Fire[Which_Fire].collider.enabled = true;
				Floor_Fire[Which_Fire].renderer.enabled = true;
				Random_Fire ();
				Floor_Fire[Which_Fire].collider.enabled = true;
				Floor_Fire[Which_Fire].renderer.enabled = true;
				Fire_Timer = 0;
				Fire_On = true;
			}
		}
	}
}
