using UnityEngine;
using System.Collections;

public class Trigger : MonoBehaviour {
	
	
	// ==========================================================================
	// Variables
	// ==========================================================================
	
	//	public string sceneName;			// Let user pick the scene they want to go to next
	public float delayTime;				// Let user decide how long each gaze should be
	//	private float waitTime = 1f;				// Let user decide how long each gaze should be
	//	
	//	public Transform[] triggerList;		// Add all the objects from editor that should be triggered once gazeSelected is true
	//	
	private CardboardHead head;
	private GameObject gazeFuse;
	private Vector3 gazeFuseWidth;
	//	//	private Vector3 startingPosition;
	private float delay = 0.0f;
	//	private float newDelay = 0.0f;
	//	private float waitDelay = 0.0f;
	//	private bool gazeSelected = false;
	//	
	//	private GameObject gameMaster;
	//	private GameObject cameraMain;
	
	public bool trigger = false;
	private bool gazeComplete = false;
	
	
	// ==========================================================================
	// Functions
	// ==========================================================================

	IEnumerator ScaleOverTime(float time) {
		Vector3 originalScale = gazeFuse.transform.localScale;
		Vector3 destinationScale = new Vector3(gazeFuseWidth.x, 0f, gazeFuseWidth.z);
		
		float currentTime = 0.0f;
		
		do {
			gazeFuse.transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / delayTime);
			currentTime += Time.deltaTime;
			yield return null;
		} while (currentTime <= delayTime);

//		gazeFuse.GetComponent<MeshRenderer> ().enabled = false;			// Disable gaze fuse
	}
	
	void GazeTimer (bool isLookedAt) {
		if (isLookedAt) {
//			gazeFuse.GetComponent<MeshRenderer> ().enabled = true;		// Enable gaze fuse
			StartCoroutine(ScaleOverTime(1));		// Shrink gaze width along with the time of the fuse * time.deltatime
		} else if (!isLookedAt) {
//			gazeFuse.GetComponent<MeshRenderer> ().enabled = false;			// Disable gaze fuse
			gazeFuse.transform.localScale = gazeFuseWidth;		// Set scale back to original
		}
	}
	
	void Start () {
		head = Camera.main.GetComponent<StereoController>().Head;
		
		gazeFuse = GameObject.Find ("Gaze Fuse");		// Get gaze fuse GameObject
		gazeFuseWidth = gazeFuse.transform.localScale;		// Get original scale for the gaze timer
	}
	
	void Update () {
		
		RaycastHit hit;
		bool isLookedAt = GetComponent<Collider>().Raycast(head.Gaze, out hit, Mathf.Infinity);
		
		// Is the object being looked at?
		if (isLookedAt) { // Currently looking at object
//			GetComponent<Renderer>().material.color = Color.yellow;
//			print("looked at");
//			GetComponent<Animator> ().enabled = true; // Play animation
		} else if (!isLookedAt) {		// Not looking at object
//			GetComponent<Renderer>().material.color = Color.red;
//			print("not looking at");
			delay = Time.time + delayTime; // Update the delay
			trigger = false;
//			GetComponent<Animator> ().enabled = false; // Stop animation
		}
		
		GazeTimer (isLookedAt);
		
		// If looking at object for delay seconds
		if (isLookedAt && (Time.time > delay)) {
			trigger = true;
		}
	}	
}
