﻿using System;
using UnhollowerRuntimeLib;
using UnityEngine;
using UnityEngine.UI;
using Nocturnal.Ui;
namespace Nocturnal.Apis
{
    internal class Inputpopout
    {
        public Inputpopout(string name, Action<string> setOutput, Action action)
        {
            if (Objects._QuickMenuCanvas.activeSelf)
                Objects._QuickMenuCanvas.gameObject.SetActive(false);
            VRCUiPopupManager.field_Private_Static_VRCUiPopupManager_0.Method_Public_Void_String_String_InputType_Boolean_String_Action_3_String_List_1_KeyCode_Text_Action_String_Boolean_Action_1_VRCUiPopup_Boolean_PDM_0(name, "", InputField.InputType.Standard, false, "Enter",
                            DelegateSupport.ConvertDelegate<Il2CppSystem.Action<string, Il2CppSystem.Collections.Generic.List<KeyCode>, Text>>
                                (new Action<string, Il2CppSystem.Collections.Generic.List<KeyCode>, Text>
                                (delegate (string s, Il2CppSystem.Collections.Generic.List<KeyCode> k, Text t)
                                {
                                    setOutput(s);
                                    action.Invoke();
                                })), null, "", true, null);

        }




    }
    }
