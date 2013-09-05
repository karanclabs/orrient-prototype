using UnityEngine;
using System.Collections.Generic;

public class ArrangeAlongSphere : MonoBehaviour {
    public GameObject prefab;
	public static int CubeNo = 0;
   private  int count = 50;
   private  float size = 2;
   private GameObject[] Cube; 
	public GameObject Linerenderer;
	private PrimitiveType mPrimitiveType;
	LineRenderer LinRen; 
	static int LineIndex = 1;
	int VertexCount;
	Collider[] TargetCube;
    [ContextMenu("Create Points")]
    void Create () {
        var points = UniformPointsOnSphere(count, size);
        for(var i=0; i<count; i++) {
            var g = Instantiate(prefab, transform.position+points[i], Quaternion.identity) as GameObject;
            g.transform.parent = transform;
			g.transform.name = "Cube"+i;
        }
    }
    
    Vector3[] UniformPointsOnSphere(float N, float scale) {
        var points = new List<Vector3>();
        var i = Mathf.PI * (3 - Mathf.Sqrt(5));
        var o = 2 / N;
        for(var k=0; k<N; k++) {
            var y = k * o - 1 + (o / 2);
            var r = Mathf.Sqrt(1 - y*y);
            var phi = k * i;
			//Debug.Log(new Vector3(Mathf.Cos(phi)*r, y, Mathf.Sin(phi)*r) * scale);
			if( CubeNo <= 100 && System.Text.Encoding.UTF8.GetString( System.IO.File.ReadAllBytes( GetDocumentsDirectoryPath() + "/CubePoints.xls" )) == "" )
			{
				CubeNo ++;
				SavePointToFile( new Vector3(Mathf.Cos(phi)*r, y , Mathf.Sin(phi)*r) * scale );
			}
			//Debug.Log( "y: " + y );
//			LinRen.SetVertexCount( CubeNo + 1 );
//			LinRen.SetPosition( CubeNo, new Vector3(Mathf.Cos(phi)*r, y, Mathf.Sin(phi)*r) * scale );
            points.Add(new Vector3(Mathf.Cos(phi)*r, y, Mathf.Sin(phi)*r) * scale);
        }
        return points.ToArray();
    }
	
	void Start(){
		var Path = Application.dataPath.Substring ( 0, Application.dataPath.Length - 5 );
		Path = Path.Substring( 0, Path.LastIndexOf( '/') );
		Path = Path + "/Documents";
		
		if( System.IO.File.Exists( Path + "/CubePoints.xls" ) && System.IO.Directory.Exists( Path )  )
		{
			//Debug.Log( "File Already Exists" );
		}
		else
		{
			System.IO.Directory.CreateDirectory( Path );
			System.IO.FileStream DataStream = System.IO.File.Create( Path + "/CubePoints.xls" );
			//Debug.Log( "File Created" );
		}
		
		if( System.IO.File.Exists( Path + "/ConnectedPoints.xls" )  )
		{
			//Debug.Log( "Already Exists" );
		}
		
		else
		{
			System.IO.FileStream DataStream = System.IO.File.Create( Path + "/ConnectedPoints.xls" );
			//Debug.Log( "File Created" );
		}
		
		print ("er");
		//LinRen = GameObject.Find( "Line" ).GetComponent<LineRenderer>();
		 Create();
		Cube = new GameObject[ GameObject.Find( "THE_FINAL_BALL" ).GetComponentsInChildren<MeshRenderer>().Length ];
		for( int i = 0; i < Cube.Length; i++ )
		{
			Cube[i] = GameObject.Find( "THE_FINAL_BALL" ).GetComponentsInChildren<MeshFilter>()[i].gameObject;
			//Debug.Log("Pos: " + Cube[i].transform.position.ToString() );
		}
		
		VertexCount = Cube.Length;
		if( System.Text.Encoding.UTF8.GetString( System.IO.File.ReadAllBytes( GetDocumentsDirectoryPath() + "/ConnectedPoints.xls" )) == "" )
		{
			for ( int k = 1; k <= 95; k++ )
			{
				SavePointToOtherOne( k );
			}
		}
		for ( int i = 1; i < 251; i++ )
		{
			var g = Instantiate( Linerenderer ) as GameObject;
			g.transform.parent = transform;
			g.name = "Line" + i;
		}
	}
	
	private void SavePointToFile( Vector3 Point )
	{
		string PreviousData = System.Text.Encoding.UTF8.GetString( System.IO.File.ReadAllBytes( GetDocumentsDirectoryPath() + "/CubePoints.xls" ));
		string DataToSave = PreviousData + CubeNo + "\t" + Point.ToString() + "\n";
		var bytes = System.Text.Encoding.UTF8.GetBytes( DataToSave );
		System.IO.File.WriteAllBytes ( GetDocumentsDirectoryPath() + "/CubePoints.xls", bytes );
	}
	
	
	void Update()
	{
//		for( int j = 0; j < Cube.Length; j++ )
//		{	
//			LinRen.SetWidth( 0.1f, 0.1f);
//			LinRen.SetVertexCount( j+1 );
//			LinRen.SetPosition( j, Cube[j + 1].transform.position );
//			//Debug.Log( "LineDrawn" );
//		}
		for( int i = 0; i <= 7; i++ )
		{
			DrawAlllines( i );
		}
//		
		//DrawAlllines( 1 );
		
	}
	
	
	
