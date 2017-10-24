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
#include <opencv2/ml/ml.hpp>
#include "Spectre.libClassifier/ConfusionMatrix.h"
#include "Spectre.libClassifier/SplittedOpevCvDataset.h"
#include "Spectre.libGenetic/FitnessFunction.h"
#include "Spectre.libGenetic/RaportGenerator.h"

namespace Spectre::libClassifier
{

class SVMFitnessFunction : public Spectre::libGenetic::FitnessFunction
{
public:
    SVMFitnessFunction(SplittedOpenCvDataset&& data, RaportGenerator& raportGenerator);
    libGenetic::ScoreType fit(const libGenetic::Individual &individual) override;
    ~SVMFitnessFunction() override;
private:
    cv::Ptr<cv::ml::SVM> m_SVM;
    SplittedOpenCvDataset m_Dataset;
    RaportGenerator* mRaportGenerator;

    ConfusionMatrix getResultMatrix(OpenCvDataset data) const;
    void train(OpenCvDataset data) const;
    ConfusionMatrix predict() const;
};

}
