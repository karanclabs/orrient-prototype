using UnityEngine;
using System.Collections.Generic;

public class ArrangeAlongSphere : MonoBehaviour {
    public GameObject prefab;
	public static int CubeNo = 0;
   private  int count = 50;
   private  float size = 2;
   private GameObject[] Cube; 
	private GameObject[] CubeMesh;
 	public GameObject Linerenderer;
	public GameObject Sphere;
	private PrimitiveType mPrimitiveType;
	LineRenderer LinRen; 
	static int LineIndex = 1;
	 public float explosionRadius = 5.0F;
	Vector3 []midpts = new Vector3[200];
	int VertexCount;
	Collider[] TargetCube;
	public Material Matt;
	public GameObject empty;
	//private List<Collider[]> Nearpoints = new List<Collider[]>();
	static Collider[][] Nearpoints = new Collider[51][];
	int val;
	//Mesh newmesh = new Mesh();
    [ContextMenu("Create Points")]
    void Create () {
        var points = UniformPointsOnSphere(count, size);
        for(var i=0; i<count; i++) {
            var g = Instantiate(prefab, transform.position+points[i], Quaternion.identity) as GameObject;
			
            g.transform.parent = transform;

			g.transform.name = "Cube"+(i+1).ToString();
			g.GetComponentInChildren<TextMesh>().text = "";
			
			
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
		CubeMesh = new GameObject[502];
		for(int i=0;i<=500;i++)
		{
			CubeMesh[i] = Instantiate(empty,transform.position,Quaternion.identity) as GameObject;
			CubeMesh[i].name = "cubemesh"+i;
			CubeMesh[i].transform.parent = GameObject.Find("THE_FINAL_BALL").transform;
		}
		//CubeMesh = new GameObject[200];
		
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
		Debug.Log( Cube.Length );
		for( int i = 1; i < 51; i++ )
		{
			Cube[i] = GameObject.Find( "THE_FINAL_BALL" ).GetComponentsInChildren<MeshFilter>()[i].gameObject;
			Debug.Log("Pos: " + Cube[i].transform.position.ToString() );
		}
		
		VertexCount = Cube.Length;
		
		for ( int i = 1; i < 500; i++ )
		{
			var g = Instantiate( Linerenderer ) as GameObject;
			g.transform.parent = transform;
			g.name = "Line" + i;
		}
		for ( int k = 1; k <= 50; k++ )
		{
		
			//Nearpoints.Add( Physics.OverlapSphere( Cube[k].transform.position, 10f ) );
			Nearpoints[k] = new Collider[ Physics.OverlapSphere( Cube[k].transform.position, 1f ).Length ];
			Nearpoints[k] = Physics.OverlapSphere( Cube[k].transform.position, 1f );
		//	Debug.Log( Nearpoints[k][0].gameObject.transform.position );
			
		}
		if( System.Text.Encoding.UTF8.GetString( System.IO.File.ReadAllBytes( GetDocumentsDirectoryPath() + "/ConnectedPoints.xls" )) == "" )
		{
			for ( int k = 1; k <= 50; k++ )
			{
				SavePointToOtherOne( k );
				Debug.Log( "Data Saved" );
			}
		}
		
		//Plotlines();
		PlotMeshes();
		//PlotStatic();
//			int[] Points = new int[6];
//			Points = NearestPoints( 15 );
			
		//	DrawTriangles( 15, Points );
		

		//Sort(Points,15);
		
		
//		for(int j = 1; j < 51; j++ )
//		{
//			DrawDynamicLineRenderers( j );
//		}
//		for ( int u = 1; u < 51; u++ )
//		{
//			meshmaker1( u, 1,2);
//			meshmaker1( u, 2,3);
//			meshmaker1( u, 1,3);
//			
//		}
//		NearestPoints( 1 );
//		DraWStaticLineRenderers();
//		Drawmeshes();
//		for( int i = 1; i <= 45; i++ )
//		{
//			DrawAlllines( i );
//		}
	}
	
	void Sort(int [] Points , int index){
		
		
		
		
	}
	void PlotStatic()
	{
					CubeMesh[val].AddComponent("MeshFilter"); 
					CubeMesh[val].AddComponent("MeshRenderer"); 
					Debug.Log("val"+val);
					Mesh newmesh = new Mesh();
					Debug.Log(newmesh);
					//Debug.Log(Index);
					Vector3 []vert = new Vector3[3]{Cube[22].transform.position,Cube[30].transform.position,Cube[17].transform.position};
					newmesh.vertices = vert;
					Vector2 []uvs = new Vector2[3]; 
			 		for (int k=0; k<uvs.Length; k++)
					{
			 			uvs[k] = new Vector2(newmesh.vertices[k].x, newmesh.vertices[k].z);
			 		}
			 		newmesh.uv = uvs;
					//CubeMesh[val].GetComponent<MeshRenderer>().materials[0]. = new Color( Random.Range( 1,255),Random.Range( 1,255),Random.Range( 1,255),1);
					newmesh.triangles = new int[]{2,0,1};
					CubeMesh[val].GetComponent<MeshFilter>().mesh = newmesh;
					//int R = Random.Range(0,4);
					CubeMesh[val].renderer.material = Matt;
				val++;
		
		
	}
	void PlotMeshes()
	{
		for ( int i = 1; i <= 50; i++ )
		{
			int[] Points = new int[12];
			Points = NearestPoints( i );
			DrawTriangles( i, Points );
		}
	}
	
	Vector3 CalculateCentroid( int [] p )
	{
		Vector3 a = Vector3.zero;;
		for( int i = 1; i < p.Length; i++ )
		{
			a.x += Cube[ p[i] ].transform.position.x;
			a.y += Cube[ p[i] ].transform.position.y;
			a.z += Cube[ p[i] ].transform.position.z;
		}
		
		a = a / p.Length;
		return a;
	}
	
	int[] SortPoints( int Index, int[] x )
	{
		Vector3 Pivot =  CalculateCentroid( x );//Cube[ Index ].transform.position;
		GameObject s = Instantiate( Sphere, Pivot, Quaternion.identity ) as GameObject;
		s.transform.parent = transform;
		Vector3[] v = new Vector3[7];
		for( int i = 1; i <= 6; i++ )
		{
			v[i] = Cube[ x[i] ].transform.position - Pivot;
		}
		//float[] angles = new float[7];
		//List<GameObject,float> angles = new List< GameObject, float>();
		float[] angles = new float[7];
		for( int j = 1; j <= 6; j++ )
		{
			//angles[j] =   Mathf.Atan2(  Vector3.Dot( Cube[ Index ].transform.position , Vector3.Cross(Cube[ x[1] ].transform.position, Cube[ x[j] ].transform.position)), Vector3.Dot(Cube[ x[1] ].transform.position, Cube[ x[j] ].transform.position) * Mathf.Rad2Deg );//Vector3.Angle( v[0], v[j] );
			//Debug.Log( "Angle: " + angles[j]);
			angles[j] = Vector3.Angle( v[1], v[j] ); //Mathf.Acos( Vector3.Dot( v[1], v[j]) / (Vector3.Magnitude( v[1]) * Vector3.Magnitude( v[j]))) * Mathf.Rad2Deg;
			
//			if( angles[j] < Vector3.Angle( v[1], v[j - 1] ))
//			{
//				angles[j] = 360 - angles[j];
//			}
			//angles.Add( Cube[x[j]], angles1[j]);
			//Cube[ x[j] ].transform.tag = ((int)angles[j]).ToString();
		}
		
		float[ ] FinalAngles = new float[7];
//		for( int i = 1; i <= 6; i++ )
//		{
//			if( i < 6)
//				FinalAngles[i] = angles[i] - angles[i+1];
//			else
//				FinalAngles[i] = angles[i] - angles[1];
//		}
//		
		for( int i = 1; i <= 6; i++ )
		{
			for( int j = 1; j <= 6; j++ )
			{
				if( angles[i] < angles[j] )
				{
					int Temp = x[i];
//					float Temp2 = angles[i];
//					angles[i] = angles[j];
//					angles[j] = Temp2;
					x[i] = x[j];
					x[j] = Temp;
				}
			}
			
			
			
			
		}
		int[] IndexArray = new int[7]{ 0,1,2,3,4,5,6};
//		for( int i = 1; i <= 6; i++ )
//		{
//			for( int j = 1; j <= 5; j++ )
//			{
//				if( angles[i] < angles[j] )
//				{
//					float Temp = angles[i];				
//					angles[i] = angles[j];
//					angles[j] = Temp;
//					
//					
//				}
//				
//					
//			} 
//			
//		}
		
		int n = 7;
	for(int i=1; i<n; i++)

	{

		for(int y=1; y<n-1; y++)

		{

			if(angles[y]>angles[y+1])

			{

				float temp = angles[y+1];

				angles[y+1] = angles[y];

				angles[y] = temp;
					
				int temp1 = IndexArray[y+1];

				IndexArray[y+1] = IndexArray[y];

				IndexArray[y] = temp1;
					

			}

		}

	}
				Debug.Log("Sorted: " + angles[0] + " " + angles[1] + " " + angles[2] + " " + angles[3] + " " + angles[4] + " " + angles[5] + " " + angles[6] );

		Debug.Log("Sored: " + IndexArray[0] + " " + IndexArray[1] + " " + IndexArray[2] + " " + IndexArray[3] + " " + IndexArray[4] + " " + IndexArray[5] + " " + IndexArray[6] );
		
			for( int j = 1; j <= 6; j++ )
			{
				
					Cube[x[ IndexArray[j] ]].transform.localScale += new Vector3( j* 0.01f, j*0.01f,j*0.01f);
				
			}
				Debug.Log("CUBE: " + x[ IndexArray[0] ] +  " " +x[ IndexArray[1] ] + " " + x[ IndexArray[2] ] + " " + x[ IndexArray[3] ] + " " + x[ IndexArray[4] ] + " " + x[ IndexArray[5] ]  + " " + x[ IndexArray[6] ] );

		
		
		for( int i = 1; i <= 6; i++ )
		{
		//	Debug.Log( x[i] );
			
			//Cube[ x[i] ].transform.localScale += new Vector3( i* 0.01f, i*0.01f,i*0.01f);
//			Cube[ x[i] ].GetComponentInChildren<TextMesh>().text = angles[i].ToString();
//			Cube[ x[i] ].GetComponentInChildren<TextMesh>().fontSize = 10;
			//Debug.Log("CUBENO:" + x[i] );
		//	Debug.Log( "Index"+IndexArray[i]);
			//Debug.Log( angles[i].ToString() + " " + angles[i] );
		}
		return x;
		
	}
	private int[] NearestPointsLocal1( int Index, int[] Plot, int CubeIndex )
	{
		float[] Distance = new float[7];
		int[] CubeNo = new int[8];
		for ( int i = CubeIndex + 1; i <= 6; i++ )
		{
			Distance[i] =  CalculateDistance ( Cube[ Index ].transform.position, Cube[Plot[i]].transform.position );
			CubeNo[i - CubeIndex + 1 ] = Plot[i];
		}
		for ( int i = 1; i <= 50; i++ )
		{
			for ( int j = 1; j <= 50; j++ )
			{
				if( Distance[i] < Distance[j] )
				{
					float temp = Distance[i];
					int temp1 = CubeNo[i];
					Distance[i] = Distance[j];
					CubeNo[i] = CubeNo[j];
					Distance[j] = temp;
					CubeNo[j] = temp1;
				}
			}
		}
		
//		for( int i = 1; i <= 50; i++ )
//		{
//			Debug.Log( Distance[i] );
//			Debug.Log( CubeNo[i]);
//		}
		
		int[] Result = new int[3];
		//Result = CubeNo;
		for( int i = 1; i<= 2; i++ )
		{
			Result[i] = CubeNo[i];
		}
		return Result;
	}
	
	int[] NearestPointsLocal( Vector3 P, int[] Plot, int Pivot )
	{
		float[] Distance = new float[7]{ 0, 0, 0, 0, 0, 0, 0};
		int[] CubeNo = new int[7]{ 0,0,0,0,0,0,0};
		for( int i = 2; i <= 6; i++ )
		{
			Distance[i] = CalculateDistance( P, Cube[ Plot[i] ].transform.position );
			CubeNo[i] = Plot[i];
		}
		
		Debug.Log( "Distence Before: "+" "+Distance[1]+" "+Distance[2]+" "+Distance[3]+" "+Distance[4]+" "+Distance[5]+" "+Distance[6]);
		
		for(int i=1; i <= 6; i++)

		{
	
			for(int y=1; y<=5; y++)
	
			{
	
				if(Distance[y]>Distance[y+1])
	
				{
	
					float temp = Distance[y+1];
	
					Distance[y+1] = Distance[y];
	
					Distance[y] = temp;
					
					int temp1 = CubeNo[y+1];
	
					CubeNo[y+1] = CubeNo[y];
					
					CubeNo[y] = temp1;
					
						
						
	
				}
	
			}
	
		}
		Debug.Log( "Distence After: "+" "+Distance[1]+" "+Distance[2]+" "+Distance[3]+" "+Distance[4]+" "+Distance[5]+" "+Distance[6]);
		
		return CubeNo;
	}
	
	void DrawTriangles( int Index, int[] Plot )
	{
		//Debug.Log(Plot[0]+" "+Plot[1]+" "+Plot[2]+" "+Plot[3]+" "+Plot[4]+" "+Plot[5]+" "+Plot[6]+" "+Plot[7]);
		
		int[] Plot1 = new int[12];
//		int[][] N = new int[7][];
//		for( int i=1; i <=6; i++)
//		{
//			 N[i] = NearestPointsLocal( Cube[ Plot[i]].transform.position, Plot, Index);
//		}
		//Debug.Log( " Plot1: " + N[2] + " Plot1: " + N[3]);
		//Plot1 = SortPoints( Index, Plot );
		Plot1[0] = Plot[0];
		Plot1[1] = Plot[1] ;
		Plot1[2] = Plot[4];
		Plot1[3] = Plot[5];
		Plot1[4] = Plot[8];
		Plot1[5] = Plot[9];
		Plot1[6] = Plot[2];
		Plot1[7] = Plot[3];
		Plot1[8] = Plot[6];
		Plot1[9] = Plot[7];
		Plot1[10] = Plot[10];
		Plot1[11] = Plot[11];
		
		for( int i = 1; i <= 11; i++ )
		{
			
//			for( int j = 1; j <= 6; j++ )
//			{
//			Cube[Index].transform.localScale = new Vector3( 0.02f,0.02f,0.02f)	;
			if( i < 11 )
			{
					//Cube[ N[j][i]].transform.localScale += new Vector3( j*0.004f,j*0.002f,j*0.002f);
					CubeMesh[val].AddComponent("MeshFilter"); 
					CubeMesh[val].AddComponent("MeshRenderer"); 
					Debug.Log("val"+val);
					Mesh newmesh = new Mesh();
					Debug.Log(newmesh);
					Debug.Log(Index);
					Vector3 []vert;
					
	
					    vert = new Vector3[3]{Cube[Index].transform.position,Cube[Plot[i]].transform.position,Cube[Plot[(i+1)]].transform.position};
					
					
					
					newmesh.vertices = vert;
					Vector2 []uvs = new Vector2[3]; 
			 		for (int k=0; k<uvs.Length; k++)
					{
			 			uvs[k] = new Vector2(newmesh.vertices[k].x, newmesh.vertices[k].z);
			 		}
			 		newmesh.uv = uvs;
//					MeshRendere	r newmeshRen = CubeMesh[val].GetComponent<MeshRenderer>();
//					Material m = newmeshRen.material;
//					m.color = new Color( Random.Range( 1,255),Random.Range( 1,255),Random.Range( 1,255),1);
//					newmeshRen.material = m;
					
//					m.color = Color.blue;
//					CubeMesh[val].GetComponent<MeshRenderer>().renderer.material = m;
					newmesh.triangles = new int[]{2,0,1};
					newmesh.RecalculateNormals();
					CubeMesh[val].GetComponent<MeshFilter>().mesh = newmesh;
					//int R = Random.Range(0,4);
					
					CubeMesh[val].renderer.material = Matt;
				val++;
			}
					
				
			
			
		}
	}
	static void GetCombination( int[] a )
	{
		int[] output = new int[2]; 
	  for ( int h = 0; h < a.Length; h++ )
		{
			
		}
	}
	void meshmaker( int Index, int a, int b, int c )
	{
		CubeMesh[Index].AddComponent("MeshFilter"); 
		CubeMesh[Index].AddComponent("MeshRenderer"); 
		Mesh newmesh = new Mesh();
		Debug.Log(newmesh);
		Vector3 []vert = new Vector3[3]{Cube[a].transform.position,Cube[b].transform.position,Cube[c].transform.position};
		newmesh.vertices = vert;
		
		Vector2 []uvs = new Vector2[3]; 
 		for (int i=0; i<uvs.Length; i++)
		{
 			uvs[i] = new Vector2(newmesh.vertices[i].x, newmesh.vertices[i].z);
 		}
 		newmesh.uv = uvs;
		newmesh.triangles = new int[]{0,1,2};
		CubeMesh[Index].GetComponent<MeshFilter>().mesh = newmesh;
		//int R = Random.Range(0,4);
		CubeMesh[Index].renderer.material = Matt;
	}
	
	void meshmaker1( int Index, int a, int b )
	{
		CubeMesh[Index].AddComponent("MeshFilter"); 
		CubeMesh[Index].AddComponent("MeshRenderer"); 
		
		Mesh newmesh = new Mesh();
		Debug.Log( Nearpoints[Index][0].gameObject.transform.position);// + Nearpoints[Index][a].gameObject.transform.position + Nearpoints[Index][b].gameObject.transform.position );
		Vector3 []vert = new Vector3[3]{ Nearpoints[Index][0].gameObject.transform.position ,Nearpoints[Index][a].gameObject.transform.position,Nearpoints[Index][b].gameObject.transform.position};
		newmesh.vertices = vert;
		
		Vector2 []uvs = new Vector2[3]; 
 		for (int i=0; i<uvs.Length; i++)
		{
 			uvs[i] = new Vector2(newmesh.vertices[i].x, newmesh.vertices[i].z);
 		}
 		newmesh.uv = uvs;
		newmesh.triangles = new int[]{2,0,1};
		CubeMesh[Index].GetComponent<MeshFilter>().mesh = newmesh;
		//int R = Random.Range(0,4);
		CubeMesh[Index].renderer.material = Matt;
	}
	
	void Drawmeshes()
	{
		meshmaker(0,1,3,6);
		meshmaker(1,1,2,6);
		meshmaker(2,1,4,6);
		meshmaker(3,1,3,6);
		meshmaker(4,1,3,6);
		meshmaker(5,1,3,2);
		meshmaker(6,2,5,10);
		meshmaker(7,1,2,5);
		meshmaker(8,2,3,4);
		meshmaker(9,2,1,4);
		meshmaker(10,2,1,10);
		meshmaker(11,8,3,11);
		meshmaker(12,8,3,6);
		meshmaker(13,8,3,1);
		meshmaker(14,6,3,11);
		meshmaker(15,4,12,7);
		meshmaker(16,4,12,2);
		meshmaker(17,4,12,1);
		meshmaker(18,5,10,2);
		meshmaker(19,5,10,18);
		meshmaker(20,5,10,13);
		meshmaker(21,6,1,11);
		meshmaker(22,6,1,19);
		meshmaker(23,7,4,2);
		meshmaker(24,7,4,10);
		meshmaker(25,7,4,15);
		meshmaker(26,8,3,5);
		meshmaker(27,8,3,16);
		meshmaker(28,8,3,13);
		meshmaker(29,9,17,4);
		meshmaker(30,9,17,6);
		meshmaker(31,9,17,14);
		meshmaker(32,10,2,15);
		meshmaker(33,10,2,33);
		meshmaker(34,11,6,19);
		meshmaker(35,11,6,24);
		meshmaker(36,11,6,16);
		meshmaker(37,12,4,7);
		meshmaker(38,12,4,20);
		meshmaker(39,12,4,17);
		meshmaker(40,13,8,5);
		meshmaker(41,13,8,21);
		meshmaker(42,13,8,26);
		meshmaker(43,14,6,19);
		meshmaker(44,14,19,27);
		meshmaker(45,14,27,22);
		meshmaker(46,15,7,20);
		meshmaker(47,15,20,28);
		meshmaker(48,15,28,23);
		meshmaker(49,16,11,8);
		meshmaker(50,16,8,21);
		meshmaker(51,16,21,24);
		meshmaker(52,17,9,12);
		meshmaker(53,17,12,25);
		meshmaker(54,17,25,22);
		meshmaker(55,13,8,26);
		meshmaker(56,26,8,31);
		meshmaker(57,18,13,1);
		meshmaker(58,8,5,1);
		meshmaker(59,44,31,39);
		meshmaker(60,1,5,2);
		meshmaker(61,18,13,31);
		meshmaker(62,18,13,23);
		meshmaker(63,19,24,32);
		meshmaker(64,19,32,27);
		meshmaker(65,19,27,11);
		meshmaker(66,20,28,33);
		meshmaker(67,20,33,15);
		meshmaker(68,20,15,12);
		meshmaker(69,21,26,13);
		meshmaker(70,21,13,16);
		meshmaker(71,21,16,19);
		meshmaker(72,22,27,14);
		meshmaker(73,22,14,17);
		meshmaker(74,22,17,30);
		meshmaker(75,23,28,15);
		meshmaker(76,23,15,18);
		meshmaker(77,23,18,31);
		meshmaker(78,24,29,16);
		meshmaker(79,24,16,19);
		meshmaker(81,24,19,32);
		meshmaker(82,25,30,17);
		meshmaker(83,25,17,20);
		meshmaker(84,25,20,33);
		meshmaker(85,26,18,31);
		meshmaker(86,26,31,39);
		meshmaker(87,26,39,34);
		meshmaker(88,27,22,35);
		meshmaker(89,27,35,32);
		meshmaker(90,27,32,20);
		meshmaker(91,28,23,36);
		meshmaker(92,28,36,33);
		meshmaker(93,28,33,20);
		meshmaker(94,29,24,37);
		meshmaker(95,29,37,34);
		meshmaker(96,29,34,31);
		meshmaker(97,30,22,17);
		meshmaker(98,30,17,35);
		meshmaker(99,30,15,38);
		meshmaker(100,31,26,18);
		meshmaker(101,31,18,23);
		meshmaker(102,31,23,39);
		meshmaker(103,32,37,24);
		meshmaker(104,32,24,19);
		meshmaker(105,32,29,27);
		meshmaker(106,33,41,38);
		meshmaker(106,33,38,25);
		meshmaker(107,33,25,28);
		meshmaker(108,34,39,42);
		meshmaker(109,34,42,29);
		meshmaker(110,34,29,26);
		meshmaker(111,35,43,30);
		meshmaker(112,35,30,22);
		meshmaker(113,35,22,27);
		meshmaker(114,36,44,31);
		meshmaker(115,36,31,23);
		meshmaker(116,36,23,28);
		meshmaker(117,37,42,45);
		meshmaker(118,37,45,29);
		meshmaker(119,37,29,24);
		meshmaker(120,38,46,43);
		meshmaker(121,38,43,30);
		meshmaker(122,38,30,33);
		meshmaker(123,39,44,31);
		meshmaker(124,39,31,26);
		meshmaker(125,39,26,34);
		meshmaker(126,40,48,43);
		meshmaker(127,40,43,35);
		meshmaker(128,40,35,27);
		meshmaker(129,41,33,46);
		meshmaker(130,41,46,44);
		meshmaker(131,41,44,34);
		meshmaker(132,42,47,45);
		meshmaker(133,42,45,34);
		meshmaker(134,42,34,37);
		meshmaker(135,43,46,48);
		meshmaker(136,43,48,40);
		meshmaker(137,43,40,35);
		meshmaker(138,44,49,47);
		meshmaker(139,44,47,39);
		meshmaker(140,44,39,31);
		meshmaker(141,45,40,48);
		meshmaker(142,45,48,32);
		meshmaker(143,45,32,37);
		meshmaker(144,46,38,43);
		meshmaker(145,46,43,48);
		meshmaker(146,46,48,49);
		meshmaker(147,47,42,39);
		meshmaker(148,47,39,44);
		meshmaker(149,47,44,49);
		meshmaker(150,48,45,40);
		meshmaker(151,48,40,49);
		meshmaker(152,48,49,46);
		meshmaker(153,49,47,44);
		meshmaker(154,49,44,46);
		meshmaker(155,49,46,48);
		meshmaker(156,31,39,26);
		meshmaker(157,43,40,35);
//		meshmaker(158,26,39,31);
//		meshmaker(159,26,39,31);
//		meshmaker(160,26,39,31);
//		meshmaker(161,26,39,31);
//		meshmaker(162,26,39,31);
//		meshmaker(163,26,39,31);
//		meshmaker(164,26,39,31);
//		meshmaker(165,26,39,31);
//		meshmaker(166,26,39,31);
//		meshmaker(167,26,39,31);
//		meshmaker(168,26,39,31);
//		meshmaker(169,26,39,31);
//		meshmaker(170,26,39,31);
//		meshmaker(171,26,39,31);
//		meshmaker(172,26,39,31);
			
			
	}
	private void DraWStaticLineRenderers()
	{
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[1].transform.position );
		LinRen.SetPosition( 1, Cube[3].transform.position );
		midpts[LineIndex] = ((Cube[1].transform.position+Cube[3].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[1].transform.position );
		LinRen.SetPosition( 1, Cube[6].transform.position );
		midpts[LineIndex] = ((Cube[1].transform.position+Cube[6].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[1].transform.position );
		LinRen.SetPosition( 1, Cube[4].transform.position );
		midpts[LineIndex] = ((Cube[1].transform.position+Cube[4].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[1].transform.position );
		LinRen.SetPosition( 1, Cube[2].transform.position );
		midpts[LineIndex] = ((Cube[1].transform.position+Cube[2].transform.position)/2);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[2].transform.position );
		LinRen.SetPosition( 1, Cube[5].transform.position );
		midpts[LineIndex] = ((Cube[2].transform.position+Cube[5].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[2].transform.position );
		LinRen.SetPosition( 1, Cube[10].transform.position );
		midpts[LineIndex] = ((Cube[2].transform.position+Cube[10].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[2].transform.position );
		LinRen.SetPosition( 1, Cube[1].transform.position );
		midpts[LineIndex] = ((Cube[2].transform.position+Cube[1].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[2].transform.position );
		LinRen.SetPosition( 1, Cube[4].transform.position );
		midpts[LineIndex] = ((Cube[2].transform.position+Cube[4].transform.position)/2);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[3].transform.position );
		LinRen.SetPosition( 1, Cube[8].transform.position );
		midpts[LineIndex] = ((Cube[3].transform.position+Cube[8].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[3].transform.position );
		LinRen.SetPosition( 1, Cube[11].transform.position );
		midpts[LineIndex] = ((Cube[3].transform.position+Cube[11].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[3].transform.position );
		LinRen.SetPosition( 1, Cube[6].transform.position );
		midpts[LineIndex] = ((Cube[3].transform.position+Cube[6].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[3].transform.position );
		LinRen.SetPosition( 1, Cube[1].transform.position );
		midpts[LineIndex] = ((Cube[3].transform.position+Cube[1].transform.position)/2);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[4].transform.position );
		LinRen.SetPosition( 1, Cube[12].transform.position );
		midpts[LineIndex] = ((Cube[4].transform.position+Cube[12].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[4].transform.position );
		LinRen.SetPosition( 1, Cube[7].transform.position );
		midpts[LineIndex] = ((Cube[4].transform.position+Cube[7].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[4].transform.position );
		LinRen.SetPosition( 1, Cube[2].transform.position );
		midpts[LineIndex] = ((Cube[4].transform.position+Cube[2].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[4].transform.position );
		LinRen.SetPosition( 1, Cube[1].transform.position );
		midpts[LineIndex] = ((Cube[4].transform.position+Cube[1].transform.position)/2);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[5].transform.position );
		LinRen.SetPosition( 1, Cube[10].transform.position );
		midpts[LineIndex] = ((Cube[5].transform.position+Cube[10].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[5].transform.position );
		LinRen.SetPosition( 1, Cube[2].transform.position );
		midpts[LineIndex] = ((Cube[5].transform.position+Cube[2].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[5].transform.position );
		LinRen.SetPosition( 1, Cube[18].transform.position );
		midpts[LineIndex] = ((Cube[5].transform.position+Cube[18].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[5].transform.position );
		LinRen.SetPosition( 1, Cube[13].transform.position );
		midpts[LineIndex] = ((Cube[5].transform.position+Cube[13].transform.position)/2);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[6].transform.position );
		LinRen.SetPosition( 1, Cube[1].transform.position );
		midpts[LineIndex] = ((Cube[6].transform.position+Cube[1].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[6].transform.position );
		LinRen.SetPosition( 1, Cube[3].transform.position );
		midpts[LineIndex] = ((Cube[6].transform.position+Cube[3].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[6].transform.position );
		LinRen.SetPosition( 1, Cube[11].transform.position );
		midpts[LineIndex] = ((Cube[6].transform.position+Cube[11].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[6].transform.position );
		LinRen.SetPosition( 1, Cube[19].transform.position );
		midpts[LineIndex] = ((Cube[6].transform.position+Cube[19].transform.position)/2);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[7].transform.position );
		LinRen.SetPosition( 1, Cube[4].transform.position );
		midpts[LineIndex] = ((Cube[7].transform.position+Cube[4].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[7].transform.position );
		LinRen.SetPosition( 1, Cube[2].transform.position );
		midpts[LineIndex] = ((Cube[7].transform.position+Cube[2].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[7].transform.position );
		LinRen.SetPosition( 1, Cube[10].transform.position );
		midpts[LineIndex] = ((Cube[7].transform.position+Cube[10].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[7].transform.position );
		LinRen.SetPosition( 1, Cube[15].transform.position );
		midpts[LineIndex] = ((Cube[7].transform.position+Cube[15].transform.position)/2);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[8].transform.position );
		LinRen.SetPosition( 1, Cube[3].transform.position );
		midpts[LineIndex] = ((Cube[8].transform.position+Cube[3].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[8].transform.position );
		LinRen.SetPosition( 1, Cube[5].transform.position );
		midpts[LineIndex] = ((Cube[8].transform.position+Cube[5].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[8].transform.position );
		LinRen.SetPosition( 1, Cube[16].transform.position );
		midpts[LineIndex] = ((Cube[8].transform.position+Cube[16].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[8].transform.position );
		LinRen.SetPosition( 1, Cube[13].transform.position );
		midpts[LineIndex] = ((Cube[8].transform.position+Cube[13].transform.position)/2);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[9].transform.position );
		LinRen.SetPosition( 1, Cube[17].transform.position );
		midpts[LineIndex] = ((Cube[9].transform.position+Cube[17].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[9].transform.position );
		LinRen.SetPosition( 1, Cube[4].transform.position );
		midpts[LineIndex] = ((Cube[9].transform.position+Cube[4].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[9].transform.position );
		LinRen.SetPosition( 1, Cube[6].transform.position );
		midpts[LineIndex] = ((Cube[9].transform.position+Cube[6].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[9].transform.position );
		LinRen.SetPosition( 1, Cube[14].transform.position );
		midpts[LineIndex] = ((Cube[9].transform.position+Cube[14].transform.position)/2);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[10].transform.position );
		LinRen.SetPosition( 1, Cube[2].transform.position );
		midpts[LineIndex] = ((Cube[10].transform.position+Cube[2].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[10].transform.position );
		LinRen.SetPosition( 1, Cube[7].transform.position );
		midpts[LineIndex] = ((Cube[10].transform.position+Cube[7].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[10].transform.position );
		LinRen.SetPosition( 1, Cube[15].transform.position );
		midpts[LineIndex] = ((Cube[10].transform.position+Cube[15].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[10].transform.position );
		LinRen.SetPosition( 1, Cube[23].transform.position );
		midpts[LineIndex] = ((Cube[10].transform.position+Cube[23].transform.position)/2);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[11].transform.position );
		LinRen.SetPosition( 1, Cube[6].transform.position );
		midpts[LineIndex] = ((Cube[11].transform.position+Cube[6].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[11].transform.position );
		LinRen.SetPosition( 1, Cube[19].transform.position );
		midpts[LineIndex] = ((Cube[11].transform.position+Cube[19].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[11].transform.position );
		LinRen.SetPosition( 1, Cube[24].transform.position );
		midpts[LineIndex] = ((Cube[11].transform.position+Cube[24].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[11].transform.position );
		LinRen.SetPosition( 1, Cube[16].transform.position );
		midpts[LineIndex] = ((Cube[11].transform.position+Cube[16].transform.position)/2);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[12].transform.position );
		LinRen.SetPosition( 1, Cube[4].transform.position );
		midpts[LineIndex] = ((Cube[12].transform.position+Cube[4].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[12].transform.position );
		LinRen.SetPosition( 1, Cube[7].transform.position );
		midpts[LineIndex] = ((Cube[12].transform.position+Cube[7].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[12].transform.position );
		LinRen.SetPosition( 1, Cube[20].transform.position );
		midpts[LineIndex] = ((Cube[12].transform.position+Cube[20].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[12].transform.position );
		LinRen.SetPosition( 1, Cube[17].transform.position );
		midpts[LineIndex] = ((Cube[12].transform.position+Cube[17].transform.position)/2);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[13].transform.position );
		LinRen.SetPosition( 1, Cube[8].transform.position );
		midpts[LineIndex] = ((Cube[13].transform.position+Cube[8].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[13].transform.position );
		LinRen.SetPosition( 1, Cube[5].transform.position );
		midpts[LineIndex] = ((Cube[13].transform.position+Cube[5].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[13].transform.position );
		LinRen.SetPosition( 1, Cube[21].transform.position );
		midpts[LineIndex] = ((Cube[13].transform.position+Cube[21].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[13].transform.position );
		LinRen.SetPosition( 1, Cube[26].transform.position );
		midpts[LineIndex] = ((Cube[13].transform.position+Cube[26].transform.position)/2);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[14].transform.position );
		LinRen.SetPosition( 1, Cube[6].transform.position );
		midpts[LineIndex] = ((Cube[14].transform.position+Cube[6].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[14].transform.position );
		LinRen.SetPosition( 1, Cube[19].transform.position );
		midpts[LineIndex] = ((Cube[14].transform.position+Cube[19].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[14].transform.position );
		LinRen.SetPosition( 1, Cube[27].transform.position );
		midpts[LineIndex] = ((Cube[14].transform.position+Cube[27].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[14].transform.position );
		LinRen.SetPosition( 1, Cube[22].transform.position );
		midpts[LineIndex] = ((Cube[14].transform.position+Cube[22].transform.position)/2);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[15].transform.position );
		LinRen.SetPosition( 1, Cube[7].transform.position );
		midpts[LineIndex] = ((Cube[15].transform.position+Cube[7].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[15].transform.position );
		LinRen.SetPosition( 1, Cube[20].transform.position );
		midpts[LineIndex] = ((Cube[15].transform.position+Cube[20].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[15].transform.position );
		LinRen.SetPosition( 1, Cube[28].transform.position );
		midpts[LineIndex] = ((Cube[15].transform.position+Cube[28].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[15].transform.position );
		LinRen.SetPosition( 1, Cube[23].transform.position );
		midpts[LineIndex] = ((Cube[15].transform.position+Cube[23].transform.position)/2);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[16].transform.position );
		LinRen.SetPosition( 1, Cube[11].transform.position );
		midpts[LineIndex] = ((Cube[16].transform.position+Cube[11].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[16].transform.position );
		LinRen.SetPosition( 1, Cube[8].transform.position );
		midpts[LineIndex] = ((Cube[16].transform.position+Cube[8].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[16].transform.position );
		LinRen.SetPosition( 1, Cube[21].transform.position );
		midpts[LineIndex] = ((Cube[16].transform.position+Cube[21].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[16].transform.position );
		LinRen.SetPosition( 1, Cube[24].transform.position );
		midpts[LineIndex] = ((Cube[16].transform.position+Cube[24].transform.position)/2);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[17].transform.position );
		LinRen.SetPosition( 1, Cube[9].transform.position );
		midpts[LineIndex] = ((Cube[17].transform.position+Cube[9].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[17].transform.position );
		LinRen.SetPosition( 1, Cube[12].transform.position );
		midpts[LineIndex] = ((Cube[17].transform.position+Cube[12].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[17].transform.position );
		LinRen.SetPosition( 1, Cube[25].transform.position );
		midpts[LineIndex] = ((Cube[17].transform.position+Cube[25].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[17].transform.position );
		LinRen.SetPosition( 1, Cube[22].transform.position );
		midpts[LineIndex] = ((Cube[17].transform.position+Cube[22].transform.position)/2);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[18].transform.position );
		LinRen.SetPosition( 1, Cube[13].transform.position );
		midpts[LineIndex] = ((Cube[18].transform.position+Cube[13].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[18].transform.position );
		LinRen.SetPosition( 1, Cube[26].transform.position );
		midpts[LineIndex] = ((Cube[18].transform.position+Cube[26].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[18].transform.position );
		LinRen.SetPosition( 1, Cube[31].transform.position );
		midpts[LineIndex] = ((Cube[18].transform.position+Cube[31].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[18].transform.position );
		LinRen.SetPosition( 1, Cube[23].transform.position );
		midpts[LineIndex] = ((Cube[18].transform.position+Cube[23].transform.position)/2);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[19].transform.position );
		LinRen.SetPosition( 1, Cube[24].transform.position );
		midpts[LineIndex] = ((Cube[19].transform.position+Cube[24].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[19].transform.position );
		LinRen.SetPosition( 1, Cube[32].transform.position );
		midpts[LineIndex] = ((Cube[19].transform.position+Cube[32].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[19].transform.position );
		LinRen.SetPosition( 1, Cube[27].transform.position );
		midpts[LineIndex] = ((Cube[19].transform.position+Cube[27].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[19].transform.position );
		LinRen.SetPosition( 1, Cube[11].transform.position );
		midpts[LineIndex] = ((Cube[19].transform.position+Cube[11].transform.position)/2);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[20].transform.position );
		LinRen.SetPosition( 1, Cube[28].transform.position );
		midpts[LineIndex] = ((Cube[20].transform.position+Cube[28].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[20].transform.position );
		LinRen.SetPosition( 1, Cube[33].transform.position );
		midpts[LineIndex] = ((Cube[20].transform.position+Cube[33].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[20].transform.position );
		LinRen.SetPosition( 1, Cube[15].transform.position );
		midpts[LineIndex] = ((Cube[20].transform.position+Cube[15].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[20].transform.position );
		LinRen.SetPosition( 1, Cube[12].transform.position );
		midpts[LineIndex] = ((Cube[20].transform.position+Cube[12].transform.position)/2);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[21].transform.position );
		LinRen.SetPosition( 1, Cube[26].transform.position );
		midpts[LineIndex] = ((Cube[21].transform.position+Cube[26].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[21].transform.position );
		LinRen.SetPosition( 1, Cube[13].transform.position );
		midpts[LineIndex] = ((Cube[21].transform.position+Cube[13].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[21].transform.position );
		LinRen.SetPosition( 1, Cube[16].transform.position );
		midpts[LineIndex] = ((Cube[21].transform.position+Cube[16].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[21].transform.position );
		LinRen.SetPosition( 1, Cube[19].transform.position );
		midpts[LineIndex] = ((Cube[21].transform.position+Cube[19].transform.position)/2);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[22].transform.position );
		LinRen.SetPosition( 1, Cube[27].transform.position );
		midpts[LineIndex] = ((Cube[22].transform.position+Cube[27].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[22].transform.position );
		LinRen.SetPosition( 1, Cube[14].transform.position );
		midpts[LineIndex] = ((Cube[22].transform.position+Cube[14].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[22].transform.position );
		LinRen.SetPosition( 1, Cube[17].transform.position );
		midpts[LineIndex] = ((Cube[22].transform.position+Cube[17].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[22].transform.position );
		LinRen.SetPosition( 1, Cube[30].transform.position );
		midpts[LineIndex] = ((Cube[22].transform.position+Cube[30].transform.position)/2);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[23].transform.position );
		LinRen.SetPosition( 1, Cube[28].transform.position );
		midpts[LineIndex] = ((Cube[23].transform.position+Cube[28].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[23].transform.position );
		LinRen.SetPosition( 1, Cube[15].transform.position );
		midpts[LineIndex] = ((Cube[23].transform.position+Cube[15].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[23].transform.position );
		LinRen.SetPosition( 1, Cube[18].transform.position );
		midpts[LineIndex] = ((Cube[23].transform.position+Cube[18].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[23].transform.position );
		LinRen.SetPosition( 1, Cube[31].transform.position );
		midpts[LineIndex] = ((Cube[23].transform.position+Cube[31].transform.position)/2);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[24].transform.position );
		LinRen.SetPosition( 1, Cube[29].transform.position );
		midpts[LineIndex] = ((Cube[24].transform.position+Cube[29].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[24].transform.position );
		LinRen.SetPosition( 1, Cube[16].transform.position );
		midpts[LineIndex] = ((Cube[24].transform.position+Cube[16].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[24].transform.position );
		LinRen.SetPosition( 1, Cube[19].transform.position );
		midpts[LineIndex] = ((Cube[24].transform.position+Cube[19].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[24].transform.position );
		LinRen.SetPosition( 1, Cube[32].transform.position );
		midpts[LineIndex] = ((Cube[24].transform.position+Cube[32].transform.position)/2);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[25].transform.position );
		LinRen.SetPosition( 1, Cube[30].transform.position );
		midpts[LineIndex] = ((Cube[25].transform.position+Cube[30].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[25].transform.position );
		LinRen.SetPosition( 1, Cube[17].transform.position );
		midpts[LineIndex] = ((Cube[25].transform.position+Cube[17].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[25].transform.position );
		LinRen.SetPosition( 1, Cube[20].transform.position );
		midpts[LineIndex] = ((Cube[25].transform.position+Cube[20].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[25].transform.position );
		LinRen.SetPosition( 1, Cube[33].transform.position );
		midpts[LineIndex] = ((Cube[25].transform.position+Cube[33].transform.position)/2);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[26].transform.position );
		LinRen.SetPosition( 1, Cube[18].transform.position );
		midpts[LineIndex] = ((Cube[26].transform.position+Cube[18].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[26].transform.position );
		LinRen.SetPosition( 1, Cube[31].transform.position );
		midpts[LineIndex] = ((Cube[26].transform.position+Cube[31].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[26].transform.position );
		LinRen.SetPosition( 1, Cube[39].transform.position );
		midpts[LineIndex] = ((Cube[26].transform.position+Cube[39].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[26].transform.position );
		LinRen.SetPosition( 1, Cube[34].transform.position );
		midpts[LineIndex] = ((Cube[26].transform.position+Cube[34].transform.position)/2);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[27].transform.position );
		LinRen.SetPosition( 1, Cube[22].transform.position );
		midpts[LineIndex] = ((Cube[27].transform.position+Cube[22].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[27].transform.position );
		LinRen.SetPosition( 1, Cube[35].transform.position );
		midpts[LineIndex] = ((Cube[27].transform.position+Cube[35].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[27].transform.position );
		LinRen.SetPosition( 1, Cube[32].transform.position );
		midpts[LineIndex] = ((Cube[27].transform.position+Cube[32].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[27].transform.position );
		LinRen.SetPosition( 1, Cube[20].transform.position );
		midpts[LineIndex] = ((Cube[27].transform.position+Cube[20].transform.position)/2);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[28].transform.position );
		LinRen.SetPosition( 1, Cube[23].transform.position );
		midpts[LineIndex] = ((Cube[28].transform.position+Cube[23].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[28].transform.position );
		LinRen.SetPosition( 1, Cube[36].transform.position );
		midpts[LineIndex] = ((Cube[28].transform.position+Cube[36].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[28].transform.position );
		LinRen.SetPosition( 1, Cube[33].transform.position );
		midpts[LineIndex] = ((Cube[28].transform.position+Cube[33].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[28].transform.position );
		LinRen.SetPosition( 1, Cube[20].transform.position );
		midpts[LineIndex] = ((Cube[28].transform.position+Cube[20].transform.position)/2);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[29].transform.position );
		LinRen.SetPosition( 1, Cube[24].transform.position );
		midpts[LineIndex] = ((Cube[29].transform.position+Cube[24].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[29].transform.position );
		LinRen.SetPosition( 1, Cube[37].transform.position );
		midpts[LineIndex] = ((Cube[29].transform.position+Cube[37].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[29].transform.position );
		LinRen.SetPosition( 1, Cube[34].transform.position );
		midpts[LineIndex] = ((Cube[29].transform.position+Cube[34].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[29].transform.position );
		LinRen.SetPosition( 1, Cube[31].transform.position );
		midpts[LineIndex] = ((Cube[29].transform.position+Cube[31].transform.position)/2);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[30].transform.position );
		LinRen.SetPosition( 1, Cube[22].transform.position );
		midpts[LineIndex] = ((Cube[30].transform.position+Cube[22].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[30].transform.position );
		LinRen.SetPosition( 1, Cube[17].transform.position );
		midpts[LineIndex] = ((Cube[30].transform.position+Cube[17].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[30].transform.position );
		LinRen.SetPosition( 1, Cube[35].transform.position );
		midpts[LineIndex] = ((Cube[30].transform.position+Cube[35].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[30].transform.position );
		LinRen.SetPosition( 1, Cube[38].transform.position );
		midpts[LineIndex] = ((Cube[30].transform.position+Cube[38].transform.position)/2);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[31].transform.position );
		LinRen.SetPosition( 1, Cube[26].transform.position );
		midpts[LineIndex] = ((Cube[31].transform.position+Cube[26].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[31].transform.position );
		LinRen.SetPosition( 1, Cube[18].transform.position );
		midpts[LineIndex] = ((Cube[31].transform.position+Cube[18].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[31].transform.position );
		LinRen.SetPosition( 1, Cube[23].transform.position );
		midpts[LineIndex] = ((Cube[31].transform.position+Cube[23].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[31].transform.position );
		LinRen.SetPosition( 1, Cube[39].transform.position );
		midpts[LineIndex] = ((Cube[31].transform.position+Cube[39].transform.position)/2);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[32].transform.position );
		LinRen.SetPosition( 1, Cube[37].transform.position );
		midpts[LineIndex] = ((Cube[32].transform.position+Cube[37].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[32].transform.position );
		LinRen.SetPosition( 1, Cube[24].transform.position );
		midpts[LineIndex] = ((Cube[31].transform.position+Cube[24].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[32].transform.position );
		LinRen.SetPosition( 1, Cube[19].transform.position );
		midpts[LineIndex] = ((Cube[32].transform.position+Cube[19].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[32].transform.position );
		LinRen.SetPosition( 1, Cube[27].transform.position );
		midpts[LineIndex] = ((Cube[32].transform.position+Cube[27].transform.position)/2);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[33].transform.position );
		LinRen.SetPosition( 1, Cube[41].transform.position );
		midpts[LineIndex] = ((Cube[33].transform.position+Cube[41].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[33].transform.position );
		LinRen.SetPosition( 1, Cube[38].transform.position );
		midpts[LineIndex] = ((Cube[33].transform.position+Cube[38].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[33].transform.position );
		LinRen.SetPosition( 1, Cube[25].transform.position );
		midpts[LineIndex] = ((Cube[33].transform.position+Cube[25].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[33].transform.position );
		LinRen.SetPosition( 1, Cube[28].transform.position );
		midpts[LineIndex] = ((Cube[33].transform.position+Cube[28].transform.position)/2);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[34].transform.position );
		LinRen.SetPosition( 1, Cube[39].transform.position );
		midpts[LineIndex] = ((Cube[34].transform.position+Cube[39].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[34].transform.position );
		LinRen.SetPosition( 1, Cube[42].transform.position );
		midpts[LineIndex] = ((Cube[34].transform.position+Cube[42].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[34].transform.position );
		LinRen.SetPosition( 1, Cube[29].transform.position );
		midpts[LineIndex] = ((Cube[34].transform.position+Cube[29].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[34].transform.position );
		LinRen.SetPosition( 1, Cube[26].transform.position );
		midpts[LineIndex] = ((Cube[34].transform.position+Cube[26].transform.position)/2);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[35].transform.position );
		LinRen.SetPosition( 1, Cube[43].transform.position );
		midpts[LineIndex] = ((Cube[35].transform.position+Cube[43].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[35].transform.position );
		LinRen.SetPosition( 1, Cube[30].transform.position );
		midpts[LineIndex] = ((Cube[35].transform.position+Cube[30].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[35].transform.position );
		LinRen.SetPosition( 1, Cube[22].transform.position );
		midpts[LineIndex] = ((Cube[35].transform.position+Cube[22].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[35].transform.position );
		LinRen.SetPosition( 1, Cube[27].transform.position );
		midpts[LineIndex] = ((Cube[35].transform.position+Cube[27].transform.position)/2);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[36].transform.position );
		LinRen.SetPosition( 1, Cube[44].transform.position );
		midpts[LineIndex] = ((Cube[36].transform.position+Cube[44].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[36].transform.position );
		LinRen.SetPosition( 1, Cube[31].transform.position );
		midpts[LineIndex] = ((Cube[36].transform.position+Cube[31].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[36].transform.position );
		LinRen.SetPosition( 1, Cube[23].transform.position );
		midpts[LineIndex] = ((Cube[36].transform.position+Cube[23].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[36].transform.position );
		LinRen.SetPosition( 1, Cube[28].transform.position );
		midpts[LineIndex] = ((Cube[36].transform.position+Cube[28].transform.position)/2);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[37].transform.position );
		LinRen.SetPosition( 1, Cube[42].transform.position );
		midpts[LineIndex] = ((Cube[37].transform.position+Cube[42].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[37].transform.position );
		LinRen.SetPosition( 1, Cube[45].transform.position );
		midpts[LineIndex] = ((Cube[37].transform.position+Cube[45].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[37].transform.position );
		LinRen.SetPosition( 1, Cube[29].transform.position );
		midpts[LineIndex] = ((Cube[37].transform.position+Cube[29].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[37].transform.position );
		LinRen.SetPosition( 1, Cube[24].transform.position );
		midpts[LineIndex] = ((Cube[37].transform.position+Cube[24].transform.position)/2);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[38].transform.position );
		LinRen.SetPosition( 1, Cube[46].transform.position );
		midpts[LineIndex] = ((Cube[38].transform.position+Cube[46].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[38].transform.position );
		LinRen.SetPosition( 1, Cube[43].transform.position );
		midpts[LineIndex] = ((Cube[38].transform.position+Cube[43].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[38].transform.position );
		LinRen.SetPosition( 1, Cube[30].transform.position );
		midpts[LineIndex] = ((Cube[38].transform.position+Cube[30].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[38].transform.position );
		LinRen.SetPosition( 1, Cube[33].transform.position );
		midpts[LineIndex] = ((Cube[38].transform.position+Cube[33].transform.position)/2);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[39].transform.position );
		LinRen.SetPosition( 1, Cube[44].transform.position );
		midpts[LineIndex] = ((Cube[39].transform.position+Cube[44].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[39].transform.position );
		LinRen.SetPosition( 1, Cube[31].transform.position );
		midpts[LineIndex] = ((Cube[39].transform.position+Cube[31].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[39].transform.position );
		LinRen.SetPosition( 1, Cube[26].transform.position );
		midpts[LineIndex] = ((Cube[39].transform.position+Cube[26].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[39].transform.position );
		LinRen.SetPosition( 1, Cube[34].transform.position );
		midpts[LineIndex] = ((Cube[39].transform.position+Cube[34].transform.position)/2);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[40].transform.position );
		LinRen.SetPosition( 1, Cube[48].transform.position );
		midpts[LineIndex] = ((Cube[40].transform.position+Cube[48].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[40].transform.position );
		LinRen.SetPosition( 1, Cube[43].transform.position );
		midpts[LineIndex] = ((Cube[40].transform.position+Cube[43].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[40].transform.position );
		LinRen.SetPosition( 1, Cube[35].transform.position );
		midpts[LineIndex] = ((Cube[40].transform.position+Cube[35].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[40].transform.position );
		LinRen.SetPosition( 1, Cube[27].transform.position );
		midpts[LineIndex] = ((Cube[40].transform.position+Cube[27].transform.position)/2);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[41].transform.position );
		LinRen.SetPosition( 1, Cube[33].transform.position );
		midpts[LineIndex] = ((Cube[41].transform.position+Cube[33].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[41].transform.position );
		LinRen.SetPosition( 1, Cube[46].transform.position );
		midpts[LineIndex] = ((Cube[41].transform.position+Cube[46].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[41].transform.position );
		LinRen.SetPosition( 1, Cube[44].transform.position );
		midpts[LineIndex] = ((Cube[41].transform.position+Cube[44].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[41].transform.position );
		LinRen.SetPosition( 1, Cube[34].transform.position );
		midpts[LineIndex] = ((Cube[41].transform.position+Cube[34].transform.position)/2);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[42].transform.position );
		LinRen.SetPosition( 1, Cube[47].transform.position );
		midpts[LineIndex] = ((Cube[42].transform.position+Cube[47].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[42].transform.position );
		LinRen.SetPosition( 1, Cube[45].transform.position );
		midpts[LineIndex] = ((Cube[42].transform.position+Cube[45].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[42].transform.position );
		LinRen.SetPosition( 1, Cube[34].transform.position );
		midpts[LineIndex] = ((Cube[42].transform.position+Cube[34].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[42].transform.position );
		LinRen.SetPosition( 1, Cube[37].transform.position );
		midpts[LineIndex] = ((Cube[42].transform.position+Cube[37].transform.position)/2);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[43].transform.position );
		LinRen.SetPosition( 1, Cube[46].transform.position );
		midpts[LineIndex] = ((Cube[43].transform.position+Cube[46].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[43].transform.position );
		LinRen.SetPosition( 1, Cube[48].transform.position );
		midpts[LineIndex] = ((Cube[43].transform.position+Cube[48].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[43].transform.position );
		LinRen.SetPosition( 1, Cube[40].transform.position );
		midpts[LineIndex] = ((Cube[43].transform.position+Cube[40].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[43].transform.position );
		LinRen.SetPosition( 1, Cube[35].transform.position );
		midpts[LineIndex] = ((Cube[43].transform.position+Cube[35].transform.position)/2);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[44].transform.position );
		LinRen.SetPosition( 1, Cube[49].transform.position );
		midpts[LineIndex] = ((Cube[44].transform.position+Cube[49].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[44].transform.position );
		LinRen.SetPosition( 1, Cube[47].transform.position );
		midpts[LineIndex] = ((Cube[44].transform.position+Cube[47].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[44].transform.position );
		LinRen.SetPosition( 1, Cube[39].transform.position );
		midpts[LineIndex] = ((Cube[44].transform.position+Cube[39].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[44].transform.position );
		LinRen.SetPosition( 1, Cube[37].transform.position );
		midpts[LineIndex] = ((Cube[44].transform.position+Cube[37].transform.position)/2);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[45].transform.position );
		LinRen.SetPosition( 1, Cube[40].transform.position );
		midpts[LineIndex] = ((Cube[45].transform.position+Cube[40].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[45].transform.position );
		LinRen.SetPosition( 1, Cube[48].transform.position );
		midpts[LineIndex] = ((Cube[45].transform.position+Cube[48].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[45].transform.position );
		LinRen.SetPosition( 1, Cube[32].transform.position );
		midpts[LineIndex] = ((Cube[45].transform.position+Cube[32].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[45].transform.position );
		LinRen.SetPosition( 1, Cube[37].transform.position );
		midpts[LineIndex] = ((Cube[45].transform.position+Cube[37].transform.position)/2);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[46].transform.position );
		LinRen.SetPosition( 1, Cube[38].transform.position );
		midpts[LineIndex] = ((Cube[46].transform.position+Cube[38].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[46].transform.position );
		LinRen.SetPosition( 1, Cube[43].transform.position );
		midpts[LineIndex] = ((Cube[46].transform.position+Cube[43].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[46].transform.position );
		LinRen.SetPosition( 1, Cube[48].transform.position );
		midpts[LineIndex] = ((Cube[46].transform.position+Cube[48].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[46].transform.position );
		LinRen.SetPosition( 1, Cube[49].transform.position );
		midpts[LineIndex] = ((Cube[46].transform.position+Cube[49].transform.position)/2);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[47].transform.position );
		LinRen.SetPosition( 1, Cube[42].transform.position );
		midpts[LineIndex] = ((Cube[47].transform.position+Cube[42].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[47].transform.position );
		LinRen.SetPosition( 1, Cube[39].transform.position );
		midpts[LineIndex] = ((Cube[47].transform.position+Cube[39].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[47].transform.position );
		LinRen.SetPosition( 1, Cube[44].transform.position );
		midpts[LineIndex] = ((Cube[47].transform.position+Cube[44].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[47].transform.position );
		LinRen.SetPosition( 1, Cube[49].transform.position );
		midpts[LineIndex] = ((Cube[47].transform.position+Cube[49].transform.position)/2);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[48].transform.position );
		LinRen.SetPosition( 1, Cube[45].transform.position );
		midpts[LineIndex] = ((Cube[48].transform.position+Cube[45].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[48].transform.position );
		LinRen.SetPosition( 1, Cube[40].transform.position );
		midpts[LineIndex] = ((Cube[48].transform.position+Cube[40].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[48].transform.position );
		LinRen.SetPosition( 1, Cube[49].transform.position );
		midpts[LineIndex] = ((Cube[48].transform.position+Cube[49].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[48].transform.position );
		LinRen.SetPosition( 1, Cube[46].transform.position );
		midpts[LineIndex] = ((Cube[48].transform.position+Cube[46].transform.position)/2);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[49].transform.position );
		LinRen.SetPosition( 1, Cube[47].transform.position );
		midpts[LineIndex] = ((Cube[49].transform.position+Cube[47].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[49].transform.position );
		LinRen.SetPosition( 1, Cube[44].transform.position );
		midpts[LineIndex] = ((Cube[49].transform.position+Cube[44].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[49].transform.position );
		LinRen.SetPosition( 1, Cube[46].transform.position );
		midpts[LineIndex] = ((Cube[49].transform.position+Cube[46].transform.position)/2);
		LineIndex++;
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[49].transform.position );
		LinRen.SetPosition( 1, Cube[48].transform.position );
		midpts[LineIndex] = ((Cube[49].transform.position+Cube[48].transform.position)/2);
		LineIndex++;
		
		
	
		
//		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
//		LinRen.SetVertexCount( 2 );
//		LinRen.SetPosition( 0, Cube[50].transform.position );
//		LinRen.SetPosition( 1, Cube[3].transform.position );
//		LineIndex++;
//		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
//		LinRen.SetVertexCount( 2 );
//		LinRen.SetPosition( 0, Cube[50].transform.position );
//		LinRen.SetPosition( 1, Cube[6].transform.position );
//		LineIndex++;
//		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
//		LinRen.SetVertexCount( 2 );
//		LinRen.SetPosition( 0, Cube[50].transform.position );
//		LinRen.SetPosition( 1, Cube[4].transform.position );
//		LineIndex++;
//		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
//		LinRen.SetVertexCount( 2 );
//		LinRen.SetPosition( 0, Cube[50].transform.position );
//		LinRen.SetPosition( 1, Cube[2].transform.position );
//		LineIndex++;
//		
//		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
//		LinRen.SetVertexCount( 2 );
//		LinRen.SetPosition( 0, Cube[1].transform.position );
//		LinRen.SetPosition( 1, Cube[3].transform.position );
//		LineIndex++;
//		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
//		LinRen.SetVertexCount( 2 );
//		LinRen.SetPosition( 0, Cube[1].transform.position );
//		LinRen.SetPosition( 1, Cube[6].transform.position );
//		LineIndex++;
//		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
//		LinRen.SetVertexCount( 2 );
//		LinRen.SetPosition( 0, Cube[1].transform.position );
//		LinRen.SetPosition( 1, Cube[4].transform.position );
//		LineIndex++;
//		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
//		LinRen.SetVertexCount( 2 );
//		LinRen.SetPosition( 0, Cube[1].transform.position );
//		LinRen.SetPosition( 1, Cube[2].transform.position );
//		LineIndex++;
		
	}
	
	private void DrawDynamicLineRenderers( int Index )
	{
		for ( int i = 0; i < Nearpoints[ Index ].Length; i++ )
		{
			LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
			LinRen.SetVertexCount( 2 );
			LinRen.SetPosition( 0, Nearpoints[Index][0].gameObject.transform.position );
			LinRen.SetPosition( 1, Nearpoints[Index][i].gameObject.transform.position );
			//midpts[LineIndex] = ((Cube[1].transform.position+Cube[3].transform.position)/2);
			LineIndex++;
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
		
//		
		//DrawAlllines( 1 );
		
	}
	
	 void OnDrawGizmosSelected() {
//        Gizmos.color = Color.white;
//        Gizmos.DrawWireSphere(transform.position, explosionRadius);
		   Vector3 center = renderer.bounds.center;
        float radius = renderer.bounds.extents.magnitude;
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(center, radius);
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
		int PositionOfIndex = Data.IndexOf( "\n" + Index + "\t") + "\n".Length + Index.ToString().Length;
		string TrimmedData = Data.Substring( PositionOfIndex, Data.Length - PositionOfIndex );
		int IndexOfcloseBracket = TrimmedData.IndexOf( ")" );
		TrimmedData = TrimmedData.Substring( 0, IndexOfcloseBracket );
		TrimmedData = TrimmedData.Replace( Index.ToString(), "" ).Trim();
		var Numbers = TrimmedData.Split( ","[0]);
		Numbers[0] = Numbers[0].Replace( "(", "");
		Numbers[2] = Numbers[2].Replace( ")", "");
//		Debug.Log( Numbers[0] + Numbers[1] + Numbers[2]);
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
		int[] PointsInd = new int[2];
		Vector3[] varray = new Vector3[2];
		int Index0;
		if( Index == 1 )
		{
			Index0 =  Data.IndexOf( Index.ToString() + "\t" ) + ( Index.ToString() + "\t").Length;
		}
		else
		{
			Index0 =  Data.IndexOf( "\n" + Index.ToString() + "\t" ) + ( "\n" + Index.ToString() + "\t").Length;
		}
		Debug.Log( "Index 0 "+ Index0 );
		Data = Data.Substring( Index0, Data.Length - Index0 );
		int Index1 = 4;
		Debug.Log( Data );
		//Debug.Log( Index0 + " " + (Index1 - Index0).ToString() );
		Debug.Log( Data.Substring( 0, 4 ));
		var DataArray = Data.Substring( 0, 4 ).Split ( new string []{ "\t" }, System.StringSplitOptions.None );
		Debug.Log( DataArray[0] + " " + DataArray[1] );
		for( int i = 0; i < DataArray.Length; i++ )
		{
			Debug.Log( DataArray[i] );
			PointsInd[i] = int.Parse( DataArray[i] );
			varray[i] = GetPoint( PointsInd[i] );
		}
		return varray;
	}
	
	void Plotlines()
	{
		for ( int i = 1; i <= 50; i++)
		{
			int[] Points = new int[7];
			Points = NearestPoints( i );
			DrawLines( i, Points);
		}
	}
	
	void DrawLines( int Index, int[] Plot )
	{
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[Index].transform.position );
		LinRen.SetPosition( 1, Cube[ Plot[1] ].transform.position);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[Index].transform.position );
		LinRen.SetPosition( 1, Cube[ Plot[2] ].transform.position);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[Index].transform.position );
		LinRen.SetPosition( 1, Cube[ Plot[3] ].transform.position);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[Index].transform.position );
		LinRen.SetPosition( 1, Cube[ Plot[4] ].transform.position);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[Index].transform.position );
		LinRen.SetPosition( 1, Cube[ Plot[5] ].transform.position);
		LineIndex++;
		
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Cube[Index].transform.position );
		LinRen.SetPosition( 1, Cube[ Plot[6] ].transform.position);
		LineIndex++;
		
//		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
//		LinRen.SetVertexCount( 2 );
//		LinRen.SetPosition( 0, Cube[Index].transform.position );
//		LinRen.SetPosition( 1, Cube[ Plot[7] ].transform.position);
//		LineIndex++;
		
	}
	void DrawAlllines( int Index )
	{
		Vector3 Start = GetPoint( Index );
		Debug.Log("Srtatt: " + Index );
		Vector3[] target = GetTargetPoints( Index );
		Mesh mMesh = new Mesh();
		Vector3[] MeshTargets = new Vector3[3]{ Start, target[0], target[1] };
		Debug.Log( Start + " " + target[0] + " " + target[1] );
		TargetCube = Physics.OverlapSphere( target[0], 0.1f );
//		//Debug.Log( TargetCube[0].gameObject.name );
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Start );
		//Debug.Log(" hahahahhahahah: " + TargetCube[0].gameObject.transform.position );
		//LinRen.SetPosition( 1, TargetCube[0].gameObject.transform.position );
		LinRen.SetPosition( 1, target[0] );
		LineIndex++;
//		
		TargetCube = Physics.OverlapSphere( target[1], 0.1f );
//		//Debug.Log( TargetCube[0].gameObject.name );
		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
		LinRen.SetVertexCount( 2 );
		LinRen.SetPosition( 0, Start );
		//Debug.Log( TargetCube[0].GetType() );
		//LinRen.SetPosition( 1, TargetCube[0].gameObject.transform.position );
		LinRen.SetPosition( 1, target[1] );
		LineIndex++;
		mMesh.vertices= MeshTargets;
		mMesh.uv = new Vector2[]{ new Vector2 (0, 0), new Vector2 (0, 1), new Vector2(1, 1), new Vector2 (1, 0) };
		mMesh.triangles = new int[]{ 0, 1, 2, 0, 2, 3 };
		mMesh.RecalculateNormals();
		var G2 = Instantiate( GameObject.CreatePrimitive( PrimitiveType.Plane ), Start, Quaternion.identity ) as GameObject;
		G2.transform.parent = transform;
		G2.gameObject.GetComponent<MeshFilter>().mesh = mMesh;
//		//Debug.Log("target[2]"+target[2]);
//		
//		TargetCube = Physics.OverlapSphere( target[2], 1.0f );
//		//Debug.Log( TargetCube[0].gameObject.name );
////		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
////		LinRen.SetVertexCount( 2 );
////		LinRen.SetPosition( 0, Start );
////		//Debug.Log( TargetCube[0].GetType() );
////		LinRen.SetPosition( 1, TargetCube[0].gameObject.transform.position );
//		//Debug.Log( TargetCube[0].GetType() );
//		LineIndex++;
//		TargetCube = Physics.OverlapSphere( target[3], 0.1f );
//		//Debug.Log( TargetCube[0].gameObject.name );
////		LinRen = GameObject.Find( "Line" + LineIndex ).GetComponent<LineRenderer>();
////		LinRen.SetVertexCount( 2 );
////		LinRen.SetPosition( 0, Start );
////		LinRen.SetPosition( 1, TargetCube[0].gameObject.transform.position );
//		LineIndex++;
//		
////		TargetCube = Physics.OverlapSphere( target[4], 0.1f );
////		//Debug.Log( TargetCube[0].gameObject.name );
////		LinRen = GameObject.Find( "Line5" ).GetComponent<LineRenderer>();
////		LinRen.SetVertexCount( 2 );
////		LinRen.SetPosition( 0, Start );
////		LinRen.SetPosition( 1, TargetCube[0].gameObject.transform.position );
		
		
	}
	
	void SavePointToOtherOne1( int TargetIndex )
	{
		//int TargetIndex = 2;
		int[] TargetPoints = new int[2];
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
		
		TargetPoints[1] = TargetIndex + 4 ;//new Vector3( 0.4f, -1.9f, 0.6f  );
		
		//TargetPoints[2] = TargetIndex + 7 ;//new Vector3( -0.8f, -1.8f, -0.1f );
		
//		TargetPoints[3] = TargetIndex + 4 ;//new Vector3( 0.8f, -1.8f, -0.5f );
//		
//		TargetPoints[4] = TargetIndex + 5 ;//new Vector3( -0.3f, -1.7f, 1.0f );
		string PreviousData = System.Text.Encoding.UTF8.GetString( System.IO.File.ReadAllBytes( GetDocumentsDirectoryPath() + "/ConnectedPoints.xls" ));
		string DataToSave = PreviousData + TargetIndex.ToString() + "\t" + TargetPoints[0].ToString() + "\t" + TargetPoints[1].ToString() + /*"\t" + TargetPoints[2].ToString() + "\t" + TargetPoints[3].ToString() + "\t" + TargetPoints[4].ToString() + "\t" +*/ "\n";
		DataToSave = DataToSave.Replace( "0", "");
		var bytes = System.Text.Encoding.UTF8.GetBytes( DataToSave );
		System.IO.File.WriteAllBytes ( GetDocumentsDirectoryPath() + "/ConnectedPoints.xls", bytes );
	}
	
	void SavePointToOtherOne( int Index )
	{
		string PreviousData = System.Text.Encoding.UTF8.GetString( System.IO.File.ReadAllBytes( GetDocumentsDirectoryPath() + "/ConnectedPoints.xls" ));
		string PointsToSave = "";
		for( int i = 0; i < Nearpoints[ Index ].Length; i++ )
		{
			PointsToSave = PointsToSave + "\t" + Nearpoints[ Index ][i].gameObject.transform.position;
		}
		string DataToSave = PreviousData + Index.ToString() + PointsToSave + "\n";//"\t" + Nearpoints[ Index ][0].gameObject.transform.position + "\t" + Nearpoints[ Index ][1].gameObject.transform.position + "\n";// + "\t" + Nearpoints[ Index ][2].gameObject.transform.position + "\t" + Nearpoints[ Index ][3].gameObject.transform.position + "\n" ;
		var bytes = System.Text.Encoding.UTF8.GetBytes( DataToSave );
		System.IO.File.WriteAllBytes ( GetDocumentsDirectoryPath() + "/ConnectedPoints.xls", bytes );
	}
	
	float CalculateDistance( Vector3 a, Vector3 b )
	{
		float distance;
		distance = Mathf.Sqrt(  Mathf.Pow ( a.x - b.x, 2 ) + Mathf.Pow ( a.y - b.y, 2 ) +Mathf.Pow ( a.z - b.z, 2 ) );
		return distance;
	}
	
	private int[] NearestPoints( int Index )
	{
		float[] Distance = new float[51];
		int[] CubeNo = new int[51];
		for ( int i = 1; i <= 50; i++ )
		{
			Distance[i] =  CalculateDistance ( Cube[ Index ].transform.position, Cube[i].transform.position );
			CubeNo[i] = i;
		}
		for ( int i = 1; i <= 50; i++ )
		{
			for ( int j = 1; j <= 50; j++ )
			{
				if( Distance[i] < Distance[j] )
				{
					float temp = Distance[i];
					int temp1 = CubeNo[i];
					Distance[i] = Distance[j];
					CubeNo[i] = CubeNo[j];
					Distance[j] = temp;
					CubeNo[j] = temp1;
				}
			}
		}
		
//		for( int i = 1; i <= 50; i++ )
//		{
//			Debug.Log( Distance[i] );
//			Debug.Log( CubeNo[i]);
//		}
		
		int[] Result = new int[12];
		//Result = CubeNo;
		for( int i = 0; i<= 11; i++ )
		{
			Result[i] = CubeNo[i+1];
		}
		return Result;
	}
}