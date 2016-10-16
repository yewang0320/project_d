using UnityEngine;
using System.Collections;

public class Control_Input : MonoBehaviour {
	public bool red, blue, pink, green;
	// Use this for initialization
	void Start () {
		red = true;
		blue = true;
		pink = true;
		green = true;
	}

	// Update is called once per frame
	void Update () {
		bool redToggled = Input.GetKeyDown (KeyCode.D);
		bool blueToggled = Input.GetKeyDown (KeyCode.S);
		bool pinkToggled = Input.GetKeyDown (KeyCode.A);
		bool greenToggled = Input.GetKeyDown (KeyCode.W);

		bool reset = Input.GetKeyUp (KeyCode.R);

		if (redToggled) {
			red = !red;
		} else if (blueToggled) {
			blue = !blue;
		} else if (pinkToggled) {
			pink = !pink;
		} else if (greenToggled) {
			green = !green;
		}
			
	}
}
