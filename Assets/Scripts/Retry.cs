using UnityEngine;
using System.Collections;

public class Retry : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick(){
		BirdControll.score = 0;
		BirdControll.isStart = false;
		BirdControll.canRestart = false;
		BirdControll.gameOver = false;
		Application.LoadLevel (1);
	}
}
