
#ifndef PINMUX_H_
#define PINMUX_H_

#ifndef F_CPU
#define F_CPU 16000000UL
#endif

// Led position on the GreenDeal board.
enum Leds : uint8_t
{
	Error = 1, // Red.
	Fail = 2, // Red.
	Send = 4, // Blue.
	Load = 8, // Blue.
	Ready = 16, // Green.
	Online = 32, // Green.
	Working = 64, // Yellow.
	Busy = 128, // Yellow.
};

// Microcontroller pins.
#define Button1 0x04
#define Reset
#define SerialPortexpanderA 0x04
#define SerialPortexpanderB 0x08

// Extern.
#define PCF8574_ADDRESS 0x38

#endif /* PINMUX_H_ */