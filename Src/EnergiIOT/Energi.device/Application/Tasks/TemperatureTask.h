

#ifndef __TEMPERATURETASK_H__
#define __TEMPERATURETASK_H__

#include "IRunnable.h"
#include "../../Boards/GreenDealDevice/Board.h"

class TemperatureTask : public IRunnable
{

	public:
	TemperatureTask(Board& board, publishMessage_t& publishMessage);

	~TemperatureTask() = default;

	void Service() final override;
	private:
	Board& _board;
	publishMessage_t& _publishMessage;
};

#endif
