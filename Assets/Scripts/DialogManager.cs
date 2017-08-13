using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

	public GameObject textBox;
	private Text dialogueText, nameText;
	private Queue<string> sentences;

	void Start () {

		sentences = new Queue<string>();

		Text[] aux = textBox.GetComponentsInChildren<Text>();

		dialogueText = aux[0];
		nameText = aux[1];
	}
	

	public void StartDialog(Dialogue d){

		sentences.Clear();

		nameText.text = d.name;

		foreach(string sentence in d.sentences){
			sentences.Enqueue(sentence);
		}
	}

	private void NextSentence(){

		if(sentences.Count == 0){
			EndDialogue();
			return;
		}

		StopCoroutine(PrintChar(sentences.Dequeue()));
		StartCoroutine(PrintChar(sentences.Dequeue()));
	}

	public void EndDialogue(){

	}

	IEnumerator PrintChar(string sentence){

		foreach(char c in sentence.ToCharArray()){
			dialogueText.text += c;
			yield return null;
		}
	}
}
