# VRFan
VRFan consists of firmware, schematic, and PCB for an adapter used to convert a standard 'dumb' USB fan into a HID device, with the intention of doing something with the USB port available on the Oculus Rift DevKit 2 HMD.
As a HID device, the fan is software controllable and can be turned on or off at times appropriate for a particular VR scene.

Programs to test device functionality for both windows (in C#) and Linux (in C) are in the WindowsApp and VRFan_LinuxTester directories respectively.

The device is powered over USB and utilizes a PIC16F1455 as the controller and MPLab/XC8 as the toolchain.  Schematic and PCB are in KiCad.

