using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    int[,] array = new int[,] { { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },};
    public Transform count;
    public int barrierrowSize, barriercolSize, barrierrowSize1, barriercolSize1, barrierrowSize2, barriercolSize2, barrierrowSize3, barriercolSize3;
    public int playerbeginrowSize, playerbegincolSize;
    public int enemybeginrowSize, enemybegincolSize;
    public int awardcollectionrowSize, awardcollectioncolSize;
    public bool leftdownindex=false,rightdownindex=false,leftupindex=false,rightupindex=false;
    // Start is called before the first frame update
    void Start()
    {
        Setboundary();
        array[playerbeginrowSize, playerbegincolSize] = 2; //玩家出生点
        array[enemybeginrowSize, enemybegincolSize] = 9; //挑战点
        array[awardcollectionrowSize, awardcollectioncolSize] = 3; //挑战点
        if (leftdownindex)
        {
            LeftdownBarrier();
        }
        if (rightdownindex)
        {
            RightdownBarrier();
        }
        if (leftupindex)
        {
            LeftupBarrier();
        }
        if (rightupindex)
        {
            RightupBarrier();
        }
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                Loadoutput(i, j);
            }
        }
    }
    #region 障碍物
    /// <summary>
    /// 右上三角障碍物
    /// </summary>
    private void RightupBarrier()
    {
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                array[barrierrowSize3, barriercolSize3] = 0;
                Downlocation(barrierrowSize3, barriercolSize3);
                Downlocation(barrierrowSize3 + 1, barriercolSize3);
                Liftlocation(barrierrowSize3, barriercolSize3);
                Liftlocation(barrierrowSize3, barriercolSize3 - 1);
                LeftDownlocation(barrierrowSize3, barriercolSize3);
            }
        }
    }
    /// <summary>
    /// 左上三角障碍物
    /// </summary>
    private void LeftupBarrier()
    {
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                array[barrierrowSize2, barriercolSize2] = 0;
                Downlocation(barrierrowSize2, barriercolSize2);
                Downlocation(barrierrowSize2+1, barriercolSize2);
                Rightlocation(barrierrowSize2, barriercolSize2);
                Rightlocation(barrierrowSize2, barriercolSize2 + 1);
                RightDownlocation(barrierrowSize2, barriercolSize2);
            }
        }
    }
    /// <summary>
    /// 右下三角障碍物
    /// </summary>
    /// <exception cref="System.NotImplementedException"></exception>
    private void RightdownBarrier()
    {
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                array[barrierrowSize1, barriercolSize1] = 0;
                Liftlocation(barrierrowSize1, barriercolSize1);
                Liftlocation(barrierrowSize1, barriercolSize1 - 1);
                Uplocation(barrierrowSize1, barriercolSize1);
                Uplocation(barrierrowSize1 - 1, barriercolSize1);
                LeftUplocation(barrierrowSize1, barriercolSize1);
            }
        }
    }
    /// <summary>
    /// 左下三角障碍物
    /// </summary>
    /// <exception cref="System.NotImplementedException"></exception>
    private void LeftdownBarrier()
    {
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                array[barrierrowSize, barriercolSize] = 0;
                Rightlocation(barrierrowSize, barriercolSize);
                Rightlocation(barrierrowSize, barriercolSize+1);
                Uplocation(barrierrowSize, barriercolSize);
                Uplocation(barrierrowSize-1, barriercolSize);
                RightUplocation(barrierrowSize, barriercolSize);
            }
        }
    }
    #endregion

    /// <summary>
    /// 固定边界
    /// </summary>
    private void Setboundary()
    {
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                if (i == 0 || j==10 ||i==10 || j == 0)
                {
                    array[i, j] = 0;
                }
            }
        }
    }
    /// <summary>
    /// 加载资源
    /// </summary>
    /// <param name="i">行</param>
    /// <param name="j">列</param>
    private void Loadoutput(int i, int j)
    {
        //不可行走区域 
        if (array[i, j] == 0)
        {
            GameObject map = Instantiate(Resources.Load<GameObject>("Plane"), count, false);
            map.transform.position = new Vector3(i, 0, j);
        }
        //可行走区域
        else if (array[i, j] == 1)
        {
            GameObject map = Instantiate(Resources.Load<GameObject>("Plane 1"), count, false);
            map.transform.position = new Vector3(i, 0, j);
        }
        //出生点
        else if (array[i, j] == 2)
        {
            GameObject map = Instantiate(Resources.Load<GameObject>("BeginPlane"), count, false);
            map.transform.position = new Vector3(i, 0, j);
        }
        //奖励领取点
        else if (array[i, j] == 3)
        {
            GameObject map = Instantiate(Resources.Load<GameObject>("Plane 1"), count, false);
            map.transform.position = new Vector3(i, 0, j);
            GameObject mapend= Instantiate(Resources.Load<GameObject>("tree"));
            mapend.transform.position = map.transform.position;
        }
        //挑战开启点
        else if (array[i, j] == 9)
        {
            GameObject map = Instantiate(Resources.Load<GameObject>("Plane 1"), count, false);
            map.transform.position = new Vector3(i, 0, j);
            GameObject mapend = Instantiate(Resources.Load<GameObject>("Cube"));
            mapend.transform.position = map.transform.position;
        }
    }
    #region  四周方向区域
    /// <summary>
    /// 右位置
    /// </summary>
    /// <param name="rowSize">行下标</param>
    /// <param name="colSize">列下标</param>
    public void Rightlocation(int rowSize, int colSize)
    {
        if (colSize >= 0 && colSize < 9)
        {
            array[rowSize, colSize + 1] = 0; 
        }
    }
    /// <summary>
    /// 左位置
    /// </summary>
    /// <param name="rowSize"></param>
    /// <param name="colSize"></param>
    private void Liftlocation(int rowSize, int colSize)
    {
        if (colSize <= 9 && colSize > 0)
        {
            array[rowSize, colSize - 1] = 0;
        }
    }
    /// <summary>
    /// 上位置
    /// </summary>
    /// <param name="rowSize"></param>
    /// <param name="colSize"></param>
    private void Uplocation(int rowSize, int colSize)
    {
        if (rowSize <= 9 && rowSize > 0)
        {
            array[rowSize - 1, colSize] = 0;
        }
    }
    /// <summary>
    /// 下位置
    /// </summary>
    /// <param name="rowSize"></param>
    /// <param name="colSize"></param>
    private void Downlocation(int rowSize, int colSize)
    {
        if (rowSize >= 0 && rowSize < 9)
        {
            array[rowSize + 1, colSize] = 0;
        }
    }
    /// <summary>
    /// 右下
    /// </summary>
    /// <param name="rowSize"></param>
    /// <param name="colSize"></param>
    /// <exception cref="System.NotImplementedException"></exception>
    private void RightDownlocation(int rowSize, int colSize)
    {
        if (colSize >= 0 && colSize < 9 && rowSize >= 0 && rowSize < 9)
        {
            array[rowSize + 1, colSize + 1] = 0;
        }
    }
    /// <summary>
    /// 左上
    /// </summary>
    /// <param name="rowSize"></param>
    /// <param name="colSize"></param>
    /// <exception cref="System.NotImplementedException"></exception>
    private void LeftUplocation(int rowSize, int colSize)
    {
        if (colSize > 0 && colSize <= 9 && rowSize > 0 && rowSize <= 9)
        {
            array[rowSize - 1, colSize - 1] = 0;
        }
    }
    /// <summary>
    /// 右上
    /// </summary>
    /// <param name="rowSize"></param>
    /// <param name="colSize"></param>
    /// <exception cref="System.NotImplementedException"></exception>
    private void RightUplocation(int rowSize, int colSize)
    {
        if (colSize >= 0 && colSize < 9 && rowSize > 0 && rowSize <= 9)
        {
            array[rowSize - 1, colSize + 1] = 0;
        }
    }
    /// <summary>
    /// 左下
    /// </summary>
    /// <param name="rowSize"></param>
    /// <param name="colSize"></param>
    /// <exception cref="System.NotImplementedException"></exception>
    private void LeftDownlocation(int rowSize, int colSize)
    {
        if (colSize > 0 && colSize <= 9 && rowSize >= 0 && rowSize < 9)
        {
            array[rowSize + 1, colSize - 1] = 0;
        }
    }
    #endregion
}
