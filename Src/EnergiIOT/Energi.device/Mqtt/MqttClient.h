
#ifndef __MQTTCLIENT_H__
#define __MQTTCLIENT_H__

#include "IMqttClient.h"
#include "IWifi.h"
#include "IChronos.h"
#include <avr/io.h>
#include <stdbool.h>

class MqttClient : public IMqttClient
{
	public:
	MqttClient(IWifi* wifiClient, IChronos* chronos);
	~MqttClient() = default;
	
	void Connect(connectSettings_t* settings) final override;
	void PingReq(connectSettings_t* settings) final override;
	void Publish(publishMessage_t* message) final override;
	void Subscribe(subscribeTopic_t* topic) final override;
	bool IsConnected() final override;
	
	private:
	MqttClient( const MqttClient &c );
	MqttClient& operator=( const MqttClient &c );

	IWifi* _wifiClient;
	IChronos* _chronos;
	char messageBuffer[80];
	char MQTT_VERSION_3_1_1 = 0x04;
	bool _connectStatus;
	uint8_t _pakatID = 1; // This can only hold 256 IDs before it rolls over. This results in not all message IDs will be unique!

	void (*_callback)();
};

#endif