﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MShopBaseApi.Model;

namespace MShopBaseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoodsTypeController : ControllerBase
    {
        public List<GoodsTypeModel> Get()
        {
            string sql = $"SELECT * FROM GoodsType  ";    
            List<GoodsTypeModel> good = DBHelper.GetToList<GoodsTypeModel>(sql);
            return good;
        }

    }
}
