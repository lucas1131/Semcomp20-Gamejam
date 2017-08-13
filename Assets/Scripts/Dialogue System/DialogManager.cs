using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

	public bool fastText = true;
	public GameObject textBox;
	private Text dialogueText, nameText;
	private Queue<string> sentences;
	private Queue<float> delays;
	private Dialogue currentDiag;

	void Start () {

		sentences = new Queue<string>();
		delays = new Queue<float>();

		Text[] aux = textBox.GetComponentsInChildren<Text>();

		dialogueText = aux[0];
		nameText = aux[1];
	}
	

	public void StartDialog(Dialogue d, string name){
		
		// Clear current dialog state
		sentences.Clear();
		delays.Clear();
		
		currentDiag = d; // Assign new dialog

		textBox.SetActive(true); // Activate textbox

		nameText.text = name;

		foreach(string sentence in d.sentences)
			sentences.Enqueue(sentence);

		foreach(float delay in d.sentenceDelay)
			delays.Enqueue(delay);

		NextSentence();
	}

	private void NextSentence(){
		if(sentences.Count == 0){
			EndDialogue();
			return;
		}

		string s = sentences.Dequeue();
		dialogueText.text = "";

		StopCoroutine(PrintChar(s));
		StartCoroutine(PrintChar(s));
	}

	public void EndDialogue(){
		currentDiag.diagFunc();
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
		yield return new WaitForSeconds(delays.Dequeue());
		NextSentence();
	}
}
