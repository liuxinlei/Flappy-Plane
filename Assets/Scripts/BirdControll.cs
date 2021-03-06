﻿using UnityEngine;
using System.Collections;

public class BirdControll : MonoBehaviour {
	public static bool gameOver = false;
	public static bool isStart = false;
	public static int score = 0;
	public static bool canRestart = false;
	public bool canControll;
	public GameObject reStart;
	public GameObject teach;
	public AudioClip zhuangqiang;
	public AudioClip click;
	public AudioClip die;
	public AudioClip getPoint;
	public GameObject zhangAi;
	public GameObject fire;
	public float maxY = 1.2f;
	public float minY = 0.2f;
	bool isDie = false;
	bool isAddScore = false;
	bool isUp = true;
	float jumpForce = 420.0f;
	Rigidbody2D rb;

	void Awake () {
		Application.targetFrameRate = 45;
	}
	// Use this for initialization
	void Start () {
		Screen.fullScreen = !Screen.fullScreen;
		rb = rigidbody2D;
		rb.Sleep ();
		if(!canControll){
			return;
		}
		fire.SetActive(false);
		NGUITools.SetActive (reStart,false);
	}
	
	// Update is called once per frame
	void Update () {
		if (gameOver) {
			return;
		}
		if(!isStart){
			AutoMove();
		}
		if(!canControll){
			return;
		}
		if (Input.GetKeyDown (KeyCode.A)) {
			if(!isStart){
				for (int i = 0; i < 3; i++) {
					Instantiate(zhangAi,new Vector3(-5 - 4*i,Random.Range(2,-2)),Quaternion.identity);
				}
				isStart = true;
				NGUITools.SetActive(teach,false);
				rb.WakeUp();
			}
			audio.clip = click;
			audio.Play();
			rb.AddForce (Vector2.up*jumpForce - rb.velocity * 54);
			StartCoroutine(Fire());
		}
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
			if(!isStart){
				for (int i = 0; i < 3; i++) {
					Instantiate(zhangAi,new Vector3(-5 - 4*i,Random.Range(2,-2)),Quaternion.identity);
				}
				isStart = true;
				NGUITools.SetActive(teach,false);
				rb.WakeUp();
			}
			audio.clip = click;
			audio.Play();
			rb.AddForce (Vector2.up*jumpForce - rb.velocity * 54);
			StartCoroutine(Fire());
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag == "zhangai"){
			if(GameDataManager.instance.gameData.PlayerScore < score){
				GameDataManager.instance.gameData.PlayerScore = score;
				GameDataManager.instance.Save();
				MaxScore.instance.ChangeScore(score);
			}
			if(!gameOver){
				audio.clip = zhuangqiang;
				audio.Play();
				rb.Sleep();
				ShakeCamera.shakeCamera();
			}
			gameOver = true; 
			StartCoroutine(ShowWindow());
		}else if (col.gameObject.tag == "score") {
			if(!isAddScore){
				score ++;
				audio.clip = getPoint;
				audio.Play();
				Score.instance.ChangeScore(score);
				isAddScore = true;
			}
		}
	}

	void OnTriggerExit2D(Collider2D col){
		if (col.gameObject.tag == "score") {
			isAddScore = false;
		}
	}

	void AutoMove(){
		rb.Sleep ();
		if(transform.position.y >= maxY){
			isUp = false;
		}else if(transform.position.y <= minY){
			isUp = true;
		}
		if (isUp) {
			transform.Translate (Vector3.up*1.7f*Time.deltaTime,Space.World);	
		}else{
			transform.Translate (Vector3.down*Time.deltaTime,Space.World);	
		}

	}

	IEnumerator Fire(){
		fire.SetActive(true);
		yield return new WaitForSeconds (0.3f);
		fire.SetActive(false);
	}

	IEnumerator ShowWindow(){
		yield return new WaitForSeconds (0.5f);
		if (!isDie) {
			audio.clip = die;
			audio.Play ();		
			isDie = true;
		}
		rb.WakeUp();
		yield return new WaitForSeconds (1);
		canRestart = true;
		NGUITools.SetActive (reStart,true);
	}
}
