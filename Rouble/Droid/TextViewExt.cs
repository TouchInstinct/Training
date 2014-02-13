using System;
using Android.App;
using Android.Graphics;
using Android.Text.Style;
using Android.Widget;
using Android.Text;

namespace Rouble
{
	public static class TextViewExt
	{
		private static CharacterStyle RoubleSpan { get; set; }
		private static Typeface Rouble { get; set; }

		static TextViewExt()
		{
			Rouble = Typeface.CreateFromAsset(Application.Context.Assets, "Rouble.ttf");
			RoubleSpan = new CustomTypefaceSpan(Rouble);
		}

		public static void SetTextWithRouble(this TextView label, string text, RoubleType roubleType)
		{
			var textFormatted = new SpannableString(text + Roubles.GetRoubleSymbFor(roubleType));
			textFormatted.SetSpan(RoubleSpan, textFormatted.Length() - 1, textFormatted.Length(), SpanTypes.ExclusiveExclusive);
			label.TextFormatted = textFormatted;
		}

		private class CustomTypefaceSpan: MetricAffectingSpan
		{
			private Typeface _typeface;

			public CustomTypefaceSpan(Typeface typeface)
			{
				_typeface = typeface;
			}

			public override void UpdateDrawState(TextPaint drawState)
			{
				Apply(drawState);
			}

			public override void UpdateMeasureState(TextPaint paint)
			{
				Apply(paint);
			}

			private void Apply(Paint paint)
			{
				paint.SetTypeface(_typeface);
			}
		}
	}
}

