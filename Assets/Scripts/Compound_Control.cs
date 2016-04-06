using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Compound_Control : MonoBehaviour {
	public char color;
	private bool visibility = true;
	public GameObject Control_Unit;
	public Control_Input control_script;
	private Rigidbody2D rb;
	private PolygonCollider2D bc, bc_tri;
	private MeshRenderer rend;
	private Material mat_on, mat_off;
	public Material m_red_on, m_blue_on, m_pink_on, m_green_on, m_black;
	public Material m_red_off, m_blue_off, m_pink_off, m_green_off;
	Vector2 savedVelocity = new Vector2 (0,0);
	float savedAngularVelocity = 0;
	private int _count;
	private List<GameObject> inObjects;

	// Use this for initialization
	void Start () {
		inObjects = new List<GameObject>();
		_count = 0;
		Control_Unit = GameObject.Find ("Control_Unit");
		control_script = Control_Unit.GetComponent<Control_Input> ();
		if (GetComponent<Rigidbody2D>() != null) {
			rb = GetComponent<Rigidbody2D> ();
		}
		foreach (PolygonCollider2D bc2d in GetComponents<PolygonCollider2D>()) {
			if (bc2d.usedByEffector == false) {
				bc = bc2d;
			} else if (bc2d.usedByEffector == true) {
				bc_tri = bc2d;
			}
		}
		rend = GetComponent<MeshRenderer> ();
		switch (color) 
		{
		case 'r':
			visibility = control_script.red;
			mat_on = m_red_on;
			mat_off = m_red_off;
			break;
		case 'b':
			visibility = control_script.blue;
			mat_on = m_blue_on;
			mat_off = m_blue_off;
			break;
		case 'p':
			visibility = control_script.pink;
			mat_on = m_pink_on;
			mat_off = m_pink_off;
			break;
		case 'g':
			visibility = control_script.green;
			mat_on = m_green_on;
			mat_off = m_green_off;
			break;
		default:
			mat_on = m_black;
			mat_off = m_black;
			break;
		}
	}

	void Update() {
		
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		switch (color) 
		{
		case 'r':
			visibility = isEmpty() && control_script.red;
			break;
		case 'b':
			visibility = isEmpty() && control_script.blue;
			break;
		case 'p':
			visibility = isEmpty() && control_script.pink;
			break;
		case 'g':
			visibility = isEmpty() && control_script.green;
			break;
		}
		if (visibility) {
			if (rend.sharedMaterial == mat_off) {
				//rb.velocity.Set (savedVelocity.x, savedVelocity.y);
				if (rb) {
					rb.velocity = savedVelocity;
					rb.angularVelocity = savedAngularVelocity;
					rb.isKinematic = false;
				}
			}
			rend.sharedMaterial = mat_on;


		} else {
			if (rend.sharedMaterial == mat_on) {
				if (rb) {
					savedVelocity = rb.velocity;
					savedAngularVelocity = rb.angularVelocity;
					rb.isKinematic = true;
				}
			}
			rend.sharedMaterial = mat_off;
		}

		if (GetComponent<Rigidbody2D>() != null) {
			rb.isKinematic = !visibility;
		}
		bc.isTrigger = !visibility && rend.isVisible;
	}

	void OnTriggerEnter2D( Collider2D other) {
		if (!visibility && bc_tri.IsTouching(other) && !inObjects.Contains(other.gameObject)) {
			//++_count;
			inObjects.Add (other.gameObject);
		} 

	}

	void OnTriggerExit2D (Collider2D other) {
		if (!visibility && bc.IsTouching(other) && !bc_tri.IsTouching(other)) {
			//--_count;
			inObjects.Remove (other.gameObject);
		} 
	}

	void OnTriggerStay2D (Collider2D other) {
		if (!visibility && !inObjects.Contains(other.gameObject) && bc_tri.IsTouching(other)) {
			inObjects.Add(other.gameObject);
		}
	}

	/*void OnCollisionStay2D (Collision2D other) {
		if (true && !inObjects.Contains (other.gameObject)) {
			inObjects.Add (other.gameObject);
		}
	}

	void OnCollisionEnter2D (Collision2D other) {
		if (visibility) {
			inObjects.Add (other.gameObject);
		}
	}

	void OnCollisionExit2D (Collision2D other) {
		if (visibility) {
			inObjects.Remove (other.gameObject);
		}
	}*/

	bool isEmpty() {
		_count = 0;
		foreach (GameObject obj in inObjects) {
			if (obj.GetComponent<PolygonCollider2D>()) {
				if (obj.GetComponent<PolygonCollider2D> ().isTrigger == false) {
					_count++;
				}
			} else if (obj.GetComponent<CircleCollider2D>()) {
				if (obj.GetComponent<CircleCollider2D> ().isTrigger == false) {
					_count++;
				}
			} else if (obj.GetComponent<BoxCollider2D>()) {
				if (obj.GetComponent<BoxCollider2D> ().isTrigger == false) {
					_count++;
				}
			}
		}
		Debug.Log (_count);
		if (_count == 0) {
			return true;
		} else if (_count != 0 && visibility) {
			return true;
		} else {
			return false;
		}
	}
			
}
