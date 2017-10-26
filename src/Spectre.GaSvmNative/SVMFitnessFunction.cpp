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

#include <ctime>
#include "Spectre.libClassifier/ConfusionMatrix.h"
#include "Spectre.libPlatform/Filter.h"
#include "SVMFitnessFunction.h"

namespace Spectre::GaSvmNative
{
using namespace libClassifier;
using namespace libGenetic;

SVMFitnessFunction::SVMFitnessFunction(SplittedOpenCvDataset&& data, RaportGenerator& raportGenerator, uint svmIterations, double svmTolerance)
    : m_Dataset(std::move(data)),
      m_RaportGenerator(raportGenerator),
      m_SvmIterations(svmIterations),
      m_SvmTolerance(svmTolerance)
{
}

ScoreType SVMFitnessFunction::computeFitness(const Individual &individual)
{
    if (m_Dataset.trainingSet.size() != individual.size())
    {
        throw libException::InconsistentArgumentSizesException("data", m_Dataset.trainingSet.size(), "individual", individual.size());
    }    

    const auto& dataToFilter = m_Dataset.trainingSet.GetData();
    const auto twoDimensionalFilteredData = libPlatform::Functional::filter(dataToFilter, individual.getData());
    std::vector<DataType> oneDimensionalFilteredData;
    oneDimensionalFilteredData.reserve(twoDimensionalFilteredData.size() * twoDimensionalFilteredData[0].size());
    for (const auto& observation: twoDimensionalFilteredData)
    {
        oneDimensionalFilteredData.insert(oneDimensionalFilteredData.end(), observation.begin(), observation.end());
    }
    const auto filteredLabels = libPlatform::Functional::filter(m_Dataset.trainingSet.GetSampleMetadata(), individual.getData());
    OpenCvDataset individualDataset(oneDimensionalFilteredData, filteredLabels);
    
    const auto result = getResultMatrix(std::move(individualDataset), individual);

    return result.DiceIndex;
}

ConfusionMatrix SVMFitnessFunction::getResultMatrix(const OpenCvDataset& data, const Individual& individual) const
{
    Svm svm(m_SvmIterations, m_SvmTolerance);
    const auto trainingBegin = clock();
    svm.Fit(data);
    const auto trainingEnd = clock();
    const auto trainingTime = double(trainingEnd - trainingBegin) / CLOCKS_PER_SEC;

    const auto classificationBegin = clock();
    const auto predictions = svm.Predict((m_Dataset.testSet));
    const auto classificationEnd = clock();
    const auto classificationTime = double(classificationEnd - classificationBegin) / CLOCKS_PER_SEC;
    ConfusionMatrix confusions(predictions, m_Dataset.testSet.GetSampleMetadata());

    m_RaportGenerator.Write(confusions, individual, trainingTime, classificationTime / m_Dataset.testSet.size());

    return confusions;
}
}
