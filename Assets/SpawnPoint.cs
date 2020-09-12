using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject[] fruitPreb;

    private void Start()
    {
        ini();
    }

    //公用函数，供GameObject调用
    public void ini() {
        //开启协程，每两秒钟生成一个水果
        StartCoroutine(createFruit());
    }

    //生成水果
    IEnumerator createFruit() {
        while(true){
            //根据提供的水果数组，实例化游戏对象
            GameObject fruitClone = Instantiate(fruitPreb[Random.Range(0,fruitPreb.Length)]);
            Rigidbody rb = fruitClone.GetComponent<Rigidbody>();
            //给每个水果一个向上的速度，由于重力，水果在到达一个顶点后开始下落，从而实现上抛运动
            rb.velocity = new Vector3(0, 8.0f, 0);
            //使水果具有随机的旋转速度
            rb.angularVelocity = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
            //获取生成点坐标
            Vector3 pos = transform.position;
            //在出生点水平位置1米范围内获取随机位置
            pos.x += Random.Range(-1f, 1f);
            fruitClone.transform.position = pos;
            yield return new WaitForSeconds(0.5f);
            Destroy(fruitClone, 3);
        }
    }

    public void OnGameOver()
    {
        //停止所有协程，不再继续生成水果
        StopAllCoroutines();
    }

}
