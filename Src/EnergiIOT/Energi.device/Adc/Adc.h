
#ifndef __ADC_H__
#define __ADC_H__

#include <avr/io.h>
#include "IAdc.h"

class Adc : public IAdc
{

public:
	Adc(uint8_t channel);
	~Adc() = default;
	
	void Initialize() final override;

private:
uint8_t _channel;

};

#endif //__ADC_H__
