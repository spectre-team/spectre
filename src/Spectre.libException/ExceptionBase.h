/*
* ExceptionBase.h
* Common exception base for all native libraries.
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

#pragma once

#include <string>

namespace Spectre::libException
{
/// <summary>
/// Common base for all our exceptions, which should be further migrated
/// and enhanced.
/// </summary>
class ExceptionBase : public std::exception
{
public:
    /// <summary>
    /// Initializes a new instance of the <see cref="ExceptionBase"/> class.
    /// </summary>
    /// <param name="">The error message.</param>
    explicit ExceptionBase(const std::string);
    /// <summary>
    /// Returns error message.
    /// </summary>
    /// <returns>Error message</returns>
    char const* what() const override;
private:
    const std::string m_message;
};
}
