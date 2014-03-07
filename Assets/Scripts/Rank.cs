using UnityEngine;
using System.Collections;

public class Rank : MonoBehaviour {
	
 	GameCenter Gc;

	void OnClick()
	{
		if(Gc)
			Gc.ShowLeaderboard();
	}

	// Use this for initialization
	void Start () {
		Gc = (GameCenter)GameObject.Find("GameCenter").GetComponent("GameCenter");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
