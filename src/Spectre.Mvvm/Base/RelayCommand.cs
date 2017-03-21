/*
 * RelayCommand.cs
 * Bindable ICommand implementation.
 * 
   Copyright 2017 Grzegorz Mrukwa

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/
using System;
using System.Windows.Input;
using Spectre.Mvvm.Helpers;

namespace Spectre.Mvvm.Base
{
    /// <summary>
    /// Provides a base class simplifying creation of commands to bind to from
    /// GUI.
    /// </summary>
    /// <seealso cref="System.Windows.Input.ICommand" />
    public class RelayCommand : ICommand
    {
        #region Fields
        /// <summary>
        /// Service handling UI effects.
        /// </summary>
        private static UiService uiService = new UiService();

        /// <summary>
        /// Action to be executed.
        /// </summary>
        private Action<object> _execute;

        /// <summary>
        /// Function stating, whether _execute action can be ran.
        /// </summary>
        private Predicate<object> _canExecute;

        /// <summary>
        /// Occurs when _canExecute changess.
        /// </summary>
        private event EventHandler CanExecuteChangedInternal;
        #endregion

        #region IntroduceUiMock
        /// <summary>
        /// Allows to inject mock as a service.
        /// </summary>
        /// <param name="mock">UI services mock.</param>
        public static void IntroduceUiMock(UiService mock)
        {
            uiService = mock;
        }
        #endregion

        #region Constructors
        #region action
        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand"/> class
        /// with default CanExecute function returning true.
        /// </summary>
        /// <param name="execute">An action to be executed.</param>
        public RelayCommand(Action execute) : this(execute, DefaultCanExecute)
        {

        }
        #endregion

        #region action-func
        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand"/> class
        /// with a condition function which states whether an action can be
        /// executed.
        /// </summary>
        /// <param name="execute">An action to be executed.</param>
        /// <param name="canExecute">Condition if the action can be performed.</param>
        public RelayCommand(Action execute, Func<bool> canExecute) : this(execute, o => canExecute())
        {

        }
        #endregion

        #region action-predicate
        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand"/> class
        /// with an action and condition of execution.
        /// </summary>
        /// <param name="execute">An action to be executed.</param>
        /// <param name="canExecute">Condition if the action can be performed.</param>
        public RelayCommand(Action execute, Predicate<object> canExecute) : this(o => execute(), canExecute)
        {

        }
        #endregion

        #region action-generic
        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand"/> class
        /// with a parameterized action to be executed, which always can be fired.
        /// </summary>
        /// <param name="execute">An action to be executed.</param>
        public RelayCommand(Action<object> execute) : this(execute, DefaultCanExecute)
        {

        }
        #endregion

        #region action generic with predicate
        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand"/> class.
        /// </summary>
        /// <param name="execute">An action to be executed.</param>
        /// <param name="canExecute">A condition of execution.</param>
        /// <exception cref="System.ArgumentNullException">
        /// When execute is null
        /// or
        /// canExecute is null
        /// </exception>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException(nameof(execute));
            }

            if (canExecute == null)
            {
                throw new ArgumentNullException(nameof(canExecute));
            }

            this._execute = execute;
            this._canExecute = canExecute;
        }
        #endregion
        #endregion

        #region CanExecuteChanged
        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                this.CanExecuteChangedInternal += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
                this.CanExecuteChangedInternal -= value;
            }
        }
        #endregion

        #region CanExecute
        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        /// <returns>
        /// true if this command can be executed; otherwise, false.
        /// </returns>
        public bool CanExecute(object parameter)
        {
            return this._canExecute != null && this._canExecute(parameter);
        }
        #endregion

        #region Execute
        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public void Execute(object parameter)
        {
            uiService.SetBusyState();
            this._execute(parameter);
        }
        #endregion

        #region OnCanExecuteChanged
        /// <summary>
        /// Called when CanExecute changed.
        /// </summary>
        public void OnCanExecuteChanged()
        {
            EventHandler handler = this.CanExecuteChangedInternal;
            //DispatcherHelper.BeginInvokeOnUIThread(() => handler?.Invoke(this, EventArgs.Empty));
            handler?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region Destroy
        /// <summary>
        /// Destroys this instance.
        /// </summary>
        public void Destroy()
        {
            this._canExecute = _ => false;
            this._execute = _ => { return; };
        }
        #endregion

        #region DefaultCanExecute
        /// <summary>
        /// Default state of condition of execution.
        /// </summary>
        /// <param name="parameter">The necessary parameter.</param>
        /// <returns>True.</returns>
        private static bool DefaultCanExecute(object parameter)
        {
            return true;
        }
        #endregion
    }
}