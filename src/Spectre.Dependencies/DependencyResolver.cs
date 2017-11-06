/*
 * DependencyResolver.cs
 * Singleton class for applying chosen dependencies to different parts of
 * the application.
 *
   Copyright 2017 Dariusz Kuchta
   
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
using Ninject;
using Ninject.Modules;
using Spectre.Dependencies.Modules;

namespace Spectre.Dependencies
{
    /// <summary>
    /// Singleton class for applying chosen dependencies to different parts of the application.
    /// </summary>
    public sealed class DependencyResolver
    {
        private static readonly IKernel Kernel = new StandardKernel(new DefaultModule());

        /// <summary>
        /// Method for loading new modules containing bindings to types for dependency resolving.
        /// </summary>
        /// <param name="newModule">Module to load.</param>
        public static void AddModule(INinjectModule newModule)
        {
            if(!Kernel.HasModule(newModule.Name))
                Kernel.Load(newModule);
        }

        /// <summary>
        /// Method for getting a certain service according to its type.
        /// </summary>
        /// <returns>Found service.</returns>
        public static T GetService<T>() where T : class => Kernel.TryGet(typeof(T)) as T;
    }
}
