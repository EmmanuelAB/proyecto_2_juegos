using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DeliveryBehaviour : MonoBehaviour
{

    public GameObject package;
    public GameObject deliveriesInUI;
    public GameObject exposureInUI;
    public GameObject exposureHalo;
    public DialogueManager dialogManager;
    public float enterTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Boolean isDeliverySpot(Collision2D collision)
    {
        return collision.gameObject.name.Contains("DeliverySpot");
    }
        
    private void hidePackage()
    {
        package.SetActive(false);
    }

    private void showPackage()
    {
        package.SetActive(true);
    }

    private bool hasPackage()
    {
        return package.activeSelf;
    }

    private void showExposureHalo() { exposureHalo.SetActive(true); }
    private void hideExposureHalo() { exposureHalo.SetActive(false); }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDeliverySpot(collision) && hasPackage())
        {
            GameData.deliveriesFinished++;
            updateDeliveriesInUI();
            hidePackage();
            showDeliveredDialogue();
            checkIfWinned();
        }
        else if (isPharmacy(collision)) 
        {

            showPackage();
            showPickupDialogue();

        }
        else if (isCitizen(collision))
        {
            enterTime = Time.time;
            showExposureHalo();
            updateDeliveriesInUI();
              
        }
    }

    private void checkIfWinned()
    {
        if (GameData.deliveriesFinished == GameData.DELIVERIES_TO_WIN) {
            Dialogue d = new Dialogue();
            d.name = "¡HAS GANADO!";
            d.sentences = new string[] {
            "Has logrado entregar los "+GameData.DELIVERIES_TO_WIN+" paquetes."
        };
            dialogManager.StartDialogue(d);
            GameData.gameRunning = false;

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (isCitizen(collision))
        {
            float exitTime = Time.time;
            float inTime = exitTime - enterTime;
            hideExposureHalo();
            GameData.exposedTime += inTime;
            UpdateExposureInUI();
            if (GameData.exposedTime >= GameData.MAX_EXPOSURE_TIME)
                EndGame();


        }
    }
    private void EndGame()
    {
        showEndGameDialogue();
        GameData.gameRunning = false;
    }

    private void showEndGameDialogue()
    {
        Dialogue d = new Dialogue();
        d.name = "FIN DEL JUEGO";
        d.sentences = new string[] {
            "Te has expuesto por más de "+GameData.MAX_EXPOSURE_TIME+" segundos a cercanía con otras personas"
        };
        dialogManager.StartDialogue(d);
    }

    private void UpdateExposureInUI()
    {
        exposureInUI.GetComponent<TMPro.TextMeshProUGUI>().text = "" + GameData.exposedTime.ToString("F1")+
            " / "+GameData.MAX_EXPOSURE_TIME+" segs";
    }

    private void showPickupDialogue()
    {
        Dialogue d = new Dialogue();
        d.name = "Farmacia:";
        d.sentences = new string[] {
            "Debes entregar este paquete \n en la verdurería"
        };
        dialogManager.StartDialogue(d);
    }

    private void showDeliveredDialogue()
    {
        Dialogue d = new Dialogue();
        d.name = "Cliente:";
        d.sentences = new string[] {
            "Siempre a tiempo Pedro \n ¡Muchas gracias!"
        };
        dialogManager.StartDialogue(d);
    }

    private bool isCitizen(Collision2D collision)
    {
        return collision.gameObject.name.Contains("Citizen");
    }

    private void updateDeliveriesInUI()
    {
        deliveriesInUI.GetComponent<TMPro.TextMeshProUGUI>().text = ""+GameData.deliveriesFinished;
    }

    private bool isPharmacy(Collision2D collision)
    {
        return collision.gameObject.name.Contains("Pharmacy");
    }
}
