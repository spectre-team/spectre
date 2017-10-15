#include "SVMFitnessFunction.h"
#include "Spectre.libClassifier/ConfusionMatrix.h"
#include "Spectre.libPlatform/Filter.h"

namespace Spectre::libClassifier
{

SVMFitnessFunction::SVMFitnessFunction(SplittedOpenCvDataset&& data)
    : m_Dataset(std::move(data))
{
    m_SVM = cv::ml::SVM::create();
    m_SVM->setType(cv::ml::SVM::C_SVC);
    m_SVM->setKernel(cv::ml::SVM::LINEAR);
}

libGenetic::ScoreType SVMFitnessFunction::fit(const libGenetic::Individual &individual)
{
    if (m_Dataset.trainingSet.size() != individual.size())
    {
        throw libException::InconsistentArgumentSizesException("data", m_Dataset.trainingSet.size(), "individual", individual.size());
    }
    gsl::span<const Observation> dataToFilter = m_Dataset.trainingSet.GetData();
    std::vector<Observation> twoDimentionalFilteredData = libPlatform::Functional::filter(dataToFilter, individual.getData());
    std::vector<DataType> oneDimentionalFilteredData;
    for (Observation observation: twoDimentionalFilteredData)
    {
        for (float number: observation)
        {
            oneDimentionalFilteredData.push_back(number);
        }
    }
    std::vector<Label> filteredLabels = libPlatform::Functional::filter(m_Dataset.trainingSet.GetSampleMetadata(), individual.getData());
    OpenCvDataset individualDataset(oneDimentionalFilteredData, filteredLabels);
    ConfusionMatrix result = getResultMatrix(std::move(individualDataset));
    return result.getScore();
}

ConfusionMatrix SVMFitnessFunction::getResultMatrix(OpenCvDataset data) const
{
    train(std::move(data));
    ConfusionMatrix goodNr = predict();
    return goodNr;
}

void SVMFitnessFunction::train(OpenCvDataset data) const
{
    m_SVM->train(data.getMatData(), cv::ml::ROW_SAMPLE, data.getMatLabels());
}

ConfusionMatrix SVMFitnessFunction::predict() const
{
    ConfusionMatrix results = ConfusionMatrix();
    for (auto i = 0; i < m_Dataset.testSet.getMatData().rows; i++)
    {
        float prediction = m_SVM->predict(m_Dataset.testSet.getMatData().row(i));
        auto tmp = m_Dataset.testSet.getMatData().row(i).data;
        if (*tmp == 1)
        {
            if (prediction == 1)
            {
                results.true_positive++;
            }
            else
            {
                results.true_negative++;
            }
        }
        else
        {
            if (prediction == 1)
            {
                results.false_positive++;
            }
            else
            {
                results.false_negative++;
            }
        }
    }
    return results;
}

SVMFitnessFunction::~SVMFitnessFunction() {}

}
