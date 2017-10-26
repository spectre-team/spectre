/*
* SVMFitnessFunction.h
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

#pragma once
#include "Spectre.libClassifier/ConfusionMatrix.h"
#include "Spectre.libClassifier/SplittedOpevCvDataset.h"
#include "Spectre.libClassifier/Svm.h"
#include "Spectre.libGenetic/FitnessFunction.h"
#include "RaportGenerator.h"

namespace Spectre::GaSvmNative
{
class SVMFitnessFunction : public libGenetic::FitnessFunction
{
public:
    SVMFitnessFunction(libClassifier::SplittedOpenCvDataset&& data, RaportGenerator& raportGenerator, uint svmIterations=100u, double svmTolerance=1e-6);
    libGenetic::ScoreType computeFitness(const libGenetic::Individual &individual) override;
    virtual ~SVMFitnessFunction() = default;
private:
    libClassifier::SplittedOpenCvDataset m_Dataset;
    RaportGenerator& m_RaportGenerator;
    const uint m_SvmIterations;
    const double m_SvmTolerance;

    libClassifier::ConfusionMatrix getResultMatrix(const libClassifier::OpenCvDataset& data, const libGenetic::Individual& individual) const;
};
}
