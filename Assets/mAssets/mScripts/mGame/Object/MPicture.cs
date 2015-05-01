using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class MPicture : MBase {

	public enum PicState
	{
		Active,
		Close,
		Hover,
		Hide,
	}
	public PicState state{
		get{
			return _state;
		}
		set{
			SetState( value );
		}
	}

	public PicState _state = PicState.Close;
	public float AlphaRate = 1.0f;

	public void SetState(PicState _sta )
	{
		_state = _sta;

	}

	public Uni2DSprite sprite;
	public Uni2DSprite whiteCover;

	public float whiteFadeTime = 0.5f;

	public Gesture dealGesture;

	void Awake()
	{
		transform.parent = Global.ImageRoot.transform;
		if (sprite == null )
		sprite = GetComponent<Uni2DSprite>();
		if ( whiteCover == null )
		{
			whiteCover = gameObject.GetComponentInChildren<Uni2DSprite>();

		}
		if ( whiteCover != null && sprite != null )
		{
//			Debug.Log( "sprite width "+ sprite.SpriteData.spriteWidth.ToString() + " height" + sprite.SpriteData.spriteHeight.ToString() );
//			Debug.Log( "wc width "+ whiteCover.SpriteData.spriteWidth.ToString() + " height" + whiteCover.SpriteData.spriteHeight.ToString() );
			float spriteWidth = sprite.SpriteData.spriteWidth * sprite.SpriteData.scale * 100f;
			float spriteHeight = sprite.SpriteData.spriteHeight * sprite.SpriteData.scale * 100f;
			whiteCover.SpriteData.scale = 1.0f;
			Vector3 WCScale = new Vector3( spriteWidth / whiteCover.SpriteData.spriteWidth 
			                              , spriteHeight / whiteCover.SpriteData.spriteHeight
			                              , 1.0f );
//			Debug.Log( "scale " + WCScale.ToString());
			whiteCover.transform.localScale = WCScale;
			whiteCover.VertexColor = Global.WhiteCoverDisactive;
		}
		gameObject.tag = Global._PIC_TAG_;

		if ( state == PicState.Active )
		{
			SetAsChoosen();
		}
	}
	
	void OnTap( TapGesture g )
	{
		
		Debug.Log("OnTap");
		if ( state == PicState.Hide || state == PicState.Hover )
		{
			Debug.Log( "set whiteCover");
			whiteCover.VertexColor = Global.WhiteCoverActive;
//			HOTween.To( whiteCover
//			           , whiteFadeTime * Global.WhiteCoverActiveTime
//			           , new TweenParms()
//			           .Prop( "VertexColor" , Global.WhiteCoverActive , false));
//				
//			HOTween.To( whiteCover
//			           , whiteFadeTime * ( 1 - Global.WhiteCoverActiveTime)
//			           , new TweenParms()
//			           .Prop( "VertexColor" , Global.WhiteCoverDisactive , false)
//			           .Delay( whiteFadeTime * Global.WhiteCoverActiveTime)    );
			
			state = PicState.Hide;
		}
	}

	void OnLongPress( LongPressGesture g )
	{
		if ( g.isHover  )
		{
			Hover( g );
		}else{
			SetAsChoosen();
		}

//		Quaternion towrad = Quaternion.LookRotation(transform.position);
//		Global.ImageRoot.transform.rotation = Quaternion.Slerp( Global.ImageRoot.transform.rotation , towrad , 1.0f);
	}


	void Hover( Gesture g )
	{
		if ( state == PicState.Hide )
		{
			state = PicState.Hover;
			dealGesture = g;
		}
	}

	void SetAsChoosen()
	{
		PictureManager.Instance.currentPic = this;
		
		Vector3 myEular = transform.localRotation.eulerAngles;
		PictureManager.ImgRota = Global.StandardEular( -myEular);
		Vector3 ImgTempEular = Global.ImageRoot.transform.rotation.eulerAngles;
		Global.ImageRoot.transform.rotation = Quaternion.Euler( -myEular );
		
		Vector3 imgPos = Global.ImageRoot.transform.position;
		Vector3 myPos = transform.position;	
		Vector3 IMGPos = imgPos + Global.picViewPosition - myPos;
		PictureManager.ImgPos = IMGPos;

		Global.ImageRoot.transform.rotation = Quaternion.Euler( ImgTempEular );

	}


	void Update()
	{
		SetAlpha( Time.deltaTime );
		SetAlphaWhiteCover( Time.deltaTime );
	}

	void SetAlpha( float deltaTime )
	{
		Color32 tempColor = sprite.VertexColor;
		Color32 expColor = GetExpColor();
		sprite.VertexColor = Color32.Lerp( tempColor , expColor , AlphaRate * deltaTime );
	}

	public float WhiteApearRate = 1.5f;
	public float WhiteCoverFadeRate = 1.5f;
	void SetAlphaWhiteCover( float deltaTime )
	{
		switch( _state )
		{
		case PicState.Hide:
			if ( whiteCover.VertexColor.a > Global.WhiteCoverDisactive.a )
				whiteCover.VertexColor = Color32.Lerp( whiteCover.VertexColor , Global.WhiteCoverDisactive , WhiteCoverFadeRate*deltaTime );
			break;
		case PicState.Hover:
			if ( whiteCover.VertexColor.a < Global.WhiteCoverActive.a )
				whiteCover.VertexColor = Color32.Lerp( whiteCover.VertexColor , Global.WhiteCoverActive , WhiteApearRate*deltaTime );
			break;
		case PicState.Active:
				if ( whiteCover.VertexColor.a > Global.WhiteCoverDisactive.a )
					whiteCover.VertexColor = Color32.Lerp( whiteCover.VertexColor , Global.WhiteCoverDisactive , WhiteCoverFadeRate*deltaTime*2f );
			break;
		default:
			break;
		}

	}

	 public Color32 GetExpColor( )
	{
		switch( _state )
		{
		case PicState.Active:
			return new Color32( 255 , 255 , 255 , 255 );
		case PicState.Hide:
		case PicState.Hover:
			return new Color32( 55 , 55 , 55 , 55 );
		default:
			return new Color32( 0 , 0 , 0 , 0 );
		}

	}
}
