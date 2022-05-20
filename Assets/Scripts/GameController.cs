using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
public class GameController : MonoBehaviour {
    [SerializeField] Sprite OBArea1;
    [SerializeField] Sprite OBArea2;
    [SerializeField] Sprite OBArea3;
    [SerializeField] Sprite OBArea4;
    [SerializeField] Sprite OBArea5;
    [SerializeField] Sprite OBArea6;
    [SerializeField] Sprite OBArea7;
    [SerializeField] Sprite OBArea8;
    [SerializeField] Sprite OBArea9;
    [SerializeField] Sprite OBArea10;
    [SerializeField] Sprite OBArea11;

    [SerializeField] Sprite BOArea1;
    [SerializeField] Sprite BOArea2;
    [SerializeField] Sprite BOArea3;
    [SerializeField] Sprite BOArea4;
    [SerializeField] Sprite BOArea5;
    [SerializeField] Sprite BOArea6;
    [SerializeField] Sprite BOArea7;
    [SerializeField] Sprite BOArea8;
    [SerializeField] Sprite BOArea9;
    [SerializeField] Sprite BOArea10;
    [SerializeField] Sprite BOArea11;
    [SerializeField] Sprite BOArea12;
    [SerializeField] Sprite BOArea13;
    [SerializeField] Sprite BOArea14;
    [SerializeField] Sprite BOArea15;
    [SerializeField] Sprite BOArea16;
    [SerializeField] Sprite BOArea17;
    [SerializeField] Sprite BOArea18;
    [SerializeField] Sprite BOArea19;
    [SerializeField] Sprite BOArea20;
    [SerializeField] int manPower;
    [SerializeField] int moneyPower;
    [SerializeField] int oppositePower;
    [SerializeField] int year;
    ArrayList fethedilmis = new ArrayList();
    

    private Dictionary<string, Sprite> areamap = new Dictionary<string, Sprite>();
    [SerializeField] Text manPowerText;
    [SerializeField] Text moneyPowerText;
    [SerializeField] Text oppositePowerText;
    [SerializeField] Text yearText;

    [SerializeField] GameObject panel;
    [SerializeField] Text panelText;
    [SerializeField] Button panelButtonYes;
    [SerializeField] Button panelButtonNo;

    private float nextActionTime = 2.0f;
    public float period = 2.0f;

    ArrayList events = new ArrayList();

    private bool readyToChangeOppositePower = true;
    private bool readyToChangeMoneyAndManPower = true;
    private bool readyToChangeManAndMoneyBy20 = true;
    private bool readyToChangeManPower = true;

    private int eventCounter = 0;

