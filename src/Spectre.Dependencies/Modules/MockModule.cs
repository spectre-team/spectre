/*
 * DefaultModule.cs
 * Module containing bindings for mocks of certain parts of the application
 * for testing purposes.
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

using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using Ninject.Modules;

namespace Spectre.Dependencies.Modules
{
    /// <summary>
    /// Module containing bindings for mocks of certain parts of the application
    /// for testing purposes.
    /// </summary>
    public class MockModule : NinjectModule
    {
        #region Overrides of NinjectModule

        /// <inheritdoc />
        public override void Load()
        {
            Rebind<IFileSystem>()
                .To<MockFileSystem>()
                .InSingletonScope();
        }

        #endregion
    }
}