	static string GetDocumentsDirectoryPath()
	{
		var Path = Application.dataPath.Substring ( 0, Application.dataPath.Length - 5 );
		Path = Path.Substring( 0, Path.LastIndexOf( '/') );
		Path = Path + "/Documents";
		return Path;
	}
	
	public Vector3 GetPoint( int Index )
	{
		string Data = System.Text.Encoding.UTF8.GetString( System.IO.File.ReadAllBytes( GetDocumentsDirectoryPath() + "/CubePoints.xls" ));
		int PositionOfIndex = Data.IndexOf( Index + "\t") + "\n".Length + Index.ToString().Length;
		string TrimmedData = Data.Substring( PositionOfIndex, Data.Length - PositionOfIndex );
		int IndexOfcloseBracket = TrimmedData.IndexOf( ")" );
		TrimmedData = TrimmedData.Substring( 0, IndexOfcloseBracket );
		TrimmedData = TrimmedData.Replace( Index.ToString(), "" ).Trim();
		var Numbers = TrimmedData.Split( ","[0]);
		Numbers[0] = Numbers[0].Replace( "(", "");
		Numbers[2] = Numbers[2].Replace( ")", "");
		Debug.Log( Numbers[0] + Numbers[1] + Numbers[2]);
		Vector3 ReturnVector = new Vector3( float.Parse( Numbers[0] ), float.Parse( Numbers[1] ), float.Parse( Numbers[2] ));
		return ReturnVector;
	}
	
	public Vector3[] GetTargetPoints1( int Index )
	{
		string Data = System.Text.Encoding.UTF8.GetString( System.IO.File.ReadAllBytes( GetDocumentsDirectoryPath() + "/ConnectedPoints.xls" ));
//		int PositionOfIndex = Data.IndexOf( "\n" + Index + "\t") + "\n".Length;
//		var TrimmedData = Data.Substring( PositionOfIndex, Data.Length - PositionOfIndex );
//		int IndexofNextLine = TrimmedData.IndexOf( "\n" + ( Index + 1).ToString() );
//		//Debug.Log( IndexofNextLine );
//		TrimmedData = TrimmedData.Substring( 0, IndexofNextLine );
//		TrimmedData = TrimmedData.Replace( Index.ToString(), "" ).Trim();
//		string[] VectorString = new string[10];
//		VectorString = TrimmedData.Split( new string[]{ ")" + "\t" + "("}, System.StringSplitOptions.None );
//		var v3Array = new Vector3 [ VectorString.Length ];
//		for( int i = 0; i < VectorString.Length; i++ )
//			{
// 				//Debug.Log( VectorString[i] );
//				var numbers = VectorString[i].Split( ","[0] );
//				numbers[0] = numbers[0].Replace( "(", "");
//				
//				//Debug.Log( numbers[0]);
//				//Debug.Log( numbers[1]);
//				//Debug.Log( numbers[2]);
//				numbers[2] = numbers[2].Replace( ")", "");
//				//Debug.Log( numbers[0] );
//      		    v3Array[i] = new Vector3( float.Parse(numbers[0]), float.Parse(numbers[1]), float.Parse(numbers[2] ) );
//			} 
//		return v3Array;
		
			int Index0 = ( Index.ToString() + "\t").Length;
			int Index1 = Data.IndexOf( "\n" );//Mathf.Min ( Data.IndexOf( "\n" ) ,Data.IndexOf( "\t" + "(0.0, 0.0, 0.0)" ) );
			//Debug.Log( Index0 + " " + (Index1 - Index0).ToString() );
			var DataArray = Data.Substring( Index0, Index1 - Index0 ).Replace("\"", "" ).Split ( new string []{ ")"+"\t"+"("}, System.StringSplitOptions.None );
			//Debug.Log( DataArray[0] );
			var v3Array = new Vector3 [ DataArray.Length ];
			for( int i = 0; i < DataArray.Length; i++ )
			{
 				//Debug.Log( DataArray[i] );
			 	DataArray[i] = DataArray[i].Replace( "\"", "" );
				var numbers = DataArray[i].Split( ","[0] );
				numbers[0] = numbers[0].Replace( "(", "");
				
				//Debug.Log( numbers[0]);
				//Debug.Log( numbers[1]);
				
				numbers[2] = numbers[2].Replace( ")", "");
				//Debug.Log( numbers[2]);
      		    v3Array[i] = new Vector3( float.Parse(numbers[0]), float.Parse(numbers[1]), float.Parse(numbers[2] ) );
			}
			return v3Array;
	}
	
