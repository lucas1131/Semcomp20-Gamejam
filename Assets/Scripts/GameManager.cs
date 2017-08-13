using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	[UnityEngine.SerializeField]
	private PlayerManager pm;

	[UnityEngine.SerializeField]
	public static bool isPlaying { get; private set; }

	public GameObject instructions;

	// Use this for initialization
	void Start () {
		pm = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if(isPlaying){ // Playing



		} else { // Waiting for game to start

			if(Input.GetKeyDown(KeyCode.Space))
				StartGame();

			for(char i = 'a'; i <= 'z'; i++){
				if(Input.GetKeyDown(i + "")){ // It's always lower case
					
					if(pm.KeyExists(i)) pm.RemoveChar(i);
					else pm.PushChar(i);
				}
			}
		}
	}

	public void StartGame(){

		instructions.SetActive(false);
		isPlaying = true;

		StartInitialDialog();

	}

	private void StartInitialDialog(){

	}
}