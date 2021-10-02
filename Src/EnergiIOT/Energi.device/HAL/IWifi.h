

#ifndef __IWIFI_H__
#define __IWIFI_H__

#include <avr/io.h>

typedef enum Port
{
	TCP = 0,
	TCPssl = 1
};

typedef struct wifiSettings_t
{
	char* ssid;
	char* password;
} wifiSettings_t;

typedef struct serverInfo_t
{
	char* serverIp;
	Port port;

} serverInfo_t;

class IWifi
{
	public:
	virtual ~IWifi() = default;
	
	virtual bool Initialize(wifiSettings_t* settings) = 0;
	virtual bool IsPresented() = 0;
	virtual uint8_t Start(char* domain, char* port) = 0;
	virtual void SendData(serverInfo_t* serverInfo, char data[], uint8_t dataLength) = 0;
	virtual void Ping(const char *host) = 0;
	virtual uint8_t IsConnected() = 0;
};

#endif
