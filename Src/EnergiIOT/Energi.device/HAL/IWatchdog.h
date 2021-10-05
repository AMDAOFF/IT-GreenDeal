
#ifndef __IWATCHDOG_H__
#define __IWATCHDOG_H__

#include <avr/wdt.h>

// This needs to be moved to the board
typedef enum WdtTime
{
	MS15 = WDTO_15MS,
	MS30 = WDTO_30MS,
	MS60 = WDTO_60MS,
	MS120 = WDTO_120MS,
	MS250 = WDTO_250MS,
	MS500 = WDTO_500MS,
	S1 = WDTO_1S,
	S2 = WDTO_2S,
	S4 = WDTO_4S,
	S8 = WDTO_8S
};

/**
* @brief Provides an interface to kick the watchdog.
*/
class IWatchdog
{

	public:
	virtual ~IWatchdog() = default;
	
	/**
	* @brief Enables the watchdog.
	*/
	virtual void Enable(WdtTime time) = 0;
	
	/**
	* @brief Resdets the watchdog. This must be done regulary to avoid timeout and system reset.
	*/
	virtual void ResetTimer() = 0;
	
	/**
	* @brief Force a software reset
	*/
	virtual void ResetBoard() = 0;
	
	/**
	* @brief Disables the watchdog.
	*/
	virtual void Disable() = 0;
};

#endif
