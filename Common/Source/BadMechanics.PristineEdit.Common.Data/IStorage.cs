// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStorage.cs" company="Bad Mechanics">
//   Copyright © 2014 Bad Mechanics. All Rights Reserved.
// </copyright>
// <summary>
//   Defines the IStorage type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BadMechanics.PristineEdit.Common.Data
{
    using System;

    /// <summary>
    /// The Storage interface.
    /// </summary>
    public interface IStorage
    {
        /// <summary>
        /// The document was read.
        /// </summary>
        event EventHandler DocumentRead;

        /// <summary>
        /// The document was saved.
        /// </summary>
        event EventHandler DocumentSaved;

        /// <summary>
        /// The save.
        /// </summary>
        /// <param name="document">
        /// The document.
        /// </param>
        void Save(Document document);

        /// <summary>
        /// The load.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// The <see cref="Document"/>.
        /// </returns>
        Document Load(string path);
    }
}
