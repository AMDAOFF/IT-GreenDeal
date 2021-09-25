

#include "MqttPingTask.h"

MqttPingTask::MqttPingTask(IMqttClient& mqttClient, connectSettings_t& connectSettings ) :
_mqttClient { mqttClient },
_connectionSettings { connectSettings }
{
} //MqttPingTask


void MqttPingTask::Service()
{
	_mqttClient.PingReq(&_connectionSettings);
	return;
}