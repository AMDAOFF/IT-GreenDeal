
#include "PCF8574.h"

PCF8574::PCF8574(II2C* i2c, uint8_t address) :
_i2c { i2c },
_address{ address },
_ledMask { 0 }
{}

void PCF8574::SetLedState (uint8_t ledNumber, bool state)
{
	if (state)
	{
		_ledMask |= ledNumber;
	}
	else
	{
		_ledMask &= ~(ledNumber);
	}
	
	_i2c->WriteRegister(&_address, &_ledMask);
}

void PCF8574::ToggleLed(uint8_t ledNumber)
{
	_ledMask ^= ledNumber;
	
	_i2c->WriteRegister(&_address, &_ledMask);
}