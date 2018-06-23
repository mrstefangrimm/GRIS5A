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
    pwm.setPWM(n, 0, _currentPosition[n]);
  }
}


void loop() {

  int key = Serial.read();

  if (key >= '0' && key <= 'O') {
    // Debug: Serial.println(key);
    if      (key == '9') { _currentPosition[LURTN] += 5; }
    else if (key == ':') { _currentPosition[LURTN] -= 5; }
    else if (key == '8') { _currentPosition[LULNG] -= 5; }
    else if (key == '<') { _currentPosition[LULNG] += 5; }
    else if (key == ';') { _currentPosition[LLRTN] -= 5; }
    else if (key == '>') { _currentPosition[LLRTN] += 5; }
    else if (key == '=') { _currentPosition[LLLNG] += 5; }
    else if (key == '?') { _currentPosition[LLLNG] -= 5; }

    else if (key == 'A') { _currentPosition[RURTN] += 5; }
    else if (key == 'B') { _currentPosition[RURTN] -= 5; }
    else if (key == '@') { _currentPosition[RULNG] += 5; }
    else if (key == 'C') { _currentPosition[RULNG] -= 5; }
    else if (key == 'G') { _currentPosition[RLRTN] -= 5; }
    else if (key == 'E') { _currentPosition[RLRTN] += 5; }
    else if (key == 'D') { _currentPosition[RLLNG] -= 5; }
    else if (key == 'F') { _currentPosition[RLLNG] += 5; }

    else if (key == 'J') { _currentPosition[GARTN] += 5; }
    else if (key == 'K') { _currentPosition[GARTN] -= 5; }
    else if (key == 'I') { _currentPosition[GALNG] -= 5; }
    else if (key == 'O') { _currentPosition[GALNG] += 5; }

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
