
#include "ErrorLog.h"

ErrorLog::ErrorLog(){}

void ErrorLog::Initialize(ISerialport* serial, SerialPorts defaultPort)
{
	if (DebugMode)
	{
		_serial = serial;
		_defaultPort = defaultPort;
	}
}

void ErrorLog::Log(const char msg[])
{
	if (DebugMode)
	{
		_serial->Write("> Log : ", _defaultPort);
		_serial->WriteLine(msg, _defaultPort);
	}
}

void ErrorLog::LogError(const char msg[])
{
	if (DebugMode)
	{
		_serial->Write("> ERROR : ", _defaultPort);
		_serial->WriteLine(msg, _defaultPort);
	}
}