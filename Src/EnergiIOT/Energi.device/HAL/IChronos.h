

#ifndef __ICHRONOS_H__
#define __ICHRONOS_H__

#include <avr/io.h>

/**
* @brief Interface for chronos, also called stopwatch.
* It defines time control functionality.
* This has nothing to do with a real time clock
*/
class IChronos
{
	public:
	virtual ~IChronos() = default;
	
	/**
	* @brief Call this to get current time in ms.
	* @return Current value of time.
	*/
	virtual unsigned long Time() = 0;
	
	/**
	* @brief Reset the time counter to 0.
	*/
	virtual void ResetTimer() = 0;
	
	/**
	* @brief This can make a delay and block code execution, for a given time.
	* @param timeout is the time in ms.
	*/
	virtual void Delay(unsigned long timeout) = 0;
};

#endif
