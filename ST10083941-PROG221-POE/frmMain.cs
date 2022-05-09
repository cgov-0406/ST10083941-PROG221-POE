﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ST10083941_PROG221_POE.Classes;

namespace ST10083941_PROG221_POE
{
    public partial class frmMain : Form
    {
        Groceries groceries = new();
        HomeLoan homeLoan = new();
        Other other = new();
        PhoneBill phoneBill = new();
        Rent rent = new();
        Tax tax = new();
        Travel travel = new();
        Utility utility = new();

        bool bRenting;
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnExpenses_Click(object sender, EventArgs e)
        {

            frmExpenses frmExpenses = new();
            this.Hide();
            frmExpenses.ShowDialog();
            this.Show();
            groceries.SetCost(frmExpenses.Groceries);
            utility.SetCost(frmExpenses.Utilities);
            travel.SetCost(frmExpenses.Travel);
            phoneBill.SetCost(frmExpenses.PhoneBill);
            other.SetCost(frmExpenses.Other);

            string monthlyExpenses = 
                  groceries.CostMessage() + "\n" + utility.CostMessage() + "\n" + travel.CostMessage() + "\n" +
                  phoneBill.CostMessage() + "\n" + other.CostMessage();

            rtbExpenses.Text = "COST OF EXPENSES PER MONTH:" + "\n" +  monthlyExpenses;
        }

        private void btnMortgage_Click(object sender, EventArgs e)
        {
            bRenting = false;
            frmHomeLoan frmHomeLoan = new();
            this.Hide();
            frmHomeLoan.ShowDialog();
            this.Show();
            double propertyPrice = frmHomeLoan.PropertyPrice;
            double totalDeposit = frmHomeLoan.TotalDeposit;
            double interest = frmHomeLoan.InterestRate;
            int monthsToRepay = frmHomeLoan.MonthsToRepay;
            double monthlyLoanCost = homeLoan.CalculateCost(propertyPrice, totalDeposit, interest, monthsToRepay);
            homeLoan.SetCost(monthlyLoanCost);
            rtbAccommodation.Text = "COST OF ACCOMMODATION PER MONTH:" + "\n" + homeLoan.CostMessage();
        }

        private void btnRent_Click(object sender, EventArgs e)
        {
            bRenting = true;
            frmRent frmRent = new();
            this.Hide();
            frmRent.ShowDialog();
            this.Show();
            rent.SetCost(frmRent.Rent);
            rtbAccommodation.Text = "COST OF ACCOMMODATION PER MONTH:" + "\n" + rent.CostMessage();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            double monthlyIncome = Convert.ToDouble(nudIncome.Value);
            tax.SetCost(Convert.ToDouble(nudTax.Value));

            double availableIncome = monthlyIncome - (groceries.GetCost() + other.GetCost() + phoneBill.GetCost() + 
               tax.GetCost() + travel.GetCost() + utility.GetCost() );

            if (bRenting == true)
            {
                availableIncome = availableIncome - rent.GetCost();
            }
            else
            {
                availableIncome = availableIncome - homeLoan.GetCost();
            }

            string intro = "BUDGET REPORT\n";
            string monthlyExpenses = tax.CostMessage() + "\n" +groceries.CostMessage() + "\n" + utility.CostMessage() + "\n" + travel.CostMessage() + "\n" +
                  phoneBill.CostMessage() + "\n" + other.CostMessage() + "\n";

            string accommodation;

            if (bRenting == true)
            {
                accommodation = rent.CostMessage() + "\n";
            }
            else
            {
                accommodation = homeLoan.CostMessage() + "\n";
            }

            string end = $"YOUR MONTHLY AVAILABLE INCOME: {availableIncome}";

            string message = intro + monthlyExpenses + accommodation + end;

            rtbReport.Text = message;
        }
    }
}
