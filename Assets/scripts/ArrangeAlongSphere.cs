using UnityEngine;
using System.Collections.Generic;

public class ArrangeAlongSphere : MonoBehaviour {
    public GameObject prefab;
   private  int count = 100;
   private  float size = 2;
    
    [ContextMenu("Create Points")]
    void Create () {
        var points = UniformPointsOnSphere(count, size);
        for(var i=0; i<count; i++) {
            var g = Instantiate(prefab, transform.position+points[i], Quaternion.identity) as GameObject;
            g.transform.parent = transform;
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
			Debug.Log(new Vector3(Mathf.Cos(phi)*r, y, Mathf.Sin(phi)*r) * scale);
            points.Add(new Vector3(Mathf.Cos(phi)*r, y, Mathf.Sin(phi)*r) * scale);
        }
        return points.ToArray();
    }
	
	void Start(){
		print ("er");
		 Create();
	}
}