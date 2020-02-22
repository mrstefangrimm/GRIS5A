/* IPhantom.java - Dedicated Keyboard Processing 3.0 software for the GRIS5A (C) motion phantom
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

interface IPhantom {
  
  boolean isPresentation(int model);
  
  void drawMotionControls();
  
  void drawMotionState();
  
  void setMotionState(int motor, int value);
  
  void onMousePressed(int x, int y);
  
}
