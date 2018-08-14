using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuddleManager : MonoBehaviour {

	public GameObject DayPuddle;
	public GameObject NightPuddle;


	public enum TimeMode
	{
		Day = 0,
		Night = 1,
		Random = 2,
	};

	public TimeMode timeMode = TimeMode.Random;

	// Use this for initialization
	void Start () {
		GameObject puddle = new GameObject();

		switch (timeMode) 
		{
		case TimeMode.Day:
			puddle = DayPuddle;
			break;
		case TimeMode.Night:
			puddle = NightPuddle;
			break;
		case TimeMode.Random:
			puddle = (Random.Range (0, 2) == 0) ? DayPuddle : NightPuddle;
			break;
		}

		for (int count = Random.Range (1, 11); count>0; count = count - 1) {
			print (count);
			GameObject obj = (GameObject)Instantiate(puddle,new Vector3(Random.Range(-6.5f,8.5f),0.51f,Random.Range(-16.8f,-1.8f)), Quaternion.identity);
			obj.SetActive(true);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
