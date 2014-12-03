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
    public interface IPlugin
    {
        string Name { get; }
        void Do();
    }
}
