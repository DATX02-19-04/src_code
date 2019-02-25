#ifndef CONTROLLERBOARD
#define CONTROLLERBOARD


#define DPAD_UP     1<<28
#define DPAD_DOWN   1<<29
#define DPAD_LEFT   1<<30
#define DPAD_RIGTH  1<<4

#define BTN_X       1<<2
#define BTN_Y       1<<24
#define BTN_A       1<<26
#define BTN_B       1<<25

#define BTN_SELCT   1<<23
#define BTN_START   1<<22

#define BTN_L       1<<31
#define BTN_R       1<<27

#define BTN_PRV     1<<11
#define BTN_NXT     1<<12

#define BAT_VLT     1<<3 // Analog in

#define LED_PINS_NUMBER 8

void bsp_board_led_invert(uint32_t led_idx);



#define LED_START      13
#define LED_1          13
#define LED_2          14
#define LED_3          15
#define LED_4          16
#define LED_5          17
#define LED_6          18
#define LED_7          19
#define LED_8          20
#define LED_STOP       20

#define LEDS_ACTIVE_STATE 0

#define LEDS_INV_MASK  LEDS_MASK

#define LED_PINS_LIST { LED_1, LED_2, LED_3, LED_4, LED_5, LED_6, LED_7, LED_8 }
//const char LED_PINS[] = {13, 14, 15, 16, 17, 18, 19, 20}; // Utgångar
#endif