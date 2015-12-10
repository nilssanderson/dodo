using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public GUIText countText;
	public GUIText winText;
	private int count;

	void Start () {
		count = 0;
		SetCountText ();
		winText.text = "";
	}

	// Update is called once per frame
	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal") * speed;
		float moveVertical = Input.GetAxis ("Vertical") * speed;

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		GetComponent<Rigidbody>().AddForce (movement * speed * Time.deltaTime);
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "pickup") {
			other.gameObject.SetActive (false);
			count++;
			SetCountText ();
		}
	}

	void SetCountText () {
		countText.text = "Count " + count.ToString ();
		if(count >= 10) {
			winText.text = "YOU WIN!";
			Destroy(countText);
		}
	}
}
