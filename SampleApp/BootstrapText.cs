using System;
using System.Collections.Generic;
using Android.Content;
using Android.Runtime;
using Android.Text;
using Java.IO;
using StringBuilder = System.Text.StringBuilder;

namespace SampleApp
{
    public class BootstrapText : SpannableString, ISerializable
    {
        public BootstrapText(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        private BootstrapText(string source)
            : base(source)
        {
        }

        /**
         * This class should be used to construct BootstrapText instances. Text is appended to itself
         * in the order in which it was added.
         */
        public class Builder
        {
            private readonly StringBuilder _sb;
            private readonly Context _context;
            private readonly bool _editMode;
            private readonly Dictionary<int, IIconSet> _fontIndicesMap;

            public Builder(Context context)
            {
                _fontIndicesMap = new Dictionary<int, IIconSet>();
                _sb = new StringBuilder();
                _context = context.ApplicationContext;
                _editMode = false;
            }

            public Builder(Context context, bool editMode)
            {
                _fontIndicesMap = new Dictionary<int, IIconSet>();
                _sb = new StringBuilder();
                _context = context.ApplicationContext;
                _editMode = editMode;
            }

            /**
             * Appends a regular piece of text to the BootstrapText under construction.
             *
             * @param text a regular piece of text
             * @return the updated builder instance
             */
            public Builder AddText(string text)
            {
                _sb.Append(text);
                return this;
            }

            /**
             * Appends a FontAwesomeIcon to the BootstrapText under construction
             *
             * @return the updated builder instance
             */
            public Builder AddFontAwesomeIcon(string iconCode)
            {
                IIconSet iconSet = TypefaceProvider.RetrieveRegisteredIconSet(FontAwesome.FontAwesomePath, _editMode);
                _sb.Append(iconSet.UnicodeForKey(iconCode.Replace("\\-", "_")));
                _fontIndicesMap.Add(_sb.Length, iconSet);
                return this;
            }

            /**
             * Appends a font icon to the BootstrapText under construction
             *
             * @param iconSet a font icon
             * @return the updated builder instance
             */
            public Builder AddIcon(string iconCode, IIconSet iconSet)
            {
                _sb.Append(iconSet.UnicodeForKey(iconCode.Replace("\\-", "_")));
                _fontIndicesMap.Add(_sb.Length, iconSet);
                return this;
            }

            /**
             * @return a new instance of BootstrapText, constructed according to Builder arguments.
             */
            public BootstrapText Build()
            {
                BootstrapText bootstrapText = new BootstrapText(_sb.ToString());

                foreach (var entry in _fontIndicesMap)
                {
                    int index = entry.Key;

                    var span = new AwesomeTypefaceSpan(TypefaceProvider.GetTypeface(_context.ApplicationContext, entry.Value));
                    bootstrapText.SetSpan(span, index - 1, index, SpanTypes.InclusiveInclusive);
                }
                return bootstrapText;
            }
        }
    }
}