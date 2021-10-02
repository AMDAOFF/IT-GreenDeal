

#ifndef __WATCHDOGTASK_H__
#define __WATCHDOGTASK_H__

#include "IRunnable.h"
#include "IWatchdog.h"
#include "../../Boards/GreenDealDevice/Board.h"

class WatchdogTask : public IRunnable
{

	public:
	WatchdogTask(IWatchdog& watchdog);
	~WatchdogTask() = default;

	void Service() final override;

	private:
	IWatchdog& _watchdog;

};

#endif
