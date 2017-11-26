/*
* HeatmapDataScalling.h
* Bridge class for C# and native heatmap data scalling algorithms in C++.
*
Copyright 2017 Daniel Babiak
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

using namespace System;

namespace Spectre::HeatmapDataScallingCli {

	/// <summary>
	/// Wrapper for accessing C++ algorithms from C#.
	/// </summary>
	public ref class HeatmapDataScallingWrapper
	{
	public:
		/// <summary>
		/// Initializes a new instance of the <see cref="HeatmapDataScallingWrapper"/> class.
		/// </summary>
		/// <param name="managedData">Dataset coming from managed code.</param>
		HeatmapDataScallingWrapper(array<double>^ managedData)
		{
			data = toNative(managedData);
		}

		/// <summary>
		/// Default finalizer.
		/// </summary>
		!HeatmapDataScallingWrapper()
		{
			delete data;
			data = nullptr;
		}

		/// <summary>
		/// Default destructor.
		/// </summary>
		~HeatmapDataScallingWrapper()
		{
			this->!HeatmapDataScallingWrapper();
		}

		double *normalizeLinear()
		{

		}

		double *normalizeLinear(int min, int max)
		{

		}

		double *enhanceContrastWithSupressionAlgorithm()
		{

		}

		double *enhanceContrastWithSupressionAlgorithm(double topPercent)
		{

		}

		double* blurWithGaussianAlgorithm(int window)
		{

		}

		double* blurWithBilateralAlgorithm(int window)
		{

		}


	private:
		double *data;

		static double *toNative(array<double>^ managedCollection)
		{
			double *native;
			for (auto i = 0; i < managedCollection->Length; ++i)
			{
				native[i] = managedCollection[i];
			}
			return native;
		}
	};
}