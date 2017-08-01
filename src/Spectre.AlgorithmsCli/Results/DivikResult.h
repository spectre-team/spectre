/*
* DivikResult.h
* Contains structure organizing all the output from DiviK algorithm.
*
Copyright 2017 Dariusz Kuchta

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

#include "DivikResultStruct.h"

#include <memory>

using namespace System;

namespace Spectre::AlgorithmsCli::Results
{
	/// <summary>
	/// Wraps DiviK algorithm results.
	/// </summary>
	public ref class DivikResult
	{
	public:

		/// <summary>
		/// Clustering quality index
		/// </summary>
		property double QualityIndex
		{
			double get()
			{
				return m_ResultStruct->get()->qualityIndex;
			}
		}

		/// <summary>
		/// Centroids obtained in clustering
		/// </summary>
		property array<double, 2> ^Centroids
		{
			array<double, 2> ^get()
			{
				auto &data = m_ResultStruct->get()->centroids;

				int size1 = (int)data.size();
				int size2;
				if (size1)
					size2 = (int)data.front().size();
				else
					size2 = 0;

				array<double, 2> ^result = gcnew array<double, 2>(size1, size2);

				for (int i = 0; i < size1; ++i)
					for (int j = 0; j < size2; ++j)
						result[i, j] = data[i][j];

				return result;
			}
		}

		/// <summary>
		/// Partition of data
		/// </summary>
		property array<int> ^Partition
		{
			array<int> ^get()
			{
				auto& data = m_ResultStruct->get()->partition;
				int size = (int)data.size();
				array<int> ^result = gcnew array<int>(size);
				for (int i = 0; i < size; ++i)
					result[i] = data[i];
				return result;
			}
		}

		/// <summary>
		/// Amplitude threshold
		/// </summary>
		property double AmplitudeThreshold
		{
			double get()
			{
				return m_ResultStruct->get()->amplitudeThreshold;
			}
		}

		/// <summary>
		/// Amplitude filter
		/// </summary>
		property array<bool> ^AmplitudeFilter
		{
			array<bool> ^get()
			{
				auto& data = m_ResultStruct->get()->amplitudeFilter;
				int size = (int)data.size();
				array<bool> ^result = gcnew array<bool>(size);
				for (int i = 0; i < size; ++i)
					result[i] = data[i];
				return result;
			}
		}

		/// <summary>
		/// Variance threshold
		/// </summary>
		property double VarianceThreshold
		{
			double get()
			{
				return m_ResultStruct->get()->varianceThreshold;
			}
		}

		/// <summary>
		/// Variance filter
		/// </summary>
		property array<bool> ^VarianceFilter
		{
			array<bool> ^get()
			{
				auto& data = m_ResultStruct->get()->varianceFilter;
				int size = (int)data.size();
				array<bool> ^result = gcnew array<bool>(size);
				for (int i = 0; i < size; ++i)
					result[i] = data[i];
				return result;
			}
		}

		/// <summary>
		/// Downmerged partition
		/// </summary>
		property array<int> ^Merged
		{
			array<int> ^get()
			{
				auto& data = m_ResultStruct->get()->merged;
				int size = (int)data.size();
				array<int> ^result = gcnew array<int>(size);
				for (int i = 0; i < size; ++i)
					result[i] = data[i];
				return result;
			}
		}

		/// <summary>
		/// Result of further splits
		/// </summary>
		property array<DivikResult^> ^Subregions
		{
			array<DivikResult^> ^get()
			{
				auto& data = m_ResultStruct->get()->subregions;
				int size = (int)data.size();
				array<DivikResult^> ^result = gcnew array<DivikResult^>(size);
				for (int i = 0; i < size; ++i)
					result[i] = gcnew DivikResult(std::make_unique<DivikResultStruct>(data[i]));
				return result;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DivikResult"/> class with empty result struct.
		/// </summary>
		DivikResult()
		{
			m_ResultStruct = new std::unique_ptr<DivikResultStruct>(std::make_unique<DivikResultStruct>());
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DivikResult"/> class.
		/// </summary>
		/// <param name="result">The result coming from native Divik algorithm.</param>
		DivikResult(std::unique_ptr<DivikResultStruct> result)
		{
			m_ResultStruct = new std::unique_ptr<DivikResultStruct>(std::move(result));
		}

		/// <summary>
		/// Default finalizer.
		/// </summary>
		!DivikResult()
		{
			delete m_ResultStruct;
		}

		/// <summary>
		/// Default destructor.
		/// </summary>
		~DivikResult()
		{
			this->!DivikResult();
		}

		/// <summary>
		/// Determines whether the specified <see cref="System::Object" /> is equal to this instance.
		/// </summary>
		/// <param name="obj">The <see cref="System::Object" /> to compare with this instance.</param>
		/// <returns>
		///   <c>true</c> if the specified <see cref="System::Object" /> is equal to this instance; otherwise, <c>false</c>.
		/// </returns>
		bool Equals(Object %obj);

		/// <summary>
		/// Returns hash code for given instance.
		/// </summary>
		int GetHashCode() override;

	private:

		std::unique_ptr<DivikResultStruct>* m_ResultStruct;

	};
}
