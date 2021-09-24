
#include "Chronos.h"

#include <avr/io.h>
#include <util/atomic.h>
#include <avr/interrupt.h>

//NOTE: A unsigned long holds values from 0 to 4,294,967,295 (2^32 - 1). It will roll over to 0 after reaching its maximum value.
volatile unsigned long _timer0_millis;

ISR(TIMER0_COMPA_vect)
{
	_timer0_millis++;
}

Chronos::Chronos(unsigned long f_cpu)
{
	TCNT0 = 0;
	// CTC mode.
	TCCR0A |= (1<<WGM01);
	// Prescaler is set to 64.
	TCCR0B |= (1<<CS01) | (1<<CS00);
	// Timer0 Overflow Interrupt Enable
	TIMSK0 |= (1<<OCIE0A);
	OCR0A = 249;
	sei();		
}


unsigned long Chronos::Time()
{
	unsigned long millis_return;
	
	// Ensure this cannot be disrupted
	ATOMIC_BLOCK(ATOMIC_FORCEON) {
		millis_return = _timer0_millis;
	}
	return millis_return;
}

void Chronos::ResetTimer()
{
	_timer0_millis = 0;
}

void Chronos::Delay(unsigned int timeout)
{
	unsigned long start = Time();
	
	while (Time() - start < timeout){}
}