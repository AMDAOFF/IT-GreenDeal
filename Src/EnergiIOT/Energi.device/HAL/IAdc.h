

#ifndef __IADC_H__
#define __IADC_H__

#include <avr/io.h>

class IAdc
{
	public:
	virtual ~IAdc() = default;
	virtual void Initialize() = 0;
};

#endif //__IADC_H__
