

#ifndef __IADC_H__
#define __IADC_H__

#include <avr/io.h>

  /**
  * @brief Reader interface for a single ADC channel. In order to sample on
  * the channel, the application must implement the ISR.
  */
class IAdc
{
	public:
	virtual ~IAdc() = default;
	 /**
    * @brief This performs the basic initialization etc.
    * of the ADC after the system, pins and clocks has been configured.
    */
	virtual void Initialize() = 0;
};

#endif
