using System;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Widget;

namespace SampleApp
{
    [Register("SampleApp.SampleApp.AwesomeRadioButton")]
    public class AwesomeRadioButton : RadioButton, IBootstrapTextView
    {
        private BootstrapText _bootstrapText;

        public AwesomeRadioButton(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        public AwesomeRadioButton(Context context)
            : base(context)
        {
        }

        public AwesomeRadioButton(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            Init(attrs);
        }

        public AwesomeRadioButton(Context context, IAttributeSet attrs, int defStyleAttr)
            : base(context, attrs, defStyleAttr)
        {
            Init(attrs);
        }

        private void Init(IAttributeSet attrs)
        {
            var typedArray = Context.ObtainStyledAttributes(attrs, Resource.Styleable.AwesomeRadioButton);
            String markdownText;

            try
            {
                markdownText = typedArray.GetString(Resource.Styleable.AwesomeRadioButton_awesome_text);
            }
            finally
            {
                typedArray.Recycle();
            }
            if (markdownText != null)
            {
                SetMarkdownText(markdownText);
            }
            UpdateBootstrapState();
        }

        public void SetBootstrapText(BootstrapText bootstrapText)
        {
            _bootstrapText = bootstrapText;
            UpdateBootstrapState();
        }

        public BootstrapText GetBootstrapText()
        {
            return _bootstrapText;
        }

        public void SetMarkdownText(string text)
        {
            String textSpace = text + " ";
            SetBootstrapText(IconResolver.ResolveMarkdown(Context, textSpace, IsInEditMode));
        }

        protected void UpdateBootstrapState()
        {
            if (_bootstrapText != null)
            {
                TextFormatted = _bootstrapText;
            }
        }
    }
}