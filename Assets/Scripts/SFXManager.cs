﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour {

	private AudioSource SFX;
	public AudioSource BGM;
	public AudioClip clap, boo, win, drums, pergunta, categoryTV, categoryCinema;

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

		case "win":
			SFX.clip = win;

			break;

		case "drums":
			SFX.clip = drums;

			break;

		case "pergunta":
			SFX.clip = pergunta;

			break;

		case "categoryTV":
			SFX.clip = categoryTV;

			break;

		case "categoryCinema":
			SFX.clip = categoryCinema;

			break;
		}

		SFX.Play();
	}
}