	public Vector3[] GetTargetPoints( int Index )
	{
		string Data = System.Text.Encoding.UTF8.GetString( System.IO.File.ReadAllBytes( GetDocumentsDirectoryPath() + "/ConnectedPoints.xls" ));
		int[] PointsInd = new int[5];
		Vector3[] varray = new Vector3[5];
		int Index0 = ( Index.ToString() + "\t").Length;
		int Index1 = 12;
		Debug.Log( Data );
		Debug.Log( Index0 + " " + (Index1 - Index0).ToString() );
		var DataArray = Data.Substring( Index0, Index1 - Index0 ).Trim().Split ( new string []{ "\t" }, System.StringSplitOptions.None );
		Debug.Log( DataArray[0] );
		for( int i = 0; i < DataArray.Length; i++ )
		{
			PointsInd[i] = int.Parse( DataArray[i] );
			varray[i] = GetPoint( PointsInd[i] );
		}
		return varray;
	}
	
	void DrawAlllines( int Index )
	{
		Vector3 Start = GetPoint( Index );
		Vector3[] target = GetTargetPoints( Index );
		
		TargetCube = Physics.OverlapSphere( target[0], 0.1f );
		//Debug.Log( TargetCube[0].gameObject.name );
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Start );
		LinRen.SetPosition( 1, TargetCube[0].gameObject.transform.position );
		LineIndex++;
		
		TargetCube = Physics.OverlapSphere( target[1], 0.1f );
		//Debug.Log( TargetCube[0].gameObject.name );
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Start );
		//Debug.Log( TargetCube[0].GetType() );
		LinRen.SetPosition( 1, TargetCube[0].gameObject.transform.position );
		LineIndex++;
		//Debug.Log("target[2]"+target[2]);
		
		TargetCube = Physics.OverlapSphere( target[2], 1.0f );
		//Debug.Log( TargetCube[0].gameObject.name );
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Start );
		//Debug.Log( TargetCube[0].GetType() );
		LinRen.SetPosition( 1, TargetCube[0].gameObject.transform.position );
		//Debug.Log( TargetCube[0].GetType() );
		LineIndex++;
		TargetCube = Physics.OverlapSphere( target[3], 0.1f );
		//Debug.Log( TargetCube[0].gameObject.name );
//		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
//		LinRen.SetVertexCount( 2 );
//		LinRen.SetPosition( 0, Start );
//		LinRen.SetPosition( 1, TargetCube[0].gameObject.transform.position );
		LineIndex++;
		
//		TargetCube = Physics.OverlapSphere( target[4], 0.1f );
//		//Debug.Log( TargetCube[0].gameObject.name );
//		LinRen = GameObject.Find( "Line5" ).GetComponent<LineRenderer>();
//		LinRen.SetVertexCount( 2 );
//		LinRen.SetPosition( 0, Start );
//		LinRen.SetPosition( 1, TargetCube[0].gameObject.transform.position );
		
		
	}
	
	void SavePointToOtherOne( int TargetIndex )
	{
		//int TargetIndex = 2;
		int[] TargetPoints = new int[5];
//		TargetPoints[0] = GetPoint( TargetIndex + 1 );// new Vector3( 0.1f, -1.9f, -0.6f ); 
//		
//		TargetPoints[1] = GetPoint( TargetIndex + 2 );//new Vector3( 0.4f, -1.9f, 0.6f  );
//		
//		TargetPoints[2] = GetPoint( TargetIndex + 3 );//new Vector3( -0.8f, -1.8f, -0.1f );
//		
//		TargetPoints[3] = GetPoint( TargetIndex + 4 );//new Vector3( 0.8f, -1.8f, -0.5f );
//		
//		TargetPoints[4] = GetPoint( TargetIndex + 5 );//new Vector3( -0.3f, -1.7f, 1.0f );
		
		TargetPoints[0] = TargetIndex + 1 ;// new Vector3( 0.1f, -1.9f, -0.6f ); 
		
		TargetPoints[1] = TargetIndex + 2 ;//new Vector3( 0.4f, -1.9f, 0.6f  );
		
		TargetPoints[2] = TargetIndex + 3 ;//new Vector3( -0.8f, -1.8f, -0.1f );
		
		TargetPoints[3] = TargetIndex + 4 ;//new Vector3( 0.8f, -1.8f, -0.5f );
		
		TargetPoints[4] = TargetIndex + 5 ;//new Vector3( -0.3f, -1.7f, 1.0f );
		string PreviousData = System.Text.Encoding.UTF8.GetString( System.IO.File.ReadAllBytes( GetDocumentsDirectoryPath() + "/ConnectedPoints.xls" ));
		string DataToSave = PreviousData + TargetIndex.ToString() + "\t" + TargetPoints[0].ToString() + "\t" + TargetPoints[1].ToString() + "\t" + TargetPoints[2].ToString() + "\t" + TargetPoints[3].ToString() + "\t" + TargetPoints[4].ToString() + "\t" + "\n";
		var bytes = System.Text.Encoding.UTF8.GetBytes( DataToSave );
		System.IO.File.WriteAllBytes ( GetDocumentsDirectoryPath() + "/ConnectedPoints.xls", bytes );
	}
}