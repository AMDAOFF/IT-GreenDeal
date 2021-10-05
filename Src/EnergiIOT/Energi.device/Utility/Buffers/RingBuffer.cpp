

#include "RingBuffer.h"
#include "../Logging/ErrorLog.h"

#include <stdlib.h>
#include <stdbool.h>
#include <string.h>
#include <math.h>
#include <avr/pgmspace.h>
#include <avr/io.h>
#include <avr/interrupt.h>

RingBuffer::RingBuffer(unsigned int size)
{
	_size = size;
	// Add one char to terminate the string
	ringBuf = new char[size+1];
	ringBufEnd = &ringBuf[size];
	Init();
}

void RingBuffer::ResetBuffer()
{
	ringBufP = ringBuf;
}

void RingBuffer::Init()
{
	ringBufP = ringBuf;
	memset(ringBuf, 0, _size+1);
}

void RingBuffer::Push(char c)
{
	*ringBufP = c;
	ringBufP++;
	if (ringBufP>=ringBufEnd)
	ringBufP = ringBuf;
}


bool RingBuffer::EndsWith(const char* str)
{
	int findStrLen = strlen(str);

	// b is the start position into the ring buffer
	char* buffer = ringBufP-findStrLen;
	if(buffer < ringBuf)
	buffer = buffer + _size;

	char *p1 = (char*)&str[0];
	char *p2 = p1 + findStrLen;

	for(char *p=p1; p<p2; p++)
	{
		if(*p != *buffer)
		return false;

		buffer++;
		if (buffer == ringBufEnd)
		buffer=ringBuf;
	}

	return true;
}

void RingBuffer::GetStr(char* destination, unsigned int skipChars)
{

	int len = ringBufP - ringBuf - skipChars;
	// copy buffer to destination string
	strncpy(destination, ringBuf, len);
}
