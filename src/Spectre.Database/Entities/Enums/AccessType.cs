/*
 * AccessType.cs
 * File providing enumeration for entity class.
 *
   Copyright 2017 Roman Lisak

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
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Spectre.Database.Entities.Enums
{
    /// <summary>
    /// Enumeration for types of the AccessType.
    /// </summary>
    public enum AccessType
    {
        /// <summary>
        /// Dataset available to the public.
        /// </summary>
        Public,

        /// <summary>
        /// Dataset available only to the owner.
        /// </summary>
        Private,

        /// <summary>
        /// Dataset available to users with link.
        /// </summary>
        Shared
    }
}