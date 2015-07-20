using System;
using UnityEngine;

/// <summary>
/// タッチ入力に応じた処理のイベントを提供する
/// </summary>
public class TouchListener : SingletonMonoBehaviour<TouchListener>
{
  /// <summary>
  /// オブジェクトクリック時のイベント処理
  /// </summary>
  public event EventHandler<CustomInputEventArgs> ClickEvent;

  private GameObject touchObject;
  private RaycastHit hit;

  /// <summary>
  /// 現在タッチしているオブジェクト
  /// </summary>
  public GameObject TouchObject { get { return touchObject; } }

  /// <summary>
  /// タッチによってレイが衝突した最後のポジション
  /// </summary>
  public Vector3 HitPoint
  {
    get
    {
      return hit.point;
    }
  }

  public void Awake()
  {
    TouchManager.Instance.TouchStart += OnTouchStart;
    TouchManager.Instance.TouchEnd += OnTouchEnd;
  }

  /// <summary>
  /// タッチ開始時の動作
  /// </summary>
  private void OnTouchStart(object sender, CustomInputEventArgs args)
  {
    //最初にヒットしたオブジェクトを取得する
    if(Physics.Raycast(Camera.main.ScreenPointToRay(args.Input.ScreenPosition), out hit))
      touchObject = hit.collider.gameObject;
    
  }

  /// <summary>
  /// リリース時のイベント
  /// </summary>
  private void OnTouchEnd(object sender, CustomInputEventArgs args)
  {
    RaycastHit hit;
    if(Physics.Raycast(Camera.main.ScreenPointToRay(args.Input.ScreenPosition), out hit))
      if(GameObject.ReferenceEquals(hit.collider.gameObject, touchObject))
        if(ClickEvent != null)
          ClickEvent(sender, args);
  }
}
