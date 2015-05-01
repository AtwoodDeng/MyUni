using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Global {

	public static int SECOND_TO_TICKS = 10000000;

	public static string _IMG_ROOT = "image_root";
	public static string _PIC_TAG_ = "PICTURE";

	public static Color32 WhiteCoverActive = new Color32( 125 , 125 , 125 , 125 );
	public static Color32 WhiteCoverDisactive = new Color32( 0 , 0 ,0 , 0 );
	public static float WhiteCoverActiveTime = 0.15f;

	public static GameObject ImageRoot
	{
		get{
			if ( _imageRoot == null )
			{
				_imageRoot = GameObject.Find(_IMG_ROOT);
				if ( _imageRoot == null )
				{
					_imageRoot = new GameObject();
					_imageRoot.name = _IMG_ROOT;
					_imageRoot.transform.position = Vector3.zero;
					_imageRoot.transform.localScale = Vector3.one;
				}
			}
			return _imageRoot;
		}
	}
	static GameObject _imageRoot;



	public static Vector3 picViewPosition = new Vector3( 0 , 0 , 4f );


	public static float ConstrantFullAngle = 360f;
	public static Vector3 StandardEular( Vector3 eular )
	{
		Vector3 res = eular;
		while( res.x < 0 ) 
			res.x+=ConstrantFullAngle;
		while( res.y < 0 ) 
			res.y+=ConstrantFullAngle;
		while( res.z < 0 ) 
			res.z+=ConstrantFullAngle;
		while( res.x > ConstrantFullAngle )
			res.x-=ConstrantFullAngle;
		while( res.y > ConstrantFullAngle )
			res.y -= ConstrantFullAngle;
		while( res.z > ConstrantFullAngle )
			res.z -= ConstrantFullAngle;
		return res;
	}

	public static Vector3 CloserAngle( Vector3 _angA , Vector3 _angB )
	{
		Vector3 angA = StandardEular( _angA );
		Vector3 angB = StandardEular( _angB );
		if ( Mathf.Abs( angA.x - angB.x ) > ConstrantFullAngle/2 )
		{
			if ( angA.x - angB.x > 0 )
				angA.x -= ConstrantFullAngle;
			else 
				angA.x += ConstrantFullAngle;
		}
		if ( Mathf.Abs( angA.y - angB.y ) > ConstrantFullAngle/2 )
		{
			if ( angA.y - angB.y > 0 )
				angA.y -= ConstrantFullAngle;
			else 
				angA.y += ConstrantFullAngle;
		}
		if ( Mathf.Abs( angA.z - angB.z ) > ConstrantFullAngle/2 )
		{
			if ( angA.z - angB.z > 0 )
				angA.z -= ConstrantFullAngle;
			else 
				angA.z += ConstrantFullAngle;
		}
		return angA;
	}
}
