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

    /// <summary>
    /// The C# document.
    /// </summary>
    public class CsharpDocument : Document
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CsharpDocument"/> class.
        /// </summary>
        /// <param name="backingStream">
        /// The backing stream.
        /// </param>
        public CsharpDocument(Stream backingStream)
            : base(backingStream)
        {
            
        }
    }
}
