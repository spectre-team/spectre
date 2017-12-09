/*
* IntensitiesDataConverter.h
* Intensities converter from c# array to native gsl::span.
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
#include <vector>

namespace Spectre::HeatmapDataScalingCli
{
	public ref class IntensitiesDataConverter
	{	
	public:
		template <typename T>
		static std::vector<T> *toNative(array<T>^ managedCollection)
		{
			std::vector<T> *native = new std::vector<T>();
			native->reserve(managedCollection->Length);
			for (auto i = 0; i < managedCollection->Length; ++i)
			{
				native->push_back(managedCollection[i]);
			}
			return native;
		}

		static array<double>^ toManaged(const gsl::span<double> nativeCollection)
		{
			array<double>^ managedCollection = gcnew array<double>((int)nativeCollection.size());

			for (auto i = 0; i < nativeCollection.size(); ++i)
			{
				managedCollection[i] = nativeCollection[i];
			}
			return managedCollection;
		}
	};
}