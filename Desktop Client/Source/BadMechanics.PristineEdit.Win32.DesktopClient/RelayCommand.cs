// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RelayCommand.cs" company="Bad Mechanics">
//   Copyright © 2014 Bad Mechanics. All Rights Reserved.
// </copyright>
// <summary>
//   The relay command.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BadMechanics.PristineEdit.Win32.DesktopClient
{
    using System;
    using System.Windows.Input;

    /// <summary>
    /// The relay command
    /// </summary>
    public sealed class RelayCommand : ICommand
    {
        /// <summary>
        /// The function
        /// </summary>
        private readonly Action function;

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand"/> class.
        /// </summary>
        /// <param name="function">The function to relay</param>
        public RelayCommand(Action function)
        {
            this.function = function;
        }

        /// <summary>
        /// Got no clue
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Can execute function
        /// </summary>
        /// <param name="parameter">Unused parameter</param>
        /// <returns>True if function is not null</returns>
        public bool CanExecute(object parameter)
        {
            return this.function != null;
        }

        /// <summary>
        /// Execute the function
        /// </summary>
        /// <param name="parameter">Unused parameter</param>
        public void Execute(object parameter)
        {
            if (this.function != null)
            {
                this.function();
            }
        }
    }  
}
