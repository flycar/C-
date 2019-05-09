using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing;
using System;

namespace WebApplication1
{
    public partial class Code : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //先产生数字串
            string Code = this.CreateRandomCode(6);
            //用session保存
            Session["CheckCode"] = Code;
            //作图
            CreateImage(Code);
        }

        public string CreateRandomCode(int codeCount)  //生成随机数
        {
            string allChar = "0,1,2,3,4,5,6,7,8,9";
            string[] allCharArray = allChar.Split(',');
            string randomCode = "";
            int temp = -1;
            Random rand = new Random();
            for (int i = 0; i < codeCount; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
                }
                int t = rand.Next(10);
                if (temp != -1 && temp == t)
                {
                    return CreateRandomCode(codeCount);
                }
                temp = t;
                randomCode += allCharArray[t];
            }
            return randomCode;
        }

        private void CreateImage(string checkCode)
        {
            System.Drawing.Bitmap image = new System.Drawing.Bitmap(642, 338);
            Graphics g = Graphics.FromImage(image);
            g.Clear(System.Drawing.Color.FromArgb(19,75,158));
            Random rand = new Random();
            String []s = {"电","动","自","行","车"};
            String[] name = { "购", "物", "网", "站", "制" };
            //输出不同字体和颜色的验证码字符
            for(int i = 0 ; i < 5; i++) {
                Font f = new System.Drawing.Font("宋体", 40, System.Drawing.FontStyle.Bold);
                Brush b = new System.Drawing.SolidBrush(Color.White);
                g.DrawString(s[i], f, b, 170 + (i * 60), 40);
                g.DrawString(name[i], f, b, 170 + (i * 60), 264);
            }
            for (int i = 0; i < checkCode.Length; i++)
            {
                Font f = new System.Drawing.Font("宋体", 120, System.Drawing.FontStyle.Bold);
                Brush b = new System.Drawing.SolidBrush(Color.White);
                g.DrawString(checkCode.Substring(i, 1), f, b, 5 + (i * 100), 100);
            }
            //画一个边框
            g.DrawRectangle(new Pen(Color.White, 5), 10, 10, image.Width - 20, image.Height - 20);
            //输出到浏览器
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            Response.ClearContent();
            Response.ContentType = "image/Gif";
            Response.BinaryWrite(ms.ToArray());
            g.Dispose();
            image.Dispose();
        }
    }
}
