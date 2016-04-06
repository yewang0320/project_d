using UnityEngine;
using System.Collections;

public class Camera_Follow : MonoBehaviour {
	public GameObject focal;

	public Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position - focal.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = focal.transform.position + offset;
	}
}
