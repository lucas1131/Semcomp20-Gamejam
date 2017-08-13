using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private bool isMoving = false, reachedTarget = true;

	private TextMesh text, selectText;
	private Animator anim;
	private static readonly int OPTIONS = 3;
	private static readonly float THRESHOLD = 0.5f;

	public int selected;
	
	private char _playerKey;
	public char playerKey { 
		get { return this._playerKey; }
		set { 
			this._playerKey = value;
			this.text.text = this._playerKey + "";
		}
	}

	public float speed = 3;
	public static readonly Vector2 REFERENCE = new Vector2(0, -3);

	void Awake(){
		TextMesh[] aux = gameObject.GetComponentsInChildren<TextMesh>();
		this.text = aux[0];
		this.selectText = aux[1];
	}

	void Start (){

		Color c = UnityEngine.Random.ColorHSV();

		GetComponentInChildren<SpriteRenderer>().color = c;

		Color textColor = new Color(1-c.r, 1-c.g, 1-c.b);

		this.anim = GetComponentInChildren<Animator>();
		this.text.color = textColor;
		this.selectText.color = textColor;
	}
	
	// Update is called once per frame
	void Update (){
		
		float offsetx = Random.Range(-1f, 1f);
		float offsety = Random.Range(-0.5f, 0.5f);

		switch(selected){
		case 0:
			Return();

			if(!isMoving)
				SmallMovement(PlayerManager.IDLE_MIN_X, 
					PlayerManager.IDLE_MAX_X, 
					PlayerManager.IDLE_MIN_Y, 
					PlayerManager.IDLE_MAX_Y);
			break;

		case 1:
			Goto(new Vector3(-6f + offsetx, -1.5f + offsety, 0f));
			break;

		case 2:
			Goto(new Vector3(0f + offsetx, -1.5f + offsety, 0f));
			break;

		case 3:
			Goto(new Vector3(6f + offsetx, -1.5f + offsety, 0f));
			break;
		}

		if(GameManager.isPlaying){
			if(Input.GetKeyDown(PlayerManager.ToLower(playerKey) + "")){

				selected = (selected+1)%(OPTIONS+1);
				anim.SetBool("isExcited", false);
				reachedTarget = false; // new target

				// Stop movement
				isMoving = false;

				StopAllCoroutines();

				// Update selected door
				if(selected == 0) this.selectText.text = "";
				else this.selectText.text = this.selected + "";
			}
		}
	}

	private void Goto(Vector3 target){

		if(isMoving || reachedTarget) return;

		StopAllCoroutines();
		StartCoroutine(Seek(target));
	}

	private void Return(){
		
		float y = gameObject.transform.position.y;

		if(y > PlayerManager.IDLE_MAX_Y) StartCoroutine(MoveY(-10f, 10f, -0.02f, gameObject.transform.position.y, 5));
		else if(y < PlayerManager.IDLE_MIN_Y) StartCoroutine(MoveY(-10f, 10f, 0.015f, gameObject.transform.position.y, 4));
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

	IEnumerator Seek(Vector3 target){

		isMoving = true;

		Vector3 distance = (target - gameObject.transform.position);
		Vector3 direction = distance.normalized;

		while(distance.magnitude > THRESHOLD){
	
			// Move
			gameObject.transform.Translate(direction*Time.deltaTime*speed);
			
			yield return null;

			// Recalculate distance
			distance = (target - gameObject.transform.position);
		}

		isMoving = false;
		reachedTarget = true;
		anim.SetBool("isExcited", true);
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

	public bool Equals(char key){
		
		if ((object) key == null)
			return false;

		return this.playerKey == key;
	}

	public override int GetHashCode(){ 
		return playerKey.GetHashCode();
	}
}
