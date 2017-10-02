#include "SVM.h"

SVM::SVM()
{
    mSVM = cv::ml::SVM::create();
    mSVM->setType(cv::ml::SVM::C_SVC);
    mSVM->setKernel(cv::ml::SVM::LINEAR);
}

long SVM::getResult(Spectre::libClassifier::SplittedOpenCvDataset&& data)
{
    train(std::move(data.trainingSet));
    long goodNr = predict(std::move(data.testSet));
    return goodNr;
}

void SVM::train(Spectre::libClassifier::OpenCvDataset trainingSet) const
{
    mSVM->train(trainingSet.getMatData(), cv::ml::ROW_SAMPLE, trainingSet.getMatLabels());
}

long SVM::predict(Spectre::libClassifier::OpenCvDataset testSet) const
{
    long goodNr = 0;
    cv::Mat results;
    mSVM->predict(testSet.getMatData(), results);

    for(auto i = 0; i < testSet.size(); i++)
    {
        if (testSet.GetSampleMetadata(i) == results.data[i])
        {
            goodNr++;
        }
    }
    return goodNr;
}
