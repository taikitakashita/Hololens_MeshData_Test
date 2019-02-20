using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 効果音の再生機能の読み込み
public class InstantSE : MonoBehaviour
{
    AudioSource aus;
    float length;

    // Use this for initialization
    void Start()
    {
        // AudioSourceのコンポーネントを取得し、AudioClipの長さを設定し、AudioClipを再生する。
        aus = GetComponent<AudioSource>();
        length = aus.clip.length;
        aus.Play();
        Debug.Log(length);
    }

    void Update()
    {
        Debug.Log(length);
        // AudioClipの長さが10より小さい場合はオブジェクトを削除する。
        if (length < 10)
        {
            Destroy(this.gameObject);
        }

        // AudioClipの長さからフレームごとの時間を引いていく。
        length -= Time.deltaTime;

    }
}
