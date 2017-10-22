/*
* Generation.h
* Collection of Individuals.
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
#include "Individual.h"

namespace Spectre::libGenetic
{
/// <summary>
/// Container enclosing population of individuals at time instance of single generation.
/// </summary>
class Generation
{
public:
    /// <summary>
    /// Initializes a new instance of the <see cref="Generation"/> class.
    /// </summary>
    /// <param name="generation">The container with generation.</param>
    explicit Generation(std::vector<Individual> &&generation);
    /// <summary>
    /// Creates generation from parameters.
    /// </summary>
    /// <param name="size">Size of generation.</param>
    /// <param name="individualSize">Size of every individual in generation.</param>
    /// <param name="trueAmount">Amount of true values in every individual.</param>
    Generation::Generation(_int64 size, _int64 individualSize, _int64 trueAmount);
    /// <summary>
    /// Concatenates populations.
    /// </summary>
    /// <param name="other">The other population.</param>
    /// <returns>First population and second just after.</returns>
    Generation operator+(const Generation &other) const;
    /// <summary>
    /// Overwrites population with concatenated one.
    /// </summary>
    /// <param name="other">The other population.</param>
    /// <returns>Self.</returns>
    Generation& operator+=(const Generation &other);
    /// <summary>
    /// Return immutable individual under the specified index.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <returns>A read-only individual.</returns>
    const Individual& operator[](size_t index) const;
    /// <summary>
    /// Return mutable individual under the specified index.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <returns>A writable individual.</returns>
    Individual& operator[](size_t index);
    /// <summary>
    /// Get the size of the data.
    /// </summary>
    /// <returns>Size of the data.</returns>
    size_t size() const noexcept;
    /// <summary>
    /// Return iterator for beginning of immutable sequence.
    /// </summary>
    /// <returns>Iterator for beginning of immutable sequence.</returns>
    std::vector<Individual>::const_iterator begin() const;
    /// <summary>
    /// Return iterator after the end of immutable sequence.
    /// </summary>
    /// <returns>Iterator after the end of immutable sequence.</returns>
    std::vector<Individual>::const_iterator end() const;
    virtual ~Generation() = default;

private:
    /// <summary>
    /// Container for individuals.
    /// </summary>
    std::vector<Individual> m_Generation;
};
}
