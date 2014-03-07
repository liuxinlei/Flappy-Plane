using UnityEngine;
using System.Collections;

public class GameCenter : MonoBehaviour {

	// Use this for initialization
	void Start () {	
		Object.DontDestroyOnLoad(transform.gameObject);
		if (Application.platform == RuntimePlatform.IPhonePlayer) {  
			Social.localUser.Authenticate (success => {  
				if (success) {
				} else {  
				}  
			});  
		}  
	}

	public void ReportScore (long score, string leaderboardID)  
	{  
		if (Application.platform == RuntimePlatform.IPhonePlayer) {  
			if (Social.localUser.authenticated) {  
				Social.ReportScore (score, leaderboardID, success => {  
				});  
			}  
		}  
	}  

	public void ShowLeaderboard ()  
	{  
		if (Application.platform == RuntimePlatform.IPhonePlayer) {  
			if (Social.localUser.authenticated) {  
				Social.ShowLeaderboardUI ();  
			}  
		}  
	}  
	
}
