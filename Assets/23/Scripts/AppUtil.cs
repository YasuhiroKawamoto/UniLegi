using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class AppUtil
{
    //マルチタップ時の指の位置（4本までを想定）
    private static Vector3[] _touchPositions = new Vector3[4];

    /// <summary>
    /// Androidフラグ
    /// </summary>
    static readonly bool _isAndroid = Application.platform == RuntimePlatform.Android;
    /// <summary>
    /// iOSフラグ
    /// </summary>
    static readonly bool _isIOS = Application.platform == RuntimePlatform.IPhonePlayer;
    /// <summary>
    /// エディタフラグ
    /// </summary> 
    static readonly bool IsEditor = !_isAndroid && !_isIOS;

    


    /// <summary>
    /// タッチ情報を取得(エディタと実機を考慮)
    /// </summary>
    /// <returns>タッチ情報。タッチされていない場合は null</returns>
    public static TouchInfo GetTouch()
    {
        if (IsEditor)
        {
            if (Input.GetMouseButtonDown(0)) { return TouchInfo.Began; }
            if (Input.GetMouseButton(0)) { return TouchInfo.Moved; }
            if (Input.GetMouseButtonUp(0)) { return TouchInfo.Ended; }
        }
        else
        {
            if (Input.touchCount > 0)
            {
                return (TouchInfo)((int)Input.GetTouch(0).phase);
            }
        }
        return TouchInfo.None;
    }

    /// <summary>
    /// タッチポジションを取得(エディタと実機を考慮)
    /// </summary>
    /// <returns>タッチポジション。タッチされていない場合は (0, 0, 0)</returns>
    public static Vector3[] GetTouchPosition()
    {
        if (Application.isEditor)
        {
            TouchInfo touch = AppUtil.GetTouch();
            if (touch != TouchInfo.None)
            {
                _touchPositions[0] = Input.mousePosition;
                return _touchPositions;
            }
        }
        else
        {
            if (Input.touchCount > 0)
            {
                for (int i = 0; i < Input.touchCount; i++)
                {

                    Touch touch = Input.GetTouch(i);
                    _touchPositions[i].x = touch.position.x;
                    _touchPositions[i].y = touch.position.y;

                }
                
                return _touchPositions;
            }
        }

        _touchPositions[0] = Vector3.zero;

        return _touchPositions;
    }

    /// <summary>
    /// タッチワールドポジションを取得(エディタと実機を考慮)
    /// </summary>
    /// <param name='camera'>カメラ</param>
    /// <returns>タッチワールドポジション。タッチされていない場合は (0, 0, 0)</returns>
    public static Vector3 GetTouchWorldPosition(Camera camera)
    {
        return camera.ScreenToWorldPoint(GetTouchPosition()[0]);
    }
}

/// <summary>
/// タッチ情報。UnityEngine.TouchPhase に None の情報を追加拡張。
/// </summary>
public enum TouchInfo
{
    /// <summary>
    /// タッチなし
    /// </summary>
    None = 99,

    // 以下は UnityEngine.TouchPhase の値に対応
    /// <summary>
    /// タッチ開始
    /// </summary>
    Began = 0,
    /// <summary>
    /// タッチ移動
    /// </summary>
    Moved = 1,
    /// <summary>
    /// タッチ静止
    /// </summary>
    Stationary = 2,
    /// <summary>
    /// タッチ終了
    /// </summary>
    Ended = 3,
    /// <summary>
    /// タッチキャンセル
    /// </summary>
    Canceled = 4,
}
