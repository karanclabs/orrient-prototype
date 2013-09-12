using UnityEngine;
using System.Collections;

public class RotationB : MonoBehaviour {
	
	
	Quaternion TargetRotation;
	public static string CubeName = "";
	public static bool Draw = false;
	private bool  ZoomFx = false;
	private static bool Rotate = false;
	private GameObject[] Cube;
	private GameObject TargetCube;
	public static Vector3 TargetIndex;
	private Camera camera1;
	public static int ObjectIndex;
	float fov;
	static float angle;
	static float mAngle = 0;
	float tAngle = 0;
	// Use this for initialization
	void Start () 
	{
		Cube = new GameObject[ 51];
		
		camera1 = GameObject.Find( "Main Camera" ).GetComponent<Camera>();
		fov = camera1.fieldOfView;
		
//		for( int i = 1; i < 51; i++ )
//		{
//			Cube[i] = GameObject.Find( "THE_FINAL_BALL" ).GetComponentsInChildren<MeshFilter>()[i].gameObject;
//			
//			//Debug.Log("Pos: " + Cube[i].transform.position.ToString() );
//		}
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
				//ResetObject( GameObject.Find( "THE_FINAL_BALL" ) );
				ArrangeAlongSphere.ClearAllPopups();
				//Debug.Log(" Ray Hit ");
				Debug.Log( "Name: " + Hit.transform.name );
				Debug.Log( "Target Cube " + Hit.transform.position );
				CubeName = Hit.transform.name;
				TargetIndex = GameObject.Find( Hit.transform.name ).transform.TransformPoint(GameObject.Find( Hit.transform.name ).transform.localPosition);
				
				TargetRotation = Quaternion.LookRotation( Input.mousePosition ); 
				Rotate = true;
				Draw = true;
				ZoomFx = true;
				fov = camera1.fieldOfView;
				ObjectIndex = 51 - GetIndexOfTarget( Hit.transform.name );
				TargetIndex = ArrangeAlongSphere.GetCubePosition( ObjectIndex );
				float Angle = Vector3.Angle( camera1.gameObject.transform.position, Hit.transform.position ); 
				Debug.Log( "Angle: " + Angle );
					
				//GameObject.Find( Hit.transform.name ).transform.localScale += new Vector3(0.01f, 0.01f,0.01f);
			}
		}
		if( Rotate )
		{
			angle = Mathf.Rad2Deg * Mathf.Acos ( Vector3.Dot( camera1.gameObject.transform.position, /*ArrangeAlongSphere.StaticCuceInitialPositions[ 51 - ObjectIndex ]*/ TargetIndex )/ ( Vector3.Magnitude( camera1.gameObject.transform.position ) * Vector3.Magnitude( /*ArrangeAlongSphere.StaticCuceInitialPositions[ 51 - ObjectIndex ] */TargetIndex)));
			Debug.Log( "TargetIndex: " + TargetIndex + " Orignal: " +  ArrangeAlongSphere.StaticCuceInitialPositions[ 51 - ObjectIndex ]  );
			Vector3 Axis = Vector3.Cross( /*ArrangeAlongSphere.StaticCuceInitialPositions[ 51 - ObjectIndex ] */TargetIndex,  camera1.transform.position/*CentreViewPortToWorldVector()*/ );
			Debug.Log( "Axis: " + Axis + " angle: " + angle );
			//GameObject.Find( "THE_FINAL_BALL" ).transform.position = Vector3.Slerp( transform)
			//ApplyRotation( Axis, angle );
			
			 //tAngle = Mathf.Lerp( 0, angle, Time.deltaTime*10); 
			//float diff = angle - tAngle;
			tAngle += Time.deltaTime * 100;
		//	if( tAngle > angle )
			GameObject.Find( "THE_FINAL_BALL" ).transform.RotateAround( Vector3.zero, Axis, Time.deltaTime * 100 );//*/rotation =  Quaternion.AngleAxis( tAngle, Axis ); //Quaternion.FromToRotation( camera1.gameObject.transform.position, TargetIndex);
			//Debug.Log( GameObject.Find( "THE_FINAL_BALL" ).transform.rotation );
			if( tAngle >= angle )
			{
				Rotate = false;
				tAngle = 0;
			}
			//if( GameObject.Find( "THE_FINAL_BALL" ).transform.rotation == TargetRotation ) 
			//Rotate = false;
		}
	//	mAngle = Mathf.Lerp( 0, angle, Time.deltaTime*10 );
		if( ZoomFx )
		{
			fov = Mathf.Lerp( fov, 27, Time.deltaTime * 10 );
			camera1.fieldOfView = fov;
			//Debug.Log( "Working" );
			if( camera1.fieldOfView <= 28 ) ZoomFx = false;
		}
		
		
	}
	
	Vector3 CentreViewPortToWorldVector()
	{
		Ray ray = Camera.main.ScreenPointToRay( new Vector2( Screen.width/2, Screen.height / 2 ) );
		return ray.origin;
	}
	
	int GetIndexOfTarget( string objectName )
	{
		objectName = objectName.Replace( "Cube", "" );
		objectName = objectName.Trim();
		return int.Parse( objectName ); 
	}
	
	void ResetObject( GameObject Obj )
	{
		Obj.transform.position = new Vector3(0,0,0);
		Obj.transform.rotation = new Quaternion( 0,0,0,1);
	}
	
	void ApplyRotation( Vector3 Axis, float Angle )
	{
//		float AngleLerp = 0;
//		AngleLerp = Mathf.Lerp( 0, Angle, Time.deltaTime*10 );
//		GameObject.Find( "THE_FINAL_BALL" ).transform.RotateAround( Vector3.zero, Axis, AngleLerp );
//		if( AngleLerp >= Angle-1 )
//			Rotate = false;
		
		
	}
	
}
