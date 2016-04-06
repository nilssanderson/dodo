using UnityEngine;
using System.Collections;

public class MoveTo : MonoBehaviour {
	
	
	// ==========================================================================
	// Variables
	// ==========================================================================
	
	public GameObject cameraObject;												// Allow gameObject to be dragged as a component for referencing
	// public Camera camera = Camera.main;									// Allow camera to be dragged as a component for referencing
	private float speed = 1000f;
	// public bool reverse = false;
	public Transform[] wayPointList;
	public Vector3 startOffset = new Vector3(0, 0, 0);	// Offset of the starting object
	public Vector3 endOffset = new Vector3(0, 0, 5);		// Offset of the finishing object that the camera should move to
	// Values are x,y,z
	//	 	x: would be the left and right offset
	//	 	y: would be the up and down offset
	//	 	z: would be closer and further in relation to the camera
	
	public bool trigger = false;												// Activate the trigger
	
	private Transform[] reverseWayPointList;
	private int currentWayPoint = 0;										// var to hold the current waypoint
	private Transform transformRenderer;								// var to hold the Transform of the gameObject
	private Transform targetWayPoint;										// var to hold the target waypoint
	
	
	// ==========================================================================
	// Functions
	// ==========================================================================
	
	void GetGameObjects() {
		// Stop the error messages for an unset gameObject on the TransformController
		if (!cameraObject) {
			cameraObject = GameObject.Find ("CardboardMain");
		}
	}
	
	void walk() {
		if (trigger) {
			
			if (transformRenderer.position == targetWayPoint.position) {
				//				print(currentWayPoint + " - " + transformRenderer.position + " " + targetWayPoint.position);
				trigger = false;
				
				//				if (transformRenderer.position == (wayPointList[this.wayPointList.Length - 1].position)) {
				//					// print(" hello ");
				//					trigger = false;
				//					// print(this.wayPointList);
				//					// wayPointList.reverse();
				//				} else if (currentWayPoint < (this.wayPointList.Length - 1)) {
				//					currentWayPoint++;
				//					targetWayPoint = wayPointList[currentWayPoint];
				//				} else {
				//					currentWayPoint = 0;
				//					targetWayPoint = wayPointList[currentWayPoint];
				//				}
			}
			
			// Can't limit the camera to a point in VR but kept here in case useful for anything else
			// transformRenderer.forward = Vector3.RotateTowards(transformRenderer.forward, (targetWayPoint.position - transformRenderer.position), speed*Time.deltaTime, 0.0f);
			
			// Move towards the target
			transformRenderer.position = Vector3.MoveTowards(transformRenderer.position, targetWayPoint.position, speed*Time.deltaTime);
			//			Destroy(this.gameObject);		// Destroy prompt
		}
	}
	
	void Start() {
		// Get the transform and sprite from the cameraObject
		GetGameObjects();
		transformRenderer = cameraObject.GetComponent<Transform>(); // We are accessing the Transform that is attached to the cameraObject
	}
	
	// Update is called once per frame
	void Update () {
		if (currentWayPoint >= (this.wayPointList.Length - 1)) {
			currentWayPoint = 0;
		}
		
		// Check if we have somewere to walk
		if (currentWayPoint <= (this.wayPointList.Length - 1)) {
			if (targetWayPoint == null) {
				targetWayPoint = wayPointList[currentWayPoint];
			}
			walk();
		}
	}
}
