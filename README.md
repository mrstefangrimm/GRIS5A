# GRIS5A
GRIS5A is an DIY open source motion phantom. It has 5 axes which can be individually moved in longitudinal direction and can be rotated.
The name "Gris" "5A" comes from its color (gray) and the 5 axes. 

GRIS5A is a working motion phantom. It has a respiratory motion platform and four cylindrical probe holders. The body and the cylinders are made out of wood.

GRIS5A is a working design prototype; it is meant as a source of inspiration for your own motion phantom:

 - Create your phantom body and the cylinders with different materials
 - Choose the number of axes (the software supports up to five axes)
 - Rearrange the cylinders and  respiratory motion platform
 - Change the software with different motion patterns

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

Application | Licence
----------- | -------
gris5A | GPL
calibration | LGPL
SoftDKb | LGPL
VirtualGris5A  | LGPL

Tool | Licence
---- | -------
Arduino IDE | GPL
avrdude | GPL
QM | GPL
Processing | GPL
gcc | GPL
VS2017 community | MS license

Libraries

Library | Licence
------- | -------
prfServo | LGPL
twi.h | LGPL
Wire.h | LGPL
Arduino.h | LGPL
Stream.h | LGPL
Adafruit_PWMServoDriver.h | BSD
qpn.h | GPL
Processing Core | LPGL
.NET 4.6.2 | MS license
Helix toolkit | MIT
