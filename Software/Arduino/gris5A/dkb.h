/*$file${.::dkb.h} #########################################################*/
/*
* Model: gris5A.qm
* File:  ${.::dkb.h}
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
/*$endhead${.::dkb.h} ######################################################*/
/* dkb.h - Arduino software for the GRIS5A (C) motion phantom
 * Copyright (C) 2019 by Stefan Grimm
 */

#ifndef __DKB_H
#define __DKB_H

#include "qpn.h"
#include "events.h"

struct DKbInEvArgs {
  DKbInEvArgs () : raw(0) {}
  DKbInEvArgs (const DKbInEvArgs& t) : raw(t.raw) {}
  DKbInEvArgs (uint32_t rawValue) : raw(rawValue) {}
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
      uint32_t FRM : 1;
      uint32_t FPS : 1;
      uint32_t FMM : 1;
      uint32_t FCA : 1;
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
};

bool isDKbEnabled() {
  #ifdef DKBIN
  return true;
  #else
  return false;
  #endif
}

void printDKbInData(const DKbInEvArgs& s);

#ifdef GRIS5A
void processDKb(QMActive* recv, const DKbInEvArgs& dkb)
{
  // Debug: Serial.println(dkb.raw, BIN);
  if (dkb.GAL) {
    MotorMoveRelativeEvArgs args(GARTN, FORWARDS);
    QACTIVE_POST(recv, MOTOR_MOVE_RELATIVE_SIG, args.raw);
  }
  if (dkb.GAT) {
    MotorMoveRelativeEvArgs args(GALNG, FORWARDS);
    QACTIVE_POST(recv, MOTOR_MOVE_RELATIVE_SIG, args.raw);
  }
  if (dkb.GAB) {
    MotorMoveRelativeEvArgs args(GALNG, BACKWARDS);
    QACTIVE_POST(recv, MOTOR_MOVE_RELATIVE_SIG, args.raw);
  }
  if (dkb.GAR) {
    MotorMoveRelativeEvArgs args(GARTN, BACKWARDS);
    QACTIVE_POST(recv, MOTOR_MOVE_RELATIVE_SIG, args.raw);
  }
  if (dkb.FP7) {
    ProgramChangeEvArgs args(7);
    QACTIVE_POST(recv, PROGRAM_CHANGE_SIG, args.raw);
  }
  if (dkb.FP6) {
    ProgramChangeEvArgs args(6);
    QACTIVE_POST(recv, PROGRAM_CHANGE_SIG, args.raw);
  }
  if (dkb.FP5) {
    ProgramChangeEvArgs args(5);
    QACTIVE_POST(recv, PROGRAM_CHANGE_SIG, args.raw);
  }
  if (dkb.FP8) {
    ProgramChangeEvArgs args(8);
    QACTIVE_POST(recv, PROGRAM_CHANGE_SIG, args.raw);
  }
  if (dkb.RLB) {
    MotorMoveRelativeEvArgs args(RLLNG, BACKWARDS);
    QACTIVE_POST(recv, MOTOR_MOVE_RELATIVE_SIG, args.raw);
  }
  if (dkb.RLR) {
    MotorMoveRelativeEvArgs args(RLRTN, FORWARDS);
    QACTIVE_POST(recv, MOTOR_MOVE_RELATIVE_SIG, args.raw);
  }
  if (dkb.RLL) {
    MotorMoveRelativeEvArgs args(RLRTN, BACKWARDS);
    QACTIVE_POST(recv, MOTOR_MOVE_RELATIVE_SIG, args.raw);
  }
  if (dkb.RLT) {
    MotorMoveRelativeEvArgs args(RLLNG, FORWARDS);
    QACTIVE_POST(recv, MOTOR_MOVE_RELATIVE_SIG, args.raw);
  }
  if (dkb.RUR) {
    MotorMoveRelativeEvArgs args(RURTN, FORWARDS);
    QACTIVE_POST(recv, MOTOR_MOVE_RELATIVE_SIG, args.raw);
  }
  if (dkb.RUL) {
    MotorMoveRelativeEvArgs args(RURTN, BACKWARDS);
    QACTIVE_POST(recv, MOTOR_MOVE_RELATIVE_SIG, args.raw);
  }
  if (dkb.RUT) {
    MotorMoveRelativeEvArgs args(RULNG, FORWARDS);
    QACTIVE_POST(recv, MOTOR_MOVE_RELATIVE_SIG, args.raw);
  }
  if (dkb.RUB) {
    MotorMoveRelativeEvArgs args(RULNG, BACKWARDS);
    QACTIVE_POST(recv, MOTOR_MOVE_RELATIVE_SIG, args.raw);
  }
  if (dkb.FP4) {
    ProgramChangeEvArgs args(4);
    QACTIVE_POST(recv, PROGRAM_CHANGE_SIG, args.raw);
  }
  if (dkb.FP3) {
    ProgramChangeEvArgs args(3);
    QACTIVE_POST(recv, PROGRAM_CHANGE_SIG, args.raw);
  }
  if (dkb.FP2) {
    ProgramChangeEvArgs args(2);
    QACTIVE_POST(recv, PROGRAM_CHANGE_SIG, args.raw);
  }
  if (dkb.FP1) {
    ProgramChangeEvArgs args(1);
    QACTIVE_POST(recv, PROGRAM_CHANGE_SIG, args.raw);
  }
  if (dkb.FRM) {
    QACTIVE_POST(recv, REMOTE_MODE_SIG, 0L);
  }
  if (dkb.FPS) {
    QACTIVE_POST(recv, PRESET_MODE_SIG, 0L);
  }
  if (dkb.FMM) {
    QACTIVE_POST(recv, MANUAL_MOTION_MODE_SIG, 0L);
  }
  if (dkb.FCA) {
    QACTIVE_POST(recv, CALIBRATION_MODE_SIG, 0L);
  }
  if (dkb.LLB) {
    MotorMoveRelativeEvArgs args(LLLNG, BACKWARDS);
    QACTIVE_POST(recv, MOTOR_MOVE_RELATIVE_SIG, args.raw);
  }
  if (dkb.LLR) {
    MotorMoveRelativeEvArgs args(LLRTN, FORWARDS);
    QACTIVE_POST(recv, MOTOR_MOVE_RELATIVE_SIG, args.raw);
  }
  if (dkb.LLL) {
    MotorMoveRelativeEvArgs args(LLRTN, BACKWARDS);
    QACTIVE_POST(recv, MOTOR_MOVE_RELATIVE_SIG, args.raw);
  }
  if (dkb.LLT) {
    MotorMoveRelativeEvArgs args(LLLNG, FORWARDS);
    QACTIVE_POST(recv, MOTOR_MOVE_RELATIVE_SIG, args.raw);
  }
  if (dkb.LUR) {
    MotorMoveRelativeEvArgs args(LURTN, FORWARDS);
    QACTIVE_POST(recv, MOTOR_MOVE_RELATIVE_SIG, args.raw);
  }
  if (dkb.LUL) {
    MotorMoveRelativeEvArgs args(LURTN, BACKWARDS);
    QACTIVE_POST(recv, MOTOR_MOVE_RELATIVE_SIG, args.raw);
  }
  if (dkb.LUT) {
    MotorMoveRelativeEvArgs args(LULNG, FORWARDS);
    QACTIVE_POST(recv, MOTOR_MOVE_RELATIVE_SIG, args.raw);
  }
  if (dkb.LUB) {
    MotorMoveRelativeEvArgs args(LULNG, BACKWARDS);
    QACTIVE_POST(recv, MOTOR_MOVE_RELATIVE_SIG, args.raw);
  }
  //printDKbInData(dkb);
}
#elif NO2
void processDKb(QMActive* recv, const DKbInEvArgs& dkb)
{
  if (dkb.GAL) {
    MotorMoveRelativeEvArgs args(GARTN, BACKWARDS);
    QACTIVE_POST(recv, MOTOR_MOVE_RELATIVE_SIG, args.raw);
  }
  if (dkb.GAT) {
    MotorMoveRelativeEvArgs args(GALNG, FORWARDS);
    QACTIVE_POST(recv, MOTOR_MOVE_RELATIVE_SIG, args.raw);
  }
  if (dkb.GAB) {
    MotorMoveRelativeEvArgs args(GALNG, BACKWARDS);
    QACTIVE_POST(recv, MOTOR_MOVE_RELATIVE_SIG, args.raw);
  }
  if (dkb.GAR) {
    MotorMoveRelativeEvArgs args(GARTN, FORWARDS);
    QACTIVE_POST(recv, MOTOR_MOVE_RELATIVE_SIG, args.raw);
  }
  if (dkb.FP7) {
    ProgramChangeEvArgs args(7);
    QACTIVE_POST(recv, PROGRAM_CHANGE_SIG, args.raw);
  }
  if (dkb.FP6) {
    ProgramChangeEvArgs args(6);
    QACTIVE_POST(recv, PROGRAM_CHANGE_SIG, args.raw);
  }
  if (dkb.FP5) {
    ProgramChangeEvArgs args(5);
    QACTIVE_POST(recv, PROGRAM_CHANGE_SIG, args.raw);
  }
  if (dkb.FP8) {
    ProgramChangeEvArgs args(8);
    QACTIVE_POST(recv, PROGRAM_CHANGE_SIG, args.raw);
  }
  if (dkb.RLB) {
    MotorMoveRelativeEvArgs args(RLLNG, BACKWARDS);
    QACTIVE_POST(recv, MOTOR_MOVE_RELATIVE_SIG, args.raw);
  }
  if (dkb.RLR) {
    MotorMoveRelativeEvArgs args(RLRTN, FORWARDS);
    QACTIVE_POST(recv, MOTOR_MOVE_RELATIVE_SIG, args.raw);
  }
  if (dkb.RLL) {
    MotorMoveRelativeEvArgs args(RLRTN, BACKWARDS);
    QACTIVE_POST(recv, MOTOR_MOVE_RELATIVE_SIG, args.raw);
  }
  if (dkb.RLT) {
    MotorMoveRelativeEvArgs args(RLLNG, FORWARDS);
    QACTIVE_POST(recv, MOTOR_MOVE_RELATIVE_SIG, args.raw);
  }
  if (dkb.FP4) {
    ProgramChangeEvArgs args(4);
    QACTIVE_POST(recv, PROGRAM_CHANGE_SIG, args.raw);
  }
  if (dkb.FP3) {
    ProgramChangeEvArgs args(3);
    QACTIVE_POST(recv, PROGRAM_CHANGE_SIG, args.raw);
  }
  if (dkb.FP2) {
    ProgramChangeEvArgs args(2);
    QACTIVE_POST(recv, PROGRAM_CHANGE_SIG, args.raw);
  }
  if (dkb.FP1) {
    ProgramChangeEvArgs args(1);
    QACTIVE_POST(recv, PROGRAM_CHANGE_SIG, args.raw);
  }
  if (dkb.FRM) {
    QACTIVE_POST(recv, REMOTE_MODE_SIG, 0L);
  }
  if (dkb.FPS) {
    QACTIVE_POST(recv, PRESET_MODE_SIG, 0L);
  }
  if (dkb.FMM) {
    QACTIVE_POST(recv, MANUAL_MOTION_MODE_SIG, 0L);
  }
  if (dkb.FCA) {
    QACTIVE_POST(recv, CALIBRATION_MODE_SIG, 0L);
  }
  if (dkb.LLB) {
    MotorMoveRelativeEvArgs args(LLLNG, BACKWARDS);
    QACTIVE_POST(recv, MOTOR_MOVE_RELATIVE_SIG, args.raw);
  }
  if (dkb.LLR) {
    MotorMoveRelativeEvArgs args(LLRTN, FORWARDS);
    QACTIVE_POST(recv, MOTOR_MOVE_RELATIVE_SIG, args.raw);
  }
  if (dkb.LLL) {
    MotorMoveRelativeEvArgs args(LLRTN, BACKWARDS);
    QACTIVE_POST(recv, MOTOR_MOVE_RELATIVE_SIG, args.raw);
  }
  if (dkb.LLT) {
    MotorMoveRelativeEvArgs args(LLLNG, FORWARDS);
    QACTIVE_POST(recv, MOTOR_MOVE_RELATIVE_SIG, args.raw);
  }
  printDKbInData(dkb);
}
#endif



