// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Document.cs" company="Bad Mechanics">
//   Copyright © 2014 Bad Mechanics. All Rights Reserved.
// </copyright>
// <summary>
//   Defines the Document type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BadMechanics.PristineEdit.Common.Data
{
    using System.IO;

    /// <summary>
    /// The document.
    /// </summary>
    public abstract class Document
    {
        /// <summary>
        /// The backing stream.
        /// </summary>
        private Stream backingStream;

        /// <summary>
        /// Initializes a new instance of the <see cref="Document"/> class.
        /// </summary>
        /// <param name="backingStream">
        /// The backing stream.
        /// </param>
        protected Document(Stream backingStream)
        {
            this.backingStream = backingStream;
        }
    }
}
