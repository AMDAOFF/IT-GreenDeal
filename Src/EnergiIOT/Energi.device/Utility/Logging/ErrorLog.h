
#ifndef __ERRORLOG_H__
#define __ERRORLOG_H__

#include "ISerialport.h"
#include "../Types.h"
#include "LogSettings.h"

class ErrorLog
{
	public:
	ErrorLog();
	~ErrorLog() = default;
	
	void Initialize(ISerialport* serial, SerialPorts defaultPort);
	void Log(const char msg[]);
	void LogError(const char msg[]);

	private:
	ErrorLog( const ErrorLog &c );
	ErrorLog& operator=( const ErrorLog &c );

	ISerialport* _serial;
	SerialPorts _defaultPort;
};

extern ErrorLog Logger;

#endif //__ERRORLOG_H__
