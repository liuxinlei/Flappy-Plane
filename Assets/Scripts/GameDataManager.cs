using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;
using System.Text;
using System.Xml;
using System.Security.Cryptography;

public class GameData
{
	public string key;

	public int PlayerScore;

	public GameData()
	{
		PlayerScore = 0;
	}
}

public class GameDataManager:MonoBehaviour
{
	public static GameDataManager instance;
	private string dataFileName ="FlyBirData.dat";//存档文件的名称,自己定//
	private  XmlSaver xs = new XmlSaver();
	
	public  GameData gameData;
	
	public void Awake()
	{
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		DontDestroyOnLoad (gameObject);
		gameData = new GameData();
		instance = this;
		gameData.key = SystemInfo.deviceUniqueIdentifier;
		Load();
	}

	public  void Save()
	{
		string gameDataFile = GetDataPath() + "/"+dataFileName;
		string dataString= xs.SerializeObject(gameData,typeof(GameData));

    	xs.CreateXML(gameDataFile,dataString);
	}

	public  void Load()
	{
		string gameDataFile = GetDataPath() + "/"+dataFileName;
		if(xs.hasFile(gameDataFile))
	    {
		   string dataString = xs.LoadXML(gameDataFile);
		   GameData gameDataFromXML = xs.DeserializeObject(dataString,typeof(GameData)) as GameData;

		 	if(gameDataFromXML.key == gameData.key)
			{
				gameData = gameDataFromXML;
			}
			else
			{
			}
		}
		else
		{
			if(gameData != null)
			Save();
		}
	}

	private static string GetDataPath()
	{
		if(Application.platform == RuntimePlatform.IPhonePlayer)
		{
		    string path = Application.dataPath.Substring (0, Application.dataPath.Length - 5); 
		    path = path.Substring(0, path.LastIndexOf('/'));  
		    return path + "/Documents";
		}
		else
			return Application.dataPath;
	}
}
