﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MShopBaseApi.Model;
namespace MShopBaseApi.Controllers
{
    /// <summary>
    /// 售后表
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AfterSaleController : ControllerBase
    {

        [HttpGet]
        /// <summary>
        /// 显示，单项查看售后信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Orderid"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public List<AfterOrderModel> Get(int userId, int Orderid = 0, int state = 0)
        {
            try
            {
                string msg = $"AfterSaleController 进行了查询操作 条件为userId={userId} and Orderid={Orderid},state={state}";
                LogHelper.Logger.Info(msg);

                string sql = $"select g.GImg1,g.GName,o.OrderNum,g.GPrice,o.OId,o.OrderBH,o.OrderTime,a.* from aftersale as a join orderinfo as o on a.AsId=o.OId join  goods  as g on g.`Gid`=o.GoodsId where o.UserId ={userId}";
                if (state != 0)
                {
                    sql += $" and AsState={state}";
                }
                if (Orderid != 0)
                {
                    sql += $" and o.OId={Orderid}";
                }
                List<AfterOrderModel> list = DBHelper.GetToList<AfterOrderModel>(sql);
                return list;
            }
            catch (Exception ex)
            {
                LogHelper.Logger.Error($"错误GoodsController Get方法 数据为userId={userId} ,Orderid={Orderid},state={state}", ex);
                throw;
            }

        }
        [HttpPost]
        /// <summary>
        /// 添加订单信息
        /// </summary>
        /// <param name="after"></param>
        /// <returns></returns>
        public int Post(AfterSaleModel after)
        {
            try
            {

                after.ApplyTime = DateTime.Now;
                string msg = $"AfterSaleController 进行了添加操作 数据为{after.ToString()}";
                LogHelper.Logger.Info(msg);
                string sql = $"insert into aftersale(AsState,AsRemark,UserId,OrderId,ApplyTime,AsPhone,AsImg) vlaues({after.AsState}, '{ after.AsRemark}  ', {after.UserId}, {after.OrderId}, NOW(), '{after.AsPhone}', '{after.AsImg}')";
                int n = DBHelper.ExecuteNonQuery(sql);
                return n;
            }
            catch (Exception ex)
            {
                LogHelper.Logger.Error($"错误AfterSaleController Post方法 数据为{after.ToString()}", ex);
                throw;
            }

        }
        /// <summary>
        /// 删除售后信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public int Del(int id)
        {
            try
            {
                string msg = $"AfterSaleController 进行了删除操作 数据为{id}";
                LogHelper.Logger.Info(msg);
                string sql = $"delete from aftersale where AsId={id}";
                int n = DBHelper.ExecuteNonQuery(sql);
                return n;
            }
            catch (Exception ex)
            {
                string msg = $"错误AfterSaleController 进行了删除操作 数据为{id}";
                LogHelper.Logger.Error(msg,ex);
                throw;
            }

        }
    }
    /// <summary>
    ///售后显示表
    /// </summary>
    public class AfterOrderModel
    {
        /// <summary>
        /// 商品图片
        /// </summary>
        public string GImg1 { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string GName { get; set; }
        /// <summary>
        /// 订单数量
        /// </summary>
        public int OrderNum { get; set; }
        /// <summary>
        /// 商品价格
        /// </summary>
        public decimal GPrice { get; set; }
        /// <summary>
        /// 订单表主键
        /// </summary>
        public int Old { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderBH { get; set; }
        /// <summary>
        /// 下单时间
        /// </summary>
        public DateTime OrderTime { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public int AsId { get; set; }
        /// <summary>
        /// 售后状态
        /// </summary>
        public int AsState { get; set; }
        /// <summary>
        /// 申请说明
        /// </summary>
        public string AsRemark { get; set; }
        /// <summary>
        /// 链接用户表
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 链接订单表  外键
        /// </summary>
        public int OrderId { get; set; }
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime ApplyTime { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        public string AsPhone { get; set; }
        /// <summary>
        /// 图片凭证
        /// </summary>
        public string AsImg { get; set; }

    }
}