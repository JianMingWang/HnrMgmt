﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using HnrMgmtAPI.Models.API;
using HnrMgmtAPI.Models.API.JC;
using System.Web;
using HnrMgmtAPI.Models;
using HnrMgmtAPI.Common;
using System;

namespace HnrMgmtAPI.Controllers.API.JC
{

    [RoutePrefix("api/honor")]
    public class HonorController : BaseApiController
    {
        [HttpGet, Route("testget")]
        public ApiResult TestGet(string access_token)
        {
            result = AccessToken.Check(access_token, "");
            //if (result == null)
            //{
            //    result = new ApiResult();

            //    #region
            //    try
            //    {
            //        string value = HttpRuntime.Cache.Get("key").ToString();
            //        result.status = "success";
            //        result.messages = "读取Cache成功  key:" + value;

            //        HttpRuntime.Cache.Remove("key");
            //    }
            //    catch
            //    {
            //        result.status = "error";
            //        result.messages = "读取Cache失败  key:";
            //    }
            //    #endregion

            //    return result;
            //}

            result = new ApiResult();
            result.status = "";
            result.data = "2";

            ParameterCheck.CheckParameters(result);

            return result;

            //result = AccessToken.Check(access_token, "");
            //if (result == null)
            //{

            //    #region 参数验证
            //    if (true)
            //    {
            //        result.status = "error";
            //        result.messages = "参数格式错误";
            //        return result;
            //    }
            //    #endregion

            //    #region 逻辑操作
            //    #endregion

            //}
            //return result;
        }

        [HttpGet, Route("testset")]
        public ApiResult Set(string value)
        {
            result = new ApiResult();

            HttpRuntime.Cache.Insert("key", value);

            result.status = "success";
            result.messages = "设置Cache成功  key:" + value;

            return result;
        }

        [HttpGet, Route("get")]
        public ApiResult Get(string access_token)
        {
            result = AccessToken.Check(access_token, "");
            if (result == null)
            {
                result = new ApiResult();

                #region
                Return_GetHonor returnData = new Return_GetHonor();

                result.status = "success";
                result.messages = "获取数据成功";

                var honorList = from T_Honor in db.T_Honor orderby T_Honor.Name, T_Honor.GradeName select T_Honor;
                if (honorList.Any())
                {
                    returnData.count = honorList.Count();
                    returnData.list = (List<T_Honor>)(honorList.ToList());
                }
                else
                {
                    returnData.count = 0;
                    returnData.list = null;
                }

                result.data = returnData;
                #endregion

                return result;
            }
            return result;
        }

        [HttpGet, Route("get")]
        public ApiResult Get(string access_token, int page, int limit)
        {
            result = AccessToken.Check(access_token, "");
            if (result == null)
            {

                result = new ApiResult();

                #region 参数验证
                if (page == 0 || limit == 0)
                {
                    result.status = "error";
                    result.messages = "参数格式错误";
                    return result;
                }
                #endregion

                Return_GetHonor returnData = new Return_GetHonor();

                result.status = "success";
                result.messages = "获取数据成功";

                var honorList = from T_Honor in db.T_Honor orderby T_Honor.Name, T_Honor.GradeName select T_Honor;
                if (honorList.Any())
                {
                    returnData.count = honorList.Count();
                    returnData.list = (List<T_Honor>)(honorList.Skip((page - 1) * limit).Take(limit).ToList());
                }
                else
                {
                    returnData.count = 0;
                    returnData.list = null;
                }

                result.data = returnData;
                return result;
            }
            return result;
        }

        [HttpGet, Route("add")]
        public ApiResult Add(string access_token, string Name, string GradeName)
        {
            result = AccessToken.Check(access_token, "");
            if (result == null)
            {

                #region 参数验证
                if (Name == "" || Name == null || GradeName == "" || GradeName == null)
                {
                    Dictionary<string, string> errorFields = new Dictionary<string, string>();
                    if (Name == "" || Name == null)
                    {
                        errorFields.Add("Name", "Name字段不能为空");
                    }
                    if (GradeName == "" || GradeName == null)
                    {
                        errorFields.Add("GradeName", "GradeName");
                    }
                    return Error("参数格式错误", errorFields);
                }
                #endregion

                #region 逻辑操作
                var honorModel = from T_Honor in db.T_Honor where (T_Honor.Name == Name && T_Honor.GradeName == GradeName) select T_Honor;

                if (honorModel.Any())
                {
                    return Error("该条记录已存在");
                }
                else
                {
                    try
                    {
                        T_Honor model = new T_Honor();
                        model.HonorID = Guid.NewGuid().ToString();
                        model.Name = Name;
                        model.GradeName = GradeName;
                        db.T_Honor.Add(model);
                        db.SaveChanges();

                        return Success("添加成功");
                    }
                    catch
                    {
                        return Error("添加失败");
                    }
                }
                #endregion

            }
            return result;
        }

        [HttpGet, Route("delete")]
        public ApiResult Delete(string access_token, string honorID)
        {
            result = AccessToken.Check(access_token, "");
            if (result == null)
            {

                #region 参数验证
                if (honorID == null || honorID == "")
                {
                    Dictionary<string, string> errorFields = new Dictionary<string, string>();
                    errorFields.Add("honorID", "honorID字段不能为空");
                    return Error("参数格式错误", errorFields);
                }
                #endregion

                #region 逻辑操作
                var honorModel = from T_Honor in db.T_Honor where (T_Honor.HonorID == honorID) select T_Honor;
                if (honorModel.Any())
                {
                    var honorList = from T_HnrRecord in db.T_HnrRecord where (T_HnrRecord.HonorID == honorID) select T_HnrRecord;
                    if (honorList.Any())
                    {
                        return Error("包含此奖项获奖记录，不能删除");
                    }
                    else
                    {
                        try
                        {
                            T_Honor model = db.T_Honor.Find(honorID);
                            db.T_Honor.Remove(model);
                            db.SaveChanges();

                            return Success("删除成功");
                        }
                        catch
                        {
                            return Error("删除失败");
                        }
                    }
                }
                else
                {
                    return Error("数据库中不包含此奖项ID");
                }
                #endregion

            }
            return result;
        }

        [HttpPost, Route("modify")]
        public ApiResult Modify([FromBody]HonorModify model)
        {
            result = AccessToken.Check(model.access_token, "");
            if (result == null)
            {
                result = ParameterCheck.CheckParameters(model);
                if (result == null)
                {
                    #region 参数验证
                    #endregion

                    #region 逻辑操作
                    T_Honor honorModel = db.T_Honor.Find(model.honorID);
                    if (honorModel != null)
                    {
                        try
                        {
                            honorModel.Name = model.Name;
                            honorModel.GradeName = model.GradeName;
                            db.SaveChanges();
                            return Success("修改成功");
                        }
                        catch
                        {
                            return Success("修改失败");
                        }
                    }
                    else
                    {
                        return Error("数据错误，无法查找到此条记录");
                    }
                    #endregion
                }
                return result;
            }
            return result;
        }
    }
}
