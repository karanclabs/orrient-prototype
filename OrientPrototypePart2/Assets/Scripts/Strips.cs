using UnityEngine;
using System.Collections;

public class Strips : MonoBehaviour {
	
	float Radius;	
	private Vector3[] Axis = new Vector3[] { new Vector3( -1,1,0 ),
											 new Vector3( -1,-1,0 ),
											 new Vector3( 1,-1,0 ),
											 new Vector3( 1,1,0 ),
											 new Vector3( 0,1,1 ),
											 new Vector3( 0,1,-1 ),
											 new Vector3( 0,-1,-1 ),
											 new Vector3( 1,1,1 ),
											 new Vector3( 1,0,1 ),
											 new Vector3( 1,0,-1 ),
											 new Vector3( 3,5,2 ),
											};
	private Vector3[] SphereVertices;
	public GameObject MeshObject;
	private int nHorizontalCircles = 11;
	private Vector3[] nHorizontalCentres;
	private LineRenderer CircularLine;
	public GameObject CircleObject;
	private static int CircleNo = 0;
	public Material[] Lambert;
	public GameObject LineCollider;
	static int LineNo = 0;
	static float Angle = 0, AngleToMove, AngleCamera, AngleCameraToMove;
	static Vector3 OnAxis;
	bool Rotate = false, RotateCamera = false, mRotate = false;
	static Vector3 AxisH;
	static float AngleH;
	static int lineIndex;
	static Vector3 CameraAxis;
	// Use this for initialization
	void Start () 
	{
		nHorizontalCentres = new Vector3[ nHorizontalCircles + 1 ];        // max(y) and min(y) has n+1 equal points
		Radius = renderer.bounds.max.x;
		float CircleInterval = 2 * Radius / nHorizontalCircles;
		float yCoord = Radius;
		GameObject.Find( "Line" ).transform.parent = transform;
		CircularLine = GameObject.Find( "Line" ).GetComponent< LineRenderer >();
		
		DrawCircleOnAxis( new Vector3( -1,1,0 ), Radius/(2.1f), 1,0 );
		DrawCircleOnAxis( new Vector3( 1,1,0 ), Radius/(2.08f), 1.5f,2 );
		DrawCircleOnAxis( new Vector3( 1,1,1 ), Radius/(2.04f), 2,3 );
		DrawCircleOnAxis( new Vector3( -1,1,-1 ), Radius/(2.05f), 2.5f,4 );
		DrawCircleOnAxis( new Vector3( 0,1,1 ), Radius/(2.04f), 3,1 );
		DrawCircleOnAxis( new Vector3( -15,3,10 ), Radius/(2.06f), 2.5f,2 );
		DrawCircleOnAxis( new Vector3( 1,-1,-1 ), Radius/(2.04f), 2,3 );
		DrawCircleOnAxis( new Vector3( 0,-1,1 ), Radius/(2.00f), 1.5f,4 );
		DrawCircleOnAxis( new Vector3( 3,5,-1 ), Radius/(2.02f), 1,1 );
		//DrawCircleOnAxis( new Vector3( -1,-1,0 ), Radius/2, 2,0 );
//		DrawCircleOnAxis( new Vector3( 4,4,5 ), Radius/1.98f, 2,1 );
////		DrawCircleOnAxis( new Vector3( -9,-3, 7 ), Radius/2, 2,0 );
////		DrawCircleOnAxis( new Vector3( 5, -4 , 6 ), Radius/2, 2,0 );
////		DrawCircleOnAxis( new Vector3( -4,-9, -1), Radius/2, 2,0 );
//		DrawCircleOnAxis( new Vector3( 4, 5,-9 ), Radius/1.96f, 3,2 );
//		DrawCircleOnAxis( new Vector3( 8,-6,12 ), Radius/1.94f, 4,3 );
//		DrawCircleOnAxis( new Vector3( 1,0,0 ), Radius/1.92f, 5,4 );
//	//	DrawCircleOnAxis( new Vector3( 6,1,0 ), Radius/2, 2,0 );
//		DrawCircleOnAxis( new Vector3( 4,2,3 ), Radius/1.9f, 1,5 );
//		DrawCircleOnAxis( new Vector3( 30,12,0 ), Radius/1.88f, 2,6 );
//		DrawCircleOnAxis( new Vector3( 8,1,1 ), Radius/1.86f, 3,7 );
//		DrawCircleOnAxis( new Vector3( 67,51,3 ), Radius/1.84f, 4,8 );
//		DrawCircleOnAxis( new Vector3( 9,4,-2 ), Radius/1.82f, 5,9 );
//		for( int i = 0; i < 20; i++ )
//		{
//			Axis[i] = new Vector3( Random.Range ( (float)-10, (float)10), Random.Range ((float)-10, (float)10), Random.Range ( (float)-10, (float)10 ) );
//			DrawCircleOnAxis( Axis[i], Radius/2, Random.Range( 1,5), Random.Range( 0, 9 ));
//			
//		}
//		for( int i = 0; i <= nHorizontalCircles; i++ )
//		{
//			nHorizontalCentres[i] = new Vector3( 0, yCoord, 0);
//			yCoord -= CircleInterval;
//			Debug.Log( nHorizontalCentres[i] );
//		}
		
//		for( int i = 0; i < nHorizontalCentres.Length; i++ )
//		{
//			DrawHorizontalCircle( nHorizontalCentres[i], 100 );
//		}
		
//		DrawCircleOnAxis( new Vector3( 1, 1, 0), Radius/2 ,2);
	}
	
