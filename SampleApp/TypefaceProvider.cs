using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Graphics;
using Java.Lang;
using String = System.String;

namespace SampleApp
{
    public static class TypefaceProvider
    {
        private static readonly Dictionary<string, Typeface> TypefaceMap = new Dictionary<string, Typeface>();
        private static readonly Dictionary<string, IIconSet> RegisteredIconSets = new Dictionary<string, IIconSet>();

        static TypefaceProvider()
        {
            RegisterDefaultIconSets();
        }

        /**
         * Returns a reference to the requested typeface, creating a new instance if none already exists
         *
         * @param context the current context
         * @param iconSet the icon typeface
         * @return a reference to the typeface instance
         */
        public static Typeface GetTypeface(Context context, IIconSet iconSet)
        {
            var path = iconSet.FontPath;

            if (TypefaceMap.TryGetValue(path, out var font) == false)
            {
                font = Typeface.CreateFromAsset(context.Assets, path);
                TypefaceMap.Add(path, font);
            }
            return font;
        }

        /**
         * Registers instances of the Default IconSets so that they are available throughout the whole
         * application. Currently the default icon sets include FontAwesome and Typicon.
         */
        public static void RegisterDefaultIconSets()
        {
            var fontAwesome = new FontAwesome();

            RegisteredIconSets.Add(fontAwesome.FontPath, fontAwesome);
        }

        /**
         * Registers a custom IconSet, so that it is available for use throughout the whole application.
         *
         * @param iconSet a custom IconSet
         */
        public static void RegisterCustomIconSet(IIconSet iconSet)
        {
            RegisteredIconSets.Add(iconSet.FontPath, iconSet);
        }

        /**
         * Retrieves a registered IconSet whose font can be found in the asset directory at the given path
         *
         * @param fontPath the given path
         * @param editMode - whether the view requesting the icon set is displayed in the preview editor
         * @return the registered IconSet instance
         */
        public static IIconSet RetrieveRegisteredIconSet(String fontPath, bool editMode)
        {
            if (!RegisteredIconSets.TryGetValue(fontPath, out var iconSet) && !editMode)
            {
                throw new RuntimeException($"Font '{fontPath}' not properly registered");
            }
            return iconSet;
        }

        /**
         * Retrieves a collection of all registered IconSets in the application
         *
         * @return a collection of registered IconSets.
         */
        public static List<IIconSet> GetRegisteredIconSets()
        {
            return RegisteredIconSets.Select(v => v.Value).ToList();
        }
    }
}