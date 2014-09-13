
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.IO;
using System.Text;

using NetFocus.Components.TextEditor.Document;
using NetFocus.Components.TextEditor;
using NetFocus.UtilityTool.CodeGenerator.AddIns.Codons;
using NetFocus.Components.AddIns;
using NetFocus.Components.GuiInterface.Services;
using NetFocus.Components.UtilityLibrary.CommandBars;
using NetFocus.Components.UtilityLibrary.Menus;
using NetFocus.UtilityTool.CodeGenerator.Gui.Components;
using NetFocus.UtilityTool.CodeGenerator.Gui;
using NetFocus.UtilityTool.CodeGenerator.Services;



namespace NetFocus.UtilityTool.CodeGenerator.Commands
{
	public class NewFileCommand : AbstractMenuCommand
	{
		public override void Run()
		{
			NewFileDialog nfd = new NewFileDialog();

			if(nfd.ShowDialog() == DialogResult.OK)
			{
				WorkbenchSingleton.Workbench.NewFile((string)nfd.Tag,nfd.Text);

			}

		}
	}
	public class OpenFileCommand : AbstractMenuCommand
	{
		public override void Run()
		{
			OpenFileDialog dialog = new OpenFileDialog();
			DialogResult result = dialog.ShowDialog();
			if(result == DialogResult.OK)
			{
				WorkbenchSingleton.Workbench.OpenFile(dialog.FileName);
			}
		}
	}
    public class OpenCSFilesCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            string[] files = Directory.GetFiles(WorkbenchSingleton.Workbench.DefaultFilePath + applicationName, "*.*", SearchOption.AllDirectories);
            for(int i = 0; i < files.Length; i++)
            {
                WorkbenchSingleton.Workbench.OpenFile(files[i]);
            }
        }
        private string applicationName = string.Empty;

        public void Run(string applicationName)
        {
            this.applicationName = applicationName;
            Run();
        }
    }
	public class SaveFileCommand : AbstractMenuCommand
	{
		public override void Run()
		{
			TabPage currentPage = WorkbenchSingleton.Workbench.FileTabControl.SelectedTab;
			if(currentPage != null)
			{
				TextEditorControl textEditor = currentPage.Controls[0] as TextEditorControl;
				textEditor.SaveFile(currentPage.Tag.ToString());
				if(currentPage.Text.EndsWith("*"))
				{
					currentPage.Text = currentPage.Text.Substring(0,currentPage.Text.Length - 1);
				}
			}
		}
	}
	public class SaveFileAsCommand : AbstractMenuCommand
	{
		public override void Run()
		{
			TabPage currentPage = WorkbenchSingleton.Workbench.FileTabControl.SelectedTab;
			if(currentPage != null)
			{
				TextEditorControl textEditor = currentPage.Controls[0] as TextEditorControl;
				OpenFileDialog dialog = new OpenFileDialog();
				if(dialog.ShowDialog() == DialogResult.OK)
				{
					StreamWriter writer = new StreamWriter(dialog.FileName);
					writer.Write(textEditor.Document.TextContent);
					writer.Close();
					
				}
			}
		}
	}
	public class SaveAllCommand : AbstractMenuCommand
	{
		public override void Run()
		{
			if(WorkbenchSingleton.Workbench.FileTabControl.TabPages.Count <= 0)
			{
				MessageBox.Show("û��Ҫ������ļ���","��ʾ",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
				return;
			}
			foreach(TabPage page in WorkbenchSingleton.Workbench.FileTabControl.TabPages)  
			{
                TextEditorControl textEditor = page.Controls[0] as TextEditorControl;
				string s = page.Tag.ToString();
				textEditor.SaveFile(s);
				if(page.Text.EndsWith("*"))
				{
					page.Text = page.Text.Substring(0,page.Text.Length - 1);
				}
							
			}
		}
	}
	public class DeleteFileCommand : AbstractMenuCommand
	{
		public override void Run()
		{
			TabPage page = WorkbenchSingleton.Workbench.FileTabControl.SelectedTab;
            if (page != null)
            {
                if (File.Exists(page.Tag.ToString()))
                {
                    DialogResult result = MessageBox.Show("�Ƿ�Ҫɾ����ǰ�ļ���", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        WorkbenchSingleton.Workbench.FileTabControl.TabPages.Remove(page);
                        File.Delete(page.Tag.ToString());
                    }
                }
            }
            else
            {
                MessageBox.Show("��ǰû�д򿪵��ļ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
		}
	}
	public class DeleteAllCommand : AbstractMenuCommand
	{
		public override void Run()
		{
			if(WorkbenchSingleton.Workbench.FileTabControl.TabPages.Count <= 0)
			{
				MessageBox.Show("û��Ҫɾ�����ļ���","��ʾ",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
				return;
			}
			DialogResult result = MessageBox.Show("�Ƿ�Ҫɾ�������ļ���","��ʾ",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
			
			if(result == DialogResult.Yes)
			{
				foreach(TabPage page in WorkbenchSingleton.Workbench.FileTabControl.TabPages)  
				{
					if(File.Exists(page.Tag.ToString()))
					{
						WorkbenchSingleton.Workbench.FileTabControl.TabPages.Remove(page);
						File.Delete(page.Tag.ToString());	
					}
				}
			}
		}
	}
	public class InitializeCommand : AbstractMenuCommand
	{
		public override void Run()
		{
			if(MessageBox.Show("�Ƿ�Ҫˢ��?","����",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2) == DialogResult.Yes)
			{
				WorkbenchSingleton.Workbench.TempTable.Rows.Clear();
				WorkbenchSingleton.Workbench.TempTable1.Rows.Clear();
				WorkbenchSingleton.Workbench.DataSet1 = new DataSet();
				WorkbenchSingleton.Workbench.DataSet2 = new DataSet();
				try
				{
					SqlCommand cmd = new SqlCommand();
					cmd.Connection = WorkbenchSingleton.Workbench.Conn;
					SqlDataAdapter apt = new SqlDataAdapter(cmd);

					cmd.CommandText = "select name from sysobjects where xtype = 'u' and status >= 0 order by name";
					apt.Fill(WorkbenchSingleton.Workbench.DataSet1);

			
					cmd.CommandText = "select name from sysobjects where xtype = 'p' and status >= 0 order by name";
					apt.Fill(WorkbenchSingleton.Workbench.DataSet2);

					WorkbenchSingleton.Workbench.BindDataGrids();

				}
				catch(Exception ex)
				{
                    MessageBox.Show(ex.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					Application.Exit();
				}
			}
		}
	}
	public class CloseFileCommand : AbstractMenuCommand
	{
		public override void Run()
		{
			TabPage page = WorkbenchSingleton.Workbench.FileTabControl.SelectedTab;
			if(page != null)
			{
				if(page.Text.EndsWith("*"))   //��ǰ�ļ��Ѿ����޸Ĺ�
				{
					DialogResult result = MessageBox.Show("�Ƿ񱣴���ļ� " + page.Text.Substring(0,page.Text.Length - 1) + " ���޸ģ�","��ʾ",MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question);

					if(result == DialogResult.Yes)
					{
						TextEditorControl textEditor = WorkbenchSingleton.Workbench.FileTabControl.SelectedTab.Controls[0] as TextEditorControl;
						textEditor.SaveFile(page.Tag.ToString());
						WorkbenchSingleton.Workbench.FileTabControl.TabPages.Remove(page);
					}
					else if(result == DialogResult.No)
					{
						WorkbenchSingleton.Workbench.FileTabControl.TabPages.Remove(page);
					}
				}
				else
				{
					WorkbenchSingleton.Workbench.FileTabControl.TabPages.Remove(page);
				}
			}
			else
			{
				MessageBox.Show("û��Ҫ�رյ��ļ���","��ʾ",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
				return;
			}
		}
	}
    public class DeleteSpecifiedFilesCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            if (WorkbenchSingleton.Workbench.FileTabControl.TabPages.Count == 0)
            {
                MessageBox.Show("��ǰû�д򿪵��ļ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            GetDeleteFileExtensionForm form = new GetDeleteFileExtensionForm();
            form.StartPosition = FormStartPosition.CenterScreen;
            
            if (form.ShowDialog() == DialogResult.OK)
            {
                string extension = WorkbenchSingleton.Workbench.DeleteFileExtension;

                DialogResult result = MessageBox.Show("�Ƿ�Ҫɾ�����к�׺��Ϊ" + extension + "�ļ���", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    foreach (TabPage page in WorkbenchSingleton.Workbench.FileTabControl.TabPages)
                    {
                        if (File.Exists(page.Tag.ToString()))
                        {
                            string ext = new FileInfo(page.Tag.ToString()).Extension;

                            if (ext == extension)
                            {
                                WorkbenchSingleton.Workbench.FileTabControl.TabPages.Remove(page);
                                File.Delete(page.Tag.ToString());
                            }
                        }
                    }
                }
            }

        }
    }
    public class CloseSpecifiedFilesCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            if (WorkbenchSingleton.Workbench.FileTabControl.TabPages.Count == 0)
            {
                MessageBox.Show("��ǰû�д򿪵��ļ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            GetCloseFileExtensionForm form = new GetCloseFileExtensionForm();
            form.StartPosition = FormStartPosition.CenterScreen;

            if (form.ShowDialog() == DialogResult.OK)
            {
                string extension = WorkbenchSingleton.Workbench.CloseFileExtension;
                
                new SaveAllCommand().Run();

                foreach (TabPage page in WorkbenchSingleton.Workbench.FileTabControl.TabPages)
                {
                    string ext = new FileInfo(page.Tag.ToString()).Extension;

                    if (ext == extension)
                    {
                        WorkbenchSingleton.Workbench.FileTabControl.TabPages.Remove(page);
                    }

                }

            }

        }
    }
	public class CloseAllCommand : AbstractMenuCommand
	{
		public override void Run()
		{
			if(WorkbenchSingleton.Workbench.FileTabControl.TabPages.Count <= 0)   //�ж��Ƿ���Ҫ�رյ��ļ�
			{
				MessageBox.Show("û��Ҫ�رյ��ļ���","��ʾ",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
				return;
			}
			bool haveDirtyFile = false;
			foreach(TabPage page in WorkbenchSingleton.Workbench.FileTabControl.TabPages)  
			{
				if(page.Text.EndsWith("*"))
				{
					haveDirtyFile = true;
					break;
				}
			}
			if(haveDirtyFile)  //�ж��Ƿ�������һ���޸Ĺ����ļ�
			{
				DialogResult result = MessageBox.Show("�Ƿ񱣴�������ļ����޸ģ�","��ʾ",MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question);

				if(result == DialogResult.Yes) //�������е��ļ����ر�
				{
					foreach(TabPage page in WorkbenchSingleton.Workbench.FileTabControl.TabPages)  
					{
						TextEditorControl textEditor = WorkbenchSingleton.Workbench.FileTabControl.SelectedTab.Controls[0] as TextEditorControl;
						textEditor.SaveFile(page.Tag.ToString());
						WorkbenchSingleton.Workbench.FileTabControl.TabPages.Remove(page);
							
					}
				}
				else if(result == DialogResult.No) //ֱ�ӹر������ļ�
				{
					foreach(TabPage page in WorkbenchSingleton.Workbench.FileTabControl.TabPages)  
					{
						WorkbenchSingleton.Workbench.FileTabControl.TabPages.Remove(page);
					}
				}
				else if(result == DialogResult.Cancel)
				{
					return;
				}
			}
			//���ȫ���ļ���û�޸Ĺ�����ֱ��ɾ��
			foreach(TabPage page in WorkbenchSingleton.Workbench.FileTabControl.TabPages)  
			{
				WorkbenchSingleton.Workbench.FileTabControl.TabPages.Remove(page);
			}

		}
	}
	public class ExitCommand : AbstractMenuCommand
	{
		public override void Run()
		{
			Application.Exit();
		}
	}
}
