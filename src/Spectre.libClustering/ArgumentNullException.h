#pragma once

#include <stdexcept>
#include <string>

class ArgumentNullException : public std::invalid_argument
{
public:

	ArgumentNullException(const char* argumentName) : std::invalid_argument("Argument Null Exception."),
		argumentName(argumentName)
	{
	}

	virtual const char* what() const throw()
	{
		std::string message = "ArgumentNullException has been thrown. Argument name: " + argumentName;
		return message.c_str();
	}

private:
	std::string argumentName;
};