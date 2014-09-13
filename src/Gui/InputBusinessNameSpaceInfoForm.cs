using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace NetFocus.UtilityTool.CodeGenerator.Gui
{
	public class InputBusinessNameSpaceInfoForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label namespaceLabel;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.TextBox namespaceTextBox;
        private TextBox referenceTextBox;
        private Label label1;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

        public InputBusinessNameSpaceInfoForm()
		{
			//
			// Windows ���������֧���������
			//
			InitializeComponent();

			//
			// TODO: �� InitializeComponent ���ú������κι��캯������
			//
		}

		/// <summary>
		/// ������������ʹ�õ���Դ��
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows ������������ɵĴ���
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputBusinessNameSpaceInfoForm));
            this.namespaceLabel = new System.Windows.Forms.Label();
            this.namespaceTextBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.referenceTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // namespaceLabel
            // 
            this.namespaceLabel.Location = new System.Drawing.Point(32, 24);
            this.namespaceLabel.Name = "namespaceLabel";
            this.namespaceLabel.Size = new System.Drawing.Size(64, 23);
            this.namespaceLabel.TabIndex = 0;
            this.namespaceLabel.Text = "�����ռ�";
            this.namespaceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // namespaceTextBox
            // 
            this.namespaceTextBox.Location = new System.Drawing.Point(129, 24);
            this.namespaceTextBox.Name = "namespaceTextBox";
            this.namespaceTextBox.Size = new System.Drawing.Size(218, 21);
            this.namespaceTextBox.TabIndex = 0;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(113, 105);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(56, 23);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "ȷ ��";
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(217, 105);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(56, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "ȡ ��";
            this.cancelButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // referenceTextBox
            // 
            this.referenceTextBox.Location = new System.Drawing.Point(129, 59);
            this.referenceTextBox.Name = "referenceTextBox";
            this.referenceTextBox.Size = new System.Drawing.Size(218, 21);
            this.referenceTextBox.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(31, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 21);
            this.label1.TabIndex = 4;
            this.label1.Text = "���ݲ������ռ�";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // InputBusinessNameSpaceInfoForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(390, 140);
            this.Controls.Add(this.referenceTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.namespaceTextBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.namespaceLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "InputBusinessNameSpaceInfoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "����ҵ���߼���������ռ�";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void okButton_Click(object sender, System.EventArgs e)
		{
			if(this.namespaceTextBox.Text.Trim().Length == 0)
			{
				MessageBox.Show("�����ռ䲻��Ϊ��!","��ʾ",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
				return;
			}

			this.Text = this.namespaceTextBox.Text.Trim();
            this.Tag = this.referenceTextBox.Text.Trim();

			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		
		private void button2_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}


	}
}