void processSoftDKb(QMActive* recv, const SerialInEvArgs& softdkb)
{
  int bitToSet = softdkb.Data;
  uint32_t dkbRaw = 0;
  bitSet(dkbRaw, bitToSet);
  DKbInEvArgs dkb(dkbRaw);
  processDKb(recv, dkb);
}

void printDKbInData(const DKbInEvArgs& s) {
  if (s.FMM) { Serial.println(F("Pressed Function Manual Motion")); }
  if (s.FPS) { Serial.println(F("Pressed Function Pre-Set")); }
  if (s.FRM) { Serial.println(F("Pressed Function Remote")); }
  if (s.FP2) { Serial.println(F("Pressed Function Program 2")); }
  if (s.FP1) { Serial.println(F("Pressed Function Program 1")); }
  if (s.FCA) { Serial.println(F("Pressed Function Calibration")); }
  if (s.FP3) { Serial.println(F("Pressed Function Program 3")); }
  if (s.FP4) { Serial.println(F("Pressed Function Program 4")); }
  if (s.LUT) { Serial.println(F("Pressed Left Upper Top")); }
  if (s.LUR) { Serial.println(F("Pressed Left Upper Right")); }
  if (s.LUL) { Serial.println(F("Pressed Left Upper Left")); }
  if (s.LLL) { Serial.println(F("Pressed Left Lower Left")); }
  if (s.LUB) { Serial.println(F("Pressed Left Upper Bottom")); }
  if (s.LLT) { Serial.println(F("Pressed Left Lower Top")); }
  if (s.LLR) { Serial.println(F("Pressed Left Lower Right")); }
  if (s.LLB) { Serial.println(F("Pressed Left Lower Bottom")); }
  if (s.RUT) { Serial.println(F("Pressed Right Upper Top")); }
  if (s.RUR) { Serial.println(F("Pressed Right Upper Right")); }
  if (s.RUL) { Serial.println(F("Pressed Right Upper Left")); }
  if (s.RUB) { Serial.println(F("Pressed Right Upper Bottom")); }
  if (s.RLT) { Serial.println(F("Pressed Right Lower Top")); }
  if (s.RLL) { Serial.println(F("Pressed Right Lower Left")); }
  if (s.RLB) { Serial.println(F("Pressed Right Lower Bottom")); }
  if (s.RLR) { Serial.println(F("Pressed Right Lower Right")); }
  if (s.FP8) { Serial.println(F("Pressed Function Program 8")); }
  if (s.GAT) { Serial.println(F("Pressed Gating Top")); }
  if (s.GAR) { Serial.println(F("Pressed Gating Right")); }
  if (s.GAL) { Serial.println(F("Pressed Gating Left")); }
  if (s.FP5) { Serial.println(F("Pressed Function Program 5")); }
  if (s.FP6) { Serial.println(F("Pressed Function Program 6")); }
  if (s.FP7) { Serial.println(F("Pressed Function Program 7")); }
  if (s.GAB) { Serial.println(F("Pressed Gating Bottom")); }
}

#endif