

#include "Adc.h"
#include <stdlib.h>
#include <avr/io.h>
#include <util/delay.h>
#include <avr/interrupt.h>

Adc::Adc(uint8_t channel) :
_channel { channel }
{}



void Adc::Initialize()
{
	
		ADMUX = (1 << REFS0);	// 5V supply used for ADC reference, select ADC channel 0
		DIDR0 = (1 << ADC0D);	// disable digital input on ADC0 pin
		// enable ADC, start ADC, Enable Interrupt, ADC clock = 16MHz / 128 = 125kHz
		ADCSRA = (1<<ADEN) | (1<<ADSC) | (1<<ADATE) | (1<<ADIE) | (1<<ADPS2) | (1<<ADPS1) | (1<<ADPS0);
		sei();
}

