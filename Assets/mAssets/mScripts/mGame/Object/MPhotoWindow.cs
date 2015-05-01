using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MPhotoWindow : MonoBehaviour {

	public GameObject PhotoUIObject;
	public UIGrid grid;
	public List<MPhotoUI> PhotoUIList;

	// Use this for initialization
	void Awake () {
		for( int i = 0 ; i < 7 ; ++ i )
		{
			GameObject _PhotoUI = Instantiate( PhotoUIObject ) as GameObject;
			MPhotoUI photoUI = _PhotoUI.GetComponent<MPhotoUI>();
			_PhotoUI.transform.parent = grid.transform;
			_PhotoUI.transform.localScale = Vector3.one;
			grid.repositionNow = true;
			if ( photoUI != null )
			{
				PhotoUIList.Add( photoUI );
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
