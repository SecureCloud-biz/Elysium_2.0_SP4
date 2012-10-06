namespace Elysium.Notifications.Server
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
            this.NotificationsServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.NotificationsServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // NotificationsServiceProcessInstaller
            // 
            this.NotificationsServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.NotificationsServiceProcessInstaller.Password = null;
            this.NotificationsServiceProcessInstaller.Username = null;
            // 
            // NotificationsServiceInstaller
            // 
            this.NotificationsServiceInstaller.DisplayName = "Elysium Notifications";
            this.NotificationsServiceInstaller.ServiceName = "ElysiumNotifications-v1.5.19.0-v4.5";
            this.NotificationsServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.NotificationsServiceProcessInstaller,
            this.NotificationsServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller NotificationsServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller NotificationsServiceInstaller;
    }
}