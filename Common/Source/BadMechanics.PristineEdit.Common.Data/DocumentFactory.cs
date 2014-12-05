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

    /// <summary>
    /// The document factory.
    /// </summary>
    public static class DocumentFactory
    {
        public static Document GetDocument(string filePath)
        {
            var explodedFilePath = filePath.Split('.');
            
        }
    }
}
