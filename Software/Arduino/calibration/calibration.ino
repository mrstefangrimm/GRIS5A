/* calibration.ino - Calibration Arduino software for the GRIS5A (C) motion phantom 
 * Copyright (C) 2018 by Stefan Grimm
 *
 * This is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This software is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Lesser General Public License for more details.

 * You should have received a copy of the GNU Lesser General Public License
 * along with the calibration software.  If not, see
 * <http://www.gnu.org/licenses/>.
 */

#include <Wire.h>
#include <Adafruit_PWMServoDriver.h>

Adafruit_PWMServoDriver pwm = Adafruit_PWMServoDriver();

#define NUMSERVOS 10
int _currentPosition[NUMSERVOS];
int _sendCounter = 0;

enum MotorID {
  LURTN = 0,
  LULNG,
  LLRTN,
  LLLNG,
  RLLNG,
  RLRTN,
  RULNG,
  RURTN,
  GALNG,
  GARTN
};

typedef struct DKbInData_r1_t {
  DKbInData_r1_t () : raw(0) {}
  DKbInData_r1_t (const struct DKbInData_r1_t& t) : raw(t.raw) {}
  DKbInData_r1_t (uint32_t rawValue) : raw(rawValue) {}
  union {
    struct {
      uint32_t GAL : 1;
      uint32_t GAT : 1;
      uint32_t GAB : 1;
      uint32_t GAR : 1;
      uint32_t FP7 : 1;
      uint32_t FP6 : 1;
      uint32_t FP5 : 1;
      uint32_t FP8 : 1;
      uint32_t RLB : 1;
      uint32_t RLR : 1;
      uint32_t RLL : 1;
      uint32_t RLT : 1;
      uint32_t RUR : 1;
      uint32_t RUL : 1;
      uint32_t RUT : 1;
      uint32_t RUB : 1;
      uint32_t FP4 : 1;
      uint32_t FP3 : 1;
      uint32_t FP2 : 1;
      uint32_t FP1 : 1;
      uint32_t FPG : 1;
      uint32_t FPS : 1;
      uint32_t FMM : 1;
      uint32_t FPP : 1;
      uint32_t LLB : 1;
      uint32_t LLR : 1;
      uint32_t LLL : 1;
      uint32_t LLT : 1;
      uint32_t LUR : 1;
      uint32_t LUL : 1;
      uint32_t LUT : 1;
      uint32_t LUB : 1;
    };
    uint32_t raw;
  };
} DKbInData;


void setup() {
  Serial.begin(9600);
  delay(50);
    
  Serial.println("Calibration");
  pwm.begin();
  pwm.setPWMFreq(60);
  for (int n=0; n<NUMSERVOS; n++) {
    pwm.setPWM(n, 0, 0);
    _currentPosition[n] = 400;
  }
  delay(1000);
  for (int n=0; n<NUMSERVOS; n++) {
    delay(100);
    pwm.setPWM(n, 0, _currentPosition[n]);
  }
}


void loop() {

  int serin = Serial.read();
  int key = serin;
  if (serin > -1 && (serin & 0x7) == 1) {
    // Debug: Serial.println(serin, HEX);
    uint32_t databuf = 0;
    int bitToSet = (serin >> 3);
    bitSet(databuf, bitToSet);
    DKbInData data(databuf);
    
    if      (data.LUR) { _currentPosition[LURTN] += 5; }
    else if (data.LUL) { _currentPosition[LURTN] -= 5; }
    else if (data.LUT) { _currentPosition[LULNG] -= 5; }
    else if (data.LUB) { _currentPosition[LULNG] += 5; }
    else if (data.LLR) { _currentPosition[LLRTN] += 5; }
    else if (data.LLL) { _currentPosition[LLRTN] -= 5; }
    else if (data.LLT) { _currentPosition[LLLNG] += 5; }
    else if (data.LLB) { _currentPosition[LLLNG] -= 5; }

    else if (data.RUR) { _currentPosition[RURTN] += 5; }
    else if (data.RUL) { _currentPosition[RURTN] -= 5; }
    else if (data.RUT) { _currentPosition[RULNG] += 5; }
    else if (data.RUB) { _currentPosition[RULNG] -= 5; }
    else if (data.RLR) { _currentPosition[RLRTN] += 5; }
    else if (data.RLL) { _currentPosition[RLRTN] -= 5; }
    else if (data.RLT) { _currentPosition[RLLNG] -= 5; }
    else if (data.RLB) { _currentPosition[RLLNG] += 5; }

    else if (data.GAR) { _currentPosition[GARTN] += 5; }
    else if (data.GAL) { _currentPosition[GARTN] -= 5; }
    else if (data.GAT) { _currentPosition[GALNG] -= 5; }
    else if (data.GAB) { _currentPosition[GALNG] += 5; }

    for (int n=0; n<NUMSERVOS; n++) {
      pwm.setPWM(n, 0, _currentPosition[n]);
    }
  }

  // Send one servo position at a time
  Serial.print("|");
  Serial.print(_sendCounter);
  Serial.print(_currentPosition[_sendCounter]);
  Serial.print("|");
  _sendCounter++;
  if (_sendCounter >= NUMSERVOS) { _sendCounter = 0; }

  if (key == 'p') {
    for (int n=0; n<NUMSERVOS; n++) {
      Serial.print(n);
      Serial.print(" : ");
      Serial.println(_currentPosition[n]);
    }
  }
  
  delay(200);
}
