
#ifndef __IWATCHDOG_H__
#define __IWATCHDOG_H__

#include <avr/wdt.h>

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

class IWatchdog
{

public:
	virtual ~IWatchdog() = default;
	
	virtual void Enable(WdtTime time) = 0;
	virtual void ResetTimer() = 0;
	virtual void ResetBoard() = 0;
	virtual void Disable() = 0;
}; 

#endif 
