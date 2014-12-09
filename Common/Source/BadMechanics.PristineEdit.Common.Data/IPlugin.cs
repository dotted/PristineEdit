// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPlugin.cs" company="Bad Mechanics">
//   Copyright © 2014 Bad Mechanics. All Rights Reserved.
// </copyright>
// <summary>
//   Defines the IPlugin type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BadMechanics.PristineEdit.Common.Interfaces
{
    /// <summary>
    /// The Plugin interface.
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The do.
        /// </summary>
        void Do();
    }
}
