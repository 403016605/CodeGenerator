using System;
using System.CodeDom.Compiler;
using System.Windows.Forms;


namespace NetFocus.UtilityTool.CodeGenerator.Gui
{
	public class WorkbenchSingleton
	{
		
		static DefaultWorkbench workbench    = null;

		static void CreateWorkspace()
		{
			DefaultWorkbench w = new DefaultWorkbench();
			workbench = w;				
			w.InitializeWorkspace();//��ʼ���˵�,������,״̬��֮��Ķ���.

		}
		

		public static DefaultWorkbench Workbench 
		{
			get {
				if (workbench == null) {  //���Գ�ʼ��
					CreateWorkspace();
				}
				return workbench;
			}
		}
		
	}
}
