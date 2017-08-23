#pragma once
#include <memory>

namespace Spectre::libClassifier {

class Empty final
{
public:
	static const Empty& instance();
    Empty(const Empty&) = delete;
    Empty(Empty&&) = delete;
private:
	static Empty m_instance;
	Empty();
};
}
