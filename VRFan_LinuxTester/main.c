#include <stdlib.h>
#include <stdio.h>
#include <sys/ioctl.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <asm/types.h>
#include <fcntl.h>
#include <unistd.h>
#include <linux/hiddev.h>
#include <linux/input.h>


#define VRFAN_FAN_OFF   0x30
#define VRFAN_FAN_ON    0x31
#define VRFAN_LED_OFF   0x40
#define VRFAN_LED_ON    0x41


int main(int argc, char **argv) {


	if (argc != 2) {
		printf("\n\nUsage: VRFanTester /path/to/hid/device\n\n");
		exit(1);
	}


	int fd = -1;
	fd = open(argv[1], O_RDONLY );
	if (fd < 0) {
		printf("Could not open: %s, make sure the device exists and you have the appropriate permissions\n", argv[1]);
		printf("You can find the device name by running: 'dmesg | grep VRFan' and look for 'hiddevX in the output\n");
		exit(1);
	}


	bool done = false;

	int cmd = 0;
	unsigned char cmdByte = 0x00;
	while (!done) {
		printf("\n");
		printf("\t1 - Turn LED on\n");
		printf("\t2 - Turn LED off\n");
		printf("\t3 - Turn Fan on\n");
		printf("\t4 - Turn Fan off\n");
		printf("\t5 - Exit\n");

		scanf("%d", &cmd);

		cmdByte = 0;
		if (cmd == 5) {
			done = true;
		}
		else if (cmd == 1) {
			cmdByte = VRFAN_LED_ON;
		}
		else if (cmd == 2) {
			cmdByte = VRFAN_LED_OFF;
		}
		else if (cmd == 3) {
			cmdByte = VRFAN_FAN_ON;
		}
		else if (cmd == 4) {
			cmdByte = VRFAN_FAN_OFF;
		}
		if (cmdByte != 0) {
		
			int n = 64;
        		struct hiddev_report_info rep_info_i, rep_info_u;
        		struct hiddev_usage_ref_multi ref_multi_i, ref_multi_u;
        		
			rep_info_u.report_type = HID_REPORT_TYPE_OUTPUT;
        		rep_info_i.report_type = HID_REPORT_TYPE_INPUT;
        		rep_info_u.report_id = HID_REPORT_ID_FIRST;
			rep_info_i.report_id = HID_REPORT_ID_FIRST;
        		rep_info_u.num_fields = 1;
			rep_info_i.num_fields = 1;
        		ref_multi_u.uref.report_type = HID_REPORT_TYPE_OUTPUT;
        		ref_multi_i.uref.report_type = HID_REPORT_TYPE_INPUT;
        		ref_multi_u.uref.report_id = HID_REPORT_ID_FIRST;
			ref_multi_i.uref.report_id = HID_REPORT_ID_FIRST;
        		ref_multi_u.uref.field_index = 0;
			ref_multi_i.uref.field_index = 0;
        		ref_multi_u.uref.usage_index = 0;
			ref_multi_i.uref.usage_index = 0;
        		ref_multi_u.num_values = n;
			ref_multi_i.num_values = n;
 			
			for(int i = 0; i < n; i++) {
				ref_multi_u.values[i] = cmdByte;
			}

			ioctl(fd,HIDIOCSUSAGES, &ref_multi_u);
			ioctl(fd,HIDIOCSREPORT, &rep_info_u);
			ioctl(fd,HIDIOCGUSAGES, &ref_multi_i);
			ioctl(fd,HIDIOCGREPORT, &rep_info_i);
		}
	}

	close(fd);

	return 1;
}
