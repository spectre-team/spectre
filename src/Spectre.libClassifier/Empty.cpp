#include "Empty.h"

namespace Spectre::libClassifier {
Empty Empty::m_instance;

Empty::Empty()
{

}

const Empty& Empty::instance()
{
	return m_instance;
}
}