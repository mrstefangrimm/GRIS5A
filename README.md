# GRIS5A
GRIS5A is an DIY open source motion phantom with 5 axes and linear as well as rotary motion.
The name "Gris" "5A" comes from its color (gray) and the 5 axes. 

GRIS5A is a working motion phantom. It has a respiratory motion platform and four cylindrical probe holders. The body and the cylinders are made out of wood.

GRIS5A is a working design prototype, This means take it as a source of inspiration for your own motion phantom:

 - Create your phantom body and the cylinders with different materials
 - Choose the number of axes (the software support up to five axes)
 - Rearrange the cylinders and  respiratory motion platform
 - Change the software with different motion pattern

## File structure
This motion phantom is a (mechanical) device which is controlled by an Arduino (software) and has an input device (hardware).
The repository has therefore the sub folders:

- Mechanics
- Hardware
- Software

## Tool Chain and Software libraries

GRIS5A is built with open source tools and open source libraries.

### Mechanics

Tool | Licence
---- | -------
SketchUp | SketchUp Make 2017 User
Ultimaker Cura | GPL

### Hardware

Tool | Licence
---- | -------
 Fritzing | GPL

### Software

Application || Licence
----------- | -------
gris5A | GPL
calibration | LGPL
SoftDKb | LGPL

Tool | Licence
---- | -------
Arduino IDE | GPL
avrdude | GPL
QM | GPL
Processing | GPL
gcc | GPL

Libraries

Library | Licence
------- | -------
prfServo | LGPL
twi.h | LGPL
Wire.h | LGPL
Arduino.h | LGPL
Stream.h | LGPL
Adafruit_PWMServoDriver.h | ?
qpn.h | GPL
Processing Core | LPGL
