namespace WindowsBankingApplication
{
    partial class frmClients
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

                #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label accountNumberLabel;
            System.Windows.Forms.Label dateCreatedLabel;
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbAcctNumber = new System.Windows.Forms.ComboBox();
            this.lblNote = new System.Windows.Forms.Label();
            this.lblAcctType = new System.Windows.Forms.Label();
            this.lblState = new System.Windows.Forms.Label();
            this.lblBalance = new System.Windows.Forms.Label();
            this.lblAcctNumber = new System.Windows.Forms.Label();
            this.lnkTransaction = new System.Windows.Forms.LinkLabel();
            this.lnkDetails = new System.Windows.Forms.LinkLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblDateCreated1 = new EWSoftware.MaskedLabelControl.MaskedLabel();
            this.clientBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblPC1 = new System.Windows.Forms.Label();
            this.lblProvince1 = new EWSoftware.MaskedLabelControl.MaskedLabel();
            this.lblCity1 = new System.Windows.Forms.Label();
            this.addressLabel1 = new System.Windows.Forms.Label();
            this.lblFullName = new System.Windows.Forms.Label();
            this.lblClientNum = new System.Windows.Forms.Label();
            this.lblDateCreated = new System.Windows.Forms.Label();
            this.lblPC = new System.Windows.Forms.Label();
            this.lblProvince = new System.Windows.Forms.Label();
            this.lblCity = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblFName = new System.Windows.Forms.Label();
            this.clientNumberMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.lblRFID = new System.Windows.Forms.Label();
            this.bankAccountBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.descriptionLabel1 = new System.Windows.Forms.Label();
            this.balanceLabel1 = new System.Windows.Forms.Label();
            this.descriptionLabel2 = new System.Windows.Forms.Label();
            this.notesLabel1 = new System.Windows.Forms.Label();
            accountNumberLabel = new System.Windows.Forms.Label();
            dateCreatedLabel = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clientBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bankAccountBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // accountNumberLabel
            // 
            accountNumberLabel.AutoSize = true;
            accountNumberLabel.Location = new System.Drawing.Point(180, 55);
            accountNumberLabel.Name = "accountNumberLabel";
            accountNumberLabel.Size = new System.Drawing.Size(0, 17);
            accountNumberLabel.TabIndex = 15;
            // 
            // dateCreatedLabel
            // 
            dateCreatedLabel.AutoSize = true;
            dateCreatedLabel.Location = new System.Drawing.Point(140, 181);
            dateCreatedLabel.Name = "dateCreatedLabel";
            dateCreatedLabel.Size = new System.Drawing.Size(0, 17);
            dateCreatedLabel.TabIndex = 53;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.notesLabel1);
            this.groupBox2.Controls.Add(this.descriptionLabel2);
            this.groupBox2.Controls.Add(this.balanceLabel1);
            this.groupBox2.Controls.Add(this.descriptionLabel1);
            this.groupBox2.Controls.Add(this.cbAcctNumber);
            this.groupBox2.Controls.Add(accountNumberLabel);
            this.groupBox2.Controls.Add(this.lblNote);
            this.groupBox2.Controls.Add(this.lblAcctType);
            this.groupBox2.Controls.Add(this.lblState);
            this.groupBox2.Controls.Add(this.lblBalance);
            this.groupBox2.Controls.Add(this.lblAcctNumber);
            this.groupBox2.Controls.Add(this.lnkTransaction);
            this.groupBox2.Controls.Add(this.lnkDetails);
            this.groupBox2.Location = new System.Drawing.Point(13, 358);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(835, 306);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Account Data";
            // 
            // cbAcctNumber
            // 
            this.cbAcctNumber.DataSource = this.bankAccountBindingSource;
            this.cbAcctNumber.DisplayMember = "AccountNumber";
            this.cbAcctNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAcctNumber.FormattingEnabled = true;
            this.cbAcctNumber.Location = new System.Drawing.Point(137, 55);
            this.cbAcctNumber.Name = "cbAcctNumber";
            this.cbAcctNumber.Size = new System.Drawing.Size(141, 24);
            this.cbAcctNumber.TabIndex = 21;
            this.cbAcctNumber.ValueMember = "AccountNumber";
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Location = new System.Drawing.Point(18, 141);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(42, 17);
            this.lblNote.TabIndex = 14;
            this.lblNote.Text = "Note:";
            // 
            // lblAcctType
            // 
            this.lblAcctType.AutoSize = true;
            this.lblAcctType.Location = new System.Drawing.Point(448, 101);
            this.lblAcctType.Name = "lblAcctType";
            this.lblAcctType.Size = new System.Drawing.Size(99, 17);
            this.lblAcctType.TabIndex = 12;
            this.lblAcctType.Text = "Account Type:";
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Location = new System.Drawing.Point(18, 98);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(45, 17);
            this.lblState.TabIndex = 10;
            this.lblState.Text = "State:";
            // 
            // lblBalance
            // 
            this.lblBalance.AutoSize = true;
            this.lblBalance.Location = new System.Drawing.Point(484, 56);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(63, 17);
            this.lblBalance.TabIndex = 8;
            this.lblBalance.Text = "Balance:";
            // 
            // lblAcctNumber
            // 
            this.lblAcctNumber.AutoSize = true;
            this.lblAcctNumber.Location = new System.Drawing.Point(18, 55);
            this.lblAcctNumber.Name = "lblAcctNumber";
            this.lblAcctNumber.Size = new System.Drawing.Size(117, 17);
            this.lblAcctNumber.TabIndex = 6;
            this.lblAcctNumber.Text = "Account Number:";
            // 
            // lnkTransaction
            // 
            this.lnkTransaction.AutoSize = true;
            this.lnkTransaction.Enabled = false;
            this.lnkTransaction.Location = new System.Drawing.Point(235, 261);
            this.lnkTransaction.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lnkTransaction.Name = "lnkTransaction";
            this.lnkTransaction.Size = new System.Drawing.Size(137, 17);
            this.lnkTransaction.TabIndex = 4;
            this.lnkTransaction.TabStop = true;
            this.lnkTransaction.Text = "Perform Transaction";
            this.lnkTransaction.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkTransaction_LinkClicked);
            // 
            // lnkDetails
            // 
            this.lnkDetails.AutoSize = true;
            this.lnkDetails.Enabled = false;
            this.lnkDetails.Location = new System.Drawing.Point(453, 261);
            this.lnkDetails.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lnkDetails.Name = "lnkDetails";
            this.lnkDetails.Size = new System.Drawing.Size(84, 17);
            this.lnkDetails.TabIndex = 5;
            this.lnkDetails.TabStop = true;
            this.lnkDetails.Text = "View Details";
            this.lnkDetails.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDetails_LinkClicked);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(dateCreatedLabel);
            this.groupBox1.Controls.Add(this.lblDateCreated1);
            this.groupBox1.Controls.Add(this.lblPC1);
            this.groupBox1.Controls.Add(this.lblProvince1);
            this.groupBox1.Controls.Add(this.lblCity1);
            this.groupBox1.Controls.Add(this.addressLabel1);
            this.groupBox1.Controls.Add(this.lblFullName);
            this.groupBox1.Controls.Add(this.lblClientNum);
            this.groupBox1.Controls.Add(this.lblDateCreated);
            this.groupBox1.Controls.Add(this.lblPC);
            this.groupBox1.Controls.Add(this.lblProvince);
            this.groupBox1.Controls.Add(this.lblCity);
            this.groupBox1.Controls.Add(this.lblAddress);
            this.groupBox1.Controls.Add(this.lblFName);
            this.groupBox1.Controls.Add(this.clientNumberMaskedTextBox);
            this.groupBox1.Controls.Add(this.lblRFID);
            this.groupBox1.Location = new System.Drawing.Point(13, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(787, 288);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Client Data";
            // 
            // lblDateCreated1
            // 
            this.lblDateCreated1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDateCreated1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.clientBindingSource, "DateCreated", true));
            this.lblDateCreated1.Location = new System.Drawing.Point(136, 181);
            this.lblDateCreated1.Name = "lblDateCreated1";
            this.lblDateCreated1.Size = new System.Drawing.Size(162, 23);
            this.lblDateCreated1.TabIndex = 54;
            // 
            // clientBindingSource
            // 
            this.clientBindingSource.DataSource = typeof(BankOfBIT_XW.Models.Client);
            // 
            // lblPC1
            // 
            this.lblPC1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPC1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.clientBindingSource, "PostalCode", true));
            this.lblPC1.Location = new System.Drawing.Point(629, 140);
            this.lblPC1.Name = "lblPC1";
            this.lblPC1.Size = new System.Drawing.Size(100, 23);
            this.lblPC1.TabIndex = 53;
            // 
            // lblProvince1
            // 
            this.lblProvince1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblProvince1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.clientBindingSource, "Province", true));
            this.lblProvince1.Location = new System.Drawing.Point(403, 141);
            this.lblProvince1.Name = "lblProvince1";
            this.lblProvince1.Size = new System.Drawing.Size(91, 23);
            this.lblProvince1.TabIndex = 52;
            // 
            // lblCity1
            // 
            this.lblCity1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCity1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.clientBindingSource, "City", true));
            this.lblCity1.Location = new System.Drawing.Point(137, 143);
            this.lblCity1.Name = "lblCity1";
            this.lblCity1.Size = new System.Drawing.Size(161, 23);
            this.lblCity1.TabIndex = 51;
            // 
            // addressLabel1
            // 
            this.addressLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.addressLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.clientBindingSource, "Address", true));
            this.addressLabel1.Location = new System.Drawing.Point(137, 107);
            this.addressLabel1.Name = "addressLabel1";
            this.addressLabel1.Size = new System.Drawing.Size(592, 23);
            this.addressLabel1.TabIndex = 50;
            // 
            // lblFullName
            // 
            this.lblFullName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFullName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.clientBindingSource, "FullName", true));
            this.lblFullName.Location = new System.Drawing.Point(137, 76);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(592, 23);
            this.lblFullName.TabIndex = 49;
            // 
            // lblClientNum
            // 
            this.lblClientNum.AutoSize = true;
            this.lblClientNum.Location = new System.Drawing.Point(29, 35);
            this.lblClientNum.Name = "lblClientNum";
            this.lblClientNum.Size = new System.Drawing.Size(101, 17);
            this.lblClientNum.TabIndex = 45;
            this.lblClientNum.Text = "Client Number:";
            // 
            // lblDateCreated
            // 
            this.lblDateCreated.AutoSize = true;
            this.lblDateCreated.Location = new System.Drawing.Point(29, 181);
            this.lblDateCreated.Name = "lblDateCreated";
            this.lblDateCreated.Size = new System.Drawing.Size(96, 17);
            this.lblDateCreated.TabIndex = 43;
            this.lblDateCreated.Text = "Date Created:";
            // 
            // lblPC
            // 
            this.lblPC.AutoSize = true;
            this.lblPC.Location = new System.Drawing.Point(531, 141);
            this.lblPC.Name = "lblPC";
            this.lblPC.Size = new System.Drawing.Size(88, 17);
            this.lblPC.TabIndex = 41;
            this.lblPC.Text = "Postal Code:";
            // 
            // lblProvince
            // 
            this.lblProvince.AutoSize = true;
            this.lblProvince.Location = new System.Drawing.Point(334, 142);
            this.lblProvince.Name = "lblProvince";
            this.lblProvince.Size = new System.Drawing.Size(63, 17);
            this.lblProvince.TabIndex = 39;
            this.lblProvince.Text = "Province";
            // 
            // lblCity
            // 
            this.lblCity.AutoSize = true;
            this.lblCity.Location = new System.Drawing.Point(29, 144);
            this.lblCity.Name = "lblCity";
            this.lblCity.Size = new System.Drawing.Size(35, 17);
            this.lblCity.TabIndex = 37;
            this.lblCity.Text = "City:";
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(29, 107);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(64, 17);
            this.lblAddress.TabIndex = 35;
            this.lblAddress.Text = "Address:";
            // 
            // lblFName
            // 
            this.lblFName.AutoSize = true;
            this.lblFName.Location = new System.Drawing.Point(29, 76);
            this.lblFName.Name = "lblFName";
            this.lblFName.Size = new System.Drawing.Size(75, 17);
            this.lblFName.TabIndex = 33;
            this.lblFName.Text = "Full Name:";
            // 
            // clientNumberMaskedTextBox
            // 
            this.clientNumberMaskedTextBox.Location = new System.Drawing.Point(136, 35);
            this.clientNumberMaskedTextBox.Mask = "0000-0000";
            this.clientNumberMaskedTextBox.Name = "clientNumberMaskedTextBox";
            this.clientNumberMaskedTextBox.Size = new System.Drawing.Size(162, 22);
            this.clientNumberMaskedTextBox.TabIndex = 32;
            this.clientNumberMaskedTextBox.Leave += new System.EventHandler(this.clientNumberMaskedTextBox_Leave);
            // 
            // lblRFID
            // 
            this.lblRFID.ForeColor = System.Drawing.Color.Red;
            this.lblRFID.Location = new System.Drawing.Point(344, 27);
            this.lblRFID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRFID.Name = "lblRFID";
            this.lblRFID.Size = new System.Drawing.Size(285, 28);
            this.lblRFID.TabIndex = 30;
            this.lblRFID.Text = "RFID unavailable.  Enter Client ID manually.";
            this.lblRFID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bankAccountBindingSource
            // 
            this.bankAccountBindingSource.DataSource = typeof(BankOfBIT_XW.Models.BankAccount);
            // 
            // descriptionLabel1
            // 
            this.descriptionLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.descriptionLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bankAccountBindingSource, "AccountState.Description", true));
            this.descriptionLabel1.Location = new System.Drawing.Point(137, 99);
            this.descriptionLabel1.Name = "descriptionLabel1";
            this.descriptionLabel1.Size = new System.Drawing.Size(141, 23);
            this.descriptionLabel1.TabIndex = 22;
            // 
            // balanceLabel1
            // 
            this.balanceLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.balanceLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bankAccountBindingSource, "Balance", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C2"));
            this.balanceLabel1.Location = new System.Drawing.Point(567, 55);
            this.balanceLabel1.Name = "balanceLabel1";
            this.balanceLabel1.Size = new System.Drawing.Size(162, 23);
            this.balanceLabel1.TabIndex = 23;
            // 
            // descriptionLabel2
            // 
            this.descriptionLabel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.descriptionLabel2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bankAccountBindingSource, "description", true));
            this.descriptionLabel2.Location = new System.Drawing.Point(567, 101);
            this.descriptionLabel2.Name = "descriptionLabel2";
            this.descriptionLabel2.Size = new System.Drawing.Size(162, 23);
            this.descriptionLabel2.TabIndex = 24;
            // 
            // notesLabel1
            // 
            this.notesLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.notesLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bankAccountBindingSource, "Notes", true));
            this.notesLabel1.Location = new System.Drawing.Point(137, 141);
            this.notesLabel1.Name = "notesLabel1";
            this.notesLabel1.Size = new System.Drawing.Size(592, 76);
            this.notesLabel1.TabIndex = 25;
            // 
            // frmClients
            // 
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(848, 715);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "frmClients";
            this.Text = "Client Information";
            this.Load += new System.EventHandler(this.frmClients_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clientBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bankAccountBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.LinkLabel lnkTransaction;
        private System.Windows.Forms.LinkLabel lnkDetails;
        private System.Windows.Forms.Label lblRFID;
        private System.Windows.Forms.Label lblClientNum;
        private System.Windows.Forms.Label lblDateCreated;
        private System.Windows.Forms.Label lblPC;
        private System.Windows.Forms.Label lblProvince;
        private System.Windows.Forms.Label lblCity;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.MaskedTextBox clientNumberMaskedTextBox;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.Label lblAcctType;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.Label lblBalance;
        private System.Windows.Forms.Label lblAcctNumber;
        private System.Windows.Forms.Label addressLabel1;
        private System.Windows.Forms.BindingSource clientBindingSource;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.Label lblFName;
        private System.Windows.Forms.Label lblCity1;
        private EWSoftware.MaskedLabelControl.MaskedLabel lblDateCreated1;
        private System.Windows.Forms.Label lblPC1;
        private EWSoftware.MaskedLabelControl.MaskedLabel lblProvince1;
        private System.Windows.Forms.ComboBox cbAcctNumber;
        private System.Windows.Forms.BindingSource bankAccountBindingSource;
        private System.Windows.Forms.Label notesLabel1;
        private System.Windows.Forms.Label descriptionLabel2;
        private System.Windows.Forms.Label balanceLabel1;
        private System.Windows.Forms.Label descriptionLabel1;
 
    }
}