using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Continue_Game : MonoBehaviour 
{
	public int Continues = 3;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	void OnGUI()
	{
		GUI.skin.box.fontSize = 35;
		
		GUI.Box (new Rect(Screen.width/2f - 100f,Screen.height-80f,200f,100f),"Retry?");
		
		GUI.skin.box.fontSize = 30;
		
		if(GUI.Button (new Rect((Screen.width/2f) - 50f,Screen.height-35f,35f,25f),"Yes"))
		{
			Application.LoadLevel("Motion_Test");
		}
		
		GUI.skin.box.fontSize = 30;
		
		if(GUI.Button (new Rect((Screen.width/2f) + 10f,Screen.height-35f,35f,25f),"No"))
		{
			Application.LoadLevel("Credits");
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
