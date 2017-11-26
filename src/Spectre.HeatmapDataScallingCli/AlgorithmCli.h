/*
* AlgorithmCli.h
* Abstract class with scaleData virtual method.
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

public ref class AlgorithmCli
{
protected:
	Algorithm * algorithm;
	std::vector<double> * intensities;
	template <typename T>
	static std::vector<T> toNative(array<T>^ managedCollection)
	{
		std::vector<T> native;
		native.reserve(managedCollection->Length);
		for (auto i = 0; i < managedCollection->Length; ++i)
		{
			native.push_back(managedCollection[i]);
		}
		return native;
	}
public:
	AlgorithmCli() {}

	~AlgorithmCli()
	{
		delete algorithm;
		delete intensities;
	}

	virtual std::vector<double> scaleData() = 0;
};

