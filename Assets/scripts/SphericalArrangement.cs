using UnityEngine;
using System.Collections;

public class SphericalArrangement : MonoBehaviour {
	
	public GameObject Prefab;
	int Count = 20;
	Vector3[] TPoints;
	static int Length;
	static int Counter = 1;
	// Use this for initialization
	void Create () 
	{
		for ( int j = 0; j < 10; j++ )
		{
			TPoints = ArrangeObjects( j+1 );
			for( int i = 0; i < Length; i++ )
			{
				var g = Instantiate( Prefab, TPoints[i], Quaternion.identity ) as GameObject;
				g.transform.parent = transform;
				g.transform.name = "Cube"+i;
			}
			Counter++;
		}
		
	}
	
	public Vector3[] ArrangeObjects( int Radius )
	{
		float[] XPos = new float[ (int)Random.Range( 1, 10) ];
		float[] YPos = new float[ XPos.Length ];
		float[] ZPos = new float[ XPos.Length ];
		Vector3[] Points = new Vector3[ XPos.Length ];
		Length = XPos.Length;
		Debug.Log( "Length: "+XPos.Length );
		for( int i = 0; i < XPos.Length; i++ )
		{
			XPos[i] = Random.Range( -Radius, Radius );
			Debug.Log( XPos[i]);
			YPos[i] = Mathf.Sqrt( (Radius*Radius) - (XPos[i] * XPos[i]) );
			ZPos[i] = Mathf.Sqrt( (Radius*Radius) - (YPos[i] * YPos[i]) - (XPos[i] * XPos[i]) );
			Points[i] = new Vector3( XPos[i], YPos[i], ZPos[i] );
			Debug.Log( Points[i]);
		}
		return Points;
	}
	// Update is called once per frame
	void Start () 
	{
		Create();
	}
}
