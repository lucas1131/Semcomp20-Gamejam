using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void DEvent();

[System.Serializable]
public class Dialogue {
	public Sentence[] sentences;
	public DEvent endDialog;

	public Dialogue(int n){
		sentences = new Sentence[n];

		for(int i = 0; i < n; i++)
			sentences[i] = new Sentence();
	}
}

[System.Serializable]
public class Sentence {
	
	[TextArea(3, 5)]
	public string text;
	public float delay;
	public AudioClip voice;

	// Dialogue event
	public DEvent diagEvent;
}
