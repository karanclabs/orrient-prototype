using UnityEngine;
using System.Collections;

public class FadeInEffect : MonoBehaviour {
	
	float DisplayTime;
	public static bool LogTime = false;
	
	// Use this for initialization
	void Start () {
		TweenAlpha.Begin( GameObject.Find( "QuestionL1" ), 0, 0 );
		TweenAlpha.Begin( GameObject.Find( "QuestionL2" ), 0, 0 );
		TweenAlpha.Begin( GameObject.Find( "AnswerL1" ), 0, 0 );
		TweenAlpha.Begin( GameObject.Find( "AnswerL2" ), 0, 0 );
		TweenAlpha.Begin( GameObject.Find( "AnswerL3" ), 0, 0 );
	}
	
	// Update is called once per frame
	void Update () {
		
		if( Bridge.ShowText )
		{
			TweenAlpha.Begin( GameObject.Find( "QuestionL1" ), 0.5f, 1 );
			TweenAlpha.Begin( GameObject.Find( "QuestionL2" ), 0.5f, 1 );
			if( LogTime )
			{
				DisplayTime = Time.time;
				LogTime = false;
			}
		}
		else
		{
			TweenAlpha.Begin( GameObject.Find( "QuestionL1" ), 0.5f, 0 );
			TweenAlpha.Begin( GameObject.Find( "QuestionL2" ), 0.5f, 0 );
			TweenAlpha.Begin( GameObject.Find( "AnswerL1" ), 0.5f, 0 );
			TweenAlpha.Begin( GameObject.Find( "AnswerL2" ), 0.5f, 0 );
			TweenAlpha.Begin( GameObject.Find( "AnswerL3" ), 0.5f, 0 );
		}
		
		if( Bridge.ShowText && Time.time - DisplayTime > 0.8f )
		{
			//Debug.Log("HAHAHAHAH");
			TweenAlpha.Begin( GameObject.Find( "AnswerL1" ), 0.5f, 1 );
			TweenAlpha.Begin( GameObject.Find( "AnswerL2" ), 0.5f, 1 );
			TweenAlpha.Begin( GameObject.Find( "AnswerL3" ), 0.5f, 1 );
		}
	
	}
}
