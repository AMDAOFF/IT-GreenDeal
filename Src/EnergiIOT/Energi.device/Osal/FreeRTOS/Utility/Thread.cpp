

#include "Thread.h"

      /**
      * @brief StaticThreadHelper
      * Helps transferring the thread function to the interface which is provided in the thread constructor
      * @param interfacePtr - Pointer to an object implementing the IRunnable interface
      */
      static void StaticThreadHelper(void* interfacePtr)
      {
        //if (interfacePtr != nullptr)
        //{
          //Cast to the IRunnable interface
          IRunnable* object = reinterpret_cast<IRunnable*>(interfacePtr);
          object->Service();
        //}
      }


//// default constructor
//Thread::Thread()
//{
//} //Thread

// default destructor
//Thread::~Thread()
//{
//} //~Thread

      Thread::Thread(const char* name, uint8_t stackSize, uint8_t priority, IRunnable& runnable)
      {
	      //Create a task using a static free function. The function gets the runnable object in the parameters and calls run upon it
	      _isCreated = (xTaskCreate(StaticThreadHelper, name, 100, &runnable, priority, &_threadhandle) == pdPASS);
      }

      bool Thread::IsCreated() const
      {
	      return _isCreated;
      }

      const TaskHandle_t& Thread::GetThreadHandle() const
      {
	      return _threadhandle;
      }

      char* Thread::GetThreadName()
      {
	      return pcTaskGetName(_threadhandle);
      }

      //std::uint32_t Thread::GetMinimumRemainingStackSpace()
      //{
	      //if (INCLUDE_uxTaskGetStackHighWaterMark == 1)
	      //{
		      ////Watermark is in words so consider the word size
		      //return uxTaskGetStackHighWaterMark(_threadhandle) * sizeof(portSTACK_TYPE);
	      //}
	      //else
	      //{
		      //return 0;
	      //}
      //}