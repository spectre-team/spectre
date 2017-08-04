#include "Spectre.libException/InconsistentArgumentSizesException.h"
#include "Spectre.libException/OutOfRangeException.h"
#include "OpenCvDataset.h"

namespace Spectre::libClassifier {

const int ColumnMatrixWidth = 1;

OpenCvDataset::OpenCvDataset(gsl::span<const DataType> data, gsl::span<const Label> labels):
	m_Data(data.begin(), data.end()),
	m_Mat(static_cast<int>(labels.size()), static_cast<int>(data.size() / labels.size()), CV_TYPE, m_Data.data()),
	m_labels(labels.begin(), labels.end()),
	m_MatLabels(static_cast<int>(labels.size()), ColumnMatrixWidth, CV_LABEL_TYPE, m_labels.data()),
	m_observations(static_cast<int>(labels.size()))
{
	if (data.size() % labels.size() != 0 || m_Mat.rows != static_cast<int>(m_labels.size()))
	{
		throw libException::InconsistentArgumentSizesException("data", m_Mat.rows, "labels", static_cast<int>(m_labels.size()));
	}
    const auto numberOfColumns = data.size() / labels.size();
    auto rowBegin = m_Data.data();
    for (auto i=0u; i < labels.size(); ++i)
	{
		m_observations[i] = Observation(rowBegin, numberOfColumns);
		rowBegin += numberOfColumns;
	}
}

OpenCvDataset::OpenCvDataset(cv::Mat data, cv::Mat labels):
	m_Data(data.ptr<DataType>(), data.ptr<DataType>() + data.rows * data.cols),
	m_Mat(data.rows, data.cols, CV_TYPE, m_Data.data()),
	m_labels(labels.ptr<Label>(), labels.ptr<Label>() + labels.rows * labels.cols),
	m_MatLabels(static_cast<int>(m_labels.size()), 1, CV_LABEL_TYPE, m_labels.data()),
	m_observations(data.rows)
{
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
