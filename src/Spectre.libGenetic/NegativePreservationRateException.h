/*
* NegativePreservationRateException.h
* Thrown when preservation rate is negative.
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
#include "Spectre.libException/ArgumentOutOfRangeException.h"

namespace Spectre::libGenetic
{
/// <summary>
/// Thrown when preservation rate is negative.
/// </summary>
class NegativePreservationRateException: public libException::ArgumentOutOfRangeException<double>
{
public:
    /// <summary>
    /// Initializes a new instance of the <see cref="NegativePreservationRateException"/> class.
    /// </summary>
    /// <param name="actual">The value of preservation rate.</param>
    explicit NegativePreservationRateException(double actual);
};
}
