/*
* Svm.h
* Simple wrapper for OpenCV SVM.
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

#include "Svm.h"
#include "Spectre.libClassifier/OpenCvDataset.h"
#include "Spectre.libClassifier/UnsupportedDatasetTypeException.h"

namespace Spectre::libClassifier
{
Svm::Svm()
{
    m_Svm = cv::ml::SVM::create();
    m_Svm->setType(cv::ml::SVM::C_SVC);
    m_Svm->setKernel(cv::ml::SVM::LINEAR);
    const auto termination = cv::TermCriteria(cv::TermCriteria::MAX_ITER, 100, 1e-6);
    m_Svm->setTermCriteria(termination);
}


void Svm::Fit(LabeledDataset dataset)
{
    const auto& data = asSupported(dataset);
    m_Svm->clear();
    m_Svm->train(data.getMatData(), cv::ml::ROW_SAMPLE, data.getMatLabels());
}

std::vector<Label> Svm::Predict(LabeledDataset dataset) const
{
    const auto& data = asSupported(dataset);
    const auto numberOfObservations = static_cast<unsigned>(data.getMatData().rows);
    std::vector<Label> predictions(numberOfObservations, 0);
    cv::Mat predictionOutput(numberOfObservations, 1, CV_LABEL_TYPE, predictions.data());
    m_Svm->predict(data.getMatData(), predictionOutput);
    return predictions;
}

const OpenCvDataset& Svm::asSupported(LabeledDataset dataset)
{
    try
    {
        const auto& casted = dynamic_cast<const OpenCvDataset&>(dataset);
        return casted;
    }
    catch (const std::bad_cast& exception)
    {
        throw UnsupportedDatasetTypeException(dataset);
    }
}
}
