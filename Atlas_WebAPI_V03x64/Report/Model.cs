using System.Collections.Generic;


namespace Atlas_WebAPI_V03x64.Report
{
    public class TargetRect
    {
        public string id;
        public string PictureDesc;//图片描述 “A相”
        public string fileName; //文件名(文件绝对路径)
        public string TopMaxTemperature;//上部高温高温点
        public string BottomMaxTemp;//下部高温高温点
        public string Reference;//参考高温点

    }
    public class ReportModel
    {
        public string CableName { get; set; }//电缆名称
        public string Department { get; set; }//所属班组
        public string AreaName { get; set; }//所属地区
        public string VoltageClasses { get; set; }//电压等级(kV)
        public string Site { get; set; }//检测地点
        public string InspectionDate { get; set; }//检测日期
        public string Inspector { get; set; }//检测人员
        public string DeviceName { get; set; }//检测设备
        public string AirTemperature { get; set; }//气温
        public string Humidity { get; set; }//湿度 
        public string DeviceCameraID { get; set; }//设备镜头 
        public string MeasuringRang { get; set; }//测量范围

        public List<TargetRect> targetRects { get; set; }
    }
}
