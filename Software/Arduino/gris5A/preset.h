/*$file${.::preset.h} ######################################################*/
/*
* Model: gris5A.qm
* File:  ${.::preset.h}
*
* This code has been generated by QM 4.4.0 (https://www.state-machine.com/qm).
* DO NOT EDIT THIS FILE MANUALLY. All your changes will be lost.
*
* This program is open source software: you can redistribute it and/or
* modify it under the terms of the GNU General Public License as published
* by the Free Software Foundation.
*
* This program is distributed in the hope that it will be useful, but
* WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
* or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License
* for more details.
*/
/*$endhead${.::preset.h} ###################################################*/
/* preset.h - Arduino software for the GRIS5A (C) and No2 (C) motion phantom
 * Copyright (C) 2019 by Stefan Grimm
 */

#ifndef __PRESET_H
#define __PRESET_H

#include "qpn.h"
#include "events.h"
#include "constants.h"

#ifdef GRIS5A

void prog1(QMActive* recv, uint16_t& preSetTimer) {
    // Marker Position 1

    static const uint8_t PROGMEM STEPSZ = 2;
    if (preSetTimer == 0) {
      MotorEvArgs lulng(LULNG, 100, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lulng.raw);
      MotorEvArgs lurtn(LURTN, 100, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lurtn.raw);
      MotorEvArgs rulng(RULNG, 100, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rulng.raw);
      MotorEvArgs rurtn(RURTN, 100, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rurtn.raw);
      MotorEvArgs lllng(LLLNG, 100, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lllng.raw);
      MotorEvArgs llrtn(LLRTN, 100, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, llrtn.raw);
      MotorEvArgs rllng(RLLNG, 100, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rllng.raw);
      MotorEvArgs rlrtn(RLRTN, 100, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rlrtn.raw);

      MotorEvArgs galng(GALNG, 0, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, galng.raw);
      MotorEvArgs gartn(GARTN, 127, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, gartn.raw);
    }
    else if (preSetTimer == 3000) {
      MotorEvArgs lulng(LULNG, 127, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lulng.raw);
      MotorEvArgs lurtn(LURTN, 127, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lurtn.raw);
      MotorEvArgs rulng(RULNG, 127, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rulng.raw);
      MotorEvArgs rurtn(RURTN, 127, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rurtn.raw);
      MotorEvArgs lllng(LLLNG, 127, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lllng.raw);
      MotorEvArgs llrtn(LLRTN, 127, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, llrtn.raw);
      MotorEvArgs rllng(RLLNG, 127, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rllng.raw);
      MotorEvArgs rlrtn(RLRTN, 127, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rlrtn.raw);
    }

    if (preSetTimer <= 3000) {
      preSetTimer += PRESETTIMERINCR;
    }
}

void prog2(QMActive* recv, uint16_t& preSetTimer) {
    // Marker Position 2

    static const uint8_t PROGMEM STEPSZ = 2;
    if (preSetTimer == 0) {
      MotorEvArgs lulng(LULNG, 100, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lulng.raw);
      MotorEvArgs lurtn(LURTN, 100, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lurtn.raw);
      MotorEvArgs rulng(RULNG, 100, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rulng.raw);
      MotorEvArgs rurtn(RURTN, 100, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rurtn.raw);
      MotorEvArgs lllng(LLLNG, 100, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lllng.raw);
      MotorEvArgs llrtn(LLRTN, 100, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, llrtn.raw);
      MotorEvArgs rllng(RLLNG, 70, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rllng.raw);
      MotorEvArgs rlrtn(RLRTN, 70, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rlrtn.raw);

      MotorEvArgs galng(GALNG, 0, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, galng.raw);
      MotorEvArgs gartn(GARTN, 127, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, gartn.raw);
    }
    else if (preSetTimer == 3000) {
      MotorEvArgs lulng(LULNG, 167, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lulng.raw);
      MotorEvArgs lurtn(LURTN, 167, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lurtn.raw);
      MotorEvArgs rulng(RULNG, 117, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rulng.raw);
      MotorEvArgs rurtn(RURTN, 117, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rurtn.raw);
      MotorEvArgs lllng(LLLNG, 87, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lllng.raw);
      MotorEvArgs llrtn(LLRTN, 137, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, llrtn.raw);
      MotorEvArgs rllng(RLLNG, 137, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rllng.raw);
      MotorEvArgs rlrtn(RLRTN, 87, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rlrtn.raw);
    }

    if (preSetTimer <= 3000) {
      preSetTimer += PRESETTIMERINCR;
    }
}

