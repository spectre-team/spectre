﻿#pragma once

public class IClassifier
{
public:
	IClassifier();
	~IClassifier();
	virtual long getScore(Individual individual);

private:

};