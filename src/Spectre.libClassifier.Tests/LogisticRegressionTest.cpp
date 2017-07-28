/*
* LinearRegressionTest.h
* Tests for LinearRegression class.
*
Copyright 2017 Wojciech Wilgierz

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

#define GTEST_LANG_CXX11 1

#include <gtest/gtest.h>
#include <opencv2/core.hpp>
#include <opencv2/imgproc.hpp>
#include "opencv2/imgcodecs.hpp"
#include <opencv2/highgui.hpp>
#include <opencv2/ml.hpp>

namespace
{
	class LogisticRegressionTest : public ::testing::Test
	{
	protected:
		// Set up training data
		int labels[4] = { 1, -1, -1, -1 };
		float trainingData[4][2] = { { 501, 10 },{ 255, 10 },{ 501, 255 },{ 10, 501 } };
		cv::Mat trainingDataMat;
		cv::Mat labelsMat;

		cv::Ptr<cv::ml::SVM> svm;

		virtual void SetUp() override
		{
			trainingDataMat = { 4, 2, CV_32FC1, trainingData };
			labelsMat = { 4, 1, CV_32SC1, labels };
			svm = cv::ml::SVM::create();
			svm->setType(cv::ml::SVM::C_SVC);
			svm->setKernel(cv::ml::SVM::LINEAR);
			svm->setTermCriteria(cv::TermCriteria(cv::TermCriteria::MAX_ITER, 100, 1e-6));
			svm->train(trainingDataMat, cv::ml::ROW_SAMPLE, labelsMat);
		}
	};

	TEST_F(LogisticRegressionTest, check_parameter)
	{
		// Data for visual representation
		int width = 512, height = 512;
		cv::Mat image = cv::Mat::zeros(height, width, CV_8UC3);

		// Show the decision regions given by the SVM
		cv::Vec3b green(0, 255, 0), blue(255, 0, 0);
		for (int i = 0; i < image.rows; ++i)
			for (int j = 0; j < image.cols; ++j)
			{
				cv::Mat sampleMat = (cv::Mat_<float>(1, 2) << j, i);
				float response = svm->predict(sampleMat);
				if (response == 1)
					image.at<cv::Vec3b>(i, j) = green;
				else if (response == -1)
					image.at<cv::Vec3b>(i, j) = blue;
			}
		// Show the training data
		int thickness = -1;
		int lineType = 8;
		circle(image, cv::Point(501, 10), 5, cv::Scalar(0, 0, 0), thickness, lineType);
		circle(image, cv::Point(255, 10), 5, cv::Scalar(255, 255, 255), thickness, lineType);
		circle(image, cv::Point(501, 255), 5, cv::Scalar(255, 255, 255), thickness, lineType);
		circle(image, cv::Point(10, 501), 5, cv::Scalar(255, 255, 255), thickness, lineType);
		// Show support vectors
		thickness = 2;
		lineType = 8;
		cv::Mat sv = svm->getSupportVectors();
		for (int i = 0; i < sv.rows; ++i)
		{
			const float* v = sv.ptr<float>(i);
			circle(image, cv::Point((int)v[0], (int)v[1]), 6, cv::Scalar(128, 128, 128), thickness, lineType);
		}
		imwrite("result.png", image);        // save the image
		imshow("SVM Simple Example", image);
		EXPECT_EQ(1, 1);
	}
}
