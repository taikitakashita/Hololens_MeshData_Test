using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 距離によって音の生成速度を変える
public class soundInstance : MonoBehaviour
{
    public AudioClip ac;
    public GameObject instance;
    [Range(0.05f, 1)] public float normalize = 1;
    float length;
    float c;
    // Use this for initialization
    void Start()
    {
        // AudioClipの音の長さを設定する。
        length = ac.length;

        // AudioClipの音の長さにnormalize変数を掛けた数字に1を足す。
        c = length * normalize + 1;
    }
    // Update is called once per frame
    void Update()
    {
        // normalize変数が0.05より小さい場合、0.05に設定する。そうでない場合はそのままの値を設定する。
        normalize = normalize < 0.05f ? 0.05f : normalize;

        // cがAudioClipの音の長さにnormalize変数を掛けた数字より大きい場合の処理
        if (c > length * normalize)
        {
            // cを0に設定する。
            c = 0;

            // AudioSourceを設定したオブジェクトを生成し、AudioSourceのAudioClipを設定する。
            GameObject obj = Instantiate(instance, transform.position, Quaternion.identity) as GameObject;
            obj.GetComponent<AudioSource>().clip = ac;
        }

        // cにフレームごとの時間を追加する。
        c += Time.deltaTime;
    }
}
