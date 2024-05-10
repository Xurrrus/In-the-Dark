using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class TeleportRaig : MonoBehaviour
{

    public TeleportationProvider TeleportXR;

    public XRRayInteractor esquerra;
    public XRRayInteractor dreta;

    public InputActionProperty esquerraCancelar;
    public InputActionProperty dretaCancelar;

    public InputActionProperty esquerraActivar;
    public InputActionProperty dretaActivar;

    void Update()
    {

        bool esquerraSobre = esquerra.TryGetHitInfo(out Vector3 posEsquerra, out Vector3 esquerraNormal, out int esquerraNumero, out bool esquerraValida);
        bool dretaSobre = esquerra.TryGetHitInfo(out Vector3 posDreta, out Vector3 dretaNormal, out int dretaNumero, out bool dretaValida);

        TeleportXR.enabled = !esquerraSobre && esquerraCancelar.action.ReadValue<float>() == 0 && esquerraActivar.action.ReadValue<float>() > 0.1f;//teleportXR

        if(!TeleportXR.enabled) TeleportXR.enabled = !dretaSobre && dretaCancelar.action.ReadValue<float>() == 0 && dretaActivar.action.ReadValue<float>() > 0.1f;



    }
}