    bool isPanelActive = false;
    void Start() {
        events.Add(new Event(1, "Din adamları daha çok cami istedi", "Kabul Et(ekonomi -10, karşıt güç +10)", "Reddet(askeri -10, karşıt güç +10)"));
        events.Add(new Event(2, "Köylüler kıtlık çekiyor", "Yardım Et(ekonomi -10, askeri +10)", "Umursama(karşıt güç +10)"));
        events.Add(new Event(3, "Vergileri yükseltmek için bir şansımız oldu", "Vergileri Yükselt(ekonomi +10, askeri -10)","Vergilere Karışma(karşıt güç -10)"));
        events.Add(new Event(4, "Ata topraklarından ziyaretçilerimiz var", "Onları Kabul Et(askeri +10)", "Onları Ticaret Bölgesine Yerleştir(ekonomik + 10)"));
        events.Add(new Event(5, "Bizans ajanı yakalandı", "Onun Kellesini Al(karşıt görüş -10)", "Onu Kullanarak Planları Ele Geçir(askeri +10)"));
        events.Add(new Event(6, "Gözcülerimiz esir düştü", "Onları Kurtar(ekonomik -10)", "Onları Önemseme(karşıt görüş +10)"));


        areamap.Add("OBArea1", OBArea1);
        areamap.Add("OBArea2", OBArea2);
        areamap.Add("OBArea3", OBArea3);
        areamap.Add("OBArea4", OBArea4);
        areamap.Add("OBArea5", OBArea5);
        areamap.Add("OBArea6", OBArea6);
        areamap.Add("OBArea7", OBArea7);
        areamap.Add("OBArea8", OBArea8);
        areamap.Add("OBArea9", OBArea9);
        areamap.Add("OBArea10", OBArea10);
        areamap.Add("OBArea11", OBArea11);

        areamap.Add("BOArea1", BOArea1);
        areamap.Add("BOArea2", BOArea2);
        areamap.Add("BOArea3", BOArea3);
        areamap.Add("BOArea4", BOArea4);
        areamap.Add("BOArea5", BOArea5);
        areamap.Add("BOArea6", BOArea6);
        areamap.Add("BOArea7", BOArea7);
        areamap.Add("BOArea8", BOArea8);
        areamap.Add("BOArea9", BOArea9);
        areamap.Add("BOArea10", BOArea10);
        areamap.Add("BOArea11", BOArea11);
        areamap.Add("BOArea12", BOArea12);
        areamap.Add("BOArea13", BOArea13);
        areamap.Add("BOArea14", BOArea14);
        areamap.Add("BOArea15", BOArea15);
        areamap.Add("BOArea16", BOArea16);
        areamap.Add("BOArea17", BOArea17);
        areamap.Add("BOArea18", BOArea18);
        areamap.Add("BOArea19", BOArea19);
        areamap.Add("BOArea20", BOArea20);

        panel.SetActive(false);
        InvokeRepeating("showEvent",2, 5);
    }
    void Update() {
        manPowerText.text = "Askeri Güç: " + manPower;
        moneyPowerText.text = "Ekonomik Güç: " + moneyPower;
        oppositePowerText.text = "Karşıt Güç: " + oppositePower;

        yearText.text = "Yıl: " + year;
        
        //Game Rules
        //Restriction
        if(moneyPower > 100) moneyPower = 100;
        if (manPower> 100) manPower= 100;
        if (oppositePower > 100) oppositePower= 100;

        if (moneyPower < 0) moneyPower = 0;
        if (manPower < 0) manPower = 0;
        if (oppositePower < 0) oppositePower = 0;

        //Ekonomik Guc 50'nin ustune cikarsa...
        if (moneyPower > 50 && readyToChangeOppositePower) {
            oppositePower -= 10;
            readyToChangeOppositePower = false;
        }

        if (moneyPower < 50) {
            readyToChangeOppositePower = true;
        }

        //Hukumdara karsit guc 40'in ustune cikarsa...
        if(oppositePower > 40 && oppositePower < 70 && readyToChangeMoneyAndManPower) {
            moneyPower -= 10;
            manPower -= 10;
            readyToChangeMoneyAndManPower = false;
        }

        if(oppositePower < 40) {
            readyToChangeMoneyAndManPower = true;
        }

        //Karsit guc 70'in ustune cikarsa...
        if (oppositePower > 70 && readyToChangeManAndMoneyBy20) {
            manPower -= 20;
            moneyPower -= 20;
            readyToChangeManAndMoneyBy20 = false;
        }

        if(oppositePower < 70) {
            readyToChangeManAndMoneyBy20 = true;
        }

        if(moneyPower >= 100 || manPower >= 100 || oppositePower >=100) {
            SceneManager.LoadScene("GameOverScene");
        }

        if(moneyPower <= 0) {
            SceneManager.LoadScene("GameOverScene");
        }

        if (manPower <= 0) {
            SceneManager.LoadScene("GameOverScene");
        }

        if (oppositePower <= 0 && readyToChangeManPower) {
            manPower += 10;
            readyToChangeManPower = false;
        }

        if(oppositePower > 0) {
            readyToChangeManPower = true;
        }

    }

