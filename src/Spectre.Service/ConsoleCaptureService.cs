/*
 * ConsoleCaptureService.cs
 * Implementation of console capture.
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
using System.IO;
using System.Text;
using System.Threading;
using Spectre.Service.Abstract;
using Timer = System.Timers.Timer;

namespace Spectre.Service
{
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Assert, Unrestricted = true)]
    public class ConsoleCaptureService: IConsoleCaptureService
    {
        #region Fields
        /// <summary>
        /// The internal writer.
        /// </summary>
        private readonly StringWriter _writer;
        /// <summary>
        /// The global stdout.
        /// </summary>
        private readonly TextWriter _stdout;
        /// <summary>
        /// The timer for update notification.
        /// </summary>
        private readonly Timer _timer;
        /// <summary>
        /// Timer update interval.
        /// </summary>
        private readonly double _updateInterval;
        /// <summary>
        /// If true, instance is useless.
        /// </summary>
        private bool _disposed;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleCaptureService"/> class.
        /// </summary>
        /// <param name="updateInterval">The update interval.</param>
        public ConsoleCaptureService(double updateInterval=1000.0)
        {
            _stdout = Console.Out;
            var builder = new StringBuilder();
            _writer = new StringWriter(builder);
            Console.SetOut(_writer);
            _updateInterval = updateInterval;
            _timer = new Timer(_updateInterval);
            Content = string.Empty;
            _timer.Elapsed += (sender, args) =>
            {
                var upToDateContent = builder.ToString();
                var suffix = builder.ToString(Content.Length, upToDateContent.Length - Content.Length);
                if (Content != upToDateContent)
                {
                    Content = upToDateContent;
                    OnWritten(suffix);
                }
            };
            _timer.Start();
        }
        #endregion

        #region IDisposable
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            Thread.Sleep((int)_updateInterval + 1);

            Console.SetOut(_stdout);
            if (disposing)
            {
                _writer.Dispose();
                _timer.Stop();
                _timer.Dispose();
            }
            _disposed = true;
        }
        #endregion

        #region IConsoleCaptureService
        /// <summary>
        /// Gets the captured content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public string Content { get; private set; }

        /// <summary>
        /// Occurs when anything was written.
        /// </summary>
        public event EventHandler<string> Written;
        #endregion

        #region OnWritten
        /// <summary>
        /// Called when console was written.
        /// </summary>
        /// <param name="entry">The entry.</param>
        protected virtual void OnWritten(string entry) => Written?.Invoke(this, entry);
        #endregion
    }
}
