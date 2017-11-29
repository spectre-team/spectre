/*
* InconsistentGenerationAndScoresLengthException.h
* Thrown when generation and its scores diverge in length.
*
Copyright 2017 Grzegorz Mrukwa, Wojciech Wilgierz

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
#include "Spectre.libException/InconsistentArgumentSizesException.h"

namespace Spectre::libGenetic
{
/// <summary>
/// Thrown when generation length and scores length are inconsistent.
/// </summary>
class InconsistentGenerationAndScoresLengthException :
    public libException::InconsistentArgumentSizesException
{
public:
    /// <summary>
    /// Initializes a new instance of the <see cref="InconsistentGenerationAndScoresLengthException"/> class.
    /// </summary>
    /// <param name="first">The generation length.</param>
    /// <param name="second">The scores length.</param>
    InconsistentGenerationAndScoresLengthException(size_t first, size_t second);
};
}