void prog3(QMActive* recv, uint16_t& preSetTimer) {
    // Marker Position 1 <-> 2

    if (preSetTimer == 0) {
      static const uint8_t PROGMEM STEPSZ = 2;
      MotorEvArgs lulng(LULNG, 147, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lulng.raw);
      MotorEvArgs lurtn(LURTN, 147, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lurtn.raw);
      MotorEvArgs rulng(RULNG, 122, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rulng.raw);
      MotorEvArgs rurtn(RURTN, 122, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rurtn.raw);
      MotorEvArgs lllng(LLLNG, 107, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lllng.raw);
      MotorEvArgs llrtn(LLRTN, 132, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, llrtn.raw);
      MotorEvArgs rllng(RLLNG, 132, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rllng.raw);
      MotorEvArgs rlrtn(RLRTN, 107, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rlrtn.raw);

      MotorEvArgs galng(GALNG, 0, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, galng.raw);
      MotorEvArgs gartn(GARTN, 127, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, gartn.raw);
    }
    else if (preSetTimer >= 3000) {
      static const uint8_t PROGMEM STEPSZ = 8;
      float targetDeltaSmall = 10 * sin((preSetTimer - 3000) / 3000.0 * PI);
      float targetDeltaLarge = 40 * sin((preSetTimer - 3000) / 3000.0 * PI);

      MotorEvArgs lulng(LULNG, 147 + targetDeltaLarge, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lulng.raw);
      MotorEvArgs lurtn(LURTN, 147 + targetDeltaLarge, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lurtn.raw);
      MotorEvArgs rulng(RULNG, 122 - targetDeltaSmall, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rulng.raw);
      MotorEvArgs rurtn(RURTN, 122 - targetDeltaSmall, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rurtn.raw);
      MotorEvArgs lllng(LLLNG, 107 - targetDeltaLarge, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lllng.raw);
      MotorEvArgs llrtn(LLRTN, 132 + targetDeltaSmall, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, llrtn.raw);
      MotorEvArgs rllng(RLLNG, 132 + targetDeltaSmall, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rllng.raw);
      MotorEvArgs rlrtn(RLRTN, 107 - targetDeltaLarge, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rlrtn.raw);
    }

    if (preSetTimer == 8960) {
      preSetTimer = 3000;
    }
    else {
      preSetTimer += PRESETTIMERINCR;
    }
}

void prog4(QMActive* recv, uint16_t& preSetTimer) {
    // Free-breath Gating

    if (preSetTimer == 0) {
      static const uint8_t PROGMEM STEPSZ = 2;
      MotorEvArgs lulng(LULNG, 127, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lulng.raw);
      MotorEvArgs lurtn(LURTN, 127, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lurtn.raw);
      MotorEvArgs rulng(RULNG, 127, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rulng.raw);
      MotorEvArgs rurtn(RURTN, 127, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rurtn.raw);
      MotorEvArgs lllng(LLLNG, 127, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lllng.raw);
      MotorEvArgs llrtn(LLRTN, 127, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, llrtn.raw);
      MotorEvArgs rllng(RLLNG, 127, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rllng.raw);
      MotorEvArgs rlrtn(RLRTN, 127, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rlrtn.raw);
      MotorEvArgs galng(GALNG, 127, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, galng.raw);
      MotorEvArgs gartn(GARTN, 127, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, gartn.raw);
    }
    else if (preSetTimer >= 3000) {
      static const uint8_t PROGMEM STEPSZ = 8;
      float target = 127 + 80 * sin((preSetTimer - 3000) / 2500.0 * PI);

      MotorEvArgs lulng(LULNG, target, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lulng.raw);
      MotorEvArgs rulng(RULNG, target, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rulng.raw);
      MotorEvArgs lllng(LLLNG, target, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lllng.raw);
      MotorEvArgs rllng(RLLNG, target, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rllng.raw);
      MotorEvArgs galng(GALNG, target, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, galng.raw);
    }

    if (preSetTimer == 7960) {
      preSetTimer = 3000;
    }
    else {
      preSetTimer += PRESETTIMERINCR;
    }
}

