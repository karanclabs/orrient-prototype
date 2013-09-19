#pragma strict


private var previousPosition :Vector3 = Vector3(0,0,0);
private var rotate:boolean=false; 
function Start () {

}

function Update () {
	
	
	if(	Input.GetMouseButton(0)  && Camera.main.fieldOfView > 1/*&& Input.touchCount==1*/ ){
		
		//print (transform.eulerAngles);
		
		if(Input.GetAxis("Mouse X") || Input.GetAxis("Mouse Y") ){
			//print("mouse Position: "+Input.mousePosition);
			var newPosition = Input.mousePosition;
			
			var    angleX = previousPosition.y - newPosition.y; 
			var    angleY = previousPosition.x - newPosition.x; 
			//	print(angleX);	
			
			if(rotate) {
				transform.Rotate(  Vector3( -angleX*0.2,angleY *0.2 ,0),Space.World );
			}
			previousPosition = newPosition;
			rotate =true;
		}
		
	}
	else {
	  rotate = false;
	}

}