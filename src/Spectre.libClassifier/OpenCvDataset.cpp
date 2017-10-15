/*
* OpenCvDataset.cpp
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

#include "Spectre.libException/InconsistentArgumentSizesException.h"
#include "Spectre.libException/OutOfRangeException.h"
#include "OpenCvDataset.h"
#include "Spectre.libClassifier/EmptyOpenCvDatasetException.h"

namespace Spectre::libClassifier {

const int ColumnMatrixWidth = 1;

constexpr size_t throwOnEmpty(size_t size)
{
    return size == 0
        ? throw libException::EmptyOpenCvDatasetException("Empty argument")
        : size;
}

OpenCvDataset::OpenCvDataset(OpenCvDataset &&other) noexcept
    : m_Data(std::move(other.m_Data)),
      m_Mat(std::move(other.m_Mat)),
      m_labels(std::move(other.m_labels)),
      m_MatLabels(std::move(other.m_MatLabels)),
      m_observations(std::move(other.m_observations))
{
    other.m_Data.clear();
    other.m_Mat.release();
    other.m_labels.clear();
    other.m_MatLabels.release();
    other.m_observations.clear();
}

OpenCvDataset::OpenCvDataset(gsl::span<const DataType> data, gsl::span<const Label> labels):
    m_Data(data.begin(), data.end()),
    m_labels(labels.begin(), labels.end()),
    m_observations(static_cast<int>(labels.size()))
{
    throwOnEmpty(data.size());
    m_Mat = cv::Mat(static_cast<int>(labels.size()), static_cast<int>(data.size() / labels.size()), CV_TYPE, m_Data.data());
    m_MatLabels = cv::Mat(static_cast<int>(labels.size()), ColumnMatrixWidth, CV_LABEL_TYPE, m_labels.data());
    if (data.size() % labels.size() != 0 || m_Mat.rows != static_cast<int>(m_labels.size()))
    {
        throw libException::InconsistentArgumentSizesException("data", m_Mat.rows, "labels", static_cast<int>(m_labels.size()));
    }
    const auto numberOfColumns = data.size() / labels.size();
    auto rowBegin = m_Data.data();
    for (auto i = 0u; i < labels.size(); ++i)
    {
        m_observations[i] = Observation(rowBegin, numberOfColumns);
        rowBegin += numberOfColumns;
    }
}

OpenCvDataset::OpenCvDataset(cv::Mat data, cv::Mat labels):
    m_Data(data.ptr<DataType>(), data.ptr<DataType>() + data.rows * data.cols),
    m_labels(labels.ptr<Label>(), labels.ptr<Label>() + labels.rows * labels.cols),
    m_observations(data.rows)
{
    throwOnEmpty(data.rows * data.cols);
    m_Mat = cv::Mat(data.rows, data.cols, CV_TYPE, m_Data.data());
    m_MatLabels = cv::Mat(static_cast<int>(m_labels.size()), 1, CV_LABEL_TYPE, m_labels.data());
    if (m_Mat.rows != static_cast<int>(m_labels.size()))
    {
        throw libException::InconsistentArgumentSizesException("data", m_Mat.rows,"labels", static_cast<int>(m_labels.size()));
    }
    auto rowBegin = m_Data.data();
    for (auto i = 0; i < data.rows; ++i)
    {
        m_observations[i] = Observation(rowBegin, data.cols);
        rowBegin += data.cols;
    }
}

const Observation& OpenCvDataset::operator[](size_t idx) const
{
    if (idx >= m_Mat.rows)
    {
        throw libException::OutOfRangeException(idx, m_Mat.rows);
    }
    return m_observations[idx];
}

const Label& OpenCvDataset::GetSampleMetadata(size_t idx) const
{
    if (idx >= m_MatLabels.rows)
    {
        throw libException::OutOfRangeException(idx, m_MatLabels.rows);
    }
    return m_labels[idx];
}

const Empty& OpenCvDataset::GetDatasetMetadata() const
{
    return Empty::instance();
}

gsl::span<const Observation> OpenCvDataset::GetData() const
{
    return m_observations;
}

gsl::span<const Label> OpenCvDataset::GetSampleMetadata() const
{
    return m_labels;
}

size_t OpenCvDataset::size() const
{
    return m_labels.size();
}

bool OpenCvDataset::empty() const
{
    return size() == 0;
}

const cv::Mat& OpenCvDataset::getMatData() const
{
    return m_Mat;
}

const cv::Mat& OpenCvDataset::getMatLabels() const
{
    return m_MatLabels;
}
}
