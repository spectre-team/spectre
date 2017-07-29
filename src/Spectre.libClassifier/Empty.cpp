#include "Empty.h"
#include "Empty.h"

namespace Spectre::libClassifier {
Empty::Empty()
{

}

const Empty& Empty::instance()
{
	if (m_instance == nullptr)
	{
		m_instance = std::move(std::unique_ptr<Empty>(new Empty()));
	}
	return *m_instance;
}
}