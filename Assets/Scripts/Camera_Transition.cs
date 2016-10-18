using UnityEngine;
using System.Collections;

public class Camera_Transition : MonoBehaviour {
	private bool isTriggered;
	public GameObject cam;
	public GameObject new_Focal;
	public Camera_Follow c_follow;
	public Vector3 target_offset;
	public float panSteps;
	public float zoom;
	private float currentStep;
	public bool isTransition = false;

	// Use this for initialization
	void Start () {
		currentStep = 0;
		isTriggered = false;
		isTransition = false;
		cam = GameObject.Find ("Main_Camera");
		c_follow = cam.GetComponent<Camera_Follow> ();
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (isTransition) {
			Update_Offset (Time.deltaTime);
		}
	}

	void OnTriggerEnter2D () {
		if (!isTriggered) {
			isTriggered = !isTriggered;
			isTransition = !isTransition;
			c_follow.focal = new_Focal;
			c_follow.offset = cam.transform.position - new_Focal.transform.position;
		}
	}

	void Update_Offset(float dt) {
		c_follow.GetComponent<Camera> ().orthographicSize = zoom;
		currentStep += dt;
		Vector3 old_offset = c_follow.offset;
		c_follow.offset = Vector3.Lerp (old_offset, target_offset, currentStep / panSteps);
		//c_follow.offset += 
		if (currentStep == panSteps) {
			isTransition = false;
			isTriggered = false;
			currentStep = 0;
		}

	}
}
