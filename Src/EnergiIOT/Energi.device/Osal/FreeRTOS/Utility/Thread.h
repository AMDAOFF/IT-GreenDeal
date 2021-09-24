/* 
* Thread.h
*
* Created: 09-09-2021 13:26:39
* Author: jpf
*/


#ifndef __THREAD_H__
#define __THREAD_H__

#include <avr/io.h>
#include <stdio.h>

#include "FreeRTOS.h"
#include "task.h"

/**
* @brief Interface for eligible thread objects
*/
class IRunnable
{
public:
    //IRunnable() noexcept = default;
    //virtual ~IRunnable() noexcept = default;
	
	    //IRunnable() = default;
	    //virtual ~IRunnable() = default;
	
    /**
    * @brief Function which runs continously in the thread.
    * NOTE: SHALL NEVER RETURN.
    */
    virtual void Service() = 0;	
	


private:

    //IRunnable(const IRunnable&) = delete;
    //IRunnable& operator=(const IRunnable&) = delete;
};

class Thread
{
//variables
public:
protected:
private:

//functions
public:
	Thread();
	~Thread();
	
		      /**
      * @brief Creates a thread, which is ready to run after the contructor completes
      * @param &name - Name of thread
      * @param stackSize - stack size to allocate for thread in 4 byte chunks
      * @param priority - priority of thread - higher value equals higher priority
      * @param &runnable - reference to interface for running the actual thread
      */
      Thread(const char* name, uint8_t stackSize, uint8_t priority, IRunnable& runnable);
	  
	      /**
      * @return - Returns true if the thread is successfully created, false otherwise
      */
      bool IsCreated() const;

	  const TaskHandle_t& GetThreadHandle() const;

      char* GetThreadName();
protected:
private:
      TaskHandle_t _threadhandle;
      bool _isCreated;

	Thread( const Thread &c );
	Thread& operator=( const Thread &c );

}; //Thread

#endif //__THREAD_H__
