/* No2.java - Dedicated Keyboard Processing 3.0 software for the GRIS5A (C) motion phantom
 * Copyright (C) 2020 by Stefan Grimm
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
 * along with the SoftDKb software.  If not, see
 * <http://www.gnu.org/licenses/>.
 */
 
import processing.serial.*;

class No2 implements IPhantom {

  public static final int NUMSERVOS = 6;
 
  private int[] _positionBuffer = new int[NUMSERVOS];
  SoftDKb _parent;
  private int _ctrlX, _ctrlY, _stateX, _stateY;
  
  public No2(SoftDKb parent, int ctrlX, int ctrlY, int stateX, int stateY) {
    _parent = parent;
    _ctrlX = ctrlX;
    _ctrlY = ctrlY;
    _stateX = stateX;
    _stateY = stateY;
  }
  
  public boolean isPresentation(int model) {
    return 0 < model && model < 4;
  }
    
  public void setMotionState(int motor, int value) {
    if (motor >= 0 && motor < NUMSERVOS) {  
      _positionBuffer[motor] = value;
    }
  }
  
  public void drawMotionControls() {
    // Left
    _parent.rect(150, 250, 40, 40);
    _parent.rect(100, 300, 40, 40);
    _parent.rect(200, 300, 40, 40);
    _parent.rect(150, 350, 40, 40);
    
    // Right
    _parent.rect(350, 250, 40, 40);
    _parent.rect(300, 300, 40, 40);
    _parent.rect(400, 300, 40, 40);
    _parent.rect(350, 350, 40, 40);
  
    // Gating
    _parent.rect(550, 250, 40, 40);
    _parent.rect(500, 300, 40, 40);
    _parent.rect(600, 300, 40, 40);
    _parent.rect(550, 350, 40, 40);
  }
  
  public void drawMotionState() {
    for (int n=0; n<NUMSERVOS; n++) {
      _parent.text(n, _stateX, _stateY + n * 50);
      _parent.text(_positionBuffer[n], 800, 300 + n*50);
    }
  }
  
  public void onMousePressed(int x, int y) {
    // Example: (31<<3) | 0x1 = 11111 << 3 | 001 = 11111001
    if      (mousePressedGL(x, y)) { _parent.comPort.write(( 0<<3) | 0x1); _parent.sendBuffer = "0x1 0000 0000 0000 0000 0000 0000 0000 0001"; }
    else if (mousePressedGT(x, y)) { _parent.comPort.write(( 1<<3) | 0x1); _parent.sendBuffer = "0x1 0000 0000 0000 0000 0000 0000 0000 0010"; } 
    else if (mousePressedGB(x, y)) { _parent.comPort.write(( 2<<3) | 0x1); _parent.sendBuffer = "0x1 0000 0000 0000 0000 0000 0000 0000 0100"; }
    else if (mousePressedGR(x, y)) { _parent.comPort.write(( 3<<3) | 0x1); _parent.sendBuffer = "0x1 0000 0000 0000 0000 0000 0000 0000 1000"; }
    else if (mousePressedRB(x, y)) { _parent.comPort.write(( 8<<3) | 0x1); _parent.sendBuffer = "0x1 0000 0000 0000 0000 0000 0001 0000 0000"; }
    else if (mousePressedRR(x, y)) { _parent.comPort.write(( 9<<3) | 0x1); _parent.sendBuffer = "0x1 0000 0000 0000 0000 0000 0010 0000 0000"; } 
    else if (mousePressedRL(x, y)) { _parent.comPort.write((10<<3) | 0x1); _parent.sendBuffer = "0x1 0000 0000 0000 0000 0000 0100 0000 0000"; }
    else if (mousePressedRT(x, y)) { _parent.comPort.write((11<<3) | 0x1); _parent.sendBuffer = "0x1 0000 0000 0000 0000 0000 1000 0000 0000"; }    
    else if (mousePressedLB(x, y)) { _parent.comPort.write((24<<3) | 0x1); _parent.sendBuffer = "0x1 0000 0001 0000 0000 0000 0000 0000 0000"; }
    else if (mousePressedLR(x, y)) { _parent.comPort.write((25<<3) | 0x1); _parent.sendBuffer = "0x1 0000 0010 0000 0000 0000 0000 0000 0000"; } 
    else if (mousePressedLL(x, y)) { _parent.comPort.write((26<<3) | 0x1); _parent.sendBuffer = "0x1 0000 0100 0000 0000 0000 0000 0000 0000"; }
    else if (mousePressedLT(x, y)) { _parent.comPort.write((27<<3) | 0x1); _parent.sendBuffer = "0x1 0000 1000 0000 0000 0000 0000 0000 0000"; } 
  }
  
  boolean mousePressedLT(int x, int y) { return ((x >= 150) && (x <= 190) && (y >= 250) && (y <= 390)); }
  boolean mousePressedLL(int x, int y) { return ((x >= 100) && (x <= 140) && (y >= 300) && (y <= 440)); }
  boolean mousePressedLR(int x, int y) { return ((x >= 200) && (x <= 240) && (y >= 300) && (y <= 440)); }
  boolean mousePressedLB(int x, int y) { return ((x >= 150) && (x <= 190) && (y >= 350) && (y <= 490)); }

  boolean mousePressedRT(int x, int y) { return ((x >= 350) && (x <= 390) && (y >= 250) && (y <= 390)); }
  boolean mousePressedRL(int x, int y) { return ((x >= 300) && (x <= 340) && (y >= 300) && (y <= 440)); }
  boolean mousePressedRR(int x, int y) { return ((x >= 400) && (x <= 440) && (y >= 300) && (y <= 440)); }
  boolean mousePressedRB(int x, int y) { return ((x >= 350) && (x <= 390) && (y >= 350) && (y <= 490)); }

  boolean mousePressedGT(int x, int y) { return ((x >= 550) && (x <= 590) && (y >= 250) && (y <= 290)); }
  boolean mousePressedGL(int x, int y) { return ((x >= 500) && (x <= 540) && (y >= 300) && (y <= 340)); }
  boolean mousePressedGR(int x, int y) { return ((x >= 600) && (x <= 640) && (y >= 300) && (y <= 340)); }
  boolean mousePressedGB(int x, int y) { return ((x >= 550) && (x <= 590) && (y >= 350) && (y <= 390)); }

}
