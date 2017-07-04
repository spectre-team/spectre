#pragma once

#include "DivikResultStruct.h"

#include <cliext/adapter>  

using namespace cliext;
using namespace System;
using namespace System::Collections::Generic;

public ref class DivikResult
{
public:

	property double QualityIndex
	{
		double get()
		{
			return m_ResultStruct->qualityIndex;
		}
	}

	property array<double, 2> ^Centroids
	{
		array<double, 2> ^get()
		{
			auto &data = m_ResultStruct->centroids;

			unsigned int size1 = data.size();
			unsigned int size2 = data.front().size();

			array<double, 2> ^result = gcnew array<double, 2>(size1, size2);

			for (int i = 0; i < size1; ++i)
				for (int j = 0; j < size2; ++j)
					result[i, j] = data[i][j];

			return result;
		}
	}

	property array<int> ^Partition
	{
		array<int> ^get()
		{
			auto& data = m_ResultStruct->partition;
			unsigned int size = data.size();
			array<int> ^result = gcnew array<int>(size);
			for (int i = 0; i < size; ++i)
				result[i] = data[i];
			return result;
		}
	}

	property double AmplitudeThreshold
	{
		double get()
		{
			return m_ResultStruct->amplitudeThreshold;
		}
	}

	property array<bool> ^AmplitudeFilter
	{
		array<bool> ^get()
		{
			auto& data = m_ResultStruct->amplitudeFilter;
			unsigned int size = data.size();
			array<bool> ^result = gcnew array<bool>(size);
			for (int i = 0; i < size; ++i)
				result[i] = data[i];
			return result;
		}
	}

	property double VarianceThreshold
	{
		double get()
		{
			return m_ResultStruct->varianceThreshold;
		}
	}

	property array<bool> ^VarianceFilter
	{
		array<bool> ^get()
		{
			auto& data = m_ResultStruct->varianceFilter;
			unsigned int size = data.size();
			array<bool> ^result = gcnew array<bool>(size);
			for (int i = 0; i < size; ++i)
				result[i] = data[i];
			return result;
		}
	}

	property array<int> ^Merged
	{
		array<int> ^get()
		{
			auto& data = m_ResultStruct->merged;
			unsigned int size = data.size();
			array<int> ^result = gcnew array<int>(size);
			for (int i = 0; i < size; ++i)
				result[i] = data[i];
			return result;
		}
	}

	property array<DivikResult^> ^Subregions
	{
		array<DivikResult^> ^get()
		{
			auto& data = m_ResultStruct->subregions;
			unsigned int size = data.size();
			array<DivikResult^> ^result = gcnew array<DivikResult^>(size);
			for (int i = 0; i < size; ++i)
				result[i] = gcnew DivikResult(&data[i]);
			return result;
		}
	}

	DivikResult() = delete;
	DivikResult(DivikResultStruct *result)
	{
		m_ResultStruct = result;
	}

private:

	DivikResultStruct *m_ResultStruct;
};