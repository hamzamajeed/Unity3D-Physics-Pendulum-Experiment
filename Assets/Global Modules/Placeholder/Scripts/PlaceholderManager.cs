using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tajurbah_Gah
{
    public class PlaceholderManager : MonoBehaviour
    {
        static PlaceholderManager instance;
        public static PlaceholderManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<PlaceholderManager>();
                }
                return instance;
            }
        }

        public Action callBack;

        [Header("Appratus Count")]
        public int appratusCount = 0;

        int no = 0;

        public void IncrementAppratusCount()
        {
            no++;

            CheckAppratusSetupCompleted(no);
        }

        void CheckAppratusSetupCompleted(int num)
        {
            if (num == appratusCount && callBack!=null)
            {
                callBack();
                callBack = null;
            }
        }
    }
}