void prog5(QMActive* recv, uint16_t& preSetTimer) {
    // Breath-hold Gating

    if (preSetTimer == 0) {
      static const uint8_t PROGMEM STEPSZ = 2;
      MotorEvArgs lulng(LULNG, 60, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lulng.raw);
      MotorEvArgs lurtn(LURTN, 127, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lurtn.raw);
      MotorEvArgs rulng(RULNG, 60, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rulng.raw);
      MotorEvArgs rurtn(RURTN, 127, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rurtn.raw);
      MotorEvArgs lllng(LLLNG, 60, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lllng.raw);
      MotorEvArgs llrtn(LLRTN, 127, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, llrtn.raw);
      MotorEvArgs rllng(RLLNG, 60, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rllng.raw);
      MotorEvArgs rlrtn(RLRTN, 127, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rlrtn.raw);
      MotorEvArgs galng(GALNG, 60, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, galng.raw);
      MotorEvArgs gartn(GARTN, 127, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, gartn.raw);
    }
    else if (preSetTimer >= 3000 && preSetTimer < 28000) {
      static const uint8_t PROGMEM STEPSZ = 8;
      float target = 60 + 50 * sin((preSetTimer - 3000) / 2500.0 * PI);

      MotorEvArgs lulng(LULNG, target, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lulng.raw);
      MotorEvArgs rulng(RULNG, target, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rulng.raw);
      MotorEvArgs lllng(LLLNG, target, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lllng.raw);
      MotorEvArgs rllng(RLLNG, target, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rllng.raw);
      MotorEvArgs galng(GALNG, target, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, galng.raw);
    }
    else if (preSetTimer > 28000 && preSetTimer < 38000) {
      static const uint8_t PROGMEM STEPSZ = 4;
      float target = 200 + 50 * cos((preSetTimer - 28000) / 40000.0 * PI);
      MotorEvArgs lulng(LULNG, target, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lulng.raw);
      MotorEvArgs rulng(RULNG, target, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rulng.raw);
      MotorEvArgs lllng(LLLNG, target, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lllng.raw);
      MotorEvArgs rllng(RLLNG, target, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rllng.raw);
      MotorEvArgs galng(GALNG, target, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, galng.raw);
    }
    else if (preSetTimer == 38000) {
      static const uint8_t PROGMEM STEPSZ = 4;
      MotorEvArgs lulng(LULNG, 10, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lulng.raw);
      MotorEvArgs rulng(RULNG, 10, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rulng.raw);
      MotorEvArgs lllng(LLLNG, 10, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lllng.raw);
      MotorEvArgs rllng(RLLNG, 10, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rllng.raw);
      MotorEvArgs galng(GALNG, 10, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, galng.raw);
    }

    if (preSetTimer == 40000) {
      preSetTimer = 6720;
    }
    else {
      preSetTimer += PRESETTIMERINCR;
    }
}

void prog6(QMActive* recv, uint16_t& preSetTimer) {
    // Free-breath Gating, Marker Position 1 <-> 2

    if (preSetTimer == 0) {
      static const uint8_t PROGMEM STEPSZ = 2;
      MotorEvArgs lulng(LULNG, 147, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lulng.raw);
      MotorEvArgs lurtn(LURTN, 147, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lurtn.raw);
      MotorEvArgs rulng(RULNG, 122, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rulng.raw);
      MotorEvArgs rurtn(RURTN, 122, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rurtn.raw);
      MotorEvArgs lllng(LLLNG, 107, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lllng.raw);
      MotorEvArgs llrtn(LLRTN, 132, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, llrtn.raw);
      MotorEvArgs rllng(RLLNG, 132, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rllng.raw);
      MotorEvArgs rlrtn(RLRTN, 107, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rlrtn.raw);

      MotorEvArgs galng(GALNG, 127, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, galng.raw);
      MotorEvArgs gartn(GARTN, 127, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, gartn.raw);
    }
    else if (preSetTimer >= 3000) {

      static const uint8_t PROGMEM STEPSZ = 8;
      float targetDeltaSmall = 10 * sin((preSetTimer - 3000) / 3000.0 * PI);
      float targetDeltaLarge = 40 * sin((preSetTimer - 3000) / 3000.0 * PI);
      float targetGating = 127 + 80 * sin((preSetTimer - 3000) / 3000.0 * PI);

      MotorEvArgs lulng(LULNG, 147 + targetDeltaLarge, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lulng.raw);
      MotorEvArgs lurtn(LURTN, 147 + targetDeltaLarge, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lurtn.raw);
      MotorEvArgs rulng(RULNG, 122 - targetDeltaSmall, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rulng.raw);
      MotorEvArgs rurtn(RURTN, 122 - targetDeltaSmall, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rurtn.raw);
      MotorEvArgs lllng(LLLNG, 107 - targetDeltaLarge, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lllng.raw);
      MotorEvArgs llrtn(LLRTN, 132 + targetDeltaSmall, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, llrtn.raw);
      MotorEvArgs rllng(RLLNG, 132 + targetDeltaSmall, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rllng.raw);
      MotorEvArgs rlrtn(RLRTN, 107 - targetDeltaLarge, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rlrtn.raw);

      MotorEvArgs galng(GALNG, targetGating, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, galng.raw);
    }

    if (preSetTimer == 8960) {
      preSetTimer = 3000;
    }
    else {
      preSetTimer += PRESETTIMERINCR;
    }
}

