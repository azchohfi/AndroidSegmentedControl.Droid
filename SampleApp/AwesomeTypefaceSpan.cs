using System;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Text.Style;

namespace SampleApp
{
    public class AwesomeTypefaceSpan : TypefaceSpan
    {
        private readonly Typeface _typeface;

        public AwesomeTypefaceSpan(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        public AwesomeTypefaceSpan(Parcel parcel)
            : base(parcel)
        {
        }

        public AwesomeTypefaceSpan(Typeface typeface)
            : base(string.Empty)
        {
            _typeface = typeface;
        }

        public override void UpdateDrawState(TextPaint ds)
        {
            ds.SetTypeface(_typeface);
        }

        public override void UpdateMeasureState(TextPaint paint)
        {
            paint.SetTypeface(_typeface);
        }
    }
}