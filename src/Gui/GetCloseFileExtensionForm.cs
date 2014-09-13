using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NetFocus.UtilityTool.CodeGenerator.Gui
{
	public partial class GetCloseFileExtensionForm: Form
	{
        public GetCloseFileExtensionForm()
		{
			InitializeComponent();
		}

        private void okButton_Click(object sender, EventArgs e)
        {
            string extension = this.extensionTextBox.Text.Trim();
            if (extension.Length < 2)
            {
                MessageBox.Show("�������׺����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (!extension.StartsWith("."))
            {
                MessageBox.Show("��Ҳ�Ǻ�׺����һ���֣��������Ե㿪ʼ�ĺ�׺����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            WorkbenchSingleton.Workbench.CloseFileExtension = extension;
            this.DialogResult = DialogResult.OK;

            this.Close();

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

            this.Close();
        }
	}
}