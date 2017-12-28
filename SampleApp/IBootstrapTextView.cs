using System;

namespace SampleApp
{
    public interface IBootstrapTextView
    {

        /**
         * Sets the view to display the given BootstrapText
         *
         * @param bootstrapText the BootstrapText
         */
        void SetBootstrapText(BootstrapText bootstrapText);

        /**
         * @return the current BootstrapText, or null if none exists
         */
        BootstrapText GetBootstrapText();

        /**
         * Sets the view to display the given markdown text, by constructing a BootstrapText. e.g.
         * "This is a {fa-stop} button"
         *
         * @param text the markdown text
         */
        void SetMarkdownText(String text);
    }
}