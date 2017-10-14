/*
* Individual.h
* Binary individual class.
*
Copyright 2017 Grzegorz Mrukwa, Wojciech Wilgierz

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

namespace Spectre::libGenetic
{
/// <summary>
/// Binary representation of an individual chromosome in genetic algorithm.
/// </summary>
class Individual
{
public:
    /// <summary>
    /// Initializes a new instance of the <see cref="Individual"/> class.
    /// </summary>
    /// <param name="binaryData">The binary data.</param>
    explicit Individual(std::vector<bool> &&binaryData);
    /// <summary>
    /// Gets the immutable data.
    /// </summary>
    /// <returns>Vector of binary data.</returns>
    std::vector<bool> getData() const;
    /// <summary>
    /// Gets the mutable data under specified index.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <returns>Single bit of data.</returns>
    std::vector<bool>::reference operator[](size_t index);
    /// <summary>
    /// Gets the immutable data under specified index.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <returns>Single bit of data.</returns>
    std::vector<bool>::const_reference operator[](size_t index) const;
    /// <summary>
    /// Return iterator for beginning of mutable sequence.
    /// </summary>
    /// <returns>Iterator for beginning of mutable sequence.</returns>
    std::vector<bool>::iterator begin();
    /// <summary>
    /// Return iterator for after the end of mutable sequence.
    /// </summary>
    /// <returns>Iterator after the end of mutable sequence.</returns>
    std::vector<bool>::iterator end();
    /// <summary>
    /// Return iterator for beginning of immutable sequence.
    /// </summary>
    /// <returns>Iterator for beginning of immutable sequence.</returns>
    std::vector<bool>::const_iterator begin() const;
    /// <summary>
    /// Return iterator after the end of immutable sequence.
    /// </summary>
    /// <returns>Iterator after the end of immutable sequence.</returns>
    std::vector<bool>::const_iterator end() const;
    /// <summary>
    /// Get the size of the data.
    /// </summary>
    /// <returns>Size of the data.</returns>
    size_t size() const;
    /// <summary>
    /// Compares object with other
    /// </summary>
    /// <param name="other">The other.</param>
    /// <returns>True, if binary data inside is the same.</returns>
    bool operator==(const Individual &other) const;
    /// <summary>
    /// Compares object with other
    /// </summary>
    /// <param name="other">The other.</param>
    /// <returns>False, if binary data inside is the same.</returns>
    bool Individual::operator!=(const Individual &other) const;
    /// <summary>
    /// Overwrites individual with another object data.
    /// </summary>
    /// <param name="other">The other.</param>
    /// <returns>This instance.</returns>
    Individual& Individual::operator=(const Individual &other);
    virtual ~Individual() = default;

private:
    /// <summary>
    /// The binary representation of chromosome.
    /// </summary>
    std::vector<bool> m_BinaryData;
};
}
