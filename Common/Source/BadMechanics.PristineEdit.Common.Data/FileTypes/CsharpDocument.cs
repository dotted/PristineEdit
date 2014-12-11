// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CsharpDocument.cs" company="Bad Mechanics">
//   Copyright © 2014 Bad Mechanics. All Rights Reserved.
// </copyright>
// <summary>
//   The csharp document.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BadMechanics.PristineEdit.Common.Data.FileTypes
{
    using System.IO;
    using System.Threading.Tasks;

    using global::Common.Logging;

    /// <summary>
    /// The C# document.
    /// </summary>
    public class CsharpDocument : Document
    {
        private static readonly ILog Log;

        static CsharpDocument()
        {
            Log = LogManager.GetCurrentClassLogger();
        }

        public CsharpDocument(Task<Stream> backingStream)
            : base(backingStream)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsharpDocument"/> class.
        /// </summary>
        /// <param name="document">
        /// The document.
        /// </param>
        public CsharpDocument(Document document)
            : base(document.Stream)
        {
        }
    }
}
