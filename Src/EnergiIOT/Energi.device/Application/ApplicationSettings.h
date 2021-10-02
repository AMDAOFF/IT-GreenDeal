


#ifndef APPLICATIONSETTINGS_H_
#define APPLICATIONSETTINGS_H_

#include "IMqttClient.h"
#include "IWifi.h"

// Wifi.
wifiSettings_t wifiSettings
{
	"JK",
	"472yO58;"
};

// Mqtt.
serverInfo_t serverInfo
{
	//"192.168.87.120", // Home
	"192.168.137.1", // EUC
	TCP,
};

connectSettings_t connectSettings =
{	
	"JKClient2",
	"guest",
	"guest",
	QoS0,
	&serverInfo
};

publishMessage_t publishMessage =
{
	"#Online#",
	"device/update/2",
	&connectSettings
};


subscribeTopic_t subscribeTopic {
	"device/settings/2",
	&connectSettings
};

#endif /* APPLICATIONSETTINGS_H_ */