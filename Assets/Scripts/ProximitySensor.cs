using HoloToolkit.Unity.SpatialMapping;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 壁の接近によってソナー音を鳴らす
public class ProximitySensor : MonoBehaviour
{
    [SerializeField]
    private float m_RayDistance; // レイを飛ばす最大距離

    [SerializeField]
    public GameObject m_AlarmSE; // 警告音の発信元のオブジェクト

    [SerializeField]
    public GameObject m_AlarmMSG; // 警告音のメッセージのオブジェクト

    [SerializeField]
    private int m_RayDirectionCount; // レイを飛ばす方向の数

    private AudioSource m_ArarmSEAudioSource; // 警告音のAudioSourceコンポーネントを格納する変数
    private bool m_ArarmSEPlaying; // 警告音の再生中かどうかを格納する変数

    // Use this for initialization
    void Start()
    {
        // 警告音のAudioSourceコンポーネントを取得する。
        m_ArarmSEAudioSource = m_AlarmSE.GetComponent<AudioSource>();

        // 警告メッセージを非表示にする。
        m_AlarmMSG.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // DirectionCount本分、周囲にレイを打ち壁に近づいたときに警告(ソナー)音を鳴らす。

        // レイが衝突した位置のリストの準備
        List<Vector3> hitvect = new List<Vector3>();

        // レイが衝突した位置の合計を格納するための準備
        Vector3 sumvect = Vector3.zero;

        RaycastHit rh;

        // レイを飛ばす方向の数分処理を繰り返す。
        for (int i = 0; i < m_RayDirectionCount; i++)
        {
            // Rayを飛ばすベクトルの方向を設定
            Quaternion euler = Quaternion.Euler(0, (360 / m_RayDirectionCount) * i, 0);
            Vector3 dirvect = euler * Vector3.forward;

            // レイが衝突した位置をリストに追加する
            if (Physics.Raycast(transform.position, dirvect, out rh, m_RayDistance, LayerMask.GetMask(new string[] { "Wall" }) , QueryTriggerInteraction.Ignore))
            {
                hitvect.Add(dirvect * rh.distance);
            }
        }

        // レイが衝突した位置を取得して、平均値を計算する。その値の位置からソナー音を鳴らす。
        // つまり、衝突しそうな壁の方向のベクトルを取得して、その方向から音が聞こえる。

        // レイが衝突した位置の和を計算
        for (int i = 0; i < hitvect.Count; i++)
            sumvect += hitvect[i];

        // メニューが非表示の場合のみ警告のチェックを行う。
        if (MenuManagement.Instance.MenuShow == false)
        {
            // レイが衝突した位置の平均位置にレイを飛ばしてヒットした時の処理
            if (Physics.Raycast(transform.position, sumvect.normalized, out rh, m_RayDistance, LayerMask.GetMask(new string[] { "Wall" }), QueryTriggerInteraction.Ignore))
            {

                // 警告音の発信元のオブジェクトをレイがヒットした位置に設定する。
                m_AlarmSE.transform.position = rh.point;
                Debug.Log("Ray Hit : " + rh.point);
                //MenuManagement.Instance.DebugTextObject.text += string.Format("\nHit Layer " + rh.collider.gameObject.layer);

                // 警告音が停止中の場合、再生する。合わせて警告メッセージを表示する。
                if (m_ArarmSEPlaying == false)
                {
                    Debug.Log("Ararm Play");
                    m_ArarmSEAudioSource.Play();
                    m_ArarmSEPlaying = true;
                    m_AlarmMSG.SetActive(true);
                }

            }
            // レイが衝突した位置の平均位置にレイを飛ばしてヒットしなかった時の処理
            else
            {
                // 警告音が停止中でなければ、停止する。合わせて警告メッセージを非表示にする。
                if (m_ArarmSEPlaying == true)
                {
                    Debug.Log("Ararm Stop");
                    m_ArarmSEAudioSource.Stop();
                    m_ArarmSEPlaying = false;
                    m_AlarmMSG.SetActive(false);
                }
            }
        }
    }
}
