/*
* OutOfRangeException.h
* Exception thrown, when referenced index is out of range.
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
/// Thrown when collection index out of bounds was requested
/// </summary>
class OutOfRange final : public ExceptionBase
{
public:
    /// <summary>
    /// Initializes a new instance of the <see cref="OutOfRange"/> class.
    /// </summary>
    /// <param name="index">The requested index.</param>
    /// <param name="size">The size of the collection.</param>
    OutOfRange(size_t index, size_t size);
};
}
