package com.github.mophdroid;

public interface ISerialObserver {

    void rawOutput(String rawData);

    void logOutput(String text);

    void statusInfo(StatusInfo status);

    void freeMemory(int freeMem);

    void servoPosition(int servoNum, int pos);

    void pageChanged(int tabPos);
}
