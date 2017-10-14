#include "SVM.h"
#include "Spectre.libClassifier/PredictionResultsMatrix.h"

SVM::SVM()
{
    mSVM = cv::ml::SVM::create();
    mSVM->setType(cv::ml::SVM::C_SVC);
    mSVM->setKernel(cv::ml::SVM::LINEAR);
}

Spectre::libClassifier::PredictionResultsMatrix SVM::getResult(Spectre::libClassifier::SplittedOpenCvDataset&& data) const
{
    train(std::move(data.trainingSet));
    Spectre::libClassifier::PredictionResultsMatrix goodNr = predict(std::move(data.testSet));
    return goodNr;
}

void SVM::train(Spectre::libClassifier::OpenCvDataset trainingSet) const
{
    mSVM->train(trainingSet.getMatData(), cv::ml::ROW_SAMPLE, trainingSet.getMatLabels());
}

Spectre::libClassifier::PredictionResultsMatrix SVM::predict(Spectre::libClassifier::OpenCvDataset testSet) const
{
    Spectre::libClassifier::PredictionResultsMatrix results = Spectre::libClassifier::PredictionResultsMatrix();
    for(auto i = 0; i < testSet.getMatData().rows; i++)
    {
        float prediction = mSVM->predict(testSet.getMatData().row(i));
        auto tmp = testSet.getMatData().row(i).data;
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
