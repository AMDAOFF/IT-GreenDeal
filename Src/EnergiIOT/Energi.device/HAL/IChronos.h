

#ifndef __ICHRONOS_H__
#define __ICHRONOS_H__

#include <avr/io.h>

class IChronos
{
	public:
	virtual ~IChronos() = default;
	
	virtual unsigned long Time() = 0;
	virtual void ResetTimer() = 0;
	virtual void Delay(unsigned long timeout) = 0;
};

#endif
