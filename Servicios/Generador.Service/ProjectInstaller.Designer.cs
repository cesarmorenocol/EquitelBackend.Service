namespace EquitelBackend.Service
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
            this.postsProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.postsInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // postsProcessInstaller
            // 
            this.postsProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.postsProcessInstaller.Password = null;
            this.postsProcessInstaller.Username = null;
            // 
            // postsInstaller
            // 
            this.postsInstaller.Description = "Servicio de generación y envío de posts";
            this.postsInstaller.DisplayName = "ServicioPosts";
            this.postsInstaller.ServiceName = "Posts";
            this.postsInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.postsProcessInstaller,
            this.postsInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller postsProcessInstaller;
        private System.ServiceProcess.ServiceInstaller postsInstaller;
    }
}