void prog7(QMActive* recv, uint16_t& preSetTimer) {
    // Free-breath Gating loosing signal

    if (preSetTimer == 0) {
      static const uint8_t PROGMEM STEPSZ = 2;
      MotorEvArgs lulng(LULNG, 127, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lulng.raw);
      MotorEvArgs lurtn(LURTN, 127, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lurtn.raw);
      MotorEvArgs rulng(RULNG, 127, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rulng.raw);
      MotorEvArgs rurtn(RURTN, 127, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rurtn.raw);
      MotorEvArgs lllng(LLLNG, 127, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lllng.raw);
      MotorEvArgs llrtn(LLRTN, 127, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, llrtn.raw);
      MotorEvArgs rllng(RLLNG, 127, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rllng.raw);
      MotorEvArgs rlrtn(RLRTN, 127, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rlrtn.raw);
      MotorEvArgs galng(GALNG, 127, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, galng.raw);
      MotorEvArgs gartn(GARTN, 127, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, gartn.raw);
    }
    else if (preSetTimer >= 3000) {
      static const uint8_t PROGMEM STEPSZ = 8;
      float target = 127 + 80 * sin((preSetTimer - 3000) / 2500.0 * PI);

      MotorEvArgs lulng(LULNG, target, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lulng.raw);
      MotorEvArgs rulng(RULNG, target, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rulng.raw);
      MotorEvArgs lllng(LLLNG, target, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lllng.raw);
      MotorEvArgs rllng(RLLNG, target, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rllng.raw);
      MotorEvArgs galng(GALNG, target, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, galng.raw);
    }

    if (preSetTimer == 25000) {
      static const uint8_t PROGMEM STEPSZ = 3;

      MotorEvArgs gartn(GARTN, 255, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, gartn.raw);
    }
    else if (preSetTimer == 35000) {
      static const uint8_t PROGMEM STEPSZ = 3;

      MotorEvArgs gartn(GARTN, 127, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, gartn.raw);
    }

    if (preSetTimer == 37960) {
      preSetTimer = 3000;
    }
    else {
      preSetTimer += PRESETTIMERINCR;
    }
}

