/* SoftDKb.pde - Dedicated Keyboard Processing 3.0 software for the GRIS5A (C) motion phantom 
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
 * GNU General Public License for more details.

 * You should have received a copy of the GNU Lesser General Public License
 * along with the SoftDKb software.  If not, see
 * <http://www.gnu.org/licenses/>.
 */

import processing.serial.*;

Serial comPort;
int sendBuffer = 32;
int[] recvBuffer = new int[10];
int readingPos = 0;
boolean receiving;

int[] positionBuffer = new int[10];
boolean led1 = true;
boolean led2 = false;
boolean led3 = false;
boolean led4 = false;

void setup() {
  
  size(1200, 800);
  
  // Debug : printArray(PFont.list());
  PFont font = createFont(PFont.list()[2], 34);
  textFont(font);
  
  // List all the available serial ports:
  String[] comPorts = Serial.list();
  printArray(comPorts); //<>//
  if (comPorts.length > 0) {
    String portName = Serial.list()[0];
    comPort = new Serial(this, portName, 9600);
  }
  else {
    println("No device found");
  }
}

void draw() {
  background(0);
  
  // Output LEDs
  if (led1) fill(color(0, 255, 0));
  else fill(200);
  rect(600, 150, 40, 40);
  if (led2) fill(color(0, 0, 255));
  else fill(200);
  rect(650, 150, 40, 40);
  if (led3) fill(color(255, 0, 0));
  else fill(200);
  rect(700, 150, 40, 40);
  if (led4) fill(color(255, 255, 0));
  else fill(200);
  rect(700, 200, 40, 40);
  
  // Function keys
  fill(200);
  rect(50, 500, 40, 40);
  rect(50, 550, 40, 40);
  rect(50, 600, 40, 40);
  rect(100, 600, 40, 40);
  
  // Program keys
  rect(100, 550, 40, 40);
  rect(150, 550, 40, 40);
  rect(200, 550, 40, 40);
  rect(250, 550, 40, 40);
  rect(300, 550, 40, 40);
  rect(350, 550, 40, 40);
  rect(400, 550, 40, 40);
  rect(450, 550, 40, 40);
              
  // left upper
  rect(150, 150, 40, 40);
  rect(100, 200, 40, 40);
  rect(200, 200, 40, 40);
  rect(150, 250, 40, 40);
  
  // left lower
  rect(150, 350, 40, 40);
  rect(100, 400, 40, 40);
  rect(200, 400, 40, 40);
  rect(150, 450, 40, 40);
  
  // right upper
  rect(350, 150, 40, 40);
  rect(300, 200, 40, 40);
  rect(400, 200, 40, 40);
  rect(350, 250, 40, 40);
  
  // right lower
  rect(350, 350, 40, 40);
  rect(300, 400, 40, 40);
  rect(400, 400, 40, 40);
  rect(350, 450, 40, 40);
  
  // Gating
  rect(550, 250, 40, 40);
  rect(500, 300, 40, 40);
  rect(600, 300, 40, 40);
  rect(550, 350, 40, 40);
  
  text("Last Sent: ", 10, 50);
  text((char)sendBuffer, 200, 50);
  text("Last Received: ", 10, 100);
  for (int n=0; n<10; n++) {
    text((char)recvBuffer[n], 200 + 50*n, 100);
    text(n, 700, 300 + n*50);
    text(positionBuffer[n], 800, 300 + n*50);
  }
}

void serialEvent(Serial myPort) {
  
  int val = myPort.read();
  if (val == -1) return;
  if (!receiving && val != '|') {
    print((char)val);
  }
    
  if (val == '|') {
    receiving = !receiving;
    if (receiving) {
      readingPos = 0;
    } 
    else if (recvBuffer[0] == 'A') { led1 = true; led2 = false; led3 = false; led4 = false; }
    else if (recvBuffer[0] == 'B') { led1 = false; led2 = true; led3 = false; led4 = false; }
    else if (recvBuffer[0] == 'C') { led1 = false; led2 = false; led3 = true; led4 = false; }
    else if (recvBuffer[0] == 'D') { led1 = false; led2 = false; led3 = false; led4 = true; }
    else {
      int motor = recvBuffer[0] - '0';    
      if (motor >= 0 && motor < 10) {
        int pos = 0;
        int pot = 0;
        for (int n=readingPos-1; n > 0; n--, pot++) {
          int nval = recvBuffer[n] - '0';
          int fac = (int)pow(10, pot);
          pos += nval * fac;
        }
        positionBuffer[motor] = pos;
      }
    }

  }
  else if (receiving) {
    recvBuffer[readingPos] = val;
    readingPos++;
    if (readingPos > 9) readingPos = 0;
  }  
}

