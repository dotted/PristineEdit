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

    using global::Common.Logging;

    /// <summary>
    /// The document factory.
    /// </summary>
    public static class DocumentFactory
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly ILog Log;

        /// <summary>
        /// 
        /// </summary>
        static DocumentFactory()
        {
            Log = LogManager.GetCurrentClassLogger();
        }

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
            Log.Trace(m => m("Getting document from path: {0}", filePath));
            var explodedFilePath = filePath.Split('.');
            //Log.Trace(m => m("Exploded path: {0}", explodedFilePath));
            var fileExtension = explodedFilePath.Last();
            Log.Trace(m => m("Detected file extension: {0}", fileExtension));

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