void prog8(QMActive* recv, uint16_t& preSetTimer) {
    // Free-breath Gating base line shift

    if (preSetTimer == 0) {
      static const uint8_t PROGMEM STEPSZ = 2;
      MotorEvArgs lulng(LULNG, 130, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lulng.raw);
      MotorEvArgs lurtn(LURTN, 127, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lurtn.raw);
      MotorEvArgs rulng(RULNG, 130, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rulng.raw);
      MotorEvArgs rurtn(RURTN, 127, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rurtn.raw);
      MotorEvArgs lllng(LLLNG, 130, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lllng.raw);
      MotorEvArgs llrtn(LLRTN, 127, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, llrtn.raw);
      MotorEvArgs rllng(RLLNG, 130, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rllng.raw);
      MotorEvArgs rlrtn(RLRTN, 127, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rlrtn.raw);
      MotorEvArgs galng(GALNG, 130, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, galng.raw);
      MotorEvArgs gartn(GARTN, 127, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, gartn.raw);
    }
    else if (preSetTimer >= 3000) {
      static const uint8_t PROGMEM STEPSZ = 8;
      float baseline = 130 + 30 * sin((preSetTimer - 3000) / 30000.0 * PI);
      float target = baseline + 50 * sin((preSetTimer - 3000) / 3000.0 * PI);

      MotorEvArgs lulng(LLNG, target, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lulng.raw);
      MotorEvArgs rulng(RLNG, target, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rulng.raw);
      MotorEvArgs lllng(LLNG, target, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lllng.raw);
      MotorEvArgs rllng(RLNG, target, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rllng.raw);
      MotorEvArgs galng(GLNG, target, STEPSZ);
      QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, galng.raw);
    }

    if (preSetTimer == 62960) {
      preSetTimer = 3000;
    }
    else {
      preSetTimer += PRESETTIMERINCR;
    }
}

#elif NO2

void prog1(QMActive* recv, uint16_t& preSetTimer) {
  //  Position 1
  static const uint8_t PROGMEM STEPSZ = 2;

  MotorEvArgs llng(LLNG, 128, STEPSZ);
  QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, llng.raw);
  MotorEvArgs rlng(RLNG, 128, STEPSZ);
  QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rlng.raw);
  MotorEvArgs glng(GLNG, 128, STEPSZ);
  QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, glng.raw);
  MotorEvArgs lrtn(LRTN, 127, STEPSZ);
  QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lrtn.raw);
  MotorEvArgs rrtn(RRTN, 127, STEPSZ);
  QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rrtn.raw);
  MotorEvArgs grtn(GRTN, 127, STEPSZ);
  QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, grtn.raw);
}

void prog2(QMActive* recv, uint16_t& preSetTimer) {
  //  Position 2
  static const uint8_t PROGMEM STEPSZ = 2;

  MotorEvArgs llng(LLNG, 0, STEPSZ);
  QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, llng.raw);
  MotorEvArgs rlng(RLNG, 40, STEPSZ);
  QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rlng.raw);
  MotorEvArgs glng(GLNG, 0, STEPSZ);
  QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, glng.raw);
  MotorEvArgs lrtn(LRTN, 255, STEPSZ);
  QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lrtn.raw);
  MotorEvArgs rrtn(RRTN, 79, STEPSZ);
  QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rrtn.raw);
  MotorEvArgs grtn(GRTN, 135, STEPSZ);
  QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, grtn.raw);
}

void prog3(QMActive* recv, uint16_t& preSetTimer) {
  // Position 1 <-> 2

  if (preSetTimer == 0) {
    static const uint8_t PROGMEM STEPSZ = 2;
    MotorEvArgs llng(LLNG, 64, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, llng.raw);
    MotorEvArgs rlng(RLNG, 84, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rlng.raw);
    MotorEvArgs glng(GLNG, 127, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, glng.raw);
    MotorEvArgs lrtn(LRTN, 191, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lrtn.raw);
    MotorEvArgs rrtn(RRTN, 103, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rrtn.raw);
    MotorEvArgs grtn(GRTN, 127, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, grtn.raw);
  }
  else if (preSetTimer >= 3000) {
    static const uint8_t PROGMEM STEPSZ = 8;

    float targetLlng = 64 - 64 * sin((preSetTimer - 3000) / 3000.0 * PI);
    float targetRlng =  84 - 44 * sin((preSetTimer - 3000) / 3000.0 * PI);
    float targetLrtn = 191 + 64 * sin((preSetTimer - 3000) / 3000.0 * PI);
    float targetRrtn = 103 - 24 * sin((preSetTimer - 3000) / 3000.0 * PI);

    MotorEvArgs llng(LLNG, targetLlng, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, llng.raw);
    MotorEvArgs rlng(RLNG, targetRlng, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rlng.raw);
    MotorEvArgs lrtn(LRTN, targetLrtn, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lrtn.raw);
    MotorEvArgs rrtn(RRTN, targetRrtn, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rrtn.raw);
  }

  if (preSetTimer == 8960) {
    preSetTimer = 3000;
  }
  else {
    preSetTimer += PRESETTIMERINCR;
  }
}

