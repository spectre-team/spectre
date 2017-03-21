/*
 * RelayCommandtests.cs
 * Tests of RelayCommand.
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
using NUnit.Framework;
using Spectre.Mvvm.Base;
using Spectre.Mvvm.Helpers;

namespace Spectre.Mvvm.Tests.Base
{
    [TestFixture, Category("MvvmFramework")]
    public class RelayCommandTests
    {
        #region SetUp
        [OneTimeSetUp]
        public void SetUpClass()
        {
            RelayCommand.IntroduceUiMock(new UiServiceMock());
        }

        private class UiServiceMock: UiService
        {
            public override void SetBusyState()
            {
                // do nothing
            }
        }
        #endregion

        #region constructor
        #region action constructor
        [Test]
        public void TestActionConstructor()
        {
            int n = 0;
            var command = new RelayCommand(() => ++n);

            Assert.IsTrue(command.CanExecute(null),
                "Should always be able to execute.");
            command.Execute(null);
            Assert.AreEqual(1, n, "Action did not execute.");
        }
        #endregion

        #region action-func constructor
        [Test]
        public void TestActionFuncConstructor()
        {
            int n = 0;
            bool canExecute = false;
            var command = new RelayCommand(() => ++n, () => canExecute);

            Assert.IsFalse(command.CanExecute(null), "Can execute was set to false.");

            canExecute = true;
            command.OnCanExecuteChanged();

            Assert.IsTrue(command.CanExecute(null), "Can execute was set true.");
            command.Execute(null);
            Assert.AreEqual(1, n, "Action did not execute.");
        }
        #endregion

        #region action-predicate constructor

        [Test]
        public void TestActionPredicateConstructor()
        {
            int n = 0;
            Predicate<object> predicate = obj => (obj is int) && ((int)obj == 0);
            var command = new RelayCommand(() => ++n, predicate);

            Assert.IsTrue(command.CanExecute(n),
                "Should be able to execute for n equal zero.");

            command.Execute(null);
            command.OnCanExecuteChanged();

            Assert.AreEqual(1, n, "Action did not execute.");
            Assert.IsFalse(command.CanExecute(n),
                "Should not be able to execute for n equal to 1.");
        }
        #endregion

        #region action generic constructor

        [Test]
        public void TestActionGenericConstructor()
        {
            int n = 0;
            var command = new RelayCommand(num => n = num as int? ?? -1);

            Assert.IsTrue(command.CanExecute(null), "Should always be able to execute");
            command.Execute(1);
            Assert.AreEqual(1, n, "Action did not run.");
            command.Execute(null);
            Assert.AreEqual(-1, n, "Action did not run.");
        }
        #endregion

        #region action generic with predicate

        [Test]
        public void TestActionGenericPredicateConstructor()
        {
            int n = 0;
            Predicate<object> predicate = obj => obj is int;
            var command = new RelayCommand(num => n = (int)num, predicate);

            Assert.IsTrue(command.CanExecute(1), "Should be able to execute for int");
            command.Execute(1);
            Assert.AreEqual(1, n, "Action did not run.");
            Assert.IsFalse(command.CanExecute(null), "Allows to run for non-int");
        }
        #endregion
        #endregion

        #region CanExecuteChanged

        [Test]
        public void TestCanExecuteChanged()
        {
            int n1 = 0;
            int n2 = 0;
            bool canExecute = false;
            var command = new RelayCommand(() => ++n1, () => canExecute);
            command.CanExecuteChanged += (obj, e) => ++n2;

            canExecute = true;
            command.OnCanExecuteChanged();

            Assert.AreEqual(1, n2, "Event has not been called.");
        }
        #endregion
    }
}
