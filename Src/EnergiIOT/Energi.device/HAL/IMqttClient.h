
#ifndef __IMQTTCLIENT_H__
#define __IMQTTCLIENT_H__

#include "IWifi.h"
#include <avr/io.h>
#include <stdbool.h>

typedef enum QoS
{
	QoS0 = 0,
	QoS1 = 1,
	QoS2 = 2
};

typedef struct connectSettings_t
{
	char* userId;
	char* userName;
	char* userPassword;
	QoS qoS;
	serverInfo_t* serverInfo;
} connectSettings_t;

typedef struct publishMessage_t
{
	char* message;
	char* topic;
	connectSettings_t* connectionSettings;
} publishMessage_t;

typedef struct subscribeTopic_t {
	char* topic;
	connectSettings_t* connectionSettings;
} subscribeTopic_t;

class IMqttClient
{
	public:
	virtual ~IMqttClient() = default;
	virtual void Connect(connectSettings_t* settings) = 0;
	virtual void PingReq(connectSettings_t* settings) = 0;
	virtual void Publish(publishMessage_t* message) = 0;
	virtual void Subscribe(subscribeTopic_t * topic) = 0;
	virtual bool IsConnected() = 0;
};

#endif
