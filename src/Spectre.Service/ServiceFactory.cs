/*
 * ServiceFactory.cs
 * Contains definition of Factory for creating services.
 *
   Copyright 2017 Michał Wolny, Grzegorz Mrukwa

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

using Spectre.Service.Abstract;

namespace Spectre.Service
{
    /// <summary>
    /// Class for creating new services.
    /// </summary>
    public class ServiceFactory : IServiceFactory
    {
        /// <summary>
        /// Gets the console capture service.
        /// </summary>
        /// <param name="updateInterval">The update interval.</param>
        /// <returns>New instance of <see cref="ConsoleCaptureService"/></returns>
        public IConsoleCaptureService GetConsoleCaptureService(double updateInterval = 1000.0)
        {
            return new ConsoleCaptureService(updateInterval);
        }

        /// <summary>
        /// Gets the path finder service.
        /// </summary>
        /// <returns>
        /// IPathFinderInstance
        /// </returns>
        public IDatasetDetailsFinderService GetDatasetDetailsFinderService()
        {
            return new DatasetDetailsFinderService();
        }
    }
}
