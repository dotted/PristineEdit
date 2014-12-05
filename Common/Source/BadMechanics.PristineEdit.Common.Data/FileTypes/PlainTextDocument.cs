// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlainTextDocument.cs" company="Bad Mechanics">
//   Copyright © 2014 Bad Mechanics. All Rights Reserved.
// </copyright>
// <summary>
//   The plain text document.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BadMechanics.PristineEdit.Common.Data.FileTypes
{
    using System.IO;

    /// <summary>
    /// The plain text document.
    /// </summary>
    public class PlainTextDocument : Document
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlainTextDocument"/> class.
        /// </summary>
        /// <param name="backingStream">
        /// The backing stream.
        /// </param>
        public PlainTextDocument(Stream backingStream)
            : base(backingStream)
        {

        }
    }
}
