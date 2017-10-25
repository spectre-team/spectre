/*
* OpenCvDataset.h
* Dataset implementation coupling with OpenCV cv::Mat.
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

#pragma once
#include "Spectre.libDataset/IReadOnlyDataset.h"
#include "Empty.h"
#include "Types.h"

namespace Spectre::libClassifier {
/// <summary>
/// Data-owning structure which couples our typed with OpenCV cv::Mat
/// </summary>
class OpenCvDataset final : public Spectre::libDataset::IReadOnlyDataset<Observation, Label, Empty>
{
public:
    /// <summary>
    /// Deleted default copy constructor.
    /// </summary>
    /// <param name="">The other.</param>
    OpenCvDataset(const OpenCvDataset&) = delete;
    /// <summary>
    /// Initializes a new instance of the <see cref="OpenCvDataset"/> class.
    /// </summary>
    /// <param name="other">The other.</param>
    OpenCvDataset(OpenCvDataset&& other) noexcept;
    /// <summary>
    /// Initializes a new instance of the <see cref="OpenCvDataset"/> class.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="labels">The labels.</param>
    OpenCvDataset(gsl::span<const DataType> data, gsl::span<const Label> labels);
    /// <summary>
    /// Initializes a new instance of the <see cref="OpenCvDataset"/> class.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="labels">The labels.</param>
    OpenCvDataset(cv::Mat data, cv::Mat labels);

    /// <summary>
    /// Gets sample under specified index in read-only fashion.
    /// </summary>
    /// <param name="idx">The index.</param>
    /// <returns>Sample</returns>
    const Observation& operator[](size_t idx) const override;
    /// <summary>
    /// Gets the sample metadata in read-only fashion.
    /// </summary>
    /// <param name="idx">The index.</param>
    /// <returns>Sample metadata</returns>
    const Label& GetSampleMetadata(size_t idx) const override;
    /// <summary>
    /// Gets the dataset metadata in read-only fashion.
    /// </summary>
    /// <returns>Dataset metadata</returns>
    const Empty& GetDatasetMetadata() const override;

    /// <summary>
    /// Gets the data in read-only fashion.
    /// </summary>
    /// <returns></returns>
    gsl::span<const Observation> GetData() const override;
    /// <summary>
    /// Gets the sample metadata in read-only fashion.
    /// </summary>
    /// <returns></returns>
    gsl::span<const Label> GetSampleMetadata() const override;

    /// <summary>
    /// Number of elements in dataset.
    /// </summary>
    /// <returns>Size</returns>
    size_t size() const override;
    /// <summary>
    /// Checks, whether dataset is empty.
    /// </summary>
    /// <returns>True, if dataset is empty.</returns>
    bool empty() const override;
    /// <summary>
    /// Returns data of OpenCvDataset as Mat.
    /// </summary>
    /// <returns></returns>
    const cv::Mat& getMatData() const;
    /// <summary>
    /// Returns labels of OpenCvDataset as Mat.
    /// </summary>
    /// <returns></returns>
    const cv::Mat& getMatLabels() const;

    ~OpenCvDataset() = default;

private:
    std::vector<DataType> m_Data;
    cv::Mat m_Mat;
    std::vector<Label> m_labels;
    cv::Mat m_MatLabels;
    std::vector<Observation> m_observations;
};
}
