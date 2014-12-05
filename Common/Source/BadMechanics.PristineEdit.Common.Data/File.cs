// --------------------------------------------------------------------------------------------------------------------
// <copyright file="File.cs" company="Bad Mechanics">
//   Copyright © 2014 Bad Mechanics. All Rights Reserved.
// </copyright>
// <summary>
//   Defines the File type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BadMechanics.PristineEdit.Common.Data
{
    using System.IO;
    using System.Threading.Tasks;

    using PCLStorage;

    /// <summary>
    /// The file.
    /// </summary>
    public static class File
    {
        /// <summary>
        /// The open.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public static async Task<Stream> Open(string path)
        {
            var file = await FileSystem.Current.GetFileFromPathAsync(path);
            return await file.OpenAsync(FileAccess.ReadAndWrite);
        }
    }
}
