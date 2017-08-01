/*
* InconsistentInputSizeException.h
* Exception thrown on inconsistent Dataset input sizes.
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
#include "Spectre.libException/ExceptionBase.h"

namespace Spectre::libDataset
{
/// <summary>
/// Thrown when two samples and metadata amounts are not equal.
/// </summary>
class InconsistentInputSizeException final : public libException::ExceptionBase
{
public:
    /// <summary>
    /// Initializes a new instance of the <see cref="InconsistentInputSizeException"/> class.
    /// </summary>
    /// <param name="samplesNumber">The samples number.</param>
    /// <param name="metadataNumber">The metadata number.</param>
    InconsistentInputSizeException(size_t samplesNumber, size_t metadataNumber);
};
}
