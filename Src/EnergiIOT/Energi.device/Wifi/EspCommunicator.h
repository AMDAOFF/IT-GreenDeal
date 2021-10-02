
#ifndef __ESPICOMMUNICATOR_H__
#define __ESPICOMMUNICATOR_H__

#include "../Utility/Logging/ErrorLog.h"
#include "../Serialport/Serialport.h"
#include "../Utility/Types.h"
#include "../Utility/Time/Chronos.h"
#include "../Utility/Buffers/RingBuffer.h"
#include "../DigitalPins/DigitalOutput.h"
#include "IDigitalOutput.h"
#include "IWifi.h"

#include <avr/io.h>

class EspCommunicator : public IWifi
{
	public:
	EspCommunicator(Serialport* serial, SerialPorts defaultPort, DigitalOutput* resetPin, Chronos* stopwatch );
	~EspCommunicator() = default;
	
	bool Initialize(wifiSettings_t* settings) final override;
	bool IsPresented() final override;
	uint8_t Start(char* domain, char* port) final override;
	void SendData(serverInfo_t* serverInfo, char data[], uint8_t dataLength) final override;
	void HardwareReset();
	void SoftwareReset();
	void Ping(const char *host) final override;
	uint8_t IsConnected() final override;	

	private:	
	uint8_t WriteCMD(const char* msg);
	uint8_t WriteCMD(const char* msg, unsigned int timeout, const char* tag);
	uint8_t ReadRespone(unsigned int timeout, const char* tag, bool findTags);
	void InitDefaultSettings();
	bool wifiConnect(const char* ssid, const char* password);
	bool NetworkStatus();
	void CheckEspVersion();
	void DisconnectAP();	
	char* GetPort(serverInfo_t* info);

	char _msgBuffer[32];
	Serialport* _serial;
	SerialPorts _defaultPort;
	DigitalOutput* _resetPin;
	Chronos* _chronos;
	uint8_t TagsCount = 5;	
	char* _ssid;
	char* _password;
	bool _espInitialize;	
	bool _isConnected;
};

#endif //__ESPICOMMUNICATOR_H__