void prog4(QMActive* recv, uint16_t& preSetTimer) {
  // Free-breath Gating

  if (preSetTimer == 0) {
    static const uint8_t PROGMEM STEPSZ = 2;
    MotorEvArgs llng(LLNG, 127, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, llng.raw);
    MotorEvArgs rlng(RLNG, 127, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rlng.raw);
    MotorEvArgs glng(GLNG, 127, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, glng.raw);
    MotorEvArgs lrtn(LRTN, 127, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lrtn.raw);
    MotorEvArgs rrtn(RRTN, 127, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rrtn.raw);
    MotorEvArgs grtn(GRTN, 127, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, grtn.raw);
  }
  else if (preSetTimer >= 3000) {
    static const uint8_t PROGMEM STEPSZ = 8;

    float target = 127 + 80 * sin((preSetTimer - 3000) / 2500.0 * PI);

    MotorEvArgs lllng(LLNG, target, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lllng.raw);
    MotorEvArgs rllng(RLNG, target, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rllng.raw);
    MotorEvArgs galng(GLNG, target, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, galng.raw);
  }

  if (preSetTimer == 7960) {
    preSetTimer = 3000;
  }
  else {
    preSetTimer += PRESETTIMERINCR;
  }
}

void prog5(QMActive* recv, uint16_t& preSetTimer) {
  // Breath-hold Gating

  if (preSetTimer == 0) {
    static const uint8_t PROGMEM STEPSZ = 2;
    MotorEvArgs llng(LLNG, 60, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, llng.raw);
    MotorEvArgs rlng(RLNG, 60, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rlng.raw);
    MotorEvArgs glng(GLNG, 60, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, glng.raw);
    MotorEvArgs lrtn(LRTN, 127, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lrtn.raw);
    MotorEvArgs rrtn(RRTN, 127, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rrtn.raw);
    MotorEvArgs grtn(GRTN, 127, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, grtn.raw);
  }
  else if (preSetTimer >= 3000 && preSetTimer < 28000) {
    static const uint8_t PROGMEM STEPSZ = 8;
    float target = 60 + 50 * sin((preSetTimer - 3000) / 2500.0 * PI);
    MotorEvArgs llng(LLNG, target, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, llng.raw);
    MotorEvArgs rlng(RLNG, target, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rlng.raw);
    MotorEvArgs glng(GLNG, target, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, glng.raw);
  }
  else if (preSetTimer > 28000 && preSetTimer < 38000) {
    static const uint8_t PROGMEM STEPSZ = 4;
    float target = 200 + 50 * cos((preSetTimer - 28000) / 40000.0 * PI);
    MotorEvArgs llng(LLNG, target, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, llng.raw);
    MotorEvArgs rlng(RLNG, target, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rlng.raw);
    MotorEvArgs glng(GLNG, target, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, glng.raw);
  }
  else if (preSetTimer == 38000) {
    static const uint8_t PROGMEM STEPSZ = 4;
    MotorEvArgs llng(LLNG, 10, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, llng.raw);
    MotorEvArgs rlng(RLNG, 10, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rlng.raw);
    MotorEvArgs glng(GLNG, 10, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, glng.raw);
  }

  if (preSetTimer == 40000) {
    preSetTimer = 6720;
  }
  else {
    preSetTimer += PRESETTIMERINCR;
  }
}

