#include <Joystick.h>
#include "I2Cdev.h"
#include "MPU6050_6Axis_MotionApps612.h"

#if I2CDEV_IMPLEMENTATION == I2CDEV_ARDUINO_WIRE
#include "Wire.h"
#endif

MPU6050 mpu;

Joystick_ Joystick(JOYSTICK_DEFAULT_REPORT_ID,JOYSTICK_TYPE_GAMEPAD,
  4, 0,                   // Button Count, Hat Switch Count
  true, true, true,       // X and Y, Z Axis
  true, true, true,       // Rx, Ry, Rz
  false, false,           // No rudder or throttle
  false, false, false);   // No accelerator, brake, or steering

#define LED_PIN 13
bool blinkState = false;

bool dmpReady = false;  // Set true if DMP init was successful
uint8_t mpuIntStatus;   // Holds actual interrupt status byte from MPU
uint8_t devStatus;      // Return status after each device operation (0 = success, !0 = error)
uint16_t packetSize;    // Expected DMP packet size (default is 42 bytes)
uint16_t fifoCount;     // Count of all bytes currently in FIFO
uint8_t fifoBuffer[64]; // FIFO storage buffer

// Quaternion Class from I2CDevLib
Quaternion q;

// Calibration Button
const uint8_t buttonPin = 6;
bool buttonState;

void setup() {
  // Join I2C Bus
  Wire.begin();
  Wire.setClock(400000);

  // Initialize device
  mpu.initialize();

  delay(20);

  // Configure the DMP
  devStatus = mpu.dmpInitialize();

  // Pre-calibrated Offsets
  mpu.setXGyroOffset(159);
  mpu.setYGyroOffset(46);
  mpu.setZGyroOffset(21);
  mpu.setXAccelOffset(-532);
  mpu.setYAccelOffset(-1020);
  mpu.setZAccelOffset(1004);

  delay(20);

  // Ensure DMP
  if (devStatus == 0) {
    // Enable DMP
    mpu.setDMPEnabled(true);

    dmpReady = true;

    // Get expected DMP packet size for later comparison
    packetSize = mpu.dmpGetFIFOPacketSize();
  }

  // Status LED
  pinMode(LED_PIN, OUTPUT);
  // Calibrate Button
  pinMode(buttonPin, INPUT);

  Joystick.begin();
  Joystick.setXAxisRange(-1000, 1000);
  Joystick.setYAxisRange(-1000, 1000);
  Joystick.setRxAxisRange(-1000, 1000);
  Joystick.setRyAxisRange(-1000, 1000);

  // To keep the Joystick Tester Happy...
  Joystick.setButton(0, 0);
  Joystick.setButton(0, 1);
  Joystick.setButton(0, 0);
}

void loop() {
  if (!dmpReady) return; // Initialization Fail

  if (mpu.dmpGetCurrentFIFOPacket(fifoBuffer)) { // Get the Latest packet 
    // Display Quaternion Values (w x y z)
    mpu.dmpGetQuaternion(&q, fifoBuffer);

    // Blink LED
    blinkState = !blinkState;
    digitalWrite(LED_PIN, blinkState);

    // Set Joystick Values
    Joystick.setXAxis(q.x * 1000);
    Joystick.setYAxis(q.y * 1000);
    Joystick.setRxAxis(q.z * 1000);
    Joystick.setRyAxis(q.w * 1000);

    // Update Button State
    Joystick.setButton(0, digitalRead(buttonPin));

    delay(10);
  }
}