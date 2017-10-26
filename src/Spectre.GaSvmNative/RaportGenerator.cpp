/*
* RaportGenerator.cpp
* Class that has static functions to create raport during for example genetic algorithm execution.
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

#include <string>
#include "Spectre.libClassifier/ConfusionMatrix.h"
#include "RaportGenerator.h"

namespace Spectre::GaSvmNative
{
RaportGenerator::RaportGenerator(std::string filename)
{
    mFile.open(filename + ".csv");
}

void RaportGenerator::close()
{
    if (!mFile) return;
    mFile.close();
}

void RaportGenerator::write(std::string text)
{
    std::basic_string<char> s = text;
    mFile << s + "\n";
}

void RaportGenerator::write(libClassifier::ConfusionMatrix matrix)
{
    std::basic_string<char> s = "true_positive," + std::to_string(matrix.TruePositive) + ",true_negative," + std::to_string(matrix.TrueNegative) +
        ",false_positive," + std::to_string(matrix.FalsePositive) + ",false_negative," + std::to_string(matrix.FalseNegative);
    mFile << s + "\n";
}

void RaportGenerator::write(libClassifier::OpenCvDataset* dataset)
{
    std::basic_string<char> s;
    for (auto i = 0; i < dataset->size(); i++)
    {
        for (libClassifier::DataType data : (*dataset)[i])
        {
            s.append(std::to_string(data) + ",");
        }
        s.append("\n");
    }
    s.append("\n\n");
    mFile << s;
}
}