    public void Fethet(string areaname, string newareaname, GameObject panel) {

        int savas = Random.Range(0, 100);

        if(savas < manPower) {
            Button button = GameObject.Find(areaname).GetComponent<Button>();
            Sprite area = areamap[newareaname];
            button.GetComponent<Image>().sprite = area;
        }
        panel.SetActive(false);
        isPanelActive = false;
        panelButtonYes.onClick.RemoveAllListeners();
        panelButtonNo.onClick.RemoveAllListeners();
    }

    private void showEvent() {
        if (isPanelActive) {
            return;
        }
        year += 10;
        eventCounter+=1;

        //Fethet
        if(eventCounter % 3 == 0) {

            int mapnumber = Random.Range(1, 20);

            if(!fethedilmis.Contains(mapnumber)) {
                fethedilmis.Add(mapnumber);
            }else {
                while(fethedilmis.Contains(mapnumber)) {
                    mapnumber = Random.Range(1, 20);
                }

                fethedilmis.Add(mapnumber);
            }

            isPanelActive = true;
            string mapname = "area" + mapnumber;
            string areaname = "BOArea" + mapnumber;

            panelText.text = "Yeni toprak fethedilebilir, fethedelim mi?";
            panelButtonYes.GetComponentInChildren<Text>().text = "Fethet %" + manPower;
            panelButtonNo.GetComponentInChildren<Text>().text = "Umursama";

            panel.SetActive(true);
            isPanelActive = true;
            panelButtonYes.onClick.AddListener(() => Fethet(mapname, areaname, panel));
            panelButtonNo.onClick.AddListener(() => proceedNo(9, panel));

            return;
        }

        int number = Random.Range(1, 6);
        isPanelActive = true;
        Event e = (Event)events[number];
        panelText.text = e.text;
        panelButtonYes.GetComponentInChildren<Text>().text = e.buttonYesText;
        panelButtonNo.GetComponentInChildren<Text>().text = e.buttonNoText;
        
        panel.SetActive(true);
        isPanelActive = true;
        
        panelButtonYes.onClick.AddListener(() => proceedYes(e.id, panel));
        panelButtonNo.onClick.AddListener(() => proceedNo(e.id, panel));

        return;
    }

    public void Decrease() {
        
        oppositePower -= 10;
    }

    public void proceedYes(int id, GameObject panel) {
        switch (id) {
            case 1:
                moneyPower -= 10;
                oppositePower += 10;
                panel.SetActive(false);
                isPanelActive = false;
                break;
            case 2:
                moneyPower -= 10;
                manPower += 10;
                panel.SetActive(false);
                isPanelActive = false;
                break;
            case 3:
                moneyPower += 10;
                manPower -= 10;
                panel.SetActive(false);
                isPanelActive = false;
                break;
            case 4:
                manPower += 10;
                panel.SetActive(false);
                isPanelActive = false;
                break;
            case 5:
                oppositePower -= 10;
                panel.SetActive(false);
                isPanelActive = false;
                break;
            case 6:
                moneyPower -= 10;
                panel.SetActive(false);
                isPanelActive = false;
                break;
            default:
                isPanelActive = false;
                break;
        }

        panelButtonYes.onClick.RemoveAllListeners();
        panelButtonNo.onClick.RemoveAllListeners();
    }

    public void proceedNo(int id, GameObject panel) {
        switch (id) {
            case 1:
                manPower -= 10;
                oppositePower -= 10;
                panel.SetActive(false);
                isPanelActive = false;
                break;
            case 2:
                oppositePower += 10;
                panel.SetActive(false);
                isPanelActive = false;
                break;
            case 3:
                oppositePower -= 10;
                panel.SetActive(false);
                isPanelActive = false;
                break;
            case 4:
                moneyPower += 10;
                panel.SetActive(false);
                isPanelActive = false;
                break;
            case 5:
                manPower += 10;
                panel.SetActive(false);
                isPanelActive = false;
                break;
            case 6:
                oppositePower += 10;
                panel.SetActive(false);
                isPanelActive = false;
                break;
            default:
                isPanelActive = false;
                break;
        }

        panelButtonYes.onClick.RemoveAllListeners();
        panelButtonNo.onClick.RemoveAllListeners();
    }
}
