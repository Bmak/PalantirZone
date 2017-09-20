using UnityEngine;

public class NGUIScriptBlocker : MonoBehaviour {

	bool scripsEnabled,collidersEnabled;
	GameObject fixedHoveredObj;

	// Use this for initialization
	void Start () {
		scripsEnabled=true;
		collidersEnabled=true;
	}
	
	// Update is called once per frame
	void Update () {
		checkNGUI();
	}

	void checkNGUI() {
		GameObject obj1=UICamera.hoveredObject;
		GameObject obj2=UICamera.selectedObject;
		//Log.Out("checkNGUI():    obj1="+(obj1?obj1.name:"-")+"    obj2="+(obj2?obj2.name:"-")+" fixedHoveredObj="+(fixedHoveredObj?fixedHoveredObj.name:"-")+"   touches="+Input.touchCount+"   GetMouseButtonDown(0)="+Input.GetMouseButtonDown(0)+","+Input.GetMouseButton(0));

		if(Input.GetMouseButtonDown(0)) {
				fixedHoveredObj=obj1;
		}
		else if(Input.GetMouseButtonUp(0)) {
			fixedHoveredObj=null;
		}

		float scroll=Input.GetAxis("Mouse ScrollWheel");
		bool isDragging=(Input.GetMouseButton(0) || scroll!=0)  && fixedHoveredObj==null; // scroll!=0 - to prevent blocking for zooming with mouse wheel

		//bool r= (obj2==null || obj1==null);
		//bool r= (obj1==null);
		bool r= (obj1==null || fixedHoveredObj==null) && isDragging; //isDragging - trick to prevent unblocking for fast mouse movements

		enableOtherScripts(r);
		enableColliders(r);
	}

	void enableOtherScripts(bool enable) {
		if(scripsEnabled==enable) return;
		scripsEnabled=enable;
		MonoBehaviour[] allScripts=gameObject.GetComponents<MonoBehaviour>();
		foreach(MonoBehaviour scrpt in allScripts) {
			if(scrpt!=this) {
				scrpt.enabled=enable;
			}
		}
	}

	void enableColliders(bool enable) {
		if(collidersEnabled==enable) return;
		collidersEnabled=enable;
		Collider[] allCollidres=gameObject.GetComponents<Collider>();
		foreach(Collider coll in allCollidres) {
			coll.enabled=enable;
		}
	}

}
