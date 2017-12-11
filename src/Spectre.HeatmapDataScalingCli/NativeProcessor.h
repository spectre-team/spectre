/*
* NativeProcessor.h
* Internally translates managed collection to native, runs callback and translates back.
*
Copyright 2017 Spectre Team

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
#include <span.h>

namespace Spectre::HeatmapDataScalingCli
{
/// <summary>
/// Internally translates managed collection to native, runs callback and translates back.
/// </summary>
template <class T1, class T2, class T3, class T4>
public ref class NativeProcessor
{
public:
    //using Map = std::vector<T2>(*map)(gsl::span<T1>);
    NativeProcessor(std::vector<T2>(*map)(gsl::span<T1>))
    {
        mMap = map;
    }
    array<T4>^ map(array<T3>^ source)
    {
        std::vector<T1> nativeSource(source->Length);
        for(auto i=0u; i< nativeSource.size(); ++i)
        {
            nativeSource[i] = source[i];
        }

        auto nativeResult = mMap(gsl::as_span(nativeSource));
        
        //translate native to managed
        array<T4>^ managedResult = gcnew array<T4>(static_cast<int>(nativeResult.size()));
        for (auto i=0u; i<nativeResult.size(); ++i)
        {
            managedResult[i] = nativeResult[i];
        }

        return managedResult;
    }

private:
    std::vector<T2>(*mMap)(gsl::span<T1>);
};

using DummyProcessor = NativeProcessor<double, double, double, double>;
}