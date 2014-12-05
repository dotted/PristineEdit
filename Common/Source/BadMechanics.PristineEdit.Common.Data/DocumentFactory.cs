// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DocumentFactory.cs" company="Bad Mechanics">
//   Copyright © 2014 Bad Mechanics. All Rights Reserved.
// </copyright>
// <summary>
//   The document factory.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BadMechanics.PristineEdit.Common.Data
{
    using System.Linq;

    using BadMechanics.PristineEdit.Common.Data.FileTypes;

    /// <summary>
    /// The document factory.
    /// </summary>
    public static class DocumentFactory
    {
        /// <summary>
        /// The get document.
        /// </summary>
        /// <param name="filePath">
        /// The file path.
        /// </param>
        /// <returns>
        /// The <see cref="Document"/>.
        /// </returns>
        public static Document GetDocument(string filePath)
        {
            var explodedFilePath = filePath.Split('.');
            var fileExtension = explodedFilePath.Last();

            switch (fileExtension)
            {
                case "cs":
                    return new CsharpDocument(File.Open(filePath));
                default:
                    return new PlainTextDocument(File.Open(filePath));
            }

        }
    }
}
