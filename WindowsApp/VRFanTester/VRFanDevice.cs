using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace VRFanTester {





    public enum VRFanCommand {
        VRFAN_QUERY = 0x00,    // check the response for the last command sent
        VRFAN_PING = 0x11,    // check if the vrfan microcontroller is active
        VRFAN_PONG = 0x22,    // the response for any valid command

        VRFAN_FAN_OFF = 0x30,
        VRFAN_FAN_ON = 0x31,

        VRFAN_LED_OFF = 0x40,
        VRFAN_LED_ON = 0x41,

        VRFAN_ERROR = 0xFF      // the response for an invalid command
    };



    public class VRFanDevice : HIDDevice {


        private const int _VRFanVendorId = 0x04D8;
        private const int _VRFanProductId = 0x922E;


        //  public bool _Connected { get; private set; }
        public bool _FanOn { get; private set; } = false;
        public bool _LEDOn { get; private set; } = false;


        private VRFanCommand _LastCommandSent;

        // Creates an input report for use in the HID device framework and returns a new input report for this device
        public override InputReport CreateInputReport() {
            return new VRFanInputReport(this);
        }



        public void Send_Command(VRFanCommand cmd) {
            VRFanOutputReport outReport = new VRFanOutputReport(this); // create output report
            outReport.Set_VRFanCommand(cmd);
            _LastCommandSent = cmd;
            try {
                Write(outReport); // write the output report
            }
            catch (Exception ex) {
                // Device may have been removed!
                throw new HIDDeviceException("Problem with hid device");
            }
        }


        public static VRFanDevice Find_VRFanDevice() {

            // 0x04D8                  Vendor ID
            // 0x922E                  Product ID  
            return (VRFanDevice)FindDevice(_VRFanVendorId, _VRFanProductId, typeof(VRFanDevice));
        }


        protected override void HandleDataReceived(InputReport inReport) {

            byte status = ((VRFanInputReport)inReport)._FanStatus;

            if (status != 0xFF) { 
                if (_LastCommandSent == VRFanCommand.VRFAN_FAN_ON) {
                    _FanOn = true;
                }
                else if (_LastCommandSent == VRFanCommand.VRFAN_FAN_OFF) {
                    _FanOn = false;
                }
                else if (_LastCommandSent == VRFanCommand.VRFAN_LED_ON) {
                    _LEDOn = true;
                }
                else if (_LastCommandSent == VRFanCommand.VRFAN_LED_OFF) {
                    _LEDOn = false;
                }
            }

            _LastCommandSent = 0x00;
        }


        protected override void Dispose(bool bDisposing) {
            if (bDisposing) {
                try {
                    Send_Command(VRFanCommand.VRFAN_FAN_OFF);
                }
                catch (Exception ex) {
                    // nothing
                }
            }
            base.Dispose(bDisposing);
        }
    }

    public class VRFanOutputReport : OutputReport {

        public VRFanOutputReport(HIDDevice oDev) : base(oDev) { }

        public void Set_VRFanCommand(VRFanCommand cmd) { 
            byte[] arrBuff = Buffer;

            arrBuff[2] = (byte)cmd;
        }
    }

    public class VRFanInputReport : InputReport {

        public byte _FanStatus { get; private set; } 

        public VRFanInputReport(HIDDevice oDev) : base(oDev) {
            _FanStatus = 0;
        }

        public override void ProcessData() {
            byte[] arrData = Buffer;
            _FanStatus = arrData[0];
        }
    }
}