void mousePressed() {
  if (comPort == null) {
    println("No device attached");
    return;
  }
  if (mousePressedGAL() == true) { comPort.write('0'); sendBuffer = '0'; }
  else if (mousePressedGAT() == true) { comPort.write('1'); sendBuffer = '1'; } 
  else if (mousePressedGAB() == true) { comPort.write('2'); sendBuffer = '2'; }
  else if (mousePressedGAR() == true) { comPort.write('3'); sendBuffer = '3'; } 
  
  else if (mousePressedFP7() == true) { comPort.write('4'); sendBuffer = '4'; } 
  else if (mousePressedFP6() == true) { comPort.write('5'); sendBuffer = '5'; }
  else if (mousePressedFP5() == true) { comPort.write('6'); sendBuffer = '6'; }
  else if (mousePressedFP8() == true) { comPort.write('7'); sendBuffer = '7'; }
  
  else if (mousePressedRLB() == true) { comPort.write('8'); sendBuffer = '8'; }
  else if (mousePressedRLR() == true) { comPort.write('9'); sendBuffer = '9'; } 
  else if (mousePressedRLL() == true) { comPort.write(':'); sendBuffer = ':'; }
  else if (mousePressedRLT() == true) { comPort.write(';'); sendBuffer = ';'; } 
    
  else if (mousePressedRUR() == true) { comPort.write('<'); sendBuffer = '<'; } 
  else if (mousePressedRUL() == true) { comPort.write('='); sendBuffer = '='; }
  else if (mousePressedRUT() == true) { comPort.write('>'); sendBuffer = '>'; }
  else if (mousePressedRUB() == true) { comPort.write('?'); sendBuffer = '?'; }
  
  else if (mousePressedFP4() == true) { comPort.write('@'); sendBuffer = '@'; }
  else if (mousePressedFP3() == true) { comPort.write('A'); sendBuffer = 'A'; } 
  else if (mousePressedFP2() == true) { comPort.write('B'); sendBuffer = 'B'; } 
  else if (mousePressedFP1() == true) { comPort.write('C'); sendBuffer = 'C'; } 
      
  else if (mousePressedFPG() == true) { comPort.write('D'); sendBuffer = 'D'; } 
  else if (mousePressedFPS() == true) { comPort.write('E'); sendBuffer = 'E'; }   
  else if (mousePressedFMM() == true) { comPort.write('F'); sendBuffer = 'F'; } 
  else if (mousePressedFPP() == true) { comPort.write('G'); sendBuffer = 'G'; }
  
  else if (mousePressedLLB() == true) { comPort.write('H'); sendBuffer = 'H'; }
  else if (mousePressedLLR() == true) { comPort.write('I'); sendBuffer = 'I'; } 
  else if (mousePressedLLL() == true) { comPort.write('J'); sendBuffer = 'J'; }
  else if (mousePressedLLT() == true) { comPort.write('K'); sendBuffer = 'K'; } 

  else if (mousePressedLUR() == true) { comPort.write('L'); sendBuffer = 'L'; } 
  else if (mousePressedLUL() == true) { comPort.write('M'); sendBuffer = 'M'; }
  else if (mousePressedLUT() == true) { comPort.write('N'); sendBuffer = 'N'; } 
  else if (mousePressedLUB() == true) { comPort.write('O'); sendBuffer = 'O'; }
}

boolean mousePressedFMM() { return ((mouseX >= 50) && (mouseX <= 90) && (mouseY >= 500) && (mouseY <= 540)); }
boolean mousePressedFPS() { return ((mouseX >= 50) && (mouseX <= 90) && (mouseY >= 550) && (mouseY <= 590)); }
boolean mousePressedFPG() { return ((mouseX >= 50) && (mouseX <= 90) && (mouseY >= 600) && (mouseY <= 640)); }
boolean mousePressedFPP() { return ((mouseX >= 100) && (mouseX <= 140) && (mouseY >= 600) && (mouseY <= 640)); }

