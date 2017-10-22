/*
* ExcessiveTrainingRateException.h
* Thrown when training rate is greater than one.
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

namespace Spectre::libClassifier
{
    class ExcessiveTrainingRateException : public libException::ArgumentOutOfRangeException<double>
    {
    public:
        explicit ExcessiveTrainingRateException(double actual);
    };
}
