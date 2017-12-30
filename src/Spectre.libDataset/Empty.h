/*
* Empty.h
* Empty class for temporary substitute of empty metadata.
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

namespace Spectre::libDataset
{
/// <summary>
/// Empty class for temporary substitute of empty metadata.
/// </summary>
class Empty final
{
public:
    /// <summary>
    /// Get single instance of this class.
    /// </summary>
    /// <returns></returns>
    static const Empty& instance();
    /// <summary>
    /// Prevents copying the instance of the <see cref="Empty"/> class.
    /// </summary>
    /// <param name="">An instance.</param>
    Empty(const Empty &) = delete;
    /// <summary>
    /// Prevents moving the instance of the <see cref="Empty"/> class.
    /// </summary>
    /// <param name="">An instance.</param>
    Empty(Empty &&) = delete;
private:
    static Empty m_instance;
    Empty();
};
}
