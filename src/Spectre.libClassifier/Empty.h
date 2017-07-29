#pragma once
#include <memory>

namespace Spectre::libClassifier {

class Empty
{
public:
	static const Empty& instance();
private:
	static std::unique_ptr<Empty> m_instance;
	Empty();
};
}
