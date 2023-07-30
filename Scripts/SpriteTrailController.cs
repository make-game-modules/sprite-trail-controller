/**
 * 这是一个Unity的SpriteController脚本，它的主要功能是让一个游戏对象在按下箭头键移动时，生成并控制一系列其精灵克隆体。
 * 这些克隆体会在游戏对象的移动方向上偏移并逐渐变透明，为游戏对象提供一种尾巴效果。当游戏对象停止移动时，所有的克隆体会被销毁。
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteTrailController: MonoBehaviour
{
    public float offset = 0.1f;  // 克隆体的偏移量
    public float transparency = 0.5f;  // 克隆体的透明度

    private SpriteRenderer spriteRenderer;  // 游戏对象的精灵渲染器
    private Vector3 lastPosition;  // 游戏对象的上一个位置
    private List<GameObject> clones = new List<GameObject>();  // 存储所有克隆体的列表

    void Start()
    {
        lastPosition = transform.position;  // 初始化上一个位置
        spriteRenderer = GetComponent<SpriteRenderer>();  // 获取精灵渲染器组件
    }

    void Update()
    {
        Vector3 deltaPosition = transform.position - lastPosition;  // 计算位置的变化

        if (deltaPosition != Vector3.zero)  // 如果位置有变化
        {
            if (clones.Count == 0)  // 如果还没有克隆体
            {
                for (int i = 0; i < 3; i++)  // 创建3个克隆体
                {
                    GameObject clone = new GameObject();  // 创建一个新的游戏对象作为克隆体
                    clone.transform.parent = transform;  // 设置克隆体的父对象为当前游戏对象
                    clone.AddComponent<SpriteRenderer>().sprite = spriteRenderer.sprite;  // 复制精灵

                    SpriteRenderer cloneSpriteRenderer = clone.GetComponent<SpriteRenderer>();  // 获取克隆体的精灵渲染器
                    Color spriteColor = cloneSpriteRenderer.color;  // 获取精灵的颜色
                    spriteColor.a = Mathf.Clamp01(spriteColor.a * Mathf.Pow(transparency, i + 1));  // 根据透明度和克隆体的索引修改精灵的颜色
                    cloneSpriteRenderer.color = spriteColor;  // 应用新的颜色

                    cloneSpriteRenderer.sortingLayerID = spriteRenderer.sortingLayerID;  // 设置渲染层ID
                    cloneSpriteRenderer.sortingOrder = spriteRenderer.sortingOrder - 1;  // 设置渲染顺序

                    clones.Add(clone);  // 把克隆体加入到列表中
                }
            }

            UpdateClonePositions();  // 更新克隆体的位置
        }
        else  // 如果位置没有变化
        {
            foreach (GameObject clone in clones)  // 对于每一个克隆体
            {
                Destroy(clone);  // 销毁它
            }
            clones.Clear();  // 清空列表
        }

        lastPosition = transform.position;  // 更新上一个位置
    }

    // 这个函数用来根据按键输入更新克隆体的位置
    void UpdateClonePositions()
    {
        Vector3 direction = Vector3.zero;  // 初始化移动方向

        if (Input.GetKey(KeyCode.LeftArrow))  // 如果按下左键
        {
            direction.x += 1;  // 向左移动
        }

        if (Input.GetKey(KeyCode.RightArrow))  // 如果按下右键
        {
            direction.x -= 1;  // 向右移动
        }

        if (Input.GetKey(KeyCode.UpArrow))  // 如果按下上键
        {
            direction.y -= 1;  // 向上移动
        }

        if (Input.GetKey(KeyCode.DownArrow))  // 如果按下下键
        {
            direction.y += 1;  // 向下移动
        }

        direction = direction.normalized;  // 归一化移动方向

        //Debug.Log(direction);  // 打印移动方向

        for (int i = 0; i < clones.Count; i++)  // 对于每一个克隆体
        {
            GameObject clone = clones[i];  // 获取克隆体
            Vector3 newPosition = transform.position + direction * (i + 1) * offset;  // 计算新的位置
            clone.transform.position = newPosition;  // 设置新的位置
        }
    }
}
