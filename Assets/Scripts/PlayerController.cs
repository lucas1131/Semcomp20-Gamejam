using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private bool isMoving = false;
	private TextMesh text;
	private static readonly int OPTIONS = 3;
	private int selected = 0;
	
	private char _playerKey;
	public char playerKey { 
		get { return this._playerKey; }
		set { 
			this._playerKey = value;
			this.text.text = this._playerKey + "";
		}
	}

	public static readonly Vector2 REFERENCE = new Vector2(0, -3);

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
		
		if(selected == 0){

			Return();

			if(!isMoving)
				SmallMovement(PlayerManager.IDLE_MIN_X, 
					PlayerManager.IDLE_MAX_X, 
					PlayerManager.IDLE_MIN_Y, 
					PlayerManager.IDLE_MAX_Y);
		}

		if(GameManager.isPlaying){
			if(Input.GetKeyDown(PlayerManager.ToLower(playerKey) + ""))
				selected = (selected+1)%OPTIONS;
		}
	}

	public void Return(){
		
		float y = gameObject.transform.position.y;

		if(y > -1.5) StartCoroutine(MoveY(-10f, 10f, -0.01f, gameObject.transform.position.y, 5));
		else if(y < -6) StartCoroutine(MoveY(-10f, 10f, 0.01f, gameObject.transform.position.y, 5));
	}

	private void SmallMovement(float minX, float maxX, float minY, float maxY){

		float move = 0.01f;

		if(Random.value < 0.4f){ // X movement

			if(Random.value < 0.5) // Random direction
				move = -move;
			
			StartCoroutine(MoveX(minX, maxX, move, transform.position.x, 30));
		
		} else if(Random.value > 0.7) { // Y movement
			
			if(Random.value < 0.5) // Random direction
				move = -move;
			
			StartCoroutine(MoveY(minY, maxY, move, transform.position.y, 25));
		} 
		// Else stay idle
		StartCoroutine(Wait());
	}

	IEnumerator Wait(){ yield return new WaitForSeconds(0.3f); }

	IEnumerator MoveX(float min, float max, float move, float pos, int frames){

		isMoving = true;

		for (int i = 0; i < frames; i++) {
			if(InRange(min, max, move, pos)) 
				transform.Translate(move, 0f, 0f);
			yield return null;
		}

		isMoving = false;
	}

	IEnumerator MoveY(float min, float max, float move, float pos, int frames){

		isMoving = true;

		for (int i = 0; i < frames; i++) {
			if(InRange(min, max, move, pos)) 
				transform.Translate(0f, move, 0f);
			yield return null;
		}

		isMoving = false;
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
