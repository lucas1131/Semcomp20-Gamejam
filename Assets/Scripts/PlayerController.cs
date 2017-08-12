using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private bool isMoving = false;
	private char _playerKey;
	public char playerKey { 
		get { return this._playerKey; }
		set { 
			this._playerKey = value;
			this.text.text = this._playerKey + "";
		}
	}

	private TextMesh text;

	void Awake(){
		this.text = gameObject.GetComponentsInChildren<TextMesh>()[0];
	}

	void Start (){

		Color c = UnityEngine.Random.ColorHSV();

		gameObject.GetComponent<SpriteRenderer>().color = c;
		this.text.color = new Color(1-c.r, 1-c.g, 1-c.b);
	}
	
	// Update is called once per frame
	void Update (){
		
		if(!isMoving)
			SmallMovement(PlayerManager.IDLE_MIN_X, 
				PlayerManager.IDLE_MAX_X, 
				PlayerManager.IDLE_MIN_Y, 
				PlayerManager.IDLE_MAX_Y);
	}

	private void SmallMovement(float minX, float maxX, float minY, float maxY){

		float move = 0.01f;

		if(Random.value < 0.4f){ // X movement

			if(Random.value < 0.5) // Random direction
				move = -move;
			
			StartCoroutine(MoveX(minX, maxX, move, transform.position.x));
		
		} else if(Random.value > 0.7) { // Y movement
			
			if(Random.value < 0.5) // Random direction
				move = -move;
			
			StartCoroutine(MoveY(minY, maxY, move, transform.position.y));
		} 
		// Else stay idle
		StartCoroutine(Wait());
	}

	IEnumerator Wait(){
		yield return new WaitForSeconds(0.3f); // wait for 5 seconds
	}

	IEnumerator MoveX(float min, float max, float move, float pos){

		isMoving = !isMoving;

		for (int i = 0; i < 30; i++) {
			if(InRange(min, max, move, pos)) 
				transform.Translate(move, 0f, 0f);
			yield return null;
		}

		isMoving = !isMoving;
	}

	IEnumerator MoveY(float min, float max, float move, float pos){

		isMoving = !isMoving;

		for (int i = 0; i < 25; i++) {
			if(InRange(min, max, move, pos)) 
				transform.Translate(0f, move, 0f);
			yield return null;
		}

		isMoving = !isMoving;
	}

	private bool InRange(float min, float max, float move, float pos){
		float target = pos + move;
		if(target < min || target > max) return false;
		return true;
	}

	public override bool Equals(System.Object obj){
		
		if (obj == null)
			return false;
		
		PlayerController pc = obj as PlayerController ;
		
		if ((System.Object) pc == null)
			return false;
		
		return this.playerKey == pc.playerKey;
	}

	// public bool Equals(PlayerController pc){
		
	// 	if ((object) pc == null)
	// 		return false;
		
	// 	return this.playerKey == pc.playerKey;
	// }

	public bool Equals(char key){
		
		if ((object) key == null)
			return false;

		return this.playerKey == key;
	}

	public override int GetHashCode(){ 
		return playerKey.GetHashCode();
	}
}
