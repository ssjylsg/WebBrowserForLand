using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sashulin.Core;
using Sashulin.common;
using Sashulin;
using System.Diagnostics;
namespace WebBrowserForLand
{
    public partial class FrmMain : DevComponents.DotNetBar.Office2007Form
    {
        Sashulin.ChromeWebBrowser chromeWebCrowser = new Sashulin.ChromeWebBrowser();
        string MoneyTextID = "";
        string MoneyPlusButtonID = "";
        string MoneySubmitButtonID = "";
        CwbElement MoneyTextElement = null;
        CwbElement MoneyPlusElement = null;
        CwbElement MoneySubmitElement = null;
        int many = 0;
        bool isStop = true;
        public FrmMain()
        {
            InitializeComponent();
            this.chromeWebCrowser = new Sashulin.ChromeWebBrowser();
            this.chromeWebCrowser.Dock = DockStyle.Fill;
            this.panelEx5.Controls.Add(this.chromeWebCrowser);
            CSharpBrowserSettings settings = new CSharpBrowserSettings();
            //settings.UserAgent = "Mozilla/5.0 (Linux; Android 4.2.1; en-us; Nexus 4 Build/JOP40D) AppleWebKit/535.19 (KHTML, like Gecko) Chrome/18.0.1025.166 Mobile Safari/535.19";
            settings.CachePath = @"C:\temp\caches";
            settings.DefaultUrl = "https://www.baidu.com";
            this.chromeWebCrowser.isOpenNewWindow = false;
            this.chromeWebCrowser.Initialize(settings);
            this.chromeWebCrowser.OpenUrl("https://www.baidu.com");
            this.chromeWebCrowser.AllowDrop = false;
            this.chromeWebCrowser.PageLoadStartEventHandler += chromeWebCrowser_PageLoadStartEventHandler;
            this.chromeWebCrowser.PageLoadFinishEventhandler+=chromeWebCrowser_PageLoadFinishEventhandler;
            this.Load+=FrmMain_Load;
            var autoPost = System.Configuration.ConfigurationSettings.AppSettings["autoPost"] == "true";
            this.Text = this.Text + (autoPost ? "_自动提交" : "_不自动提交");
        }
        private void FrmMain_Load(object sender, EventArgs e)
        {
           LandConfig landConfig =  LandConfig.GetInstance();
           this.txbManyTextID.Text = landConfig.landConfigInfo.MoneyTextID;
           this.txbManyPlusButtonID.Text = landConfig.landConfigInfo.MoneyPlusButtonID;
           this.txbSubmitButtonID.Text = landConfig.landConfigInfo.MoneySubmitButtonID;
        }
        private void chromeWebCrowser_PageLoadFinishEventhandler(object sender, EventArgs e)
        {
            this.chromeWebCrowser.isOpenNewWindow = false;
        }
        private void chromeWebCrowser_PageLoadStartEventHandler(object sender, EventArgs e)
        {
            MethodInvoker invoker = delegate
            {
                this.txbUrl.Text = this.chromeWebCrowser.Url;
            };
            if ((!base.IsDisposed) && base.InvokeRequired)
            {
                base.Invoke(invoker);
            }
            else
            {
                invoker();
            }
        }
        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (!this.txbUrl.Text.Contains("https://"))
            {
                this.txbUrl.Text = "https://" + this.txbUrl.Text.Trim();
            }
            this.chromeWebCrowser.OpenUrl(this.txbUrl.Text.Trim());
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.chromeWebCrowser.OpenUrl("https://jy.landnj.cn/User_Login.aspx");
        }
        /// <summary>
        /// 注入代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnJingJia_Click(object sender, EventArgs e)
        {
            var script = "";
            using (
                System.IO.FileStream stream =
                    new FileStream(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "web", "test2.js"),
                        FileMode.Open))
            {
                var buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                script = System.Text.ASCIIEncoding.UTF8.GetString(buffer);
            }
            this.chromeWebCrowser.ExecuteScript(script);
            MessageBox.Show("注入成功！");
        }

        private void btnJiankong_Click(object sender, EventArgs e)
        {
            this.expandablePanel1.Expanded = !this.expandablePanel1.Expanded;
        }

        private void btnDebug_Click(object sender, EventArgs e)
        {
            this.chromeWebCrowser.isOpenNewWindow = true;
            this.chromeWebCrowser.ShowDevTool();
        }

        private void btnPre_Click(object sender, EventArgs e)
        {
            this.chromeWebCrowser.GoBack();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            this.chromeWebCrowser.GoForward();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.chromeWebCrowser.OpenUrl(this.chromeWebCrowser.Url);
        }

        private void txbUrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)//判断回车键
            {
                if (System.Configuration.ConfigurationSettings.AppSettings["debug"] != "true")
                {
                    if (!this.txbUrl.Text.Contains("https://"))
                    {
                        this.txbUrl.Text = "https://" + this.txbUrl.Text.Trim();
                    }
                }
                this.chromeWebCrowser.OpenUrl(this.txbUrl.Text.Trim());
            }
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.chromeWebCrowser.Delete();
            Process.GetCurrentProcess().CloseMainWindow();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            
            this.many = 0;
            if (this.txbMany.Text.Trim() == string.Empty)
            {
                MessageBox.Show("请输入目标金额");
                return;
            }
            if (!int.TryParse(this.txbMany.Text.Trim(),out this.many))
            {
                MessageBox.Show("输入值是整数值，且不能带特殊字符");
                return;
            }
            
            DialogResult isOK = MessageBox.Show("是否启动金额为" + this.txbMany.Text.Trim() + "万元的监控", "提示", MessageBoxButtons.OKCancel);
            if (isOK == DialogResult.OK)
            {
                var autoPost = System.Configuration.ConfigurationSettings.AppSettings["autoPost"] == "true";
                this.chromeWebCrowser.ExecuteScript(string.Format("autoPost={1},targetMoney='{0}';start()",
                    this.txbMany.Text.Trim(), autoPost ? "true" : "false"));
                this.labMany.Text = this.txbMany.Text.Trim();
            }

            //    LandConfig landConfig = LandConfig.GetInstance();
            //    this.MoneyTextID = landConfig.landConfigInfo.MoneyTextID;
            //    this.MoneyPlusButtonID = landConfig.landConfigInfo.MoneyPlusButtonID;
            //    this.MoneySubmitButtonID = landConfig.landConfigInfo.MoneySubmitButtonID;
            //    this.MoneyTextElement = this.chromeWebCrowser.Document.GetElementById(this.MoneyTextID);
            //    if (this.MoneyTextElement == null)
            //    {
            //        MessageBox.Show("配置中的金额框ID不正确");
            //        return;
            //    }
            //    this.MoneyPlusElement = this.chromeWebCrowser.Document.GetElementById(this.MoneyPlusButtonID);
            //    if (this.MoneyPlusElement == null)
            //    {
            //        MessageBox.Show("配置中的金额+号按钮的ID不正确");
            //        return;

            //    }


            //    //this.MoneySubmitElement = this.chromeWebCrowser.Document.GetElementById(this.MoneySubmitButtonID);
            //    //if (MoneySubmitElement == null)
            //    //{
            //    //    MessageBox.Show("配置中的提交按钮ID不正确");
            //    //    return;
            //    //}

            //    this.isStop = false;
            //    this.btnStart.Enabled = false;
            //    System.Threading.Thread thread = new System.Threading.Thread(this.doSomthing);
            //    thread.Start();
            //    this.progressBarX1.Text = "监控已启动...";
            //    this.progressBarX1.ProgressType = DevComponents.DotNetBar.eProgressItemType.Marquee;
            //    this.labelX7.Text = "当前调整金额：";
            //    this.labelX7.Visible = true;
            //    this.labelX9.Visible = true;
            //    this.labMany.Visible = true;
            //}
        }
        private void doSomthing()
        {
            while (true)
            {
                int money = 0;
                if (this.isStop)
                {
                    return;
                }
                try
                {
                    this.MoneyTextElement = this.chromeWebCrowser.Document.GetElementById(this.MoneyTextID);
                    this.MoneyPlusElement = this.chromeWebCrowser.Document.GetElementById(this.MoneyPlusButtonID);
                    this.MoneySubmitElement = this.chromeWebCrowser.Document.GetElementById(this.MoneySubmitButtonID);
                    if (int.TryParse(this.MoneyTextElement.Value, out money))
                    {
                        if (money < this.many)
                        {
                            this.MoneyPlusElement.Click();
                            if (System.Configuration.ConfigurationSettings.AppSettings["needsleep"] == "true")
                            {
                                System.Threading.Thread.Sleep(200);
                            }
                        }
                        MethodInvoker invoker = delegate
                        {
                            this.labMany.Text = this.MoneyTextElement.Value;
                        };
                        if ((!base.IsDisposed) && base.InvokeRequired)
                        {
                            base.Invoke(invoker);
                        }
                        else
                        {
                            invoker();
                        }
                        int.TryParse(this.MoneyTextElement.Value, out money);
                        if (money == this.many)
                        {
                           // this.MoneySubmitElement.Click();
                            MethodInvoker invoker1 = delegate
                            {
                                this.labMany.Visible = false;
                                this.labelX9.Visible = false;
                                this.labelX7.Text = "当前值已达到目标值"+money.ToString()+"万元，已提交";
                                MessageBox.Show("当前值已达到目标值" + money.ToString() + "万元，已提交");
                            };
                            if ((!base.IsDisposed) && base.InvokeRequired)
                            {
                                base.Invoke(invoker1);
                            }
                            else
                            {
                                invoker1();
                            }
                            break;
                        }
                        if (money > this.many)
                        {
                            MethodInvoker invoker1 = delegate
                            {
                                this.labMany.Visible = false;
                                this.labelX9.Visible = false;
                                this.labelX7.Text = "当前值超过目标值，退出监控";
                                this.isStop = true;
                                this.btnStart.Enabled = true;
                                this.progressBarX1.Text = "监控已停止...";
                                this.progressBarX1.ProgressType = DevComponents.DotNetBar.eProgressItemType.Standard;
                                MessageBox.Show("当前值超过目标值，退出监控");
                            };
                            if ((!base.IsDisposed) && base.InvokeRequired)
                            {
                                base.Invoke(invoker1);
                            }
                            else
                            {
                                invoker1();
                            }
                            break;
                        }
                    }
                }
                catch
                {
 
                }
                System.Threading.Thread.Sleep(100);
            }
            MethodInvoker invoker2 = delegate
            {
                this.isStop = true;
                this.btnStart.Enabled = true;
                this.progressBarX1.Text = "监控已停止...";
                this.progressBarX1.ProgressType = DevComponents.DotNetBar.eProgressItemType.Standard;
                this.labelX9.Visible = false;
                this.labMany.Visible = false;
            };
            if ((!base.IsDisposed) && base.InvokeRequired)
            {
                base.Invoke(invoker2);
            }
            else
            {
                invoker2();
            }
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
          
            DialogResult isOK = MessageBox.Show("是否停止监控", "提示", MessageBoxButtons.OKCancel);
            if (isOK == DialogResult.OK)
            {
                //this.isStop = true;
                //this.btnStart.Enabled = true;
                //this.progressBarX1.Text = "监控已停止...";
                //this.progressBarX1.ProgressType = DevComponents.DotNetBar.eProgressItemType.Standard;
                //this.labelX7.Visible = false;
                //this.labelX9.Visible = false;
                //this.labMany.Visible = false;

                this.chromeWebCrowser.ExecuteScript("stop()");
            }
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            LandConfig landConfig = LandConfig.GetInstance();
            landConfig.landConfigInfo.MoneyTextID = this.txbManyTextID.Text;
            landConfig.landConfigInfo.MoneyPlusButtonID = this.txbManyPlusButtonID.Text;
            landConfig.landConfigInfo.MoneySubmitButtonID = this.txbSubmitButtonID.Text;
            landConfig.saveConfig();
            MessageBox.Show("配置保存成功");
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            foreach (Process process in processes)
            {
                if (process.Id == current.Id)
                {
                    process.Kill();
                }
            }   
        }
    }
}
