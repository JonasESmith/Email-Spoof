using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace Task_Form
{
  public partial class SpoofMain : MetroForm
  {
    public SpoofMain()
    {
      InitializeComponent();

      this.Theme      = MetroFramework.MetroThemeStyle.Dark;
      this.Style      = MetroFramework.MetroColorStyle.Blue;
                      
      nameTxt.Theme   = MetroFramework.MetroThemeStyle.Dark;
      nameTxt.Style   = MetroFramework.MetroColorStyle.Blue;

      nameLabel.Theme = MetroFramework.MetroThemeStyle.Dark;
      nameLabel.Style = MetroFramework.MetroColorStyle.Blue;

      mailButton.Theme = MetroFramework.MetroThemeStyle.Dark;
      mailButton.Style = MetroFramework.MetroColorStyle.Blue;

      detailTxt.Theme = MetroFramework.MetroThemeStyle.Dark;
      detailTxt.Style = MetroFramework.MetroColorStyle.Blue;

      detLabel.Theme = MetroFramework.MetroThemeStyle.Dark;
      detLabel.Style = MetroFramework.MetroColorStyle.Blue;

      subBox.Theme = MetroFramework.MetroThemeStyle.Dark;
      subBox.Style = MetroFramework.MetroColorStyle.Blue;

      subLabel.Theme = MetroFramework.MetroThemeStyle.Dark;
      subLabel.Style = MetroFramework.MetroColorStyle.Blue;

      spoofBox.Theme = MetroFramework.MetroThemeStyle.Dark;
      spoofBox.Style = MetroFramework.MetroColorStyle.Blue;

      spoofText.Theme = MetroFramework.MetroThemeStyle.Dark;
      spoofText.Style = MetroFramework.MetroColorStyle.Blue;

      toLabel.Theme = MetroFramework.MetroThemeStyle.Dark;
      toLabel.Style = MetroFramework.MetroColorStyle.Blue;

      recTxtBox.Theme = MetroFramework.MetroThemeStyle.Dark;
      recTxtBox.Style = MetroFramework.MetroColorStyle.Blue;

      messageLabel.Theme = MetroFramework.MetroThemeStyle.Dark;
      messageLabel.Style = MetroFramework.MetroColorStyle.Blue;

      txtBoxCount.Theme = MetroFramework.MetroThemeStyle.Dark;
      txtBoxCount.Style = MetroFramework.MetroColorStyle.Blue;

      labelCount.Theme = MetroFramework.MetroThemeStyle.Dark;
      labelCount.Style = MetroFramework.MetroColorStyle.Blue;

      // https://stackoverflow.com/questions/1357853/autocomplete-textbox-control
      // https://stackoverflow.com/questions/9768938/change-the-bordercolor-of-the-textbox
      textTest.BackColor = Color.Black;
      textTest.ForeColor = Color.Gray;
    }

    private void send()
    {
      if (spoofText.Text.Contains("@") && (recTxtBox.Text.Contains("@carthagetigers.org")) && ((spoofText.Text.Contains(".com") || (spoofText.Text.Contains(".org")))))
      {
        string name = nameTxt.Text;
        MailAddress from = new MailAddress(spoofText.Text, name);
        MailAddress to = new MailAddress(recTxtBox.Text, "Name and stuff");
        List<MailAddress> cc = new List<MailAddress>();
        SendEmail(subBox.Text, from, to, cc);

        messageLabel.Text = "Message sent";
      }
      else
      {
        messageLabel.Text = "eMail in wrong format";
      }
    }

    protected void SendEmail(string _subject, MailAddress _from, MailAddress _to, List<MailAddress> _cc, List<MailAddress> _bcc = null)
    {
      int count = 1;
      if (System.Text.RegularExpressions.Regex.IsMatch(txtBoxCount.Text, "  ^ [0-9]"))
      {
        txtBoxCount.Text = "";

      }
      else
      {
        count = Convert.ToInt32(txtBoxCount.Text);
      }

      string Text = "";
      SmtpClient mailClient = new SmtpClient("mail.carthagetigers.org");
      MailMessage msgMail;
      Text = detailTxt.Text;
      msgMail = new MailMessage();
      msgMail.From = _from;
      msgMail.To.Add(_to);

      foreach (MailAddress addr in _cc)
      {
        msgMail.CC.Add(addr);
      }
      if (_bcc != null)
      {
        foreach (MailAddress addr in _bcc)
        {
          msgMail.Bcc.Add(addr);
        }
      }
      msgMail.Subject = _subject;
      msgMail.Body = Text;
      msgMail.IsBodyHtml = true;
      for(int i = 1; i <= count; i++)
        mailClient.Send(msgMail);
      msgMail.Dispose();
    }

    private void mailButton_Click(object sender, EventArgs e)
    {
      /// https://stackoverflow.com/questions/10940732/sending-emails-from-a-windows-forms-application
      send();
    }
  }
}
