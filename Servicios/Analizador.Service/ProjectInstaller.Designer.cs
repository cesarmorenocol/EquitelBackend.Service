namespace Analizador.Service
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.analizadorProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.analizadorInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // analizadorProcessInstaller
            // 
            this.analizadorProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.analizadorProcessInstaller.Password = null;
            this.analizadorProcessInstaller.Username = null;
            // 
            // analizadorInstaller
            // 
            this.analizadorInstaller.Description = "Servicio de analisis de posts";
            this.analizadorInstaller.DisplayName = "ServicioAnalizador";
            this.analizadorInstaller.ServiceName = "Analizador";
            this.analizadorInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.analizadorProcessInstaller,
            this.analizadorInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller analizadorProcessInstaller;
        private System.ServiceProcess.ServiceInstaller analizadorInstaller;
    }
}