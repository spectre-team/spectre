/*
* ConfusionMatrix.cpp
* It contains knowledge about true positive, true negative etc. prediction of SVM
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

#include "Spectre.libException/InconsistentArgumentSizesException.h"
#include "Spectre.libClassifier/ConfusionMatrix.h"
#include "NotABinaryLabelException.h"

namespace Spectre::libClassifier {

ConfusionMatrix::ConfusionMatrix(unsigned int truePositivesNumber,
                                 unsigned int trueNegativesNumber,
                                 unsigned int falsePositivesNumber,
                                 unsigned int falseNegativesNumber):
    TruePositive(truePositivesNumber),
    TrueNegative(trueNegativesNumber),
    FalsePositive(falsePositivesNumber),
    FalseNegative(falseNegativesNumber),
    DiceIndex(2 * TruePositive / (2. * TruePositive + FalsePositive + FalseNegative))
{
}

ConfusionMatrix::ConfusionMatrix(const gsl::span<const Label> actual, const gsl::span<const Label> expected): ConfusionMatrix([&]()
{
    if(actual.size() != expected.size())
    {
        throw libException::InconsistentArgumentSizesException("actual", actual.size(), "expected", expected.size());
    }

    auto truePositives = 0u;
    auto trueNegatives = 0u;
    auto falsePositives = 0u;
    auto falseNegatives = 0u;

    const auto NEGATIVE = 0u;
    const auto POSITIVE = 1u;

    for(auto i = 0; i < actual.size(); ++i)
    {
        if(actual[i] == POSITIVE)
        {
            if(expected[i] == POSITIVE)
            {
                ++truePositives;
            }
            else if(expected[i] == NEGATIVE)
            {
                ++falsePositives;
            }
            else
            {
                throw NotABinaryLabelException(expected[i], i, "expected");
            }
        }
        else if(actual[i] == NEGATIVE)
        {
            if(expected[i] == POSITIVE)
            {
                ++falseNegatives;
            }
            else if(expected[i] == NEGATIVE)
            {
                ++trueNegatives;
            }
            else
            {
                throw NotABinaryLabelException(expected[i], i, "expected");
            }
        }
        else
        {
            throw NotABinaryLabelException(actual[i], i, "actual");
        }
    }

    return ConfusionMatrix(truePositives, trueNegatives, falsePositives, falseNegatives);
}()){}

}
