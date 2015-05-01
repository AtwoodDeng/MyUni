using UnityEngine;
using System.Collections;

public class PictureManager : MBase {
	
	public PictureManager() { s_Instance = this; }
	public static PictureManager Instance { get { return s_Instance; } }
	private static PictureManager s_Instance;

	public MPicture currentPic{
		get{
			return _currentPic;
		}
		set{
			if ( _currentPic != null )
			{
				_currentPic.state = MPicture.PicState.Hide;
				_currentPic.collider.enabled = true;
			}
			_currentPic = value;
			_currentPic.state = MPicture.PicState.Active;
			_currentPic.sprite.collider.enabled = false;
		}
	}
	MPicture _currentPic;
	public static Vector3 ImgPos;
	public static Vector3 ImgRota;

	public float moveRate = 0.05f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if ( currentPic != null )
		{
			setImg( Time.deltaTime );
		}
	}

	void setImg(float deltaTime )
	{
		Global.ImageRoot.transform.position = Vector3.Lerp( Global.ImageRoot.transform.position , ImgPos , moveRate * deltaTime ) ;
		Vector3 ImgEularAngles = Global.StandardEular( Global.ImageRoot.transform.rotation.eulerAngles ) ;
		Vector3 ImgRotaEular = Global.StandardEular( ImgRota );
		ImgEularAngles = Global.CloserAngle( ImgEularAngles , ImgRotaEular );
		Vector3 ImgEular = Vector3.Slerp( ImgEularAngles
		                                 , ImgRotaEular
		                                 , moveRate * deltaTime );
		Global.ImageRoot.transform.rotation = Quaternion.Euler( ImgEular );
	}
}
