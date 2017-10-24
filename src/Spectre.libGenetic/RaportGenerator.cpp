#include "RaportGenerator.h"
#include <string>
#include "Spectre.libClassifier/ConfusionMatrix.h"

RaportGenerator::RaportGenerator(std::basic_string<char> filename)
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

void RaportGenerator::write(Spectre::libClassifier::ConfusionMatrix matrix)
{
    std::basic_string<char> s = "true_positive," + std::to_string(matrix.TruePositive) + ",true_negative," + std::to_string(matrix.TrueNegative) +
        ",false_positive," + std::to_string(matrix.FalsePositive) + ",false_negative," + std::to_string(matrix.FalseNegative);
    mFile << s + "\n";
}

void RaportGenerator::write(Spectre::libClassifier::OpenCvDataset* dataset)
{
    std::basic_string<char> s;
    for (auto i = 0; i < dataset->size(); i++)
    {
        for (Spectre::libClassifier::DataType data : (*dataset)[i])
        {
            s.append(std::to_string(data) + ",");
        }
        s.append("\n");
    }
    s.append("\n\n");
    mFile << s;
}
