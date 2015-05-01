using UnityEngine;
using System.Collections;

public class TestGesture: MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	DiscreteGesture gesture;
	void OnTap( TapGesture _gesture )
	{
		gesture = _gesture;
		Debug.Log("get tap");
	}

	void OnLongPress( LongPressGesture _gesture )
	{
		gesture = _gesture;
		Debug.Log("get LongPress");
	}
	void OnGUI()
	{
		if ( gesture != null )
		{
			GUILayout.Label( gesture.Recognizer.name + " position" + gesture.Position.ToString() + " elapsed Time " + gesture.ElapsedTime.ToString() + " start Time " + gesture.StartTime.ToString() );
			if ( gesture.Selection != null )
				GUILayout.Label( "selection " + gesture.Selection.name );
		}
//			GUILayout.Label( "gesture position" + gesture.Position.ToString() + " elapsed Time " + gesture.ElapsedTime.ToString() + "collider name " + gesture.Hit.collider.name );
	}
}
