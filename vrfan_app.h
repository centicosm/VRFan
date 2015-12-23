#ifndef VRFAN_APP_H
#define	VRFAN_APP_H


// init the pic as a HID usb device
void Init_HIDDevice(void);

// check incoming USB data for commands
void Run_VRFan(void);   


// valid command codes for the vrfan
typedef enum {
    VRFAN_QUERY     = 0x00,    // check the response for the last command sent
    VRFAN_PING      = 0x11,    // check if the vrfan microcontroller is active
    VRFAN_PONG      = 0x22,    // the response for any valid command
    
    VRFAN_FAN_OFF  = 0x30,
    VRFAN_FAN_ON   = 0x31,

    VRFAN_LED_OFF  = 0x40,
    VRFAN_LED_ON   = 0x41,
            
    VRFAN_ERROR     = 0xFF      // the response for an invalid command

} VRFAN_COMMANDS;

#endif