	void DrawMeshesOnAlternateCircles()
	{
		Vector3[] UpperCirclePoint = new Vector3[100];
		float UpperRadius = Mathf.Sqrt( Mathf.Pow( Radius , 2 ) - Mathf.Pow( nHorizontalCentres[5].y, 2 ));
		Vector3[] LowerCirclePoint = new Vector3[100];
		float LowerRadius = Mathf.Sqrt( Mathf.Pow( Radius , 2 ) - Mathf.Pow( nHorizontalCentres[6].y, 2 ));
		float Angle = 0;
		for( int i = 0; i < 100; i++ )
		{
			UpperCirclePoint[i] = new Vector3( UpperRadius * Mathf.Cos( Angle * Mathf.Deg2Rad ), nHorizontalCentres[5].y, UpperRadius * Mathf.Sin( Angle * Mathf.Deg2Rad ));
			LowerCirclePoint[i] = new Vector3( LowerRadius * Mathf.Cos( Angle * Mathf.Deg2Rad ), nHorizontalCentres[6].y, LowerRadius * Mathf.Sin( Angle * Mathf.Deg2Rad ));
			Angle += 500/100;
		}
		
		
	}
	
	void DrawCircleOnAxis( Vector3 N, float Radius, float Popularity, int MaterialIndex )
	{

		N = N / N.magnitude;
		Vector3 U = new Vector3();
		if     ( N.x != 0 && N.y != 0 && N.z != 0 )  U = ( new Vector3( - N.x,   N.y,   N.z ) / ( new Vector3( - N.x,   N.y,   N.z ) ).magnitude ) * Radius;
		else if( N.x != 0 && N.y != 0 && N.z == 0 )  U = ( new Vector3( - N.x,   N.y,   N.z ) / ( new Vector3( - N.x,   N.y,   N.z ) ).magnitude ) * Radius;
		else if( N.x != 0 && N.y == 0 && N.z != 0 )  U = ( new Vector3( - N.x,   N.y,   N.z ) / ( new Vector3( - N.x,   N.y,   N.z ) ).magnitude ) * Radius;
		else if( N.x == 0 && N.y != 0 && N.z != 0 )  U = ( new Vector3(   N.x, - N.y,   N.z ) / ( new Vector3(   N.x, - N.y,   N.z ) ).magnitude ) * Radius;
		else if( N.x != 0 && N.y == 0 && N.z == 0 )  U = ( new Vector3(   N.x,   N.x,   N.z ) / ( new Vector3(   N.x,   N.x,   N.z ) ).magnitude ) * Radius;
		else if( N.x == 0 && N.y != 0 && N.z == 0 )  U = ( new Vector3(   N.y,   N.y,   N.z ) / ( new Vector3(   N.y,   N.y,   N.z ) ).magnitude ) * Radius;
		else if( N.x == 0 && N.y == 0 && N.z != 0 )  U = ( new Vector3(   N.z,   N.y,   N.z ) / ( new Vector3(   N.z,   N.y,   N.z ) ).magnitude ) * Radius;

		Vector3 V = ( Vector3.Cross( N, U ) / Vector3.Cross( N, U ).magnitude ) * Radius;
		var g = Instantiate( CircleObject, Vector3.zero, Quaternion.identity ) as GameObject;
		g.transform.parent = transform;
		g.transform.name = "Line" + LineNo.ToString();
		
		LineRenderer Circle = g.GetComponent<LineRenderer>();
		Circle.material = Lambert[ MaterialIndex ];
		g.GetComponent<LineRenderer>().useWorldSpace = false;
		Circle.SetVertexCount( 100 );
		Circle.SetWidth( (float)Popularity/10, (float)Popularity/10 );
		float Angle = 0;
		for ( int i = 0; i < 100; i++ )
		{
			//Circle.SetPosition( i, new Vector3( Radius * Mathf.Cos( Angle * Mathf.Deg2Rad ), Radius * Mathf.Sin( Angle * Mathf.Deg2Rad ), 0 ) );
			Circle.SetPosition( i, new Vector3( Radius * ( U.x * Mathf.Cos ( Angle * Mathf.Deg2Rad ) + V.x * Mathf.Sin ( Angle * Mathf.Deg2Rad ) ),
												Radius * ( U.y * Mathf.Cos ( Angle * Mathf.Deg2Rad ) + V.y * Mathf.Sin ( Angle * Mathf.Deg2Rad ) ), 
												Radius * ( U.z * Mathf.Cos ( Angle * Mathf.Deg2Rad ) + V.z * Mathf.Sin ( Angle * Mathf.Deg2Rad ) ) ) );
			var Collide = Instantiate( LineCollider, new Vector3( Radius * ( U.x * Mathf.Cos ( Angle * Mathf.Deg2Rad ) + V.x * Mathf.Sin ( Angle * Mathf.Deg2Rad ) ),
												Radius * ( U.y * Mathf.Cos ( Angle * Mathf.Deg2Rad ) + V.y * Mathf.Sin ( Angle * Mathf.Deg2Rad ) ), 
												Radius * ( U.z * Mathf.Cos ( Angle * Mathf.Deg2Rad ) + V.z * Mathf.Sin ( Angle * Mathf.Deg2Rad ) ) ), Quaternion.identity ) as GameObject;
			Collide.transform.parent = GameObject.Find( "Line" + LineNo.ToString() ).transform;
			Collide.transform.name = "Collider" + i;
			Debug.Log( (Radius * Mathf.Cos( Angle )).ToString() );
			Angle += 3.6f;
		}
		
		LineNo++;
	}
	
	
	void DrawHorizontalCircle( Vector3 Centre, int Points )
	{
		var g = Instantiate( CircleObject, Centre, Quaternion.identity ) as GameObject;
		g.AddComponent<MeshCollider>();
		//g.transform.LookAt( transform );
		g.transform.parent = transform;
		g.transform.name = "Circle" + CircleNo;
		CircleNo++;
	//	g.GetComponent<LineRenderer>().useWorldSpace = false;
		LineRenderer CircularLine1 =g.GetComponent<LineRenderer>();
		CircularLine1.SetVertexCount( Points + 1 );
		CircularLine1.SetWidth( 0.5f, 0.5f );
//		CircularLine1.material = Lambert;
		float CurrentRadius = Mathf.Sqrt( Mathf.Pow( Radius + 0.3f , 2 ) - Mathf.Pow( Centre.y, 2 ));
		float xCoord = Mathf.Sqrt( Mathf.Pow( CurrentRadius, 2) - Mathf.Pow( Centre.y, 2) );
		//float CurrentRadius = xCoord;
		float yCoord = Centre.y;
		Vector3 StartPosition = new Vector3( xCoord, yCoord, 0);
		float Angle = 0;
		Debug.Log(Angle);
//		CircularLine.SetPosition( 0, StartPosition );
//		CircularLine.SetPosition( 1, new Vector3( CurrentRadius * Mathf.Cos( Angle * Mathf.Deg2Rad ), yCoord, CurrentRadius * Mathf.Sin( Angle * Mathf.Deg2Rad ) ) );
		for ( int i = 0; i < Points + 1; i++ )
		{
			CircularLine1.SetPosition( i, new Vector3( CurrentRadius * Mathf.Cos( Angle * Mathf.Deg2Rad ), yCoord, CurrentRadius * Mathf.Sin( Angle * Mathf.Deg2Rad )));
			Angle += 500/Points;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		EnableRayCastHit();
	}
	
	void EnableRayCastHit()
	{
		if( Input.GetMouseButtonUp(0) )
		{
			Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
			RaycastHit Hit = new RaycastHit();
			Debug.Log( "Ray Casted" );
			if( Physics.Raycast( ray, out Hit, 1000f ))
			{
			    if( Hit.transform.name != "Sphere") 
				{
					Debug.Log( Hit.point + " " + Hit.transform.parent.name );
				    AxisH = CalculateAxis( Hit.transform.name , GetLineIndex( Hit.transform.parent.name ) );
					lineIndex = GetLineIndex( Hit.transform.parent.name );
					AngleH = CalculateAngleBetweenLineAndX( AxisH );
					AngleCameraToMove = CalculateAngleCamera( AxisH );
					Debug.Log( "Update Vala Angle " + Angle );
					//RotateCircleToHorizontal( Angle );
					RotateTowardsCamera( Hit.transform.position );
					Rotate = true;
					
				}
			}
		}
		
		if( Rotate )
		{
			
			Angle += Time.deltaTime * 1;
			GameObject.Find( "Sphere" ).transform.RotateAround( OnAxis, Time.deltaTime * 1 );
			//GameObject.Find( "Sphere" ).transform.RotateAround( AxisH, -Time.deltaTime * 1 );
			//Debug.Log( " Angle to move " + AngleToMove + " Angle " + Angle );
			if( Angle >= AngleToMove )
			{
				Rotate = false;
				Angle = 0;
				Camera.main.animation.Play();
				RotateCamera = true;
			}
		}
		
		if( RotateCamera )
		{
			AngleCameraToMove = Vector3.Angle ( new Vector3 ( 0,1,0), OnAxis ) * Mathf.Deg2Rad;
			RotateCamera = false;
			mRotate = true;
//			AngleCamera += Time.deltaTime * 2;
//			GameObject.Find( "Sphere" ).transform.RotateAround( new Vector3( 0,0,1), -Time.deltaTime * 2 );
//			Debug.Log( "To Move " + AngleCameraToMove + " Moved " + AngleCamera );
//			if( AngleCamera >= AngleCameraToMove )
//			{
//				RotateCamera = false;
//				AngleCamera = 0;
//			}
			
		}
		if( mRotate )
		{
			AngleCamera += Time.deltaTime * 2;
			GameObject.Find( "Sphere" ).transform.RotateAround( new Vector3( 0,0,1), -Time.deltaTime * 2 );
			Debug.Log( "To Move " + AngleCameraToMove + " Moved " + AngleCamera );
			if( AngleCamera >= AngleCameraToMove )
			{
				mRotate = false;
				AngleCamera = 0;
			}
		}
	}
	
	float CalculateAngleBetweenLineAndX( Vector3 Axis )
	{
	//	int Index = GetLineIndex( LineName );
		float Angle = Vector3.Angle( Axis, new Vector3( 1, 0, 0 ) );
		Debug.Log( Angle );
		return Angle;
	}
	
	int GetLineIndex( string LineName )
	{
		return int.Parse( LineName.Substring( 4, 1 ) );
	}
	
	void RotateCircleToHorizontal( float Angle )
	{
		GameObject.Find( "Sphere" ).transform.RotateAround( new Vector3( 0, 0, -1), Angle );
	}
	
	Vector3 CalculateAxis( string HitPosition, int Index )
	{
		Vector3 HitPoint = GameObject.Find( "Line" + Index + "/" + HitPosition ).transform.position;
		Debug.Log( HitPosition );
		int ColliderIndex;
	   	if( HitPosition.Length == 10 ) ColliderIndex = int.Parse( HitPosition.Substring( 8, 2 ).Trim() );
		else ColliderIndex = int.Parse( HitPosition.Substring( 8, 1 ).Trim() );
		Debug.Log( "ColliderNo " + ColliderIndex );
		Vector3 Perp;
		if( ColliderIndex < 75) Perp = GameObject.Find( "Line" + Index + "/Collider" + ( ColliderIndex+25 ).ToString() ).transform.position;
		else Perp = GameObject.Find( "Line" + Index + "/Collider" + ( 25 - ( 100 - ColliderIndex ) ).ToString() ).transform.position;
		Vector3 U = HitPoint - Vector3.zero;
		Vector3 V = Perp - Vector3.zero;
		Debug.Log( "UV Angle " + Vector3.Angle( U, V ));
		Vector3 Axis = Vector3.Cross( U, V );
		Debug.Log("Axis: " + Axis + " Angle Bw UV " + Vector3.Angle( Axis, U ) );
		return Axis;
	}
	
	void RotateTowardsCamera( Vector3 HitPoint )
	{
		 AngleToMove = Vector3.Angle( HitPoint, Camera.main.transform.position ) * Mathf.Deg2Rad;
		 OnAxis = Vector3.Cross( HitPoint, Camera.main.transform.position );
		
		//GameObject.Find( "Sphere" ).transform.RotateAround( OnAxis, 360 - AngleToMove );
	}
	
	float CalculateAngleCamera( Vector3 Axis )
	{
		return ( Vector3.Angle( Camera.main.transform.up, Axis )) * Mathf.Deg2Rad;		
	}
	
	
}
