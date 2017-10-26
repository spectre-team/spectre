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
    //JEZELI INDIVIDUAL MA WARTOSCI TYLKO FALSE TO USTAWIAM PIERWSZA WARTOSC NA TRUE
    //DO POPRAWKI, BRZYDKIE, TYMCZASOWE ROZWIAZANIE
    bool onlyFalse = true;
    for (bool flag: individual)
    {
        if (flag == true)
        {
            onlyFalse = false;
            break;
        }
    }
    if (onlyFalse)
    {
        const_cast<libGenetic::Individual&>(individual)[0] = true;
    }
    //KONIEC BRZYDKIEGO ROZWIAZANIA PROBLEMU
    const auto& dataToFilter = m_Dataset.trainingSet.GetData();
    const auto twoDimensionalFilteredData = libPlatform::Functional::filter(dataToFilter, individual.getData());
    // @gmrukwa: TODO: Reserve space.
    std::vector<DataType> oneDimensionalFilteredData;
    oneDimensionalFilteredData.reserve(twoDimensionalFilteredData.size() * twoDimensionalFilteredData[0].size());
    for (const auto& observation: twoDimensionalFilteredData)
    {
        for (float number: observation)
        {
            oneDimensionalFilteredData.push_back(number);
        }
    }
    std::vector<Label> filteredLabels = libPlatform::Functional::filter(m_Dataset.trainingSet.GetSampleMetadata(), individual.getData());
    OpenCvDataset individualDataset(oneDimensionalFilteredData, filteredLabels);
    const auto result = getResultMatrix(std::move(individualDataset));
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
    const auto time_difference = std::to_string(elapsed_secs);
    mRaportGenerator->write("Time needed to teach this individual: " + time_difference + " seconds.");

    const auto predictions = svm.Predict((m_Dataset.testSet));
    return ConfusionMatrix(predictions, m_Dataset.testSet.GetSampleMetadata());
}
}
