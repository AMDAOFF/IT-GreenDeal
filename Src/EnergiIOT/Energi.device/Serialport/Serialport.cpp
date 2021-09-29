
#include "Serialport.h"


Serialport::Serialport(CD4052BM96* portexpander, Chronos* chronos) :
_ringBuffer(32),
_portexpander { portexpander },
_chronos { chronos }
{}


void Serialport::Initialize(){
	
	UBRR0H = (unsigned char)(BRC >> 8); //baud rate register set to 9600 BAUD.
	UBRR0L = (unsigned char) BRC;
	
	//TXEN0 enables tx.
	//RXEN0 enables rx.
	//RXCIE0 enables the interrupt, and can be used with the ISR.
	UCSR0B = (1<<RXEN0) | (1<<TXEN0) | (1 << RXCIE0);
	
	//Set the frame format: 8data, 1stop bit, no parity.
	//UMSEL01 and UMSEL00 are both 0 for asynchronous communication.
	//UPM01 and UPM00 sets "no parity".
	//USBS0 set to 0 is 1 stop bit. set to 1 is 2 stop bits.
	//USCZ00 - USCZ02 sets the character size, to 8 bits.		
	UCSR0C = (1 << UCSZ01) | (3<<UCSZ00);
	
	sei();
	
	return;
}


void Serialport::Write(const char c[], SerialPorts port)
{
	_portexpander->ChangePort(port);
	
	uint8_t i;
	for(i = 0; i < strlen(c); i++) // put the string you want to write in a buffer.
	{
		Transmit(c[i]); // Transmit bite on by one.
	}
	
	return;
}

void Serialport::Write(const char c[], uint8_t strlen, SerialPorts port)
{
	_portexpander->ChangePort(port);
	
	for(uint8_t i = 0; i <= strlen; i++) // put the string you want to write in a buffer.
	{
		Transmit(c[i]); // Transmit bite on by one.
	}
	
	return;
}

void Serialport::WriteChar(const char c, SerialPorts port)
{
	_portexpander->ChangePort(port);
	Transmit(c); //put the char's one by one in the buffer
	
	return;
}

void Serialport::WriteLine(const char c[], SerialPorts port)
{
	Write(c, port);
	Write("\r\n", port);
	
	return;
}

uint8_t Serialport::ReadTagWithTimeout(SerialPorts port, char buffer[], const char tag[], unsigned int timeout)
{
	unsigned char c;
	unsigned long start = _chronos->Time();
	uint8_t result = -1;

	SerialPorts saveCurrentPort = _portexpander->GetCurrentPort();
	_portexpander->ChangePort(port);
	DisableInterrupt();

	_ringBuffer.ResetBuffer();

	while (_chronos->Time() - start < timeout)
	{
		if (ReadByteWithTimeout(&c, 5)) // Wait byte for 5 milli.
		{
			_ringBuffer.Push(c);
		}
		
		if (_ringBuffer.EndsWith(tag))
		{
			// Tag found, so stop reading...
			result = 0;
			break;
		}
	}
	
	_ringBuffer.GetStr(buffer, 0);

	if (_chronos->Time() - start >= timeout)
	{
		Logger.LogError(">>> TIMEOUT ERROR <<<");
	}
	
	_portexpander->ChangePort(saveCurrentPort);
	EnableInterrupt();
	
	return result;
}

char Serialport::ReadByte()
{
	while(!(UCSR0A & _BV(RXC0)));   // Wait for byte.
	return UDR0 ; // Data Read
}

bool Serialport::ReadByteWithTimeout(unsigned char* byte, unsigned int timeout)
{
	unsigned long start = _chronos->Time();
	
	do{
		if(UCSR0A & _BV(RXC0))
		{
			*byte = UDR0;
			return true;
		}
	}while (!(_chronos->Time() - start >= timeout));
	
	return false;
}

void Serialport::EnableInterrupt()
{
	UCSR0B |= (1 << RXCIE0);	
	return;
}

void Serialport::DisableInterrupt()
{
	UCSR0B &= ~(1 << RXCIE0);
	return;
}


void Serialport::Transmit(unsigned char data )
{
	
	while( !( UCSR0A & (1<<UDRE0)) ); // Wait for empty transmit buffer
	UDR0 = data; // Put data into buffer, for sending the data.
	return;
}
