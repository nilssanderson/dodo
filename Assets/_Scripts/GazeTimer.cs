using UnityEngine;
using System.Collections;

public class GazeTimer : MonoBehaviour {


	// ==========================================================================
	// Variables
	// ==========================================================================

	public string sceneName;			// Let user pick the scene they want to go to next
	public float delayTime;				// Let user decide how long each gaze should be
	private float waitTime = 1f;				// Let user decide how long each gaze should be

	public Transform[] triggerList;		// Add all the objects from editor that should be triggered once gazeSelected is true

	private CardboardHead head;
	//	private Vector3 startingPosition;
	private float delay = 0.0f;
	private float newDelay = 0.0f;
	private float waitDelay = 0.0f;
	private bool gazeSelected = false;

	private GameObject gameMaster;
	private GameObject cameraMain;


	// ==========================================================================
	// Functions
	// ==========================================================================
	
	void GetGameObjects() {
		gameMaster = GameObject.Find ("_GameControl");
		cameraMain = GameObject.Find ("CardboardMain");
	}

	// Set true or false based on whether or not the Editor has set a scene name for the current object
	bool SceneChange() {
		if (string.IsNullOrEmpty (sceneName)) {
			return false;
		} else {
			return true;
		}
	}

	// Set gazeSelected to true
	void GazeSelected() {
		gazeSelected = true;
//		print ("Gaze Selected: " + gazeSelected);
		gazeSelected = false;
	}

	// Only run when first frame loads
	void Start() {
		GetGameObjects ();
//		print ("Gaze Selected: " + gazeSelected);
		head = Camera.main.GetComponent<StereoController>().Head;
		//		startingPosition = transform.localPosition;
	}

	void LoadScene() {
		Application.LoadLevel(sceneName);
	}

	void TriggerEvents() {
		triggerList[0].GetComponent<MoveTo>().trigger = true;
	}

	// Fade out and changeScene or moveToLocation
	void NextAction() {
		print ("fade out");
//		gameMaster.GetComponent<Fading> ().BeginFade (1); // Fade out
	}

	// Fade back in to the new location
	void AfterAction() {
//		gameMaster.GetComponent<Fading> ().BeginFade (-1); // Fade in
//		gameMaster.GetComponent<Fading> ().EndFade (); // Fade in
		triggerList [0].GetComponent<MoveTo> ().trigger = false;
	}
		
	public void MyPointerEnter () {
		GetComponent<Renderer>().material.color = new Color (Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f), 1.0f);
		Debug.Log ("Looking at Cube");
	}

	// Run every frame
	void Update() {
		RaycastHit hit;
		bool isLookedAt = GetComponent<Collider>().Raycast(head.Gaze, out hit, Mathf.Infinity);

		// print ("Time: " + Time.time + "  ||  Delay: " + delay);

		// Is the object being looked at?
		if (isLookedAt) { // Currently looking at object
//			GetComponent<Renderer>().material.color = Color.grey;
//			GetComponent<Renderer>().material.color = new Color (Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f), 1.0f);
//			print("looked at");
			waitDelay = Time.time + (delayTime + waitTime * .4f); // Update the waitDelay
			GetComponent<Transform>().localScale = new Vector3(0.5f, 0.5f, 0.5f);
		} else if (!isLookedAt) {		// Not looking at object
			GetComponent<Renderer>().material.color = Color.white;
//			print("not looking at");
			delay = Time.time + delayTime; // Update the delay
			newDelay = Time.time + (delayTime + waitTime * .2f); // Update the newDelay
		}

		// If looking at object for delay seconds
		if (isLookedAt && (Time.time > delay)) {
			NextAction();
			GazeSelected();
		}

		// Change Scene/Trigger event
		if (Time.time > newDelay) {
//			print ("do change");
			if (SceneChange()) {
				LoadScene();
//				print("its time for scene change");
			} else {
				TriggerEvents();
//				print("its time for events");
			}
		}

		// If its time to fade back to scene
		if (Time.time > waitDelay) {
			print ("fade in");
			AfterAction ();
		}

		// Debug timing events
//		print (Time.time + " : " + delay + " : " + waitDelay);
	}

}
