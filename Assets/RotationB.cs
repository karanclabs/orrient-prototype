using UnityEngine;
using System.Collections;

public class RotationB : MonoBehaviour {
	
	
	Quaternion TargetRotation;
	public static string CubeName = "";
	public static bool Draw = false;
	private bool Rotate, ZoomFx = false;
	private GameObject[] Cube;
	private GameObject TargetCube;
	public static Vector3 TargetIndex;
	private Camera camera1;
	float fov;
	// Use this for initialization
	void Start () 
	{
		Cube = new GameObject[ GameObject.Find( "THE_FINAL_BALL" ).GetComponentsInChildren<MeshRenderer>().Length ];
		camera1 = GameObject.Find( "Main Camera" ).GetComponent<Camera>();
		fov = camera1.fieldOfView;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( Input.GetMouseButtonUp(0) )
		{
			Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
			RaycastHit Hit = new RaycastHit();
			//Debug.Log( "Ray Casted" );
			if( Physics.Raycast( ray, out Hit, 1000f ))
			{
				//Debug.Log(" Ray Hit ");
				Debug.Log( "Name: " + Hit.transform.name );
				Debug.Log( "Target Cube " + Hit.transform.position );
				CubeName = Hit.transform.name;
				TargetIndex = GameObject.Find( Hit.transform.name ).transform.position;
				TargetRotation = Quaternion.LookRotation( Input.mousePosition ); 
				Rotate = true;
				Draw = true;
				ZoomFx = true;
				fov = camera1.fieldOfView;
				float Angle = Vector3.Angle( camera1.gameObject.transform.position, Hit.transform.position ); 
				Debug.Log( "Angle: " + Angle );
				//GameObject.Find( Hit.transform.name ).transform.localScale += new Vector3(0.01f, 0.01f,0.01f);
			}
		}
//		if( Rotate )
//		{
//			GameObject.Find( "THE_FINAL_BALL" ).transform.rotation =  Quaternion.FromToRotation( camera1.gameObject.transform.position, TargetIndex);
//			//if( GameObject.Find( "THE_FINAL_BALL" ).transform.rotation == TargetRotation ) 
//				Rotate = false;
//		}
		
		if( ZoomFx )
		{
			fov = Mathf.Lerp( fov, 20, Time.deltaTime * 10 );
			camera1.fieldOfView = fov;
			//Debug.Log( "Working" );
			if( camera1.fieldOfView <= 21 ) ZoomFx = false;
		}
	}
}
