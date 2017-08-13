using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

	public static readonly float IDLE_MIN_X = -6.0f;
	public static readonly float IDLE_MAX_X = 7.0f;
	public static readonly float IDLE_MIN_Y = -4.0f;
	public static readonly float IDLE_MAX_Y = -2.5f;

	public char[] keys = new char[26];
	public GameObject[] players = new GameObject[26];
	
	public GameObject playerPrefab;

	public static char ToUpper(char key) { return Char.ToUpper(key); }
	public static char ToLower(char key) { return Char.ToLower(key); }

	public bool KeyExists(char key){ return keys.Contains(ToUpper(key)); }

	public void PushChar(char key) {
		
		key = ToUpper(key);

		int index = key - 'A';
		this.keys[index] = key;

		AddPlayer(key, index);
	}

	public void RemoveChar(char key){

		key = ToUpper(key);

		int index = key - 'A';
		this.keys[index] = '\0';

		RemovePlayer(key, index);
	}

	public void AddPlayer(char key){
		
		key = ToUpper(key);
		AddPlayer(key, key - 'A');
	}

	private void AddPlayer(char key, int index){

		float x = UnityEngine.Random.Range(IDLE_MIN_X, IDLE_MAX_X);
		float y = UnityEngine.Random.Range(IDLE_MIN_Y, IDLE_MAX_Y);
		
		players[index] = Instantiate(playerPrefab, this.transform);
		players[index].transform.position = new Vector3(x, y, 0f);
		players[index].GetComponent<PlayerController>().playerKey = key;
	}

	public void RemovePlayer(char key){

		key = ToUpper(key);
		RemovePlayer(key, key - 'A');
	}

	private void RemovePlayer(char key, int index){
		GameObject.Destroy(players[index]);
		players[index] = null;
	}

	public void ResetPlayers(){
		foreach(GameObject pc in players){
			if(pc != null)
				pc.GetComponent<PlayerController>().selected = 0;
		}
	}

	public bool PlayerListIsEmpty(){
		foreach(char c in keys)
			if(c != '\0') return false;
		return true;
	}
}
