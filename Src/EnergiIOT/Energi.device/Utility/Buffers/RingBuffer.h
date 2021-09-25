
#ifndef __RINGBUFFER_H__
#define __RINGBUFFER_H__

#include <avr/io.h>
#include "string.h"


class RingBuffer
{
	public:
	RingBuffer(unsigned int size);
	~RingBuffer() = default;

	void ResetBuffer();
	void Push(char c);
	int GetPos();
	bool EndsWith(const char* str);
	void GetStr(char * destination, unsigned int skipChars);
	
	private:
	RingBuffer( const RingBuffer &c );
	RingBuffer& operator=( const RingBuffer &c );

	void Init();

	unsigned int _size;
	char* ringBuf;
	char* ringBufEnd;
	char* ringBufP;

};

#endif //__RINGBUFFER_H__
