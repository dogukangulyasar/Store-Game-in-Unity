using System.Collections;
using System.Collections.Generic;

public class Event{
    public Event(int tmpid, string tmptext, string tmpbuttonyestext, string tmpbuttonnotext) {
        id = tmpid;
        text = tmptext;
        buttonYesText = tmpbuttonyestext;
        buttonNoText = tmpbuttonnotext;
    }

    public int id;
    public string text;
    public string buttonYesText;
    public string buttonNoText;

    public void Increase() {

    }

}
