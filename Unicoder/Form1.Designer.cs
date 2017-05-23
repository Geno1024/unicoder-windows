namespace Unicoder
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.text = new System.Windows.Forms.TextBox();
            this.unicode = new System.Windows.Forms.TextBox();
            this.nameList = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // text
            // 
            this.text.Location = new System.Drawing.Point(12, 12);
            this.text.Name = "text";
            this.text.Size = new System.Drawing.Size(754, 28);
            this.text.TabIndex = 0;
            this.text.TextChanged += new System.EventHandler(this.Text_TextChanged);
            // 
            // unicode
            // 
            this.unicode.Location = new System.Drawing.Point(12, 46);
            this.unicode.Name = "unicode";
            this.unicode.Size = new System.Drawing.Size(754, 28);
            this.unicode.TabIndex = 1;
            this.unicode.TextChanged += new System.EventHandler(this.Unicode_TextChanged);
            // 
            // nameList
            // 
            this.nameList.FormattingEnabled = true;
            this.nameList.ItemHeight = 18;
            this.nameList.Location = new System.Drawing.Point(12, 80);
            this.nameList.Name = "nameList";
            this.nameList.Size = new System.Drawing.Size(754, 454);
            this.nameList.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 544);
            this.Controls.Add(this.nameList);
            this.Controls.Add(this.unicode);
            this.Controls.Add(this.text);
            this.Name = "Form1";
            this.Text = "Unicoder";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox text;
        private System.Windows.Forms.TextBox unicode;
        private System.Windows.Forms.ListBox nameList;
    }
}

