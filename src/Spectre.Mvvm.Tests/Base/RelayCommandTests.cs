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
    [TestFixture]
    [Category(name: "MvvmFramework")]
    public class RelayCommandTests
    {
        #region SetUp

        [OneTimeSetUp]
        public void SetUpClass()
        {
            RelayCommand.IntroduceUiMock(mock: new UiServiceMock());
        }

        private class UiServiceMock : UiService
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
            var n = 0;
            var command = new RelayCommand(execute: () => ++n);

            Assert.IsTrue(condition: command.CanExecute(parameter: null),
                message: "Should always be able to execute.");
            command.Execute(parameter: null);
            Assert.AreEqual(expected: 1, actual: n, message: "Action did not execute.");
        }

        #endregion

        #region action-func constructor

        [Test]
        public void TestActionFuncConstructor()
        {
            var n = 0;
            var canExecute = false;
            var command = new RelayCommand(execute: () => ++n, canExecute: () => canExecute);

            Assert.IsFalse(condition: command.CanExecute(parameter: null), message: "Can execute was set to false.");

            canExecute = true;
            command.OnCanExecuteChanged();

            Assert.IsTrue(condition: command.CanExecute(parameter: null), message: "Can execute was set true.");
            command.Execute(parameter: null);
            Assert.AreEqual(expected: 1, actual: n, message: "Action did not execute.");
        }

        #endregion

        #region action-predicate constructor

        [Test]
        public void TestActionPredicateConstructor()
        {
            var n = 0;
            Predicate<object> predicate = obj => obj is int && ((int) obj == 0);
            var command = new RelayCommand(execute: () => ++n, canExecute: predicate);

            Assert.IsTrue(condition: command.CanExecute(n),
                message: "Should be able to execute for n equal zero.");

            command.Execute(parameter: null);
            command.OnCanExecuteChanged();

            Assert.AreEqual(expected: 1, actual: n, message: "Action did not execute.");
            Assert.IsFalse(condition: command.CanExecute(n),
                message: "Should not be able to execute for n equal to 1.");
        }

        #endregion

        #region action generic constructor

        [Test]
        public void TestActionGenericConstructor()
        {
            var n = 0;
            var command = new RelayCommand(execute: num => n = num as int? ?? -1);

            Assert.IsTrue(condition: command.CanExecute(parameter: null), message: "Should always be able to execute");
            command.Execute(parameter: 1);
            Assert.AreEqual(expected: 1, actual: n, message: "Action did not run.");
            command.Execute(parameter: null);
            Assert.AreEqual(expected: -1, actual: n, message: "Action did not run.");
        }

        #endregion

        #region action generic with predicate

        [Test]
        public void TestActionGenericPredicateConstructor()
        {
            var n = 0;
            Predicate<object> predicate = obj => obj is int;
            var command = new RelayCommand(execute: num => n = (int) num, canExecute: predicate);

            Assert.IsTrue(condition: command.CanExecute(parameter: 1), message: "Should be able to execute for int");
            command.Execute(parameter: 1);
            Assert.AreEqual(expected: 1, actual: n, message: "Action did not run.");
            Assert.IsFalse(condition: command.CanExecute(parameter: null), message: "Allows to run for non-int");
        }

        #endregion

        #endregion

        #region CanExecuteChanged

        [Test]
        public void TestCanExecuteChanged()
        {
            var n1 = 0;
            var n2 = 0;
            var canExecute = false;
            var command = new RelayCommand(execute: () => ++n1, canExecute: () => canExecute);
            command.CanExecuteChanged += (obj, e) => ++n2;

            canExecute = true;
            command.OnCanExecuteChanged();

            Assert.AreEqual(expected: 1, actual: n2, message: "Event has not been called.");
        }

        #endregion
    }
}
