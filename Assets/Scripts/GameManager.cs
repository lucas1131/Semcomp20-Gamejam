using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	[UnityEngine.SerializeField]
	private PlayerManager pm;

	// Use this for initialization
	void Start () {
		pm = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKeyDown(KeyCode.Space))
			StartGame();

		for(char i = 'a'; i <= 'z'; i++){
			if(Input.GetKeyDown(i + "")){ // It's always lower case
				
				if(pm.KeyExists(i)) pm.RemoveChar(i);
				else pm.PushChar(i);
			}
		}
	}

	public void StartGame(){
		Debug.Log("Starting Game");
	}

/*	

By key code:
	https://docs.unity3d.com/ScriptReference/KeyCode.html

By key name:
	The names of keys follow this convention:

    Normal keys: “a”, “b”, “c” …
    Number keys: “1”, “2”, “3”, …
    Arrow keys: “up”, “down”, “left”, “right”
    Keypad keys: “[1]”, “[2]”, “[3]”, “[+]”, “[equals]”
    Modifier keys: “right shift”, “left shift”, “right ctrl”, “left ctrl”, “right alt”, “left alt”, “right cmd”, “left cmd”
    Mouse Buttons: “mouse 0”, “mouse 1”, “mouse 2”, …
    Joystick Buttons (from any joystick): “joystick button 0”, “joystick button 1”, “joystick button 2”, …
    Joystick Buttons (from a specific joystick): “joystick 1 button 0”, “joystick 1 button 1”, “joystick 2 button 0”, …
    Special keys: “backspace”, “tab”, “return”, “escape”, “space”, “delete”, “enter”, “insert”, “home”, “end”, “page up”, “page down”
    Function keys: “f1”, “f2”, “f3”, …
*/
}
