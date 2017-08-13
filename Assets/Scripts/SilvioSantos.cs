using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SilvioSantos : MonoBehaviour {

	new public readonly string name = "Silvio Santos";
	public Text door1, door2, door3;

	public DialogManager dm;
	public PlayerManager pm;
	public GameManager gm;
	public SFXManager SFX;

	public Dialogue initialDialog;
	public Dialogue dialogue;

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
		
		int counter = 0;
		char winnerKey = '\0';

		foreach(GameObject go in pm.players){

			if(go == null) continue;

			PlayerController pc = go.GetComponent<PlayerController>();
			
			if(pc.selected != correct){ // Find losers to remove from the game
				pm.RemoveChar(pc.playerKey);
			
			} else { // Correct answer
				counter++;
				winnerKey = pc.playerKey; // Store potential winner key code
			}
		}

		if(counter == 1) {
			SFX.Play("win");
			gm.Winner(winnerKey); // Only one person remains!
		} else if(counter == 0) {
			SFX.Play("boo");
			gm.Draw(); // Nobody answered correctly
		} else {
			SFX.Play("pergunta");
			ReadQuestionCard(); // Next question
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

		float timeToDrums = 0;
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
		dialogue.sentenceDelay[2] = 10f;
		dialogue.sentenceDelay[3] = 3f;

		// dialogue.sentenceDelay[0] = 0.6f;
		// dialogue.sentenceDelay[1] = 0.6f;
		// dialogue.sentenceDelay[2] = 0.6f;
		// dialogue.sentenceDelay[3] = 0.6f;

		timeToDrums = dialogue.sentenceDelay[0] +
						dialogue.sentenceDelay[1] +
						dialogue.sentenceDelay[2] - 2;
		StartCoroutine(DrumRolls(timeToDrums));

		door1.enabled = true;
		door1.text = answer1;
		door2.enabled = true;
		door2.text = answer2;
		door3.enabled = true;
		door3.text = answer3;

		dm.StartDialog(dialogue, name);
		index++;
	}

	IEnumerator DrumRolls(float timeToDrums){
		yield return new WaitForSeconds(timeToDrums);
		SFX.Play("drums");
	}
}
