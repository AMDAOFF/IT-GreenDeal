


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
	"192.168.137.1",
	TCP,
};

// Connection settings.
connectSettings_t connectSettings =
{
	"JKClient1",
	"guest",
	"guest",
	QoS0,
	&serverInfo
};

// Publich settings.
publishMessage_t publishMessage =
{
	"#Online#",
	"device/update/1",
	&connectSettings
};

// Subscibe settings.
subscribeTopic_t subscribeTopic {
	"device/settings/1",
	&connectSettings
};

#endif