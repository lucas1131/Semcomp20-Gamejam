using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour {

	private AudioSource SFX;
	public AudioClip clap, boo, fanfare, drums;
	public AudioClip[] categoryTV, categoryCinema, categoryMusic, categoryEvent,
						win, lose, rodada;

	void Awake(){
		SFX = GetComponent<AudioSource>();
	}

	public void Play(string audio){
		
		switch(audio){
		case "clap":
			SFX.clip = clap;
			break;

		case "boo":
			SFX.clip = boo;
			break;

		case "fanfare":
			SFX.clip = fanfare;
			break;

		case "drums":
			SFX.clip = drums;
			break;
 		}

		SFX.Play();
	}

	public AudioClip GetClip(string audio){

		int i = 0;
		if(UnityEngine.Random.value < 0.5) i = 1;

		switch(audio){

		case "win":
			return win[0];

		case "lose":
			return lose[0];

		case "rodada":
			return rodada[i];

		case "cT":
			return categoryTV[i];

		case "cC":
			return categoryCinema[i];

		case "cM":
			return categoryMusic[i];

		case "cE":
			return categoryEvent[i];
		}

		return null;
	}

	public void PlayClap() { Play("clap"); }
	public void PlayBoo() { Play("boo"); }
	public void PlayWin() { Play("win"); }
	public void PlayDrums() { Play("drums"); }
}
