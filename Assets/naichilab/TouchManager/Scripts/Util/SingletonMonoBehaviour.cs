using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
  private static T instance;

  public static T Instance
  {
    get
    {
      if(instance == null)
      {
        instance = FindObjectOfType<T>();

        if(instance == null)
        {
          Debug.LogWarning(typeof(T) + "is nothing");
        }
      }
      return instance;
    }
  }

		private void Awake ()
		{
				if (this != Instance) {
						Destroy (this);
						return;
				}
				DontDestroyOnLoad (this.gameObject);
		}

}
