using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Assertions;

namespace UnityEngine.XR.Interaction.Toolkit
{
    public class OptionsMenu : LocomotionProvider
    {
        public Toggle m_Toggle;
        public Toggle r_Toggle;
        public TeleportationProvider tpProvider;
        public ActionBasedContinuousMoveProvider cMoveProv;

        public ActionBasedSnapTurnProvider tpRotProvider;
        public ActionBasedContinuousTurnProvider cRotProv;

        void Start()
        {
            m_Toggle.onValueChanged.AddListener(delegate {
                ToggleMoveChanged(m_Toggle);
            });

            r_Toggle.onValueChanged.AddListener(delegate {
                ToggleRotationChanged(r_Toggle);
            });
        }

        void ToggleMoveChanged(Toggle change)
        {
            if(!m_Toggle.isOn){
                tpProvider.enabled = true;
                cMoveProv.enabled = false;
            }
            else{
                tpProvider.enabled = false;
                cMoveProv.enabled = true;
            }
        }

        void ToggleRotationChanged(Toggle change)
        {
            if(!m_Toggle.isOn){
                tpRotProvider.enabled = true;
                cRotProv.enabled = false;
            }
            else{
                tpRotProvider.enabled = false;
                cRotProv.enabled = true;
            }
        }
    }
}
