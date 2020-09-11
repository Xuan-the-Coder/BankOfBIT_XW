using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using BankOfBIT_XW;
using BankOfBIT_XW.Models;

namespace WindowsBankingApplication
{
    public partial class frmBatch : Form
    {
        private BankOfBIT_XWContext db = new BankOfBIT_XWContext();

        public frmBatch()
        {
            InitializeComponent();
        }

        /// <summary>
        /// given - further code required
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmBatch_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);
            try
            {
                IQueryable<Institution> instNums = from results in db.Institutions 
                                                   select results;

                cboInstitution.DataSource = instNums.ToList();
                cboInstitution.DisplayMember = "Description";
                cboInstitution.ValueMember = "InstitutionNumber";
                cboInstitution.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// given - further code required
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkProcess_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //given - for use in encryption assignment
            if (txtKey.Text.Trim().Length != 8)
            {
                MessageBox.Show("64 Bit Decryption Key must be entered", "Enter Key");
                txtKey.Focus();
            }
            else
            {
                string retStr;
                Batch batch = new Batch();

                if (radSelect.Checked == true)
                {
                    cboInstitution.Enabled = true;

                    batch.ProcessTransmission(cboInstitution.SelectedValue.ToString(), txtKey.Text);
                    retStr = batch.WriteLogData();
                    rtxtLog.Text = retStr;
                }
                else
                {
                    IQueryable<Institution> instNums = from results in db.Institutions select results;
                    foreach (Institution s in instNums.ToList())
                    {
                        batch.ProcessTransmission(s.InstitutionNumber.ToString(), txtKey.Text);
                        retStr = batch.WriteLogData();
                        rtxtLog.Text += retStr;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radSelect_CheckedChanged(object sender, EventArgs e)
        {
            if (radSelect.Checked == true)
            {
                cboInstitution.Enabled = true;
            }
            else
            {
                cboInstitution.Enabled = false;
            }

        }

    }
}
