using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plCamController : MonoBehaviour {


	Vector2 mslook;
	Vector2 smooth;

	public float sensitivity = 5.0f;
	public float smoothing = 2.0f;

	GameObject character;
	// Use this for initialization
	void Start () {
		character = this.transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		var gg= new Vector2(Input.GetAxisRaw("Mouse X"),Input.GetAxis("Mouse Y"));
		gg = Vector2.Scale (gg, new Vector2(sensitivity * smoothing, sensitivity* smoothing));
		smooth.x = Mathf.Lerp (smooth.x , gg.x, 1f / smoothing);
		smooth.y = Mathf.Lerp (smooth.y , gg.y, 1f / smoothing);

		mslook += smooth;
		transform.localRotation = Quaternion.Euler(-mslook.y,mslook.x,0.0f);
//		transform.localRotation = Quaternion.AngleAxis(-mslook.y,Vector3.right);
//		transform.localRotation = Quaternion.AngleAxis (mslook.x,Vector3.up);
//		character.transform.localRotation = Quaternion.AngleAxis (mslook.x, character.transform.up);
	}
}
