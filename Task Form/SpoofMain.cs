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

      this.StyleManager         = myStyleManager;
      subBox.StyleManager       = myStyleManager;
      nameTxt.StyleManager      = myStyleManager;
      toLabel.StyleManager      = myStyleManager;
      detLabel.StyleManager     = myStyleManager;
      subLabel.StyleManager     = myStyleManager;
      spoofBox.StyleManager     = myStyleManager;
      nameLabel.StyleManager    = myStyleManager;
      detailTxt.StyleManager    = myStyleManager;
      spoofText.StyleManager    = myStyleManager;
      recTxtBox.StyleManager    = myStyleManager;
      mailButton.StyleManager   = myStyleManager;
      labelCount.StyleManager   = myStyleManager;
      themeToggle.StyleManager  = myStyleManager;
      txtBoxCount.StyleManager  = myStyleManager;
      messageLabel.StyleManager = myStyleManager;

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

    private void themeToggle_CheckedChanged(object sender, EventArgs e)
    {
      if (myStyleManager.Theme == MetroFramework.MetroThemeStyle.Dark)
      {
        myStyleManager.Theme = MetroFramework.MetroThemeStyle.Light;
        myStyleManager.Style = MetroFramework.MetroColorStyle.Blue;
      }
      else
      {
        myStyleManager.Theme = MetroFramework.MetroThemeStyle.Dark;
        myStyleManager.Style = MetroFramework.MetroColorStyle.Blue;
      }

      this.Refresh();
    }
  }
}
