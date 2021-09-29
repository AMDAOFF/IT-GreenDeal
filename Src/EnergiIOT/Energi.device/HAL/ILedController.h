
#ifndef __ILEDCONTROLLER_H__
#define __ILEDCONTROLLER_H__

class ILedController
{

	public:
	/////////////////////////////////////////////////////////////////////////////
	//							       WARNING!!                               //
	// The AVR-GCC compiler does not have a link to the c++ standard library,  //
	// so we can not make the destructor pure virtual.                         //
	// Do not use delete, it will cause memory leak!!!                         //
	/////////////////////////////////////////////////////////////////////////////
	~ILedController() = default;
	
	virtual void SetLedState (uint8_t ledNumber, bool state) = 0;
	virtual void ToggleLed(uint8_t ledNumber) = 0;
	virtual void ClearAll() = 0;
	

}; //ILedController

#endif