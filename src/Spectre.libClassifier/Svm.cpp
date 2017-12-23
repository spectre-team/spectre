/*
* Svm.cpp
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
#include "Spectre.libClassifier/UntrainedClassifierException.h"
#include "Spectre.libClassifier/UnsupportedSvmTypeException.h"

namespace Spectre::libClassifier
{
    Svm::Svm(SVM_TYPE type, unsigned int iterationsLimit, double tolerance)
        :m_SvmType(type)
    {
        m_Svm = cv::ml::SVM::create();
        switch (m_SvmType)
        {
        case C_SVC:
            m_Svm->setType(cv::ml::SVM::C_SVC);
            break;
        case NU_SVC:
            m_Svm->setType(cv::ml::SVM::NU_SVC);
            break;
        case ONE_CLASS:
            m_Svm->setType(cv::ml::SVM::ONE_CLASS);
            break;
        case EPS_SVR:
            m_Svm->setType(cv::ml::SVM::EPS_SVR);
            break;
        case NU_SVR:
            m_Svm->setType(cv::ml::SVM::NU_SVR);
            break;
        default:
            m_Svm->setType(cv::ml::SVM::NU_SVC);
            break;
        }
        m_Svm->setKernel(cv::ml::SVM::LINEAR);
        const auto termination = cv::TermCriteria(cv::TermCriteria::MAX_ITER, iterationsLimit, tolerance);
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
        if (!m_Svm->isTrained())
        {
            throw UntrainedClassifierException();
        }
        const auto& data = asSupported(dataset);
        const auto numberOfObservations = static_cast<unsigned>(data.getMatData().rows);
        std::vector<float> predictions(numberOfObservations, 0);
        cv::Mat predictionOutput(numberOfObservations, 1, CV_TYPE, predictions.data());
        m_Svm->predict(data.getMatData(), predictionOutput);
        std::vector<Label> labels(numberOfObservations, 0);
        cv::Mat labelsOutput(numberOfObservations, 1, CV_LABEL_TYPE, labels.data());
        predictionOutput.convertTo(labelsOutput, CV_LABEL_TYPE);
        return labels;
    }

    unsigned int Svm::GetNumberOfSupportVectors() const
    {
        if (m_SvmType == C_SVC)
        {
            throw UnsupportedSvmTypeException(m_SvmType);
        }
        return m_Svm->getSupportVectors().rows;
    }


    const OpenCvDataset& Svm::asSupported(LabeledDataset dataset)
    {
        try
        {
            const auto& casted = dynamic_cast<const OpenCvDataset&>(dataset);
            return casted;
        }
        catch (const std::bad_cast&)
        {
            throw UnsupportedDatasetTypeException(dataset);
        }
    }
}
