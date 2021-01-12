using Atlas_WebAPI_V03x64.Models;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace Atlas_WebAPI_V03x64.Report
{
    public class GenerateReport
    {
        private static readonly string ReportFormat = ".doc";
        //private static BindingFlags s_flag = BindingFlags.Instance | BindingFlags.Public;
        //private static Document doc = new Document(FileManage.GetReportModelFolderPath());
        public static string SaveReport(Report_param report)
        {
            try
            {
                Document doc = new Document(FileManage.GetReportModelFolderPath());//使用报告模板
                
                doc.Properties.FormFieldShading = true; //清除表单域阴影
                //---------------------------
                try
                {
                    foreach (PropertyInfo p in report.ReportParam.GetType().GetProperties())
                    {
                        if (p.Name == "targetRects")
                        {
                            List<TargetRect> targets = p.GetValue(report.ReportParam) as List<TargetRect>;
                            foreach (TargetRect item in targets)
                            {
                                RepalceTargetAreaElement(doc, "TopMaxTemperature", item.id, item.TopMaxTemperature);
                                RepalceTargetAreaElement(doc, "BottomMaxTemp", item.id, item.BottomMaxTemp);
                                RepalceTargetAreaElement(doc, "PictureDesc", item.id, item.PictureDesc);
                                RepalceTargetAreaElement(doc, "Reference", item.id, item.Reference);

                                RepalcePicture(doc, item, item.fileName); //插图
                            }
                        }
                        else
                        {
                            RepalceBaseInfoElement(doc, p.Name, report.ReportParam); // 替换基本信息
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                   
                string reprotname = FileManage.GenerateFileName(null) + ReportFormat;
                string save_path = Path.Combine(FileManage.GetReportSaveFolderPath(), reprotname);
                doc.SaveToFile(save_path, FileFormat.Doc);
                return save_path;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void RepalceBaseInfoElement(Document doc, string propertyName, ReportModel model)
        {
            try
            {
                FormField replaced_element = doc.Sections[0].Body.FormFields[propertyName];
                if (replaced_element == null)
                    throw new MyException($"模板中不存在属性{propertyName}");

                Type t = model.GetType();
                PropertyInfo p = t.GetProperty(propertyName);
                if (p != null)
                {
                    var value = p.GetValue(model, null);
                    if (value != null && value != DBNull.Value)
                    {
                        switch (replaced_element.Type)
                        {
                            case FieldType.FieldFormTextInput:
                                replaced_element.Text = value.ToString();
                                break;
                            default:
                                break;
                        }
                    }
                    else
                        throw new MyException($"未传入必要参数[{propertyName}]或未传入参数为null");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
          
        }
        private static void RepalceTargetAreaElement(Document doc, string propertyName, string id,string value)
        {
            string serach_element = $"{propertyName}{id}";
            FormField replaced_element = doc.Sections[0].Body.FormFields[serach_element];
            if (replaced_element == null)
                throw new MyException($"模板中不存在属性{propertyName}");

            switch (replaced_element.Type)
            {
                case FieldType.FieldFormTextInput:
                    replaced_element.Text = value;
                    break;
                default:
                    break;
            }
        }

        private static void RepalcePicture(Document doc , TargetRect item, string picPath)
        {
            if (!File.Exists(picPath))
                throw new MyException("报告生成失败,未找到对应图片！");
           
            DocPicture pic = new DocPicture(doc);
            string ss = "Illustration" + item.id.ToString();
            TextSelection selection = doc.FindString(ss, true, true);
            if (selection.Count < 1)
                return;
            pic.LoadImage(Image.FromFile(picPath));
            pic.Width = 250;
            pic.Height = 240;
            TextRange range = selection.GetAsOneRange();
            int index = range.OwnerParagraph.ChildObjects.IndexOf(range);
            range.OwnerParagraph.ChildObjects.Insert(index, pic);
            range.OwnerParagraph.ChildObjects.Remove(range);
        }
    }
}
