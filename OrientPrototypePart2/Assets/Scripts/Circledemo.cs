using UnityEngine;
using System.Collections;

public class Circledemo : MonoBehaviour {
	
	private LineRenderer Circle;

	Vector3 N = new Vector3();
	// Use this for initialization
	void Start ()  
	{
		DrawCircleOnAxis( new Vector3(  3,5,2 ), 1 );
	}
	
	void DrawCircleOnAxis( Vector3 N, float Radius )
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
		Circle = GameObject.Find( "Circle" ).GetComponent<LineRenderer>();
		Circle.SetVertexCount( 100 );
		Circle.SetWidth( 0.05f, 0.05f );
		float Angle = 0;
		for ( int i = 0; i < 100; i++ )
		{
			//Circle.SetPosition( i, new Vector3( Radius * Mathf.Cos( Angle * Mathf.Deg2Rad ), Radius * Mathf.Sin( Angle * Mathf.Deg2Rad ), 0 ) );
			Circle.SetPosition( i, new Vector3( Radius * ( U.x * Mathf.Cos ( Angle * Mathf.Deg2Rad ) + V.x * Mathf.Sin ( Angle * Mathf.Deg2Rad ) ),
												Radius * ( U.y * Mathf.Cos ( Angle * Mathf.Deg2Rad ) + V.y * Mathf.Sin ( Angle * Mathf.Deg2Rad ) ), 
												Radius * ( U.z * Mathf.Cos ( Angle * Mathf.Deg2Rad ) + V.z * Mathf.Sin ( Angle * Mathf.Deg2Rad ) ) ) );
			Debug.Log( (Radius * Mathf.Cos( Angle )).ToString() );
			Angle += 3.6f;
		}
	}
	
	
	// Update is called once per frame
	void Update () {
	
	}
}
