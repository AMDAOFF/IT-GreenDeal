
#include "IRunnable.h"
#include "IMqttClient.h"
#include "IWifi.h"
#include "avr/interrupt.h"
#include "../../Boards/GreenDealDevice/Board.h"

#ifndef __APPLICATIONTASK_H__
#define __APPLICATIONTASK_H__


class ApplicationTask : public IRunnable
{

public:
	ApplicationTask(Board& board);
	~ApplicationTask() = default;

	void Service() final override;	
	private:
	Board& _board;

}; //ApplicationTask

#endif //__APPLICATIONTASK_H__
