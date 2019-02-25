#include "nrf_gpio.h"
#include "controllerBoard.h"
#include <stdint.h>
#include <stdbool.h>
#define LED_PINS_NUMBER 8
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

#if LED_PINS_NUMBER > 0
static const uint8_t m_board_led_list[LED_PINS_NUMBER] = LED_PINS_LIST;
#endif

void bsp_cboard_led_invert(uint32_t led_idx)
{
    ASSERT(led_idx < LED_PINS_NUMBER);
    nrf_gpio_pin_toggle(m_board_led_list[led_idx]);
}