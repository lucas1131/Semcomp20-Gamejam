using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour {

	new public string name;

	[TextArea(3, 5)]
	public string[] sentences;
}
