
#ifndef __IDIGITALOUTPUT_H__
#define __IDIGITALOUTPUT_H__

#include "../Utility/Types.h"

class IDigitalOutput
{
	public:
	virtual ~IDigitalOutput() = default;
	virtual void SetValue(LogicalState state) = 0;
	virtual LogicalState GetValue() = 0;
	virtual void ChangePinDirection(LogicalDirection direction) = 0;
};

#endif //__IDIGITALOUTPUT_H__