boolean mousePressedFP1() { return ((mouseX >= 100) && (mouseX <= 140) && (mouseY >= 550) && (mouseY <= 590)); }
boolean mousePressedFP2() { return ((mouseX >= 150) && (mouseX <= 190) && (mouseY >= 550) && (mouseY <= 590)); }
boolean mousePressedFP3() { return ((mouseX >= 200) && (mouseX <= 240) && (mouseY >= 550) && (mouseY <= 590)); }
boolean mousePressedFP4() { return ((mouseX >= 250) && (mouseX <= 290) && (mouseY >= 550) && (mouseY <= 590)); }
boolean mousePressedFP5() { return ((mouseX >= 300) && (mouseX <= 340) && (mouseY >= 550) && (mouseY <= 590)); }
boolean mousePressedFP6() { return ((mouseX >= 350) && (mouseX <= 390) && (mouseY >= 550) && (mouseY <= 590)); }
boolean mousePressedFP7() { return ((mouseX >= 400) && (mouseX <= 440) && (mouseY >= 550) && (mouseY <= 590)); }
boolean mousePressedFP8() { return ((mouseX >= 450) && (mouseX <= 490) && (mouseY >= 550) && (mouseY <= 590)); }

boolean mousePressedLUT() { return ((mouseX >= 150) && (mouseX <= 190) && (mouseY >= 150) && (mouseY <= 190)); }
boolean mousePressedLUL() { return ((mouseX >= 100) && (mouseX <= 140) && (mouseY >= 200) && (mouseY <= 240)); }
boolean mousePressedLUR() { return ((mouseX >= 200) && (mouseX <= 240) && (mouseY >= 200) && (mouseY <= 240)); }
boolean mousePressedLUB() { return ((mouseX >= 150) && (mouseX <= 190) && (mouseY >= 250) && (mouseY <= 290)); }

boolean mousePressedLLT() { return ((mouseX >= 150) && (mouseX <= 190) && (mouseY >= 350) && (mouseY <= 390)); }
boolean mousePressedLLL() { return ((mouseX >= 100) && (mouseX <= 140) && (mouseY >= 400) && (mouseY <= 440)); }
boolean mousePressedLLR() { return ((mouseX >= 200) && (mouseX <= 240) && (mouseY >= 400) && (mouseY <= 440)); }
boolean mousePressedLLB() { return ((mouseX >= 150) && (mouseX <= 190) && (mouseY >= 450) && (mouseY <= 490)); }

boolean mousePressedRLT() { return ((mouseX >= 350) && (mouseX <= 390) && (mouseY >= 350) && (mouseY <= 390)); }
boolean mousePressedRLL() { return ((mouseX >= 300) && (mouseX <= 340) && (mouseY >= 400) && (mouseY <= 440)); }
boolean mousePressedRLR() { return ((mouseX >= 400) && (mouseX <= 440) && (mouseY >= 400) && (mouseY <= 440)); }
boolean mousePressedRLB() { return ((mouseX >= 350) && (mouseX <= 390) && (mouseY >= 450) && (mouseY <= 490)); }

boolean mousePressedRUT() { return ((mouseX >= 350) && (mouseX <= 390) && (mouseY >= 150) && (mouseY <= 190)); }
boolean mousePressedRUL() { return ((mouseX >= 300) && (mouseX <= 340) && (mouseY >= 200) && (mouseY <= 240)); }
boolean mousePressedRUR() { return ((mouseX >= 400) && (mouseX <= 440) && (mouseY >= 200) && (mouseY <= 240)); }
boolean mousePressedRUB() { return ((mouseX >= 350) && (mouseX <= 390) && (mouseY >= 250) && (mouseY <= 290)); }

boolean mousePressedGAT() { return ((mouseX >= 550) && (mouseX <= 590) && (mouseY >= 250) && (mouseY <= 290)); }
boolean mousePressedGAL() { return ((mouseX >= 500) && (mouseX <= 540) && (mouseY >= 300) && (mouseY <= 340)); }
boolean mousePressedGAR() { return ((mouseX >= 600) && (mouseX <= 640) && (mouseY >= 300) && (mouseY <= 340)); }
boolean mousePressedGAB() { return ((mouseX >= 550) && (mouseX <= 590) && (mouseY >= 350) && (mouseY <= 390)); }
