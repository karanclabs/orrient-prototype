using UnityEngine;

public class TypewriterEffect : MonoBehaviour
{
	public int charsPerSecond = 40;

	UILabel mLabel;
	string mText;
	int mOffset = 0;
	float mNextChar = 0f;

	void Update ()
	{
		if (mLabel == null)
		{
			mLabel = GetComponent<UILabel>();
			//TweenAlpha.Begin( this.gameObject, 0, 0 );
			mLabel.supportEncoding = false;
			mLabel.symbolStyle = UIFont.SymbolStyle.None;
			mText = mLabel.font.WrapText(mLabel.text, mLabel.lineWidth / mLabel.cachedTransform.localScale.x, mLabel.maxLineCount, false, UIFont.SymbolStyle.None);
		}

		if (mOffset < mText.Length)
		{
			if (mNextChar <= Time.time)
			{
				charsPerSecond = Mathf.Max(1, charsPerSecond);

				// Periods and end-of-line characters should pause for a longer time.
				float delay = 1f / charsPerSecond;
				char c = mText[mOffset];
				if (c == '.' || c == '\n' || c == '!' || c == '?') delay *= 4f;

				mNextChar = Time.time + delay;
				mLabel.text = mText.Substring(0, ++mOffset);
			}
		}
		else Destroy(this);
	}
}