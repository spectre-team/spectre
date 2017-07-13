/*
* Exception.cpp
* Definition of local exceptions related to Dataseet.
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

#include "Exception.h"

namespace Spectre::libDataset
{
ExceptionBase::ExceptionBase(const std::string message): m_message(message) { }

char const* ExceptionBase::what() const
{
    return m_message.c_str();
}

OutOfRange::OutOfRange(size_t index, size_t size)
    : ExceptionBase(std::string("accessed index: ") + std::to_string(index)
                    + std::string("; size: ") + std::to_string(size))
{
    
}

InconsistentInputSize::InconsistentInputSize(size_t samplesNumber, size_t metadataNumber)
    : ExceptionBase(std::string("number of samples: ") + std::to_string(samplesNumber)
                    + std::string("; amount of metadata: ") + std::to_string(metadataNumber))
{
    
}

}