﻿using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace Unchase.OpenAPI.ConnectedService.Common.Commands
{
    class StackPanelChangeVisibilityCommand :
        ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            (parameter as StackPanel)?.ChangeStackPanelVisibility();
        }

        public event EventHandler CanExecuteChanged;
    }
}