
#ifndef __DIGITALOUTPUT_H__
#define __DIGITALOUTPUT_H__

#include "IDigitalOutput.h"
#include "avr/io.h"

class DigitalOutput : public IDigitalOutput
{
	public:
	DigitalOutput(volatile uint8_t* port, uint8_t pin);
	~DigitalOutput() = default;
	
	void SetValue(LogicalState state) final override;
	LogicalState GetValue() final override;
	void ChangePinDirection(LogicalDirection direction) final override;
	
	private:
	DigitalOutput( const DigitalOutput &c );
	DigitalOutput& operator=( const DigitalOutput &c );

	volatile uint8_t* _port;
	uint8_t _pin;
	LogicalState _currentState;
};

#endif
