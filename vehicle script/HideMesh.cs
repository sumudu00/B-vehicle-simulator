using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HideMesh : MonoBehaviour {
	public GameObject[] Meshes;
	//public GameObject GunCamera;
	//public bool srt = false;
	//[SerializeField]
	//private string Commanderip;
	//[SerializeField]
	//private bool Commanderip_check = false;
	// Use this for initialization
	void Start () {
		//Meshes = GameObject.FindGameObjectsWithTag ("btr");
		//GunCamera = GameObject.FindGameObjectWithTag ("Gun_cam")as GameObject;
		//Commanderip = Network.player.ipAddress;
		//Debug.Log ("my ip "+Commanderip);
//		if (!isServer) {
			
//			/*if (string.Compare (Commanderip, "192.168.1.103") == 0) {
//				Commanderip_check = true;
//				GunCamera.SetActive (true);
//				GunCamera.gameObject.GetComponent<Camera> ().enabled = true;
//				Debug.Log ("commander");

//			}*/

//			for (int i = 0; i < Meshes.Length; i++) {
//				Debug.Log ("Server meshes");
//				Meshes [i].gameObject.GetComponent<MeshRenderer> ().enabled = false;
//			}
//		} else {
////			for (int i = 0; i < Meshes.Length; i++) {
//				Debug.Log ("Server meshes");
////				Meshes [i].gameObject.GetComponent<MeshRenderer> ().enabled = true;
////			}
//			//if (string.Compare (Commanderip, "192.168.1.132") == 0) {
//				//				Commanderip_check = true;
//				//				GunCamera.SetActive (true);
//								Debug.Log ("commander vvbbbb xc  ");
//				//
//							//}

//			//GunCamera.gameObject.GetComponent<Camera> ().rect = new Rect (0.01f, 0.73f, 0.19f, 0.25f);
//		}

	}
	
	// Update is called once per frame
	/*void Update () {
		if (isServer) {
//			GunCamera = GameObject.FindGameObjectWithTag ("Gun_cam")as GameObject;
			if (Input.GetKeyDown (KeyCode.Alpha0)) {
				Debug.Log ("Pressed ");
				srt = !srt;

				if (srt) {
					Debug.Log ("srt" + srt);

					GunCamera.SetActive (true);
					Debug.Log ("PAssed ");
					//					camera.gameObject.GetComponent<Camera> ().;
					GunCamera.gameObject.GetComponent<Camera> ().rect = new Rect (0.01f, 0.73f, 0.19f, 0.25f);

				} else {
					Debug.Log ("not PAssed ");

					GunCamera.SetActive (false);

				}
			}
		} else {
			if (string.Compare (Commanderip, "192.168.1.103") == 0) {
				
				Debug.Log ("commander");
				if (!srt) {

					if (Input.GetKeyDown (KeyCode.Alpha0)) {
						srt = !srt;
						if (srt) {
							GunCamera.SetActive (false);//
						} else {
							Commanderip_check = true;
							GunCamera.SetActive (true);
							GunCamera.gameObject.GetComponent<Camera> ().enabled = true;
//							GunCamera.SetActive (true);
							//							GunCamera.SetActive (true);
//							GunCamera.gameObjects.GetComponent<Camera> ().enabled = true;//
						}
					}
				}

			} else {
				GunCamera.SetActive (false);
			}


//			GunCamera.SetActive (false);
//			if(Commanderip_check){
//				if (!srt) {
//
//					if (Input.GetKeyDown (KeyCode.Alpha0)) {
//						srt = !srt;
//						if (srt) {
//							GunCamera.SetActive (false);//
//						} else {
//							GunCamera.SetActive (true);
////							GunCamera.SetActive (true);
//							GunCamera.gameObject.GetComponent<Camera> ().enabled = true;//
//						}
//					}
//				}
//			}
		}
	}*/
}
