using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int rowSize, colSize,rowSize1,colSize1,rowSize2,colSize2;
    int[,] array = new int[10, 10];
    public Transform count;
    // Start is called before the first frame update
    void Start()
    {
        rowSize = Random.Range(0, 10);
        //rowSize1 = Random.Range(0, 10);
        //rowSize2 = Random.Range(0, 10);
        colSize = Random.Range(0, 10);
        //colSize1 = Random.Range(0, 10);
        //colSize2 = Random.Range(0, 10);
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                array[rowSize, colSize] = 1;
                //右上
                if (colSize >= 0 && colSize < 9 && rowSize >= 0 && rowSize < 9)
                {
                    array[rowSize + 1, colSize + 1] = 1;
                }
                //左下
                if (colSize> 0 && colSize <= 9 && rowSize > 0 && rowSize <= 9)
                {
                    array[rowSize - 1, colSize - 1] = 1;
                }
                //左上
                if (colSize >= 0 && colSize < 9 && rowSize > 0 && rowSize <= 9)
                {
                    array[rowSize - 1, colSize + 1] = 1;
                }
                //右下
                if (colSize > 0 && colSize <= 9 && rowSize >= 0 && rowSize < 9)
                {
                    array[rowSize + 1, colSize - 1] = 1;
                }
                //右
                Rightlocation(rowSize, colSize);
                //上
                Onlocation(rowSize, colSize);
                //左
                Leftlocation(rowSize, colSize);
                //下
                Backlocation(colSize, rowSize);
                //array[rowSize1, colSize1] = 1;
                //Onlocation(rowSize1, colSize1);
                //Leftlocation(rowSize1, colSize1);
                //Backlocation(rowSize1, colSize1);
                //Rightlocation(rowSize1, colSize1);
                //array[rowSize2, colSize2] = 1;
                //Onlocation(rowSize2, colSize2);
                //Leftlocation(rowSize2, colSize2);
                //Backlocation(rowSize2, colSize2);
                //Rightlocation(rowSize2, colSize2);
                Loadoutput(i, j);
                //Debug.Log(array[i,j]);
            }
        }
    }
    /// <summary>
    /// 输出加载
    /// </summary>
    /// <param name="i">行</param>
    /// <param name="j">列</param>
    private void Loadoutput(int i, int j)
    {
        if (array[i, j] == 1)
        {
            GameObject map = Instantiate(Resources.Load<GameObject>("Plane"), count, false);
            map.transform.position = new Vector3(i, 0, j);
        }
        else
        {
            GameObject map = Instantiate(Resources.Load<GameObject>("Plane 1"), count, false);
            map.transform.position = new Vector3(i, 0, j);
        }
    }
    #region  四周方向区域
    /// <summary>
    /// 上位置
    /// </summary>
    /// <param name="rowSize">行下标</param>
    /// <param name="colSize">列下标</param>
    public void Onlocation(int rowSize, int colSize)
    {
        if (colSize >= 0 && colSize < 9)
        {
            array[rowSize, colSize + 1] = 1; 
        }
    }
    /// <summary>
    /// 下位置
    /// </summary>
    /// <param name="colSize"></param>
    /// <param name="rowSize"></param>
    private void Backlocation(int colSize, int rowSize)
    {
        if (colSize <= 9 && colSize > 0)
        {
            array[rowSize, colSize - 1] = 1;
        }
    }
    /// <summary>
    /// 左位置
    /// </summary>
    /// <param name="rowSize"></param>
    /// <param name="colSize"></param>
    private void Leftlocation(int rowSize, int colSize)
    {
        if (rowSize <= 9 && rowSize > 0)
        {
            array[rowSize - 1, colSize] = 1;
        }
    }
    /// <summary>
    /// 右位置
    /// </summary>
    /// <param name="rowSize"></param>
    /// <param name="colSize"></param>
    private void Rightlocation(int rowSize, int colSize)
    {
        if (rowSize >= 0 && rowSize < 9)
        {
            array[rowSize + 1, colSize] = 1;
        }
    }
    #endregion
}
