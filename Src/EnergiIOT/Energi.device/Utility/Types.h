
#pragma once

#ifndef TYPES_H_
#define TYPES_H_

/**
  * @brief This enum defines a type that can e.g. be used for
  * logical digital signal levels.
  */
  enum class LogicalState
  {
    InActive = 0,   ///< The logical signal level is inactive
    Active          ///< The logical signal level is active
  };

/**
  * @brief This enum defines a type that can e.g. be used for
  * logical digital signal modes.
  */
  enum class LogicalDirection
  {
	  Output = 0,   ///< The logical signal level is inactive
	  Input          ///< The logical signal level is active
  };

  /**
  * @brief Defines a set of edge types, typically used by the GPIO interrupts
  */
  enum class Edges
  {
    Rising,
    Falling,
    Both
  };

  /**
  * @brief Defines the serial port number.
  */
  enum class SerialPorts
  {
	  Serial1,
	  Serial2,
	  Serial3,
	  Serial4
  };

#endif 