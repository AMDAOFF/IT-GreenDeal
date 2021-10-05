
#include "Chronos.h"
#include <avr/io.h>
#include <util/atomic.h>
#include <avr/interrupt.h>

//NOTE: A unsigned long holds values from 0 to 4,294,967,295 (2^32 - 1). It will roll over to 0 after reaching its maximum value.
volatile unsigned long _millisCount;


ISR(TIMER1_COMPA_vect)
{
	_millisCount++;
}


Chronos::Chronos(unsigned long f_cpu)
{
	unsigned long ctc_overflow;
	
	ctc_overflow = ((f_cpu / 1000) / 8); //when timer1 is this value, 1ms has passed.
	
	// Clear when matching ctc_overflow and set Clock divisor to 8.
	TCCR1B |= (1 << WGM12) | (1 << CS11);
	
	// Set high byte first, then low.
	OCR1AH = (ctc_overflow >> 8);
	OCR1AL = ctc_overflow;
	
	// Enable the compare match interrupt.
	TIMSK1 |= (1 << OCIE1A);
	
	sei();
}


unsigned long Chronos::Time()
{
	unsigned long millis_return;
	
	// Ensure this cannot be disrupted
	ATOMIC_BLOCK(ATOMIC_FORCEON) {
		millis_return = _millisCount;
	}
	return millis_return;
}

void Chronos::ResetTimer()
{
	_millisCount = 0;
}

void Chronos::Delay(unsigned long timeout)
{
	unsigned long start = Time();
	
	while (Time() - start < timeout){}
}
