/*
 * DivikResultLoader.cs
 * Loads and parses DiviK results from MAT-files.
 *
   Copyright 2017 Spectre Team

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
using MatlabAlgorithmsNative;
using Spectre.Algorithms.Results;

namespace Spectre.Algorithms.Io
{
    /// <summary>
    /// Parses MAT-files with DiviK results.
    /// </summary>
    public class DivikResultLoader : IDisposable
    {
        #region Fields

        /// <summary>
        /// The segmentation context.
        /// </summary>
        private readonly Segmentation _segmentationContext;

        /// <summary>
        /// Indicates whether this instance has been disposed.
        /// </summary>
        private bool _disposed;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="DivikResultLoader"/> class.
        /// </summary>
        public DivikResultLoader()
        {
            _segmentationContext = new Segmentation();
        }
        #endregion

        #region Load

        /// <summary>
        /// Loads the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>Tree of segmentation produces by DiviK</returns>
        /// <exception cref="FileNotFoundException">path does not point file</exception>
        public DivikResult Load(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException(message: nameof(DivikResultLoader), fileName: path);
            }
            var divikTree = _segmentationContext.load_divik_result(path);
            return new DivikResult(divikTree);
        }
        #endregion

        #region IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(obj: this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _segmentationContext.Dispose();
                }
                _disposed = true;
            }
        }

        /// <summary>
        /// Validates the dispose state. If this instance has been disposed, throws an exception.
        /// </summary>
        /// <exception cref="System.ObjectDisposedException">thrown if this object has been disposed.</exception>
        private void ValidateDispose()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(objectName: nameof(DivikResultLoader));
            }
        }

        #endregion
    }
}
