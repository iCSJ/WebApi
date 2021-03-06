﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace CyApiClient
{
    /// <summary>
    /// 餐饮版增删改查HTTP服务，参数为店铺名和Mac校验字串
    /// </summary>
    public class CyHttpClient : BaseHttpClient
    {
        public const string SHOPID = "shopid";
        public const string MAC = "mac";
        public CyHttpClient(string seg) : this()
        {
            Seg = seg;
        }
        public CyHttpClient() : base() { }
        public string CreateMac(string json)
        {
            return Tools.MD5Encode(json + GlobalVar.ClientKey);
        }
        public ApiResultModel Get(string json = "", string action = ACTION_GET, bool withToken = true, string mac = null, int pageIndex = 0, int pageSize = 0, string orderBy = "Id", bool asc = true)
        {
            HttpResponseMessage response;
            using (HttpClient client = CreateClient(action))
            {
                AddJsonAndToken(client, json, withToken);
                client.AddQueryStr(SHOPID, GlobalVar.ShopId.ToString());
                client.AddQueryStr(MAC, mac ?? CreateMac(json));
                if (pageIndex > 0 && pageSize > 0)
                {
                    client.AddQueryStr("pageindex", pageIndex.ToString());
                    client.AddQueryStr("pagesize", pageSize.ToString());
                    client.AddQueryStr("orderby", orderBy);
                    client.AddQueryStr("asc", asc.ToString());
                }
                response = client.GetAsync(client.BaseAddress).Result;
            }
            return response.ParseResult();
        }
        public ApiResultModel Post(string json, string action = ACTION_POST, bool withToken = true, string mac = null)
        {
            HttpResponseMessage response;
            using (HttpClient client = CreateClient(action))
            {
                AddJsonAndToken(client, json, withToken);
                client.AddQueryStr(SHOPID, GlobalVar.ShopId.ToString());
                client.AddQueryStr(MAC, mac ?? CreateMac(json));
                response = client.GetAsync(client.BaseAddress).Result;
            }
            return response.ParseResult();
        }
        public ApiResultModel Put(string json, string action = ACTION_PUT, bool withToken = true, string mac = null)
        {
            HttpResponseMessage response;
            using (HttpClient client = CreateClient(action))
            {
                AddJsonAndToken(client, json, withToken);
                client.AddQueryStr(SHOPID, GlobalVar.ShopId.ToString());
                client.AddQueryStr(MAC, mac ?? CreateMac(json));
                response = client.GetAsync(client.BaseAddress).Result;
            }
            return response.ParseResult();
        }
        public ApiResultModel Modify(string json, string action = ACTION_MODIFY, bool withToken = true, string mac = null)
        {
            HttpResponseMessage response;
            using (HttpClient client = CreateClient(action))
            {
                AddJsonAndToken(client, json, withToken);
                client.AddQueryStr(SHOPID, GlobalVar.ShopId.ToString());
                client.AddQueryStr(MAC, mac ?? CreateMac(json));
                response = client.GetAsync(client.BaseAddress).Result;
            }
            return response.ParseResult();
        }
        public ApiResultModel Delete(string json, string action = ACTION_DELETE, bool withToken = true, string mac = null)
        {
            HttpResponseMessage response;
            using (HttpClient client = CreateClient(action))
            {
                AddJsonAndToken(client, json, withToken);
                client.AddQueryStr(SHOPID, GlobalVar.ShopId.ToString());
                client.AddQueryStr(MAC, mac ?? CreateMac(json));
                response = client.GetAsync(client.BaseAddress).Result;
            }
            return response.ParseResult();
        }
        public ApiResultModel LogicDelete(string json, string action = ACTION_LOGICDELETE, bool withToken = true, string mac = null)
        {
            HttpResponseMessage response;
            using (HttpClient client = CreateClient(action))
            {
                AddJsonAndToken(client, json, withToken);
                client.AddQueryStr(SHOPID, GlobalVar.ShopId.ToString());
                client.AddQueryStr(MAC, mac ?? CreateMac(json));
                response = client.GetAsync(client.BaseAddress).Result;
            }
            return response.ParseResult();
        }
    }
}
