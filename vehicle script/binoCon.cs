using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class binoCon : MonoBehaviour {
	public Camera binoCam;
	public GameObject ovr;
	public GameObject scbt;
	public bool bino= false;
	private bool loc= false;
	public Text distance;
	private float prv;

	Ray ray;
	// Use this for initialization
	void Start () {
		binoCam = GetComponentInChildren<Camera> ();
	}
	
	// Screen.width*0.5f,Screen.height*0.5f,0f
	void Update () {
		ray = binoCam.ScreenPointToRay (new Vector3(200,200,0));
		RaycastHit hit;
//		Debug.DrawRay (ray.origin,ray.direction*60,Color.red);
//		if(Physics.Raycast(ray ,out hit ,100)){
		Physics.Raycast(ray ,out hit );
		if( Physics.Raycast(ray ,out hit) ){
			if(loc){
				int a = (int)Mathf.Ceil (hit.distance);
				distance.text = a.ToString () + "m";
				Debug.Log ("name of :"+ hit.transform.name +" distance :" + a);
			}

//			Debug.Log ("name of "+ hit.distance);
		}

		Debug.DrawRay (ray.origin,ray.direction*60,Color.red);

		if(Input.GetButtonDown("Fire2")){
			bino = !bino;
			if (bino) {
				Setbino ();
			} else {
				UnSetbino ();
			}
		}


//		binoCam.fieldOfView = 15f;
	}

	void FixedUpdate(){
		
	}

	void Setbino (){
		loc = true;
		distance.gameObject.SetActive (true);
		ovr.SetActive (true);
		scbt.SetActive (false);
		prv = binoCam.fieldOfView;
		binoCam.fieldOfView = 15f;
	}
	void UnSetbino (){
		loc = false; 
//		ovr.SetActive (false);
		binoCam.fieldOfView = prv;
		distance.gameObject.SetActive (false);
		scbt.SetActive (true);
		ovr.SetActive (false);
//		bino = false;
	}

}
