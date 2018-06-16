using UnityEngine;
using System.Collections;

public class livingBirdsDemoScript : MonoBehaviour {
	public lb_BirdController birdControl;
	public Camera camera1;
	public Camera camera2;

	Camera currentCamera;
	bool cameraDirections = true;
	Ray ray;
	RaycastHit[] hits;

	void Start(){
		currentCamera = Camera.main;
		birdControl = GameObject.Find ("_livingBirdsController").GetComponent<lb_BirdController>();
		SpawnSomeBirds();
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)){
			camera1.transform.localEulerAngles += new Vector3(0.0f,90.0f,0.0f)*Time.deltaTime;
		}
		if(Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)){
			camera1.transform.localEulerAngles -= new Vector3(0.0f,90.0f,0.0f)*Time.deltaTime;
		}
		if(Input.GetMouseButtonDown(0)){
			ray = currentCamera.ScreenPointToRay(Input.mousePosition);
			hits = Physics.RaycastAll (ray);
			foreach(RaycastHit hit in hits){
				if (hit.collider.tag == "lb_bird"){
					hit.transform.SendMessage ("KillBirdWithForce",ray.direction*500);
					break;
				}
			}
		}
	}

	void OnGUI() {
		if (GUI.Button(new Rect(10, 10, 150, 50), "Pause"))
			birdControl.SendMessage("Pause");
		
		if (GUI.Button(new Rect(10, 70, 150, 30), "Scare All"))
			birdControl.SendMessage("AllFlee");

		if (GUI.Button(new Rect(10, 110, 150, 50), "Change Camera"))
			ChangeCamera();

		if (GUI.Button(new Rect(10, 170, 150, 50), "Revive Birds"))
			birdControl.BroadcastMessage("Revive");


		if(cameraDirections){
			GUI.Label (new Rect (170, 10, 1014, 20), "version 1.1");
			GUI.Label (new Rect (170, 30, 1014, 20), "USE ARROW KEYS TO PAN THE CAMERA");
			GUI.Label (new Rect (170, 50, 1014, 20), "Click a bird to kill it, you monster.");
		}
	}

	IEnumerator SpawnSomeBirds(){
		yield return 2;
		birdControl.SendMessage ("SpawnAmount",10);
	}

	void ChangeCamera(){
		if(camera2.gameObject.activeSelf){
			camera1.gameObject.SetActive(true);
			camera2.gameObject.SetActive(false);
			birdControl.SendMessage("ChangeCamera",camera1);
			cameraDirections = true;
			currentCamera = camera1;
		}else{
			camera1.gameObject.SetActive(false);
			camera2.gameObject.SetActive(true);
			birdControl.SendMessage("ChangeCamera",camera2);
			cameraDirections = false;
			currentCamera = camera2;
		}
	}
}
