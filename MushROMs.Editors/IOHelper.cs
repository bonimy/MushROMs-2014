using System;
using System.Collections.Generic;
using System.Text;
using MushROMs.Editors.Properties;

namespace MushROMs.Editors
{
    /// <summary>
    /// Provides constants for File IO programming.
    /// </summary>
    public static class IOHelper
    {
        #region Constant and read-only fields
        /// <summary>
        /// The predicate character for a file dialog filter.
        /// This field is constant.
        /// </summary>
        public const char FilterPredicate = '*';
        /// <summary>
        /// The separator character for a file dialog filter.
        /// This field is constant.
        /// </summary>
        public const char FilterSeparator = '|';
        /// <summary>
        /// The extension separator for a file dialog filter.
        /// This field is constant.
        /// </summary>
        public const char FilterExtSeperator = ';';
        /// <summary>
        /// The string representing no extension for a file dialog filter.
        /// This field is constant.
        /// </summary>
        public const string NoExtension = ".*";
        #endregion

        #region Methods
        /// <summary>
        /// Creates a file dialog extension filter.
        /// </summary>
        /// <param name="names">
        /// A string list containing the descriptor names of each option.
        /// </param>
        /// <param name="extensions">
        /// A string array list containing the extensions that belong to
        /// each category.
        /// </param>
        /// <returns>
        /// A file dialog extension filter.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// No names are present. -or-
        /// Names list and extensions list do not match in size. -or-
        /// No extensions exist in for this filter.
        /// </exception>
        public static string CreateFilter(List<string> names, List<string[]> extensions)
        {
            if (names.Count == 0)
                throw new ArgumentException(Resources.ErrorNoFilterNames);
            if (names.Count != extensions.Count)
                throw new ArgumentException(Resources.ErrorFilterMismatch);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < names.Count; i++)
            {
                if (extensions[i].Length == 0)
                    throw new ArgumentException(Resources.ErrorNoExtensions);

                sb.Append(names[i]);
                sb.Append(FilterSeparator);

                for (int j = 0; j < extensions[i].Length; j++)
                {
                    if (j != 0)
                    sb.Append(FilterExtSeperator);
                    sb.Append(FilterPredicate);
                    sb.Append(extensions[i][j]);
                }

                if (i != names.Count - 1)
                    sb.Append(FilterSeparator);
            }
            return sb.ToString();
        }
        #endregion
    }
}
