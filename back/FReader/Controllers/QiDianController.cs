﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Freader.Models.Entity;
using Freader.Models.Service;

namespace Freader.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class QiDianController : ControllerBase
    {
        private static BaseResult _invalidUserResult = new BaseResult()
        {
            Error = "会话已过期，请登录",
            NeedLogin = true
        };

        // 搜索书籍
        // GET /qidian/search?kw=kw&page=
        [HttpGet("search")]
        public JsonResult Search([FromHeader]string token, string kw, int page = 1)
        {
            if (!UserService.DoesTokenExist(token))
                return new JsonResult(_invalidUserResult);
            return new JsonResult(Crawler.Search(kw, RemoteSource.qidian, page, false));
        }

        //书籍详情
        //GET /qidian/detail/{bid}
        [HttpGet("detail/{bid}")]
        public JsonResult Detail([FromHeader]string token, string bid)
        {
            if (!UserService.DoesTokenExist(token))
                return new JsonResult(_invalidUserResult);
            return new JsonResult(Crawler.Detail(bid, RemoteSource.qidian, false));
        }

        //书籍目录
        //GET /qidian/catalog/{bid}
        [HttpGet("catalog/{bid}")]
        public JsonResult Catalog([FromHeader]string token, string bid)
        {
            if (!UserService.DoesTokenExist(token))
                return new JsonResult(_invalidUserResult);
            return new JsonResult(Crawler.Catalog(bid, RemoteSource.qidian, false));
        }

        //章节内容
        //GET /qidian/chapter/bid/cid
        [HttpGet("{bid}/{cid}")]
        public JsonResult Chapter([FromHeader]string token, string bid, string cid)
        {
            if (!UserService.DoesTokenExist(token))
                return new JsonResult(_invalidUserResult);
            return new JsonResult(Crawler.Chapter(bid, cid, RemoteSource.qidian, false));
        }
    }
}