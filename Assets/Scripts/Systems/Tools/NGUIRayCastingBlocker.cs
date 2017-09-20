//#define NGUI_v3 // uncomment this line for NGUI 3.x

using UnityEngine;

public class NGUIRayCastingBlocker : MonoBehaviour {

	public enum TransformSourceType {
		Collider,
		Object,
	}

	public TransformSourceType getTransformFrom=TransformSourceType.Collider;

	public string NGUICameraName;
	public string blockForCameraName;

	Camera NGUICamera;
	Camera blockForCamera;

	GameObject blockingObject;
	Vector3 corner1TagCamPrev,corner2TagCamPrev,corner3TagCamPrev,corner4TagCamPrev;
	//Collider NGUIObjectCollider;

	// Use this for initialization
	void Start () {

		//blockingObject=GameObject.CreatePrimitive(PrimitiveType.Cube);
		blockingObject=new GameObject("NGUIRayCastingBlocker for "+gameObject.name);blockingObject.AddComponent<BoxCollider>();

		// setup NGUI Camera
		if(blockForCameraName==null || blockForCameraName.Trim().Length==0) {
			NGUICamera=UICamera.mainCamera;
		}
		else {
			NGUICamera=GameObject.Find(NGUICameraName).GetComponent<Camera>();
		}

		//setup Block For Camera
		if(blockForCameraName==null || blockForCameraName.Trim().Length==0) {
			 blockForCamera=Camera.main;
		}
		else {
			blockForCamera=GameObject.Find(blockForCameraName).GetComponent<Camera>();
        }
	}
	
	void OnDisable() {
		if(blockingObject!=null)
			blockingObject.SetActive(false);
	}
	
	void OnEnable() {
		if(blockingObject!=null)
			blockingObject.SetActive(true);
	}
	
	void LateUpdate() {
		if(blockForCamera==null) return;
		CalcCollidersTransformAndAllocateObj();
	}

	void CalcCollidersTransformAndAllocateObj() {
		Vector3 center;
		float hW,hH;

#if NGUI_v3
			//------- comment this block for NGUI 2.x -- start
		var widget=GetComponent<UIWidget>(); 
		if(widget!=null) {// for NGUI 3.x
			hW=widget.width*transform.lossyScale.x*0.5f;
			hH=widget.height*transform.lossyScale.y*0.5f;
			
			//Log.Out ("widget.pivot="+widget.pivot+"     pivotOffset="+widget.pivotOffset+"     rawPivot="+widget.rawPivot);
			if(getTransformFrom==TransformSourceType.Object || collider==null) {
				center=transform.position;
			} 
			else {
				center = collider.bounds.center;
			}
			center += new Vector3(-hW*(widget.pivotOffset.x-0.5f)*2.0f,-hH*(widget.pivotOffset.y-0.5f)*2.0f,0);
		}
		else 
		//--------- comment this block for NGUI 2.x -- end
#endif
		if(getTransformFrom==TransformSourceType.Object || GetComponent<Collider>()==null) {// for NGUI < 3.x or objects without widget
			center=transform.position;
			hW=transform.lossyScale.x*0.5f;
			hH=transform.lossyScale.y*0.5f;
		} 
		else {
			center = GetComponent<Collider>().bounds.center;
			hW = GetComponent<Collider>().bounds.extents.x;
			hH = GetComponent<Collider>().bounds.extents.y;
		}

		
		Vector3 corner1=center+new Vector3(-hW,hH);
		Vector3 corner2=center+new Vector3(hW,hH);
		Vector3 corner3=center+new Vector3(hW,-hH);
		Vector3 corner4=center+new Vector3(-hW,-hH);
		
		Vector3 corner1VP=NGUICamera.WorldToViewportPoint(corner1);
		Vector3 corner2VP=NGUICamera.WorldToViewportPoint(corner2);
		Vector3 corner3VP=NGUICamera.WorldToViewportPoint(corner3);
		Vector3 corner4VP=NGUICamera.WorldToViewportPoint(corner4);

		//float zVP=0.5f;
		float zVP=blockForCamera.nearClipPlane;
		corner1VP.z=corner2VP.z=corner3VP.z=corner4VP.z=zVP;
		
		Vector3 corner1TagCam=blockForCamera.ViewportToWorldPoint(corner1VP);
		Vector3 corner2TagCam=blockForCamera.ViewportToWorldPoint(corner2VP);
		Vector3 corner3TagCam=blockForCamera.ViewportToWorldPoint(corner3VP);
		Vector3 corner4TagCam=blockForCamera.ViewportToWorldPoint(corner4VP);
		
		if(corner1TagCam!=corner1TagCamPrev || corner2TagCam!=corner2TagCamPrev || corner3TagCam!=corner3TagCamPrev || corner4TagCam!=corner4TagCamPrev) {

			AllocateObject(corner1TagCam,corner2TagCam,corner3TagCam,corner4TagCam);

			corner1TagCamPrev=corner1TagCam;
			corner2TagCamPrev=corner2TagCam;
			corner3TagCamPrev=corner3TagCam;
			corner4TagCamPrev=corner4TagCam;
		}

		//Log.Out(""+transform.lossyScale.x+", "+collider.transform.lossyScale.x+",  "+transform.localScale.x+", "+collider.transform.localScale.x+",  ");
		//Log.Out(""+collider.bounds.size.x+",   "+collider.bounds.center.x);
		//Log.Out("center="+center +"   hw="+hW+"   hH="+hH);
		//Log.Out("corner1="+corner1+"    corner3="+corner3+",    corner1VP="+corner1VP+"      corner3VP="+corner3VP+",    corner1TagCam="+corner1TagCam+"      corner3TagCam="+corner3TagCam);
	}

	void AllocateObject(Vector3 c1,Vector3 c2,Vector3 c3,Vector3 c4) {
		//Log.Out("AllocateObject");
		float w=(c1-c2).magnitude;
		float h=(c1-c4).magnitude;
		float d=(w+h)*0.001f;

		Vector3 norm=Vector3.Cross((c1-c2),(c1-c4)).normalized;
		Vector3 up=(c1-c4);

		Vector3 newCenter=(c3-c1)*0.5f+c1;
		
		const float shiftFromCameraCoef=0.1f;
		newCenter -= norm*(d*(0.5f+shiftFromCameraCoef));
		
		blockingObject.transform.position=newCenter;
		blockingObject.transform.rotation=Quaternion.LookRotation(norm,up);
		blockingObject.transform.localScale=new Vector3(w,h,d);
	}
}
