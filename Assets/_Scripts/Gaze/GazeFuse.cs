using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GazeFuse : MonoBehaviour {
	
	
	// ==========================================================================
	// Variables
	// ==========================================================================

	public GameObject gazeGameObject;
	private Image image;
	
	
	// ==========================================================================
	// Functions
	// ==========================================================================

	void Start() {
		image = GetComponent<Image>();
	}
	
	void Update() {
		if (gazeGameObject == null || GazeInputModule.gazeGameObject == gazeGameObject) {
			FuseAmountChanged(GazeInputModule.gazeFraction);
		}
	}

	void FuseAmountChanged(float fuseAmount) {
		if (image != null) {
			image.fillAmount = fuseAmount;
		}
	}

}
