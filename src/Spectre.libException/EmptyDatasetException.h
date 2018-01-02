/*
* EmptyDatasetException.h
* Thrown when input dataset was empty.
*
Copyright 2017 Spectre Team

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
#include "Spectre.libException/EmptyArgumentException.h"

namespace Spectre::libException
{
/// <summary>
/// Thrown when input dataset was empty.
/// </summary>
class EmptyDatasetException final : public EmptyArgumentException
{
public:
    /// <summary>
    /// Initializes a new instance of the <see cref="EmptyDatasetException"/> class.
    /// </summary>
    /// <param name="name">The name.</param>
    explicit EmptyDatasetException(const std::string &name);
};
}
