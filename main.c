#include "system.h"
#include <usb/usb_device_hid.h>
#include "vrfan_app.h"
#include "mcc_generated_files/mcc.h"
#include "usb_config.h"
#include "vrfan_app.h"
#include <usb/usb.h>


// the only interrupt we are working with is USB related
void interrupt Handle_Interrupt() {
    USBDeviceTasks();
} 
    



void main(void) {
    
   SYSTEM_Initialize();

   // The led is used to inidicate USB connection status,  if off, the usb connection
   //    has been initialized, otherwise it will stay on to indicate an error
   LEDPIN_SetLow();
   
   USBDeviceInit();
   USBDeviceAttach();
   
    while(true) {
            
        // wait until usb is configured to do anything
        if( USBGetDeviceState() >= CONFIGURED_STATE ){
            Run_VRFan();
        }
    }
}


// The main USB event handler
bool USER_USB_CALLBACK_EVENT_HANDLER(USB_EVENT event, void *pdata, uint16_t size) {
    
    switch(event) {
 
        case EVENT_CONFIGURED:
            Init_HIDDevice();
            break;

            
        case EVENT_EP0_REQUEST:
            USBCheckHIDRequest();
            break;


        default:
            break;
    }
    return true;
}
