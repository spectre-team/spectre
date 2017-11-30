/*
* ConfusionMatrix.h
* It contains knowledge about true positive, true negative etc. prediction of fitness function
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

/// <summary>
/// Class having results of fitness function predictions: true positive, true negative, false positive and false negative.
/// </summary>
class ConfusionMatrix
{
public:
    /// <summary>
    /// Initializes a new instance of the <see cref="ConfusionMatrix"/> class.
    /// </summary>
    /// <param name="truePositivesNumber">The number of true positives.</param>
    /// <param name="trueNegativesNumber">The number of true negatives.</param>
    /// <param name="falsePositivesNumber">The number of false positives.</param>
    /// <param name="falseNegativesNumber">The number of false negatives.</param>
    ConfusionMatrix(unsigned int truePositivesNumber,
                    unsigned int trueNegativesNumber,
                    unsigned int falsePositivesNumber,
                    unsigned int falseNegativesNumber);
    /// <summary>
    /// Initializes a new instance of the <see cref="ConfusionMatrix"/> class.
    /// </summary>
    /// <param name="actual">The number of actual correct labels.</param>
    /// <param name="expected">The number of expected correct labels.</param>
    ConfusionMatrix(const gsl::span<const Label> actual, const gsl::span<const Label> expected);

    /// <summary>
    /// The number of true positives.
    /// </summary>
    const unsigned int TruePositive;
    /// <summary>
    /// The number of true negatives.
    /// </summary>
    const unsigned int TrueNegative;
    /// <summary>
    /// The number of false positives.
    /// </summary>
    const unsigned int FalsePositive;
    /// <summary>
    /// The number of false negatives.
    /// </summary>
    const unsigned int FalseNegative;
    /// <summary>
    /// The dice index.
    /// </summary>
    const double DiceIndex;
};

}
