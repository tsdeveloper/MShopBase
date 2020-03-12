﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MShopBaseApi.Model;
using Newtonsoft.Json;

namespace MShopBaseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileeController : ControllerBase
    {
        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="id">全部数据</param>
        /// <param name="pid">反填</param>
        /// <returns></returns>
        [HttpGet]
        public List<ProfileeModel> GetPf(int id = -1, int pid = -1,int Trudd=0)
        {
            string sql = string.Format("select PfId,PfName,PfAddres,PfPhone,PfState,UserId  from profilee  join userinfo  on profilee.UserId = userinfo.UId where 1 = 1 ");
            //判断数据
            if (id != -1)
            {
                sql += string.Format(" and  UserId = '{0}' ", id);
            }
            if (pid != -1)
            {
                //反填
                sql += string.Format("  and   PfId='{0}'", pid);
            }
            if (Trudd!=0)
            {
                string.Format(" and profilee.PfState=true");
            }
            List<ProfileeModel> list = DBHelper.GetToList<ProfileeModel>(sql);
            return list;
        }

        /// <summary>
        /// 添加地址
        /// </summary>
        /// <param name="list">界面获取的值</param>
        /// <returns>然后传入到sql里进行添加</returns>
        // POST: api/Profilee
        [HttpPost]
        public int PostPf(ProfileeModel list)
        {
            string sql = $"insert into Profilee(PfName,pfAddres,PfPhone,PfState,UserId) values('{list.PfName}','{list.PfAddres}','{list.PfPhone}',{list.PfState},'{list.UserId}')";
            int n = DBHelper.ExecuteNonQuery(sql);
            return n;
        }

        // PUT: api/Profilee/5
        [HttpPut]
        public int PutPf(ProfileeModel list)
        {

            string sql = $"update Profilee set PfName='{list.PfName}',pfAddres='{list.PfAddres}',PfPhone='{list.PfPhone}',PfState='{list.PfState}' where  PfId='{list.PfId}' ";
            int n = DBHelper.ExecuteNonQuery(sql);
            return n;

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete]
        public int DeletePf(int id)
        {
            string sql = $@"delete from Profilee where PfId='{id}'";
            int n = DBHelper.ExecuteNonQuery(sql);
            return n;
        }
    }
}
