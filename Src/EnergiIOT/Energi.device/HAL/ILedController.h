
#ifndef __ILEDCONTROLLER_H__
#define __ILEDCONTROLLER_H__

/**
* @brief  This interface class specifies the functions that are available for
* an application that requires a led controller.
*/
class ILedController
{
	public:
	~ILedController() = default;
	
	/**
	* @brief Write to an led controller and performs a led state change.
	*  @param lednumber - the number of the led to change.
	*  @param state -  The state to change to.
	*/
	virtual void SetLedState (uint8_t ledNumber, bool state) = 0;
	
	/**
	* @brief Toggles the current state of a GPIO pin.
	* @param lednumber - GPIO pin to toggle.
	*/
	virtual void ToggleLed(uint8_t ledNumber) = 0;
	
	/**
	* @brief This will turn all leds to logic low.
	*/
	virtual void ClearAll() = 0;
};

#endif