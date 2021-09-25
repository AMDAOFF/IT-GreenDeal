
#ifndef __MQTTPINGTASK_H__
#define __MQTTPINGTASK_H__

#include "IRunnable.h"
#include "IMqttClient.h"
#include "IWifi.h"

class MqttPingTask : public IRunnable
{

	public:
	MqttPingTask(IMqttClient& mqttClient, connectSettings_t& connectSettings);
	~MqttPingTask() = default;
	
	void Service() final override;

	private:
	connectSettings_t& _connectionSettings;
	IMqttClient& _mqttClient;
}; //MqttPingTask

#endif //__MQTTPINGTASK_H__
