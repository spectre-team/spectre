/*
* SVMFitnessFunction.cpp
* Trains data in svm and returns score of a Individual.
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

#include "SVMFitnessFunction.h"
#include "Spectre.libClassifier/ConfusionMatrix.h"
#include "Spectre.libPlatform/Filter.h"
#include <ctime>

namespace Spectre::libClassifier
{

SVMFitnessFunction::SVMFitnessFunction(SplittedOpenCvDataset&& data, RaportGenerator& raportGenerator)
    : m_Dataset(std::move(data)),
      mRaportGenerator(&raportGenerator)
{
}

libGenetic::ScoreType SVMFitnessFunction::fit(const libGenetic::Individual &individual)
{
    if (m_Dataset.trainingSet.size() != individual.size())
    {
        throw libException::InconsistentArgumentSizesException("data", m_Dataset.trainingSet.size(), "individual", individual.size());
    }
    gsl::span<const Observation> dataToFilter = m_Dataset.trainingSet.GetData();
    std::vector<Observation> twoDimentionalFilteredData = libPlatform::Functional::filter(dataToFilter, individual.getData());
    std::vector<DataType> oneDimentionalFilteredData;
    for (Observation observation: twoDimentionalFilteredData)
    {
        for (float number: observation)
        {
            oneDimentionalFilteredData.push_back(number);
        }
    }
    std::vector<Label> filteredLabels = libPlatform::Functional::filter(m_Dataset.trainingSet.GetSampleMetadata(), individual.getData());
    OpenCvDataset individualDataset(oneDimentionalFilteredData, filteredLabels);
    ConfusionMatrix result = getResultMatrix(std::move(individualDataset));
    mRaportGenerator->write(result);
    mRaportGenerator->write(std::to_string(result.DiceIndex));
    return result.DiceIndex;
}

ConfusionMatrix SVMFitnessFunction::getResultMatrix(const OpenCvDataset& data) const
{
    Svm svm;
    const auto begin = clock();
    svm.Fit(data);
    const auto end = clock();
    const auto elapsed_secs = double(end - begin) / CLOCKS_PER_SEC;
    std::string time_difference = std::to_string(elapsed_secs);
    mRaportGenerator->write("Time needed to teach this individual: " + time_difference + " seconds.");

    ConfusionMatrix goodNr = predict(svm);
    return goodNr;
}

// @gmrukwa: TODO: Move ConfusionMatrix calculations to libClassifier
// @gmrukwa: TODO: Loosen dependencies
ConfusionMatrix SVMFitnessFunction::predict(const Svm& svm) const
{
    auto truePositives = 0u;
    auto trueNegatives = 0u;
    auto falsePositives = 0u;
    auto falseNegatives = 0u;
    auto predictions = svm.Predict(m_Dataset.testSet);
    for (auto i = 0; i < m_Dataset.testSet.getMatData().rows; i++)
    {
        auto tmp = m_Dataset.testSet.getMatData().row(i).data;
        if (*tmp == 1)
        {
            if (predictions[i] == 1)
            {
                truePositives++;
            }
            else
            {
                trueNegatives++;
            }
        }
        else
        {
            if (predictions[i] == 1)
            {
                falsePositives++;
            }
            else
            {
                falseNegatives++;
            }
        }
    }
    return ConfusionMatrix(truePositives, trueNegatives, falsePositives, falseNegatives);
}

SVMFitnessFunction::~SVMFitnessFunction() {}

}
