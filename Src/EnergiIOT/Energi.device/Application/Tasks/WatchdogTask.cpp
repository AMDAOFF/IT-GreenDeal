

#include "WatchdogTask.h"


WatchdogTask::WatchdogTask(IWatchdog& watchdog) :
_watchdog { watchdog }
{}

void WatchdogTask::Service()
{
	_watchdog.ResetTimer();
}