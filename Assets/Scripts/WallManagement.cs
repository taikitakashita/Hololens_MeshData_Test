using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA;

public class WallManagement : MonoBehaviour, IInputClickHandler {

    private bool m_SettingWall = false;

    void Start()
    {
        WorldAnchorManager.Instance.AttachAnchor(this.gameObject, this.gameObject.GetInstanceID().ToString());
    }

    void Update()
    {
        if (m_SettingWall)
        {
            this.gameObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2;
            this.gameObject.transform.LookAt(new Vector3(Camera.main.transform.position.x, this.gameObject.transform.position.y, Camera.main.transform.position.z));
            MenuManagement.Instance.DebugTextObject.text += string.Format(" Setting Wall.");
            m_SettingWall = false;
            this.gameObject.layer = LayerMask.NameToLayer("Wall");
        }
    }

    public void SettingWall()
    {
        WorldAnchorManager.Instance.RemoveAnchor(this.gameObject.GetInstanceID().ToString());
        m_SettingWall = true;
        this.gameObject.layer = LayerMask.NameToLayer("Default");
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        Debug.Log(MenuManagement.Instance.MenuSelect);

        if (MenuManagement.Instance.MenuSelect == "Save")
        {
            WorldAnchorManager.Instance.AttachAnchor(this.gameObject, this.gameObject.GetInstanceID().ToString());
            this.gameObject.layer = LayerMask.NameToLayer("Wall");
        }
    }
}
