

#ifndef __WATCHDOG_H__
#define __WATCHDOG_H__

#include "IWatchdog.h"
#include <avr/wdt.h>

class Watchdog final : public IWatchdog
{

	public:
	Watchdog();
	~Watchdog() = default;
	
	void Enable(WdtTime time) final override;
	void ResetTimer() final override;
	void ResetBoard() final override;
	void Disable() final override;
	
	private:
	bool _resetBoard;
};

#endif
