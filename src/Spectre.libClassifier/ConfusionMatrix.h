/*
* ConfusionMatrix.h
* It contains knowledge about classifier performance
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
#include <span.h>
#include "Types.h"

namespace Spectre::libClassifier {

class ConfusionMatrix
{
public:
    const unsigned int TruePositive;
    const unsigned int TrueNegative;
    const unsigned int FalsePositive;
    const unsigned int FalseNegative;
    const double DiceIndex;

    ConfusionMatrix(unsigned int truePositivesNumber,
                    unsigned int trueNegativesNumber,
                    unsigned int falsePositivesNumber,
                    unsigned int falseNegativesNumber);
    ConfusionMatrix(const gsl::span<const Label> actual, const gsl::span<const Label> expected);
};

}
