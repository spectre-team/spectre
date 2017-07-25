/*
* InconsistentArgumentSizesException.h
* Thrown when coupled arguments length differ.
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

#pragma once
#include "ExceptionBase.h"

namespace Spectre::libException
{
/// <summary>
/// Thrown when coupled arguments length differ.
/// </summary>
class InconsistentArgumentSizesException : public ExceptionBase
{
public:
    /// <summary>
    /// Initializes a new instance of the <see cref="InconsistentArgumentSizesException"/> class.
    /// </summary>
    /// <param name="nameOfFirst">The name of first argument.</param>
    /// <param name="first">The first argument size.</param>
    /// <param name="nameOfSecond">The name of second argument.</param>
    /// <param name="second">The second argument size.</param>
    InconsistentArgumentSizesException(const std::string& nameOfFirst,
                                       size_t first,
                                       const std::string& nameOfSecond,
                                       size_t second);
};
}
