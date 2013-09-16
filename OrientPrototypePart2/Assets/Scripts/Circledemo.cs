using UnityEngine;
using System.Collections;

public class Circledemo : MonoBehaviour {
	
	private LineRenderer Circle;
	float Radius = 1;

	// Use this for initialization
	void Start () 
	{
		Circle = GameObject.Find( "Circle" ).GetComponent<LineRenderer>();
		Circle.SetVertexCount( 100 );
		Circle.SetWidth( 0.01f, 0.01f );
		float Angle = 0;
		for ( int i = 0; i < 100; i++ )
		{
			Circle.SetPosition( i, new Vector3( Radius * Mathf.Cos( Angle * Mathf.Deg2Rad ), Radius * Mathf.Sin( Angle * Mathf.Deg2Rad ), 0 ) );
			Debug.Log( (Radius * Mathf.Cos( Angle )).ToString() );
			Angle += 3.6f;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
