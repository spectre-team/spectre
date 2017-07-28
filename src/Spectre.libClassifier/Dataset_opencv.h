﻿#pragma once
#include <memory>
#include <opencv2/core/mat.hpp>
#include "Spectre.libDataset/IDataset.h"

namespace Spectre::libClassifier {
	using DataType = float;
	using Observation = gsl::span<DataType>;
	using Label = signed;
	const auto CV_TYPE = CV_32FC1;
	const auto CV_LABEL_TYPE = CV_32SC1;

class Empty
{
public:
	static const Empty& instance();
private:
	static std::unique_ptr<Empty> m_instance;
	Empty();
};

class Dataset_opencv final : public Spectre::libDataset::IReadOnlyDataset<Observation, Label, Empty>
{
public:
	Dataset_opencv(gsl::span<DataType> data, gsl::span<Label> labels, size_t numberOfRows, size_t numberOfColumns);
	Dataset_opencv(cv::Mat data, cv::Mat labels);

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

	~Dataset_opencv() = default;

private:
	std::vector<DataType> m_pData;
	cv::Mat m_Mat;
	std::vector<Label> m_labels;
	cv::Mat m_MatLabels;
	std::vector<Observation> m_observations;
};
}
