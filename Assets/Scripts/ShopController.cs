using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    public int starsCount = 0;
    public Text starsValueLabel;
    public Button[] shopButtons;
    Button choosenSkin;

    void Start()
    {
        if (PlayerPrefs.HasKey("Stars"))
            starsCount = PlayerPrefs.GetInt("Stars");

        UpdateShop();
    }

    void UpdateShop()
    {
        var info = shopButtons[0].GetComponent<ShopButtonInfo>();

        if (!PlayerPrefs.HasKey("chosenSkin"))
            PlayerPrefs.SetString("chosenSkin", info.skinName);

        PlayerPrefs.SetInt(info.skinName + "buyed", 1);
        info.isBuyedSkin = true;

        PlayerPrefs.SetInt(info.skinName + "choosen", 1);
        info.isChosenSkin = true;

        choosenSkin = shopButtons[0];

        for (int i = 0; i < shopButtons.Length; ++i) 
        {
            info = shopButtons[i].GetComponent<ShopButtonInfo>();

            if (PlayerPrefs.HasKey(info.skinName + "buyed"))
                info.isBuyedSkin = true;

            if (PlayerPrefs.HasKey(info.skinName + "choosen") && PlayerPrefs.GetString("chosenSkin") == info.skinName) 
            {
                choosenSkin.GetComponent<ShopButtonInfo>().isChosenSkin = false;
                choosenSkin.gameObject.SetActive(true);
                choosenSkin.transform.GetChild(0).GetComponent<Text>().text = "вибрать";
                choosenSkin = shopButtons[i];
                info.isChosenSkin = true;
            }

            if (info.isBuyedSkin)
                shopButtons[i].transform.GetChild(0).GetComponent<Text>().text = "вибрать";

            if (info.isChosenSkin)
                shopButtons[i].gameObject.SetActive(false);
        }
    }

    public void TryBuySkin(Button button)
    {
        var buttonInfo = button.GetComponent<ShopButtonInfo>();

        if(!button.GetComponent<ShopButtonInfo>().isBuyedSkin)
        {
            if (starsCount >= buttonInfo.price)
            {
                starsCount -= buttonInfo.price;

                PlayerPrefs.SetInt("Stars", starsCount);

                starsValueLabel.text = starsCount.ToString();
                buttonInfo.isBuyedSkin = true;
                button.transform.GetChild(0).transform.GetComponent<Text>().text = "вибрать";
                PlayerPrefs.SetInt(buttonInfo.skinName + "buyed", 1);
            }

            return;
        }


        PlayerPrefs.SetInt(buttonInfo.skinName + "choosen", 1);
        PlayerPrefs.SetString("chosenSkin", buttonInfo.skinName);
        buttonInfo.isChosenSkin = true;
        choosenSkin.transform.GetChild(0).transform.GetComponent<Text>().text = "вибрать";
        choosenSkin.gameObject.SetActive(true);
        choosenSkin = button;
        choosenSkin.gameObject.SetActive(false);
    }
}
