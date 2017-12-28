namespace SampleApp
{
    public interface IIconSet
    {

        /**
         * Returns the unicode character for the current Font Icon.
         *
         * @return the unicode character
         */
        string UnicodeForKey(string key);


        /**
         * Returns the icon code for the current Font Icon.
         *
         * @return the icon code
         */
        string IconCodeForAttrIndex(int index);

        /**
         * Specifies the location that the font file resides in, starting from the assets directory
         * e.g."fontawesome-webfont.ttf"
         *
         * @return the font path
         */
        string FontPath { get; }
    }
}