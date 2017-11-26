/*
 * MockModule.cs
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

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using Moq;
using Ninject.Modules;
using Spectre.Database.Contexts;
using Spectre.Database.Entities;

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

            Rebind<DatasetsContext>()
                .ToMethod(method: x =>
                {
                    var data = new List<Dataset>
                    {
                        new Dataset { FriendlyName = "FriendlyName1", Hash = "Hash1", UploadNumber = "UploadNumber1"},
                        new Dataset { FriendlyName = "FriendlyName2", Hash = "Hash2", UploadNumber = "UploadNumber2"},
                        new Dataset { FriendlyName = "FriendlyName3", Hash = "Hash3", UploadNumber = "UploadNumber3"},
                    }.AsQueryable();

                    var mockSet = new Mock<DbSet<Dataset>>();
                    mockSet.As<IQueryable<Dataset>>().Setup(m => m.Provider).Returns(data.Provider);
                    mockSet.As<IQueryable<Dataset>>().Setup(m => m.Expression).Returns(data.Expression);
                    mockSet.As<IQueryable<Dataset>>().Setup(m => m.ElementType).Returns(data.ElementType);
                    mockSet.As<IQueryable<Dataset>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

                    var mockContext = new Mock<DatasetsContext>();
                    mockContext.Setup(c => c.Datasets).Returns(mockSet.Object);
                    return mockContext.Object;
                })
                .InTransientScope();
        }

        #endregion
    }
}
