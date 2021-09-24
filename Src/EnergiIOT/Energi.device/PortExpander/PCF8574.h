

#ifndef __PCF8574_H__
#define __PCF8574_H__


#include <avr/io.h>
#include <avr/sfr_defs.h>

#include "ILedController.h"
#include "II2C.h"

class PCF8574 final : public ILedController
{
	public:
	PCF8574(II2C* i2c, uint8_t address);
	~PCF8574();

	void SetLedState (uint8_t ledNumber, bool state) final override;
	void ToggleLed(uint8_t ledNumber) final override;
	void ClearAll() final override;
	
	private:
	PCF8574( const PCF8574 &c );
	PCF8574& operator=( const PCF8574 &c );
	
	II2C* _i2c;
	
	uint8_t _address;
	uint8_t _ledMask;
};

#endif //__PCF8574_H__
