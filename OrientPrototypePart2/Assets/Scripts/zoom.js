#pragma strict

var previousDistance:float =0;
var camera1:Camera;
var zoomNow : boolean;
var TextToDisplay:GameObject;
static var fov1:float;
function Start () {
	camera1 = this.gameObject.GetComponent(Camera);
	fov1 =camera1.fieldOfView;
}

function Update () {
	
	/*if (Input.touchCount >= 2 ) {
		var touch0:Touch;
		var touch1:Touch;
		var newDistance: float;
		touch0 = Input.GetTouch(0);
		touch1 = Input.GetTouch(1);
		
		
		if (touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved){
			
			newDistance = Vector2.Distance(touch0.position, touch1.position);
			var gap:float = newDistance - previousDistance;
			
			
			if(fov1 >=20 && fov1 <=60 && zoomNow) {
				fov1 -=gap*0.1;
			}
			 if(fov1 <20) {
			 	fov1 =20;
			 }
		    if(fov1 >60) {
			 	fov1 =60;
			}
			
			camera1.fieldOfView=fov1;
			previousDistance = newDistance;
			zoomNow =true;
			//print(gap);
			
		} 
	}
	else {
		zoomNow = false;
	} 
	*/
	
	if (Input.GetAxis("Mouse ScrollWheel")> 0)
	{
		
		fov1 -=Time.deltaTime*300 ;// fovSpeed--;
		camera1.fieldOfView =fov1;
	}

	if (Input.GetAxis("Mouse ScrollWheel")< 0)
	{
		EnableColliders();
		//Bridge.ZoomOut = true;
		//TextToDisplay.SetActive( false );
		Debug.Log( "ZZZZZZZZZZZ" );
		if( Bridge.ShowText ) fov1 =camera1.fieldOfView;
		Bridge.ShowText = false;
		fov1 += Time.deltaTime*300;// fov ++;
		camera1.fieldOfView =fov1;
	}
	if(fov1<1) fov1=1;
	if(fov1>60) fov1 =60;

//	camera1.fieldOfView = Input.GetAxis("Mouse ScrollWheel");    
}

function EnableColliders()
{
	var Colliders : BoxCollider[] = GameObject.FindObjectsOfType( typeof ( BoxCollider )  ) as BoxCollider[];
	for ( var i : int = 0; i < Colliders.Length; i++ )
	{
			Colliders[i].enabled = true;
	}
}