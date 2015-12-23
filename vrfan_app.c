
#include <usb/usb.h>
#include <usb/usb_device_hid.h>
#include <usb/usb_device.h>
#include "usb_config.h"
#include "vrfan_app.h"
#include "mcc_generated_files/mcc.h"

// Flag to indicate that we have data to send to the PC
uint8_t _USBDataStatus = 0;

// Fix the memory locations for the rx and tx buffers for the 16f1455 we are using
unsigned char _RXDataBuffer[64] @ 0x2050;
unsigned char _TXDataBuffer[64] @ 0x20A0;

volatile USB_HANDLE _USBOutHandle = 0;
volatile USB_HANDLE _USBInHandle = 0;

void USB_RX_Handler(void);
void USB_TX_Handler(void);
void Handle_VRFanCommand(void);



void Init_HIDDevice() {
    
    // if the usb connection is initialized, we turn off the light
    LEDPIN_SetHigh();
    
    _USBInHandle = 0;

    USBEnableEndpoint(HID_EP, USB_IN_ENABLED | USB_OUT_ENABLED | USB_HANDSHAKE_ENABLED | USB_DISALLOW_SETUP);
    _USBOutHandle = (volatile USB_HANDLE)HIDRxPacket(HID_EP,(uint8_t *)&_RXDataBuffer, 64);
}


// Execute a cycle of the main application process,
//     which consists of checking for ingoing and outgoing USB data
void Run_VRFan() {
    if(!HIDRxHandleBusy(_USBOutHandle))
        USB_RX_Handler();

    if( (!HIDTxHandleBusy(_USBInHandle))&& _USBDataStatus) {
        USB_TX_Handler();
        _USBDataStatus = 0;
    }
}



//  Send data to PC
void USB_TX_Handler(void) {

    _USBInHandle = HIDTxPacket(HID_EP,(uint8_t *)_TXDataBuffer, 64);
}

// Receive data from PC
void USB_RX_Handler(void) {

    Handle_VRFanCommand();

    //Re-arm the OUT endpoint for the next packet
    _USBOutHandle = HIDRxPacket(HID_EP,(uint8_t*)&_RXDataBuffer,64);
}


// we received some data, so check to see if it is a valid command
//   any valid command is responded to with a pong when next queried, or
//   an error code if last data sent was invalid
//   Any response (pong or error) is transmitted when the next command is received
//   The PC can issue a VRFAN_QUERY command in order to check the result of the last
//   command
void Handle_VRFanCommand(void) {

    // the first and only byte of anything received is the command sent from the pc
    switch (_RXDataBuffer[1]) {
        
        // query and
        case VRFAN_QUERY:
        case VRFAN_PING:
            break;

        case VRFAN_FAN_ON:
            FANPIN_SetHigh();
            break;
            
        case VRFAN_FAN_OFF:
            FANPIN_SetLow();
            break;

        case VRFAN_LED_ON:
            LEDPIN_SetLow();    // Active low
            break;
            
        case VRFAN_LED_OFF:
            LEDPIN_SetHigh();    // Active low
            break;

        // an unrecognized command was sent, so add the error response to the
        //     tx buffer
        default:
            _TXDataBuffer[0] = VRFAN_ERROR;
            _USBDataStatus = 1;
            break;
    }

    // if there is no error, we respond with a pong
    if (_USBDataStatus == 0)  {
        _TXDataBuffer[0] = VRFAN_PONG;
        _USBDataStatus = 1;
    }
}



