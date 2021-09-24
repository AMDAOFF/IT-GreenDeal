
#include "Serialport.h"


Serialport::Serialport(CD4052BM96* portexpander, Chronos* chronos) :
_ringBuffer(32),
_portexpander { portexpander },
_chronos { chronos }
{}

void Serialport::Initialize(){
	
	UBRR0H = (unsigned char)(BRC >> 8); //baud rate register set to 9600 BAUD
	UBRR0L = (unsigned char) BRC;
	
	//TXEN0 enables tx so no gpio anymore, when disabled the buffer will be finished first
	//RXEN0 enables rx
	//RXCIE0 enables the interrupt to be used with the ISR(USART_RX_vect)
	UCSR0B = (1<<RXEN0) | (1<<TXEN0) | (1 << RXCIE0);
	
	//Set frame format: 8data, 1stop bit, no parity.
	//UMSEL01 and UMSEL00 are both 0 to get asynchronous communication
	//UPM01 and UPM00 used to set parity, it's 0 here so no parity
	//USBS0 set to 0 is 1 stop bit. set to 1 is 2 stop bits.
	//USCZ00 - USCZ02 is used to set the character size, here it's set to 8 bits
	//UCPOL0 is clock parity and only used for synchronized transmission, not used
	
	UCSR0C = (1 << UCSZ01) | (3<<UCSZ00);
	//UCSR0C = (1 << UCSZ01) | (3<<UCSZ00) | (1 << UCSZ01);
	
	//sei();
	
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
		if (ReadByteWithTimeout(&c, 5)) // Wait bite for 5 mil.
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
		//Logger.LogError(">>> TIMEOUT ERROR <<<");
	}
	
	_portexpander->ChangePort(saveCurrentPort);
	EnableInterrupt();
	
	return result;
}

char Serialport::ReadByte()
{
	while(!(UCSR0A & _BV(RXC0)));   // Wait for byte
	return UDR0 ;              // Data Read
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
	//RXCIE0 enables the interrupt to be used with the ISR(USART_RX_vect)
	UCSR0B |= (1 << RXCIE0);
	
	return;
}

void Serialport::DisableInterrupt()
{
	UCSR0B &= ~(1 << RXCIE0);
	return;
}

#define RX_BUFF_SIZE 100
char _rxBuffer[RX_BUFF_SIZE];
volatile uint8_t current_index;
volatile bool process_data = false;
volatile char temp;

ISR(USART_RX_vect)
{
	temp = UDR0;
	
	if (temp == '\r' || temp == '\n' && current_index == 0)
	{
		return;
	}
	else
	{
		if (temp == '\0') // Make one big string
		{
			_rxBuffer[current_index++] = '#';
		}
		else
		{
			_rxBuffer[current_index++] = temp;
		}

		if (temp == '\n')
		{
			_rxBuffer[current_index] = '\0';
			
			
			if (_rxBuffer[current_index - 2] == '#')
			{
				process_data = true;
			}
			current_index = 0;
		}
	}
	
	// Prevent stack overflow.
	if (current_index >= RX_BUFF_SIZE)
	{
		current_index = 0;
	}

	
	return;
}

void Serialport::Transmit(unsigned char data )
{
	
	while( !( UCSR0A & (1<<UDRE0)) ); // Wait for empty transmit buffer
	UDR0 = data; // Put data into buffer, sends the data
	return;
}

char* Serialport::GetRxBuffer()
{
	return _rxBuffer;
}

bool Serialport::DataReadyForProcess()
{
	return process_data;
}

void Serialport::ClearReadyForProcess()
{
	process_data = false;
	return;
}