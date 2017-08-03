/*
* InconsistentInputSizeException.cpp
* Exception thrown on inconsistent Dataset input sizes.
*
Copyright 2017 Grzegorz Mrukwa

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

#include <string>
#include "InconsistentInputSizeException.h"

namespace Spectre::libDataset
{
InconsistentInputSizeException::InconsistentInputSizeException(size_t samplesNumber, size_t metadataNumber)
    : Spectre::libException::ExceptionBase(std::string("number of samples: ") + std::to_string(samplesNumber)
        + std::string("; amount of metadata: ") + std::to_string(metadataNumber)) { }
}
