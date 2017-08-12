using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

	public static readonly float IDLE_MIN_X = -6.0f;
	public static readonly float IDLE_MAX_X = 7.0f;
	public static readonly float IDLE_MIN_Y = -4.0f;
	public static readonly float IDLE_MAX_Y = -2.0f;

	public char[] keys = new char[26];
	public GameObject[] players = new GameObject[26];
	
	public GameObject playerPrefab;

	public char ToUpper(char key) { return key -= (char) ('a'-'A'); }

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

		int index = key - 'A';

		float x = UnityEngine.Random.Range(-6f, 7f);
		float y = UnityEngine.Random.Range(-4f, -2f);
		players[index] = Instantiate(playerPrefab, this.transform);
		players[index].transform.position = new Vector3(x, y, 0f);
		players[index].GetComponent<PlayerController>().playerKey = key;
	}

	private void AddPlayer(char key, int index){

		float x = UnityEngine.Random.Range(-6, 7);
		float y = UnityEngine.Random.Range(-4, -2);
		players[index] = Instantiate(playerPrefab, this.transform);
		players[index].transform.position = new Vector3(x, y, 0f);
		players[index].GetComponent<PlayerController>().playerKey = key;
	}

	public void RemovePlayer(char key){
		
		key = ToUpper(key);

		int index = key - 'A';

		Debug.Log("index: " + index);
		GameObject.Destroy(players[index]);
		players[index] = null;
	}

	private void RemovePlayer(char key, int index){
		GameObject.Destroy(players[index]);
		players[index] = null;
	}
}
