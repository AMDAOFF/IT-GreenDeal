


#ifndef APPLICATIONSETTINGS_H_
#define APPLICATIONSETTINGS_H_

#include "IMqttClient.h"
#include "IWifi.h"

// Wifi.
//wifiSettings_t wifiSettings
//{
//
//};

// Mqtt.
ServerInfo_t serverInfo
{
	//"192.168.87.120", // Home
	"192.168.137.1", // EUC
	TCP,
};

connectSettings_t connectSettings =
{
	"JKClient",
	"guest",
	"guest",
	QoS0,
	&serverInfo
};

publishMessage_t publishMessage =
{
	"#Online#",
	"device/update/1",
	&connectSettings
};


subscribeTopic_t subscribeTopic {
	"device/settings/1",
	&connectSettings
};

#endif /* APPLICATIONSETTINGS_H_ */