#include "Dataset_opencv.h"
#include "Spectre.libException/OutOfRangeException.h"

namespace Spectre::libClassifier {

Empty::Empty()
{
	
}

const Empty& Empty::instance()
{
	if (m_instance == nullptr)
	{
		m_instance = std::make_unique<Empty>();
	}
	return *m_instance;
}


Dataset_opencv::Dataset_opencv(gsl::span<DataType> data, gsl::span<Label> labels, size_t numberOfRows, size_t numberOfColumns):
	m_pData(data.begin(), data.end()),
	m_Mat(numberOfRows, numberOfColumns, CV_TYPE, m_pData.data()),
	m_labels(labels.begin(), labels.end()),
	m_MatLabels(numberOfRows, 1, CV_LABEL_TYPE, m_labels.data()),
	m_observations(numberOfRows)
{
	//@wwilgierz TODO sprawdx czy rozmiary sie zgadzaja
	//@wwilgierz TODO wyeliminuj zbedne parametry (nr rows, cols)
	auto rowBegin = m_pData.data();
	for (auto i=0u;i<numberOfRows; i++)
	{
		m_observations[i] = Observation(rowBegin, numberOfColumns);
		rowBegin += numberOfColumns;
	}
}

Dataset_opencv::Dataset_opencv(cv::Mat data, cv::Mat labels):
	m_pData(data.ptr<DataType>(0), data.ptr<DataType>(data.rows*data.cols)),
	m_Mat(data.rows, data.cols, CV_TYPE, m_pData.data()),
	m_labels(labels.ptr<DataType>(0), labels.ptr<DataType>(labels.rows*labels.cols)),
	m_MatLabels(m_labels.size(), 1, CV_LABEL_TYPE, m_labels.data()),
	m_observations(data.rows)
{
	//@wwilgierz TODO sprawdx czy rozmiary sie zgadzaja
	auto rowBegin = m_pData.data();
	for (auto i = 0u; i<data.rows; i++)
	{
		m_observations[i] = Observation(rowBegin, data.cols);
		rowBegin += data.cols;
	}
}

const Observation& Dataset_opencv::operator[](size_t idx) const
{
	if (idx > m_Mat.rows)
	{
		throw libException::OutOfRangeException(idx, m_Mat.rows);
	}
	return m_observations[idx];
}

const Label& Dataset_opencv::GetSampleMetadata(size_t idx) const
{
	//@wwilgierz TODO sprawdz czy nie wykracza poza rozmiar
	return m_labels[idx];
}

const Empty& Dataset_opencv::GetDatasetMetadata() const
{
	//@wwilgierz TODO wyrzuc klase Empty do innego pliku
	return Empty::instance();
}

gsl::span<const Observation> Dataset_opencv::GetData() const
{
	return m_observations;
}

gsl::span<const Label> Dataset_opencv::GetSampleMetadata() const
{
	return m_labels;
}

size_t Dataset_opencv::size() const
{
	return m_observations.size();
}

bool Dataset_opencv::empty() const
{
	return size() == 0;
}

//@wwilgierz TODO 2 funkcje MAT& pierwszy i drugi parametr z svm  (m_Map, m_labels)

}
