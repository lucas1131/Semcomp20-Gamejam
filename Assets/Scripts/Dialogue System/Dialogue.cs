using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue {

	public delegate void EndDialogFunc();
	public EndDialogFunc diagFunc; 	// Callback to function at the end of dialog
									// for closing textbox for example
	[TextArea(3, 5)]
	public string[] sentences;
	public float[] sentenceDelay;
}
