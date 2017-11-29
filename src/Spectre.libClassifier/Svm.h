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

#pragma once
#include <vector>
#include <opencv2/ml/ml.hpp>
#include "Spectre.libDataset/IReadOnlyDataset.h"
#include "Spectre.libClassifier/IClassifier.h"
#include "Spectre.libClassifier/OpenCvDataset.h"
#include "Types.h"

namespace Spectre::libClassifier
{
/// <summary>
/// SVM classifier.
/// </summary>
class Svm: public IClassifier
{
public:
    /// <summary>
    /// Initializes a new instance of the <see cref="Svm"/> class.
    /// </summary>
    /// <param name="data">The data.</param>
    explicit Svm(unsigned int iterationsLimit=100, double tolerance=1e-6);
    /// <summary>
    /// Try to fit dataset in fitness function.
    /// </summary>
    /// <param name="dataset">The dataset.</param>
    /// <returns>void</returns>
    void Fit(LabeledDataset dataset) override;
    /// <summary>
    /// Predicts labels on test set.
    /// </summary>
    /// <param name="dataset">The dataset.</param>
    /// <returns>vector of labels</returns>
    std::vector<Label> Predict(LabeledDataset dataset) const override;
    /// <summary>
    /// Gets number of support vectors.
    /// </summary>
    /// <returns>unsigned int</returns>
    unsigned int GetNumberOfSupportVectors() const;
    virtual ~Svm() = default;
private:
    static const OpenCvDataset& asSupported(LabeledDataset);
    cv::Ptr<cv::ml::SVM> m_Svm;
};
}
