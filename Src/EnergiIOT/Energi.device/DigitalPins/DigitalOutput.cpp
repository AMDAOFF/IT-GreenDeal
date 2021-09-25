

#include "DigitalOutput.h"
#include "avr/sfr_defs.h"

DigitalOutput::DigitalOutput(volatile uint8_t* port, uint8_t pin) :
_port{ port },
_pin { pin }
{
	// Set DDRx. DDRx on ATmega328p is one address below the port address.
	*(_port -1) |= _pin;
}

void DigitalOutput::SetValue(LogicalState state)
{
	if (state == LogicalState::Active)
	{
		*_port |= _pin;
	}
	else
	{
		*_port &= ~_pin;
	}
	
	_currentState = state;
}

LogicalState DigitalOutput::GetValue()
{
	return _currentState;
}

void DigitalOutput::ChangePinDirection(LogicalDirection direction)
{
	if (direction == LogicalDirection::Output)
	{
		// Set DDRx. DDRx on ATmega328p is one address below the port address.
		*(_port -1) |= _pin;
	}
	else
	{
		*(_port -1) &= ~_pin;
	}
}