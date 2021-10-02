

#include "EspCommunicator.h"
#include <stdint.h>
#include <stdio.h>
#include <avr/io.h>

const char* EspTags[] =
{
	"\r\nOK\r\n",
	"\r\nERROR\r\n",
	"\r\nFAIL\r\n",
	"\r\nSEND OK\r\n",
	" CONNECT\r\n"
};

EspCommunicator::EspCommunicator(Serialport* serial, SerialPorts defaultPort, DigitalOutput* resetPin, Chronos* stopwatch) :
_serial{ serial },
_defaultPort { defaultPort },
_resetPin { resetPin },
_chronos { stopwatch },
_isConnected { false }
{
	_resetPin->ChangePinDirection(LogicalDirection::Input);
}

bool EspCommunicator::Initialize(wifiSettings_t* settings)
{
	if (!IsPresented())
	{
		Logger.LogError(">>> ESP ERROR: ESP not presented <<<");
		return false;
	}
	
	// Save network info.
	_ssid = settings->ssid;
	_password = settings->password;
	
	// Reset esp
	HardwareReset();
	SoftwareReset();

	// Set default esp settings.
	InitDefaultSettings();
	
	// Move this too board or application!
	wifiConnect(_ssid, _password);
	
	_chronos->Delay(50);
	
	return true;
}

uint8_t EspCommunicator::WriteCMD(const char msg[])
{
	return WriteCMD(msg, 500, "\r\nOK\r\n");
}

uint8_t EspCommunicator::WriteCMD(const char* msg, unsigned int timeout, const char* tag)
{
	_serial->DisableInterrupt();
	_serial->WriteLine(msg, _defaultPort);
	uint8_t result = ReadRespone(timeout, tag, true);

	if (result == -1)
	{
		Logger.LogError(">>> COMMAND  ERROR <<<");
	}
	
	// We need, for some reason a little delay, before we can go on with the ESP....
	_chronos->Delay(3);
	_serial->EnableInterrupt();
	return result;
}

uint8_t EspCommunicator::ReadRespone(unsigned int timeout, const char* tag, bool findTags = false)
{
	uint8_t result = -1;
	
	result = _serial->ReadTagWithTimeout(_defaultPort, _msgBuffer, tag, timeout);
	
	if (DebugShowEspCOmmands)
	{
		Logger.Log(_msgBuffer);
	}

	if(findTags)
	{
		for(int i=0; i<TagsCount; i++)
		{
			if (strstr(_msgBuffer, EspTags[i]) != NULL)
			{
				result = i;
				break;
			}
		}
	}

	return result;
}

bool EspCommunicator::IsPresented()
{
	if (WriteCMD("AT") == 0)
	{
		return true;
	}
	
	return false;
}

bool EspCommunicator::wifiConnect(const char* ssid, const char* password)
{
	// TODO: Escape character is needed if "SSID" or "password" contains special characters.

	// CREATE COMMAND.
	char str[80];
	memset(str, 0, 80);
	strcpy(str, "AT+CWJAP=\"");
	strcat(str, ssid);
	strcat(str, "\",\"");
	strcat(str, password);
	strcat(str, "\"");

	WriteCMD(str, 5000, "\r\nOK\r\n");	

	return true;
}

void EspCommunicator::InitDefaultSettings()
{
	// Disable echo of commands.
	WriteCMD("ATE0"); // 0 = Disable echo (Don't send back received command.)
	
	// Set station mode.
	WriteCMD("AT+CWMODE=1"); // 1 = Station mode (client.)

	// Set multiple connections mode
	WriteCMD("AT+CIPMUX=1"); //1 = Multiple connections. (MAX 4)
	

	// Show remote IP and port with "+IPD"
	WriteCMD("AT+CIPDINFO=0"); // 1 = Show the remote host and port in “+IPD” and “+CIPRECVDATA” messages. We don't need this.
	
	// Disable autoconnect
	// Automatic connection can create problems during initialization phase at next boot.
	WriteCMD("AT+CWAUTOCONN=0"); // 0 = do not auto-connect to AP when power on.

	// Enable DHCP
	WriteCMD("AT+CWDHCP=1,1");
	
	return;
}

uint8_t EspCommunicator::Start(char* domain, char* port)
{
	// CREATE COMMAND.
	char str[80];
	memset(str, 0, 80);
	strcpy(str, "AT+CIPSTART=3,\"TCP\",\""); // 3 is a fixed number, we need to control free sockets in software.
	strcat(str, domain);
	strcat(str, "\",");
	strcat(str, port);

	return WriteCMD(str, 5000, "\r\nOK\r\n");
}

void EspCommunicator::SendData(serverInfo_t* serverInfo, char data[], uint8_t dataLength)
{
	// Make the ESP ready for transmit.
	const uint8_t bufferSize = 16;
	char str[bufferSize];
	memset(str, 0, bufferSize);
	strcpy(str, "AT+CIPSEND=3,"); // 3 is a fixed number, we need to control free TCP sockets in software.
	
	// Convert int to array.
	char array[3];
	array[2] = 0x00;
	sprintf(array, "%d", dataLength);
	
	str[bufferSize - 3] = array[0]; 
	str[bufferSize - 2] = array[1]; 
	str[bufferSize - 1] = 0x00;

	// Start connection.
	if (_isConnected == false)
	{
		Start(serverInfo->serverIp, GetPort(serverInfo));
		_isConnected = true;
	}
	
	if(WriteCMD(str, 500, "\r\nOK\r\n> "))
	{
		return;
	}
	
	// Transmit data.
	_serial->Write(data, dataLength + 1, _defaultPort);
	_serial->ReadTagWithTimeout(_defaultPort, "","\r\nSEND OK\r\n", 1000);
	_chronos->Delay(50);
	return;
}

bool EspCommunicator::NetworkStatus()
{
	if (!(WriteCMD("AT+CIFSR") == 0))
	{
		_isConnected = false;
		Logger.LogError(">>> ESP network status ERROR <<<");
		return false;
	}
	
	_isConnected = true;
	return true;
}

void EspCommunicator::SoftwareReset()
{
	WriteCMD("AT+RST", 600, "\r\nOK\r\n");
	_chronos->Delay(500);
	return;
}

void EspCommunicator::HardwareReset()
{
	_resetPin->ChangePinDirection(LogicalDirection::Output);
	_resetPin->SetValue(LogicalState::InActive);
	_chronos->Delay(2000);
	_resetPin->ChangePinDirection(LogicalDirection::Input);
	uint8_t result = ReadRespone(500, "\r\nready\r\n", true);
	return;
}

void EspCommunicator::DisconnectAP()
{
	if(!(WriteCMD("AT+CWQAP", 5000, "\r\nOK\r\n" ) == 0))
	{
		Logger.LogError(">>> ESP disconnect AP ERROR <<<");
		return;
	}
	return;
}

void EspCommunicator::Ping(const char *host)
{
	// This is only for tests and can not be used in the application.
	if (!(WriteCMD("AT+PING=\"192.168.87.146\"", 3000, "WIFI GOT IP") == 0))
	{
		Logger.LogError(">>> ESP PING ERROR <<<");
		return;
	}

	return;
}

uint8_t EspCommunicator::IsConnected()
{
	// TODO: Implement "IsConnected"!
	
	return 0;
}

void EspCommunicator::CheckEspVersion()
{
	// TODO: Implement "CheckEspVersion"!
	// The command for getting the ESP-01 version.
	// WriteCMD("AT+GMR");
	return;
}

char* EspCommunicator::GetPort(serverInfo_t* info)
{
	if (info->port == TCP)
	{
		return "1883";
	}
	else if(info->port == TCPssl)
	{
		return "8883";
	}
	
	return "PORT_ERROR";
}