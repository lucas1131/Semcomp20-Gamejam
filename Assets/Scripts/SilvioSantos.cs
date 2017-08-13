using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SilvioSantos : MonoBehaviour {

	new public readonly string name = "Silvio Santos";
	public DialogManager dm;
	public Text door1, door2, door3;

	public Dialogue initialDialog;
	public Dialogue dialogue;

	public PlayerManager pm;

	[UnityEngine.SerializeField]
	private TextAsset file;
	private string[] cards;
	private string category;
	private string question;
	private string answer1, answer2, answer3;
	private int correct;
	private int index = 0;

	void Awake(){
		initialDialog.diagFunc = EndInitialDiag;
	}

	void Start(){
		dialogue = new Dialogue();
		dialogue.diagFunc = CheckAnswer;
		Scramble();
	}

	public void Scramble(){
		cards = file.text.Split('\n');

		for(int i = 0; i < cards.Length; i++){
			int j = (int) UnityEngine.Random.Range(0, cards.Length);

			string aux = cards[i];
			cards[i] = cards[j];
			cards[j] = aux;
		}
	}

	public void CheckAnswer(){
		foreach(GameObject go in pm.players){

			if(go == null) continue;

			PlayerController pc = go.GetComponent<PlayerController>();
			
			if(pc.selected != correct){
				Debug.Log("key: " + pc.playerKey);
				pm.RemoveChar(pc.playerKey);
			}
		}
	}

	public void EndInitialDiag(){
		ReadQuestionCard();
	}

	public void Reset(){
		index = 0;
		Scramble();
	}

	public void StartInitialDialog(){
		dm.StartDialog(initialDialog, name);
	}

	public void ReadQuestionCard(){

		string[] aux = cards[index].Split('\t');

		category = aux[0];
		question = aux[1];
		answer1 = aux[2];
		answer2 = aux[3];
		answer3 = aux[4];
		correct = Int32.Parse(aux[5]);

		dialogue.sentences = new string[4];
		dialogue.sentences[0] = "Pergunta número " + (index + 1);
		dialogue.sentences[1] = category;
		dialogue.sentences[2] = question;
		dialogue.sentences[3] = "A resposta correta é a número " + correct + "!";

		dialogue.sentenceDelay = new float[4];
		dialogue.sentenceDelay[0] = 2f;
		dialogue.sentenceDelay[1] = 1.5f;
		dialogue.sentenceDelay[2] = 1.5f;
		dialogue.sentenceDelay[3] = 3f;

		dialogue.sentenceDelay[0] = 0.3f;
		dialogue.sentenceDelay[1] = 0.3f;
		dialogue.sentenceDelay[2] = 0.3f;
		dialogue.sentenceDelay[3] = 0.3f;

		door1.enabled = true;
		door1.text = answer1;
		door2.enabled = true;
		door2.text = answer2;
		door3.enabled = true;
		door3.text = answer3;

		dm.StartDialog(dialogue, name);
		index++;
	}
}
