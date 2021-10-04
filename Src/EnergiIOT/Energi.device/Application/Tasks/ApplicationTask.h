
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
	ApplicationTask(Board& board, publishMessage_t& publishMessage, connectSettings_t& connectSettings, subscribeTopic_t& subscribeTopic);
	~ApplicationTask() = default;

	void Service() final override;
	void SetConfig();
		
	private:
	Board& _board;
	publishMessage_t& _publishMessage;
	connectSettings_t& _connectionSettings;
	subscribeTopic_t& _subscribeTopic;
	//bool _gotConfig;
}; 

#endif
