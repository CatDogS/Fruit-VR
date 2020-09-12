using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCutter : MonoBehaviour
{
    //切面材质
    public Material capMat;

    //MonoBehaviour类的预置函数
    private void OnCollisionEnter(Collision collision) {
        //确定切割对象
        GameObject target = collision.gameObject;
        //通过游戏对象数组引用被切割后的目标成员
        GameObject[] pieces = BLINDED_AM_ME.MeshCut.Cut(target, transform.position, transform.right, capMat);
        //如果切割后的另外一半没有刚体组件，则为其添加，使其能够受重力下落
        if (!pieces[1].GetComponent<Rigidbody>()) {
            pieces[1].AddComponent<Rigidbody>();
        }
        //销毁切割后的水果
        Destroy(pieces[0], 3.0f);
        Destroy(pieces[1], 3.0f);

        if (target.tag == "UI")
        {
            //如果碰撞的水果tag为"UI"，则重新开始游戏；
            GameManager.Instance.RestartGame();
        }
        else {
            //否则进行加分处理
            GameManager.Instance.AddScore(5);
        }
    }


}
