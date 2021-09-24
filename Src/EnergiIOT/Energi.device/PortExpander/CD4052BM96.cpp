
#include "CD4052BM96.h"

CD4052BM96::CD4052BM96(DigitalOutput* doA, DigitalOutput* doB, SerialPorts defaultSerial) :
_doA{ doA },
_doB{ doB },
_defaultSerial { defaultSerial }
{}

void CD4052BM96::ChangePort(SerialPorts port)
{
	SetPort(port);
}

void CD4052BM96::UseDefaultPort()
{
	SetPort(_defaultSerial);
}

void CD4052BM96::SetDefaultPort(SerialPorts port)
{
	_defaultSerial = port;
}

void CD4052BM96::SetPort(SerialPorts port)
{
	if (port == SerialPorts::Serial1)
	{
		_doA->SetValue(LogicalState::InActive);
		_doB->SetValue(LogicalState::InActive);
	}
	else if (port == SerialPorts::Serial2)
	{
		_doA->SetValue(LogicalState::Active);
		_doB->SetValue(LogicalState::InActive);
	}
	else if (port == SerialPorts::Serial3)
	{
		_doA->SetValue(LogicalState::InActive);
		_doB->SetValue(LogicalState::Active);
	}
	else if (port == SerialPorts::Serial4)
	{
		_doA->SetValue(LogicalState::Active);
		_doB->SetValue(LogicalState::Active);
	}
	
	_currentPort = port;
}

SerialPorts CD4052BM96::GetCurrentPort()
{
	return _currentPort;
}