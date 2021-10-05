
#ifndef __IDIGITALOUTPUT_H__
#define __IDIGITALOUTPUT_H__

#include "../Utility/Types.h"

/**
* @brief Interface abstraction for a GPIO output pin.
*/
class IDigitalOutput
{
	public:
	virtual ~IDigitalOutput() = default;
	/**
	* @brief Sets the GPIO output pin to either active or inactive.
	* @param state - set to active or inactive state.
	*/
	virtual void SetValue(LogicalState state) = 0;
	
	/**
	* @brief this gets the GPIO pin state.
	* @return Current current state.
	*/
	virtual LogicalState GetValue() = 0;
	
	/**
	* @brief Toggles the current direction of the GPIO pin.
	*/
	virtual void ChangePinDirection(LogicalDirection direction) = 0;
};

#endif //__IDIGITALOUTPUT_H__
