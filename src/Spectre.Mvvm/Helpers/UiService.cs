/*
 * UiServices.cs
 * Helps to maintain UI states.
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
using System.Windows.Threading;

namespace Spectre.Mvvm.Helpers
{
    /// <summary>
    /// Abstraction for UI related services.
    /// </summary>
    public class UiService
    {
        /// <summary>
        ///   A value indicating whether the UI is currently busy
        /// </summary>
        private static bool _isBusy;

        /// <summary>
        /// Sets the busystate as busy.
        /// </summary>
        public virtual void SetBusyState()
        {
            UiService.SetBusyState(busy: true);
        }

        /// <summary>
        /// Sets the busystate to busy or not busy.
        /// </summary>
        /// <param name="busy">if set to <c>true</c> the application is now busy.</param>
        private static void SetBusyState(bool busy)
        {
            if (busy != UiService._isBusy)
            {
                UiService._isBusy = busy;

                var dispatcher = System.Windows.Application.Current?.Dispatcher
                                 ?? Dispatcher.CurrentDispatcher;

                if (UiService._isBusy)
                {
                    dispatcher.Invoke(callback: () => Mouse.OverrideCursor = Cursors.Wait);
                    new DispatcherTimer(
                        interval: TimeSpan.FromSeconds(value: 0),
                        priority: DispatcherPriority.ApplicationIdle,
                        callback: UiService.DispatcherTimer_Tick,
                        dispatcher: dispatcher);
                }
                else
                {
                    dispatcher.Invoke(callback: () => Mouse.OverrideCursor = Cursors.Arrow);
                }
            }
        }

        /// <summary>
        /// Handles the Tick event of the dispatcherTimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private static void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            var dispatcherTimer = sender as DispatcherTimer;
            if (dispatcherTimer != null)
            {
                UiService.SetBusyState(busy: false);
                dispatcherTimer.Stop();
            }
        }
    }
}
