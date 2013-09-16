using UnityEngine;
using System.Collections;

public class Strips : MonoBehaviour {
	
	float Radius;	
	private Vector3[] SphereVertices;
	public GameObject MeshObject;
	private int nHorizontalCircles = 11;
	private Vector3[] nHorizontalCentres;
	private LineRenderer CircularLine;
	public GameObject CircleObject;
	private static int CircleNo = 0;
	public Material Lambert;
	// Use this for initialization
	void Start () 
	{
		nHorizontalCentres = new Vector3[ nHorizontalCircles + 1 ];        // max(y) and min(y) has n+1 equal points
		Radius = renderer.bounds.max.x;
		float CircleInterval = 2 * Radius / nHorizontalCircles;
		float yCoord = Radius;
		GameObject.Find( "Line" ).transform.parent = transform;
		CircularLine = GameObject.Find( "Line" ).GetComponent< LineRenderer >();
		for( int i = 0; i <= nHorizontalCircles; i++ )
		{
			nHorizontalCentres[i] = new Vector3( 0, yCoord, 0);
			yCoord -= CircleInterval;
			Debug.Log( nHorizontalCentres[i] );
		}
		
		for( int i = 0; i < nHorizontalCentres.Length; i++ )
		{
			DrawHorizontalCircle( nHorizontalCentres[i], 100 );
		}
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
	void DrawHorizontalCircle( Vector3 Centre, int Points )
	{
		var g = Instantiate( CircleObject, Centre, Quaternion.identity ) as GameObject;
		//g.transform.LookAt( transform );
		g.transform.parent = transform;
		g.transform.name = "Circle" + CircleNo;
		CircleNo++;
	//	g.GetComponent<LineRenderer>().useWorldSpace = false;
		LineRenderer CircularLine1 =g.GetComponent<LineRenderer>();
		CircularLine1.SetVertexCount( Points + 1 );
		CircularLine1.SetWidth( 0.5f, 0.5f );
		CircularLine1.material = Lambert;
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
				Debug.Log( Hit.point );
			}
		}	
	}
}
