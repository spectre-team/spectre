#pragma once

#include "DivikResultStruct.h"

#include <memory>

using namespace System;
// TODO: Comparators, getHash, uniqueptr
namespace Divik
{
	public ref class DivikResultTest
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

		property array<int> ^Partition
		{
			array<int> ^get()
			{
				auto& data = m_ResultStruct->partition;
				int size = (int)data.size();
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
				int size = (int)data.size();
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
				int size = (int)data.size();
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
				int size = (int)data.size();
				array<int> ^result = gcnew array<int>(size);
				for (int i = 0; i < size; ++i)
					result[i] = data[i];
				return result;
			}
		}

		property array<DivikResultTest^> ^Subregions
		{
			array<DivikResultTest^> ^get()
			{
				auto& data = m_ResultStruct->subregions;
				int size = (int)data.size();
				array<DivikResultTest^> ^result = gcnew array<DivikResultTest^>(size);
				for (int i = 0; i < size; ++i)
					result[i] = gcnew DivikResultTest(&data[i]);
				return result;
			}
		}

		DivikResultTest()
		{
			m_ResultStruct = new DivikResultStruct();
		}
		DivikResultTest(DivikResultStruct *result)	// move ownership
		{
			m_ResultStruct = result;
		}
		!DivikResultTest()
		{
			delete m_ResultStruct;
		}
		~DivikResultTest()
		{
			this->!DivikResultTest();
		}

		bool Equals(Object %obj) override;

	private:

		DivikResultStruct* m_ResultStruct;

	};
}
