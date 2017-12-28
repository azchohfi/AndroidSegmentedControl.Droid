using System.Text.RegularExpressions;
using Android.Content;
using Java.Lang;
using String = System.String;

namespace SampleApp
{
    public class IconResolver
    {
        private static string REGEX_FONT_AWESOME = "(fa_|fa-)[a-z_0-9]+";
        /**
         * Resolves markdown to produce a BootstrapText instance. e.g. "{fa_android}" would be replaced
         * with the appropriate FontAwesome character and a SpannableString producec.
         *
         * @param context  the current context
         * @param markdown the markdown string
         * @param editMode - whether the view requesting the icon set is displayed in the preview editor
         * @return a constructed BootstrapText
         */
        public static BootstrapText ResolveMarkdown(Context context, String markdown, bool editMode)
        {
            if (markdown == null)
            {
                return null;
            }
            else
            { // detect {fa_*} and split into spannable, ignore escaped chars
                BootstrapText.Builder builder = new BootstrapText.Builder(context, editMode);

                int lastAddedIndex = 0;
                int startIndex = -1;
                int endIndex = -1;

                for (int i = 0; i < markdown.Length; i++)
                {
                    char c = markdown[i];

                    if (c == '\\')
                    { // escape sequence, ignore next char
                        i += 2;
                        continue;
                    }

                    if (c == '{')
                    {
                        startIndex = i;
                    }
                    else if (c == '}')
                    {
                        endIndex = i;
                    }

                    if (startIndex != -1 && endIndex != -1)
                    { // recognised markdown string

                        if (startIndex >= 0 && endIndex < markdown.Length)
                        {
                            String iconCode = markdown.Substring(startIndex + 1, endIndex - startIndex - 1).Replace("\\-", "_");
                            builder.AddText(markdown.Substring(lastAddedIndex, startIndex - lastAddedIndex));

                            if (Regex.IsMatch(iconCode, REGEX_FONT_AWESOME))
                            { // text is FontAwesome code
                                if (editMode)
                                {
                                    builder.AddText("?");
                                }
                                else
                                {
                                    builder.AddIcon(iconCode, TypefaceProvider.RetrieveRegisteredIconSet(FontAwesome.FontAwesomePath, false));
                                }
                            }
                            else
                            {
                                if (editMode)
                                {
                                    builder.AddText("?");
                                }
                                else
                                {
                                    builder.AddIcon(iconCode, ResolveIconSet(iconCode));
                                }
                            }
                            lastAddedIndex = endIndex + 1;
                        }
                        startIndex = -1;
                        endIndex = -1;
                    }
                }
                return builder.AddText(markdown.Substring(lastAddedIndex, markdown.Length - lastAddedIndex)).Build();
            }
        }

        /**
         * Searches for the unicode character value for the Font Icon Code. This method searches all
         * active FontIcons in the application.
         *
         * @param iconCode the font icon code
         * @return the unicode character matching the icon, or null if none matches
         */
        private static IIconSet ResolveIconSet(String iconCode)
        {
            foreach (var set in TypefaceProvider.GetRegisteredIconSets())
            {
                if (set.FontPath.Equals(FontAwesome.FontAwesomePath))
                {
                    continue; // already checked previously, ignore
                }

                var unicode = set.UnicodeForKey(iconCode);

                if (unicode != null)
                {
                    return set;
                }
            }

            var message = $"Could not find FontIcon value for '{iconCode}', " +
                    "please ensure that it is mapped to a valid font";

            throw new IllegalArgumentException(message);
        }
    }
}