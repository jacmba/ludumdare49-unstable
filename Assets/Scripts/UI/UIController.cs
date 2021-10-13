using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
  private Transform canvas;
  private Transform notificationPanel;
  private Text notificationText;

  // Start is called before the first frame update
  void Start()
  {
    canvas = transform.Find("Canvas");
    notificationPanel = canvas.Find("NotificationPanel");
    notificationText = notificationPanel.GetComponentInChildren<Text>();
  }

  public void notify(string text)
  {
    notificationText.text = text.ToUpper();
    notificationPanel.gameObject.SetActive(true);
  }
}
