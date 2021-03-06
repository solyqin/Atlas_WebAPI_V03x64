﻿using System;
using System.IO;
using System.Drawing;

namespace Atlas_WebAPI_V03x64.Models
{
    enum data_type
    {
        max = 1001,
        min ,
        avg ,
    }
    struct ReportResponse
    {
        public string FileName;
        public string Path;
        public ReportResponse(string name, string path)
        {
            this.FileName = name;
            this.Path = path;
        }
    }

    struct TargetPoint
    {
        public int type;
        public string name;
        public double value;
        public int point_x;
        public int point_y;
        public int rectangle_id;

        public TargetPoint(int v1, string v2, double v3, int v4, int v5, int v6) : this()
        {
            this.type = v1;
            this.name = v2;
            this.value = v3;
            this.point_x = v4;
            this.point_y = v5;
            this.rectangle_id = v6;
        }
    }
    public class ResultCollection
    {
        public Object[] Opints_result { get; set; }
        public string FileName { get; set; }
        public object PicBaseInfo { get; set; }
        
        public ResultCollection(Object[] obj, string name,object info)
        {
            this.Opints_result = obj;
            this.FileName = name;
            this.PicBaseInfo = info;
        }
    }

    public class ResultBody
    {
        public int Code { get; set; }
        public string Message { get; set; }
        //public ResultCollection resultCollection { get; set; }
        public Object resultCollection { get; set; }

        public  ResultBody(int def_code = 200,string def_message = "操作成功！")
        {
            this.Code = def_code;
            this.Message = def_message;
        }

        public void Log(int code, string msg)
        {
            this.Code = code;
            this.Message = msg;
        }

        public void NotFound()
        {
            this.Code = 404; 
            this.Message = "访问资源不存在！";
            this.resultCollection = null;
        }
    }

    public class DetailInfo
    {
        public string Title { set; get; } //图片名
        public int Width { set; get; } //宽
        public int Height { set; get; } //高
        public double Max { set; get; }//全图最高温
        public double Min { set; get; } //全图最低温
        public double Average { set; get; } //全图平均温
        public string DateTaken { set; get; } //拍摄日期
        public string AtmosphericTemperature { set; get; } //大气温度
        public string Lens { set; get; } //相机镜头信息
        public string Model { set; get; }  //相机模型信息
        public string Range_max { set; get; } //测量范围
        public string Range_min { set; get; } //测量范围
        public string SerialNumber { set; get; } //相机的序列号
        public string DistanceUnit { set; get; } //距离单位
        public double Distance { set; get; } //到被聚焦对象的距离
        public double Emissivity { set; get; } //红外图像的默认发射率
        public double RelativeHumidity { set; get; } //相对湿度(0.0 - 1.0)
        public double ReflectedTemperature { set; get; }  //反射温度

    }
}