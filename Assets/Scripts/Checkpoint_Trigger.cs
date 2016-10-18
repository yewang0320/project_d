using UnityEngine;
using System.Collections;

public class Checkpoint_Trigger : MonoBehaviour {
	public GameObject[] objects_to_save;
	public GameObject Control_Unit;
	public Control_Input control;
	public Vector2[,] vector_saved;
	public float[,] float_saved;
	public bool reset; 
	public int list_length; 
	public string [] float_to_save;
	public string[] vector_to_save;
	public bool activated;
	public bool red, blue, pink, green;
	public bool isCurrent;


	// Use this for initialization
	void Start () {
		reset = false;
		isCurrent = false;
		Control_Unit = GameObject.Find ("Control_Unit");
		control = Control_Unit.GetComponent<Control_Input> ();

		list_length = objects_to_save.Length; 
		float_to_save = new string[] {"rotation", "angularVelocity"};
		vector_to_save = new string[] { "position", "velocity" };
		vector_saved = new Vector2[list_length, vector_to_save.Length]; 
		float_saved = new float[list_length, float_to_save.Length];
	}

	void OnTriggerExit2D( Collider2D other) {
		red = control.red;
		blue = control.blue;
		pink = control.pink;
		green = control.green;
		activated = true; 
		control.currentCheckpoint = this;
		for (int i = 0; i < list_length; i++) {
			if (objects_to_save [i].GetComponent<Rigidbody2D> ()) {
				Rigidbody2D rb = objects_to_save [i].GetComponent<Rigidbody2D> ();
				for (int j = 0; j < float_to_save.Length; j++) {
					System.Type T = rb.GetType ();
					System.Reflection.PropertyInfo FI = T.GetProperty (float_to_save [j]);
					Debug.Log (FI.GetValue (rb, null));
					float_saved [i, j] = (float)FI.GetValue (rb, null);
					//FI.SetValue(rb,float_saved[i,j]);
				}
				for (int j = 0; j < vector_to_save.Length; j++) {
					System.Type T = rb.GetType ();
					System.Reflection.PropertyInfo FI = T.GetProperty (vector_to_save [j]);
					vector_saved [i, j] = (Vector2)FI.GetValue (rb, null);
					//FI.SetValue(rb,vector_saved[i,j]);
				}
			}
		}
	}

	// Update is called once per frame
	void Update () {
		if (control.currentCheckpoint == this) {
			if (reset) {
				reset = !reset;
			} 
			reset = Input.GetKeyUp (KeyCode.R);
			if (reset) {
				control.red = red;
				control.blue = blue;
				control.green = green;
				control.pink = pink;
				for (int i = 0; i < list_length; i++) {
					if (objects_to_save [i].GetComponent<Rigidbody2D> ()) {
						Rigidbody2D rb = objects_to_save [i].GetComponent<Rigidbody2D> ();


						for (int j = 0; j < float_to_save.Length; j++) {
							System.Type T = rb.GetType ();
							System.Reflection.PropertyInfo FI = T.GetProperty (float_to_save [j]);
							//float_saved[i,j] = (float)FI.GetValue(rb);
							FI.SetValue (rb, float_saved [i, j], null);
						}
						for (int j = 0; j < vector_to_save.Length; j++) {
							System.Type T = rb.GetType ();
							System.Reflection.PropertyInfo FI = T.GetProperty (vector_to_save [j]);
							//vector_saved[i,j] = (Vector2)FI.GetValue(rb);
							FI.SetValue (rb, vector_saved [i, j], null);
						}
					}

					if (objects_to_save [i].GetComponent<Ball_Control> ()) {
						objects_to_save [i].GetComponent<Ball_Control> ()._count = 0;
						objects_to_save [i].GetComponent<Ball_Control> ().inObjects.Clear ();
					} else if (objects_to_save [i].GetComponent<Compound_Control> ()) {
						objects_to_save [i].GetComponent<Compound_Control> ()._count = 0;
						objects_to_save [i].GetComponent<Compound_Control> ().inObjects.Clear ();
					} else if (objects_to_save [i].GetComponent<Domino_Control> ()) {
						objects_to_save [i].GetComponent<Domino_Control> ()._count = 0;
						objects_to_save [i].GetComponent<Domino_Control> ().inObjects.Clear ();
					}
				}
			}
		}
	}
}
