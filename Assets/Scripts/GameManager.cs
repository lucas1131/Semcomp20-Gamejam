using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	[UnityEngine.SerializeField]
	private PlayerManager pm;
	[UnityEngine.SerializeField]
	private DialogManager dm;

	[UnityEngine.SerializeField]
	public static bool isPlaying { get; set; }

	public GameObject instructions;
	public SilvioSantos silvio;

	// Use this for initialization
	void Start () {
		pm = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if(!isPlaying){ // Playing

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

	public void Reset(){
		GameManager.isPlaying = false;
		silvio.Reset();
		pm.Reset();
	}

	public void Winner(char key){
		dm.StartDialog(silvio.winDialog, silvio.name);
	}

	public void Draw(){
		dm.StartDialog(silvio.drawDialog, silvio.name);
	}

	public void StartGame(){

		if(pm.PlayerListIsEmpty()) return;

		instructions.SetActive(false);
		isPlaying = true;

		silvio.StartInitialDialog();

	}

	public void EndGame(){
		Debug.Log("Game Ended!");
	}
}