using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace quotedPrice
{
    public class chestInfo
    {
        public int IncNum { get; set; } //序号
        public string Group { get; set; }//类别
        public string Parts { get; set; }//部件
        public string Materials { get; set; }//材质要求
        public string Color { get; set; }//颜色
        public string Brand { get; set; }//品牌
        public string H { get; set; }//高
        public string W { get; set; }//宽
        public string D { get; set; }//厚
        public string Count { get; set; }//数量
        public string Unit { get; set; }//单位
        public string Area { get; set; }//面积
        public string Price { get; set; }//价格
        public string Amount { get; set; }//金额
        public string Remark { get; set; }//备注
        public string Rprice { get; set; }//标准单价
        public string Ramount { get; set; }//标准金额
        public string ChestTitle { get; set; }//标题
        public string total { get; set; }//合计
    }
}