void prog6(QMActive* recv, uint16_t& preSetTimer) {
  // Free-breath Gating, Position 1 <-> 2

  if (preSetTimer == 0) {
    static const uint8_t PROGMEM STEPSZ = 2;
    MotorEvArgs llng(LLNG, 64, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, llng.raw);
    MotorEvArgs rlng(RLNG, 84, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rlng.raw);
    MotorEvArgs glng(GLNG, 64, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, glng.raw);
    MotorEvArgs lrtn(LRTN, 191, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lrtn.raw);
    MotorEvArgs rrtn(RRTN, 103, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rrtn.raw);
    MotorEvArgs grtn(GRTN, 131, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, grtn.raw);
  }
  else if (preSetTimer >= 3000) {
    static const uint8_t PROGMEM STEPSZ = 8;

    float targetLGlng = 64 - 64 * sin((preSetTimer - 3000) / 3000.0 * PI);
    float targetRlng =  84 - 44 * sin((preSetTimer - 3000) / 3000.0 * PI);
    float targetLrtn = 191 + 64 * sin((preSetTimer - 3000) / 3000.0 * PI);
    float targetRrtn = 103 - 24 * sin((preSetTimer - 3000) / 3000.0 * PI);
    float targetGrtn = 131 +  4 * sin((preSetTimer - 3000) / 3000.0 * PI);

    MotorEvArgs llng(LLNG, targetLGlng, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, llng.raw);
    MotorEvArgs rlng(RLNG, targetRlng, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rlng.raw);
    MotorEvArgs glng(GLNG, targetLGlng, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, glng.raw);
    MotorEvArgs lrtn(LRTN, targetLrtn, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lrtn.raw);
    MotorEvArgs rrtn(RRTN, targetRrtn, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rrtn.raw);
    MotorEvArgs grtn(GRTN, targetGrtn, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, grtn.raw);
  }

  if (preSetTimer == 8960) {
    preSetTimer = 3000;
  }
  else {
    preSetTimer += PRESETTIMERINCR;
  }
}

void prog7(QMActive* recv, uint16_t& preSetTimer) {
  // Free-breath Gating loosing signal

  if (preSetTimer == 0) {
    static const uint8_t PROGMEM STEPSZ = 2;
    MotorEvArgs llng(LLNG, 127, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, llng.raw);
    MotorEvArgs lrtn(LRTN, 127, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lrtn.raw);
    MotorEvArgs rlng(RLNG, 127, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rlng.raw);
    MotorEvArgs rrtn(RRTN, 127, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rrtn.raw);
    MotorEvArgs glng(GLNG, 127, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, glng.raw);
    MotorEvArgs grtn(GRTN, 127, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, grtn.raw);
  }
  else if (preSetTimer >= 3000) {
    static const uint8_t PROGMEM STEPSZ = 8;
    float target = 127 + 80 * sin((preSetTimer - 3000) / 2500.0 * PI);

    MotorEvArgs llng(LLNG, target, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, llng.raw);
    MotorEvArgs rlng(RLNG, target, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rlng.raw);
    MotorEvArgs glng(GLNG, target, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, glng.raw);
  }

  if (preSetTimer == 25000) {
    static const uint8_t PROGMEM STEPSZ = 3;
    MotorEvArgs grtn(GRTN, 255, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, grtn.raw);
  }
  else if (preSetTimer == 35000) {
    static const uint8_t PROGMEM STEPSZ = 3;
    MotorEvArgs grtn(GRTN, 127, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, grtn.raw);
  }

  if (preSetTimer == 37960) {
    preSetTimer = 3000;
  }
  else {
    preSetTimer += PRESETTIMERINCR;
  }
}

void prog8(QMActive* recv, uint16_t& preSetTimer) {
  // Free-breath Gating base line shift

  if (preSetTimer == 0) {
    static const uint8_t PROGMEM STEPSZ = 2;
    MotorEvArgs llng(LLNG, 130, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, llng.raw);
    MotorEvArgs lrtn(LRTN, 127, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, lrtn.raw);
    MotorEvArgs rlng(RLNG, 130, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rlng.raw);
    MotorEvArgs rrtn(RRTN, 127, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rrtn.raw);
    MotorEvArgs glng(GLNG, 130, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, glng.raw);
    MotorEvArgs grtn(GRTN, 127, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, grtn.raw);
  }
  else if (preSetTimer >= 3000) {
    static const uint8_t PROGMEM STEPSZ = 8;
    float baseline = 130 + 30 * sin((preSetTimer - 3000) / 30000.0 * PI);
    float target = baseline + 50 * sin((preSetTimer - 3000) / 3000.0 * PI);
    MotorEvArgs llng(LLNG, target, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, llng.raw);
    MotorEvArgs rlng(RLNG, target, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, rlng.raw);
    MotorEvArgs glng(GLNG, target, STEPSZ);
    QACTIVE_POST(recv, MOTOR_MOVE_ASOLUTE_SIG, glng.raw);
  }

  if (preSetTimer == 62960) {
    preSetTimer = 3000;
  }
  else {
    preSetTimer += PRESETTIMERINCR;
  }
}

#endif


#endif