using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

	public bool fastText = false;
	public AudioSource source;
	public GameObject textBox;
	private Text dialogueText, nameText;
	private Queue<Sentence> sentences;
	private Sentence currentSentence;
	private Dialogue currentDiag;

	void Start () {

		sentences = new Queue<Sentence>();

		Text[] aux = textBox.GetComponentsInChildren<Text>();

		dialogueText = aux[0];
		nameText = aux[1];
	}
	
	public void Reset(){
		sentences = new Queue<Sentence>();

		Text[] aux = textBox.GetComponentsInChildren<Text>();

		dialogueText = aux[0];
		nameText = aux[1];
	}

	public void StartDialog(Dialogue d, string name){
		
		// Clear current dialog state
		sentences.Clear();
		
		currentDiag = d; // Assign new dialog

		textBox.SetActive(true); // Activate textbox

		nameText.text = name;

		foreach(Sentence sentence in d.sentences)
			sentences.Enqueue(sentence);

		NextSentence();
	}

	private void NextSentence(){

		if(sentences.Count == 0){
			EndDialogue();
			return;
		}

		currentSentence = sentences.Dequeue();
		dialogueText.text = "";

		// StopCoroutine(PrintChar(currentSentence.text));
		StopAllCoroutines();

		if(currentSentence.diagEvent != null) currentSentence.diagEvent();
		if(currentSentence.voice != null) {
			source.clip = currentSentence.voice;
			source.Play();
		}

		StartCoroutine(PrintChar(currentSentence.text));
	}

	public void EndDialogue(){
		if(currentDiag.endDialog != null)
			currentDiag.endDialog();
	}

	IEnumerator PrintChar(string sentence){

		char[] aux = sentence.ToCharArray();
		for(int i = 0; i < aux.Length; i++){
			
			dialogueText.text += aux[i];
			
			// Print additional char
			if(fastText){
				if(++i >= aux.Length) break;
				dialogueText.text += aux[i];
			}

			yield return null;
		}

		StartCoroutine(AutoAdvance());
	}

	IEnumerator AutoAdvance(){
		yield return new WaitForSeconds(currentSentence.delay);
		NextSentence();
	}
}
