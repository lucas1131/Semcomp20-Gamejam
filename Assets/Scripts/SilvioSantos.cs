using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SilvioSantos : MonoBehaviour {

	new public readonly string name = "Lombordo";
	public readonly float AUDIO_LAG = 0.5f;
	public Text door1, door2, door3;
	public GameObject[] doorBoxes;
	public char winnerKey;

	public DialogManager dm;
	public PlayerManager pm;
	public GameManager gm;
	public SFXManager SFX;

	public Dialogue initialDialog;
	public Dialogue dialogue;
	public Dialogue winDialog, drawDialog;

	[UnityEngine.SerializeField]
	private TextAsset file;
	private string[] cards;
	private string category;
	private string question;
	private string answer1, answer2, answer3;
	private int correct;
	private int index = 0;

	void Awake(){
		initialDialog.endDialog = EndInitialDiag;
		
		initialDialog.sentences[0].delay = 
			initialDialog.sentences[0].voice.length + AUDIO_LAG;

		initialDialog.sentences[1].delay = 
			initialDialog.sentences[1].voice.length + AUDIO_LAG;
	}

	void Start(){
		dialogue = new Dialogue(4);
		dialogue.endDialog = CheckAnswer;
		
		winDialog.endDialog = gm.Reset;
		drawDialog.endDialog = gm.Reset;

		Scramble();
		HideAnswers();
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
		winnerKey = '\0';

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

		HideAnswers();

		// Win
		if(counter == 1) {
			
			SFX.Play("fanfare");
			winDialog.sentences[0].text += winnerKey + "!";
			winDialog.sentences[0].delay = 2.0f;

			winDialog.sentences[1].voice = SFX.GetClip("win");
			winDialog.sentences[1].delay = 
				winDialog.sentences[1].voice.length + AUDIO_LAG;

			doorBoxes[correct-1].gameObject.SetActive(false);
			gm.Winner(winnerKey); // Only one person remains!
		
		} else if(counter == 0) {
		
			SFX.Play("boo");
			drawDialog.sentences[0].voice = SFX.GetClip("lose");
			float time = drawDialog.sentences[0].voice.length/2.8f;
			drawDialog.sentences[0].delay = time;
			drawDialog.sentences[1].delay = 
				drawDialog.sentences[0].voice.length-time;

			doorBoxes[correct-1].gameObject.SetActive(false);
			gm.Draw(); // Nobody answered correctly
		
		} else {
			SFX.Play("pergunta");
			pm.ResetPlayers();
			ReadQuestionCard(); // Next question
		}
	}

	public void EndInitialDiag(){
		ReadQuestionCard();
	}

	public void Reset(){
		index = 0;
		Scramble();
		HideAnswers();
	}

	public void StartInitialDialog(){
		HideAnswers();
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

		dialogue.sentences[0].text = "Próxima rodada!";
		dialogue.sentences[1].text = category;
		dialogue.sentences[2].text = question;
		dialogue.sentences[3].text = "A resposta correta é a número " + correct + "!";
		
		if(index == 0) dialogue.sentences[0].voice = SFX.rodada[0];
		else dialogue.sentences[0].voice = SFX.GetClip("rodada");
		dialogue.sentences[1].voice = SFX.GetClip("c" + category[0]);
		dialogue.sentences[2].voice = null;
		dialogue.sentences[3].voice = null;

		dialogue.sentences[0].delay = dialogue.sentences[0].voice.length + AUDIO_LAG;
		dialogue.sentences[1].delay = dialogue.sentences[1].voice.length + AUDIO_LAG;
		dialogue.sentences[2].delay = 10f;
		dialogue.sentences[3].delay = 2.5f;

		dialogue.sentences[0].diagEvent = null;
		dialogue.sentences[1].diagEvent = null;
		dialogue.sentences[2].diagEvent = PlayDrums;
		dialogue.sentences[3].diagEvent = null;

		door1.text = answer1;
		door2.text = answer2;
		door3.text = answer3;

		dm.StartDialog(dialogue, name);
		GameManager.isBlocked = false;
		index++;
	}

	public void PlayDrums(){
		StartCoroutine(DrumRolls(7.5f));
	}

	public void ShowAnswer(){
		doorBoxes[0].gameObject.SetActive(false);
		doorBoxes[1].gameObject.SetActive(false);
		doorBoxes[2].gameObject.SetActive(false);
		doorBoxes[correct-1].gameObject.SetActive(true);
	}

	public void ShowAnswers(){
		doorBoxes[0].gameObject.SetActive(true);
		doorBoxes[1].gameObject.SetActive(true);
		doorBoxes[2].gameObject.SetActive(true);
	}

	public void HideAnswers(){
		doorBoxes[0].gameObject.SetActive(false);
		doorBoxes[1].gameObject.SetActive(false);
		doorBoxes[2].gameObject.SetActive(false);
	}

	IEnumerator DrumRolls(float timeToDrums){
		ShowAnswers();
		yield return new WaitForSeconds(timeToDrums);
		SFX.Play("drums");
		yield return new WaitForSeconds(1.8f);
		GameManager.isBlocked = true;
		ShowAnswer();
	}
}
