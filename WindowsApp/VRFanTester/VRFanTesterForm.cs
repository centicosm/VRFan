using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VRFanTester {
    public partial class VRFanTesterForm : Form {


        private VRFanDevice _VRFanDev = null;

        public VRFanTesterForm() {
            InitializeComponent();
        }

        private void VRFanCheckButton_Click(object sender, EventArgs e) {

            if (_VRFanDev == null) {
                Check_ForVRFanDevice();
            }
        }


        private void Check_ForVRFanDevice() {

            _VRFanDev = VRFanDevice.Find_VRFanDevice();	// look for the device on the USB bus
            if (_VRFanDev != null) {
                VRFanStatusLabel.Text = "VRFan Detected";
            }
            else {
                VRFanStatusLabel.Text = "VRFan Not Found";
            }
        }

        private void LEDButton_Click(object sender, EventArgs e) {

            if (_VRFanDev != null) {
                try {
                    if (_VRFanDev._LEDOn) {
                        _VRFanDev.Send_Command(VRFanCommand.VRFAN_LED_OFF);
                        LedLabel.Text = "Off";
                    }
                    else {
                        _VRFanDev.Send_Command(VRFanCommand.VRFAN_LED_ON);
                        LedLabel.Text = "On";
                    }
                }
                catch (HIDDeviceException ex) {
                    if (ex.Message.Equals("Problem with hid device")) {
                        MessageBox.Show("Problem communicating with device, device may have been removed");
                        _VRFanDev.Dispose();
                        _VRFanDev = null;
                        VRFanStatusLabel.Text = "VRFan Not Found";
                        LedLabel.Text = "Off";
                        FanLabel.Text = "Off";
                    }
                }
            }
        }

        private void FanButton_Click(object sender, EventArgs e) {
            if (_VRFanDev != null) {
                try {
                    if (_VRFanDev._FanOn) {
                        _VRFanDev.Send_Command(VRFanCommand.VRFAN_FAN_OFF);
                        FanLabel.Text = "Off";
                    }
                    else {
                        _VRFanDev.Send_Command(VRFanCommand.VRFAN_FAN_ON);
                        FanLabel.Text = "On";
                    }
                }
                catch (HIDDeviceException ex) {
                    if (ex.Message.Equals("Problem with hid device")) {
                        MessageBox.Show("Problem communicating with device, device may have been removed");
                        _VRFanDev.Dispose();
                        _VRFanDev = null;
                        VRFanStatusLabel.Text = "VRFan Not Found";
                        LedLabel.Text = "Off";
                        FanLabel.Text = "Off";
                    }
                }
            }
        }
    }
}
