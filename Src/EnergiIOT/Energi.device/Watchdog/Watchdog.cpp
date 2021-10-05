
#include "Watchdog.h"

Watchdog::Watchdog()
{
	_resetBoard = false;
}

void Watchdog::Enable(WdtTime time)
{
	wdt_enable(time);
	return;
}

void Watchdog::ResetTimer()
{
	if (_resetBoard)
	{
		return;
	}
	wdt_reset();
	return;
}

void Watchdog::ResetBoard()
{
	_resetBoard = true;
}

void Watchdog::Disable()
{
	wdt_disable();
	return;
}