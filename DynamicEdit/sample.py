import clr
clr.AddReference("System.Windows.Forms")
from System import *
from System.Windows.Forms import *


def func():
    MessageBox.Show("X is Pressed")

def KeyEvent(c):
    if c == "X":
        return func
    # MessageBox.Show(c)
    return None

def Exit():
    MainForm.Close()

