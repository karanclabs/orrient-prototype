using UnityEngine;
using System.Collections;

public class rotate : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		 
		//transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler(90,0,0), (float)0.001 );
		
		//Input.GetTouch(0);
	if(	Input.GetMouseButton(0) ){
		
	     //print (transform.eulerAngles);
			
			print(Input.mousePosition);
			
			//transform.localEulerAngles.x =45;
			
			
			
			
		}
	}

















}
