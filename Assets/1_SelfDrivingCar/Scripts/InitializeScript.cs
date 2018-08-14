using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startup {

    public bool debugLog = false;
    public bool runInBackgroundValue = true;
    static Startup()
	{
		Debug.Log("Up and running");
		int count = UnityEngine.Random.Range (0, 6);
	}


    // Use this for initialization
    void Start()
    {
        Application.runInBackground = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (debugLog)
        {
            runInBackgroundValue = Application.runInBackground;
        }

        if (!Application.runInBackground)
        {
            Application.runInBackground = true;

            if (debugLog)
            {
                Debug.Log("Re-Setting Application.runInBackground to TRUE at: " + Time.time);
            }
        }
    }
}