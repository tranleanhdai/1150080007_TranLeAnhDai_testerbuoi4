using System;
using System.Windows.Forms;

namespace OrganizationManagement
{
    /// <summary>
    /// Form Director Management (tối thiểu theo đề):
    /// - mở sau khi Save Organization thành công
    /// - nhận Organization được truyền sang
    /// </summary>
    public class DirectorForm : Form
    {
        private readonly Organization _org;

        public DirectorForm(Organization org)
        {
            _org = org ?? throw new ArgumentNullException(nameof(org));

            Text = "Director Management";
            Width = 520;
            Height = 220;
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            var lbl = new Label
            {
                Left = 20,
                Top = 20,
                AutoSize = true,
                Text = $"Organization: {_org.OrgName} (ID: {_org.Id})"
            };

            var info = new Label
            {
                Left = 20,
                Top = 55,
                Width = 460,
                Height = 60,
            };

            var btnClose = new Button
            {
                Text = "Close",
                Left = 20,
                Top = 130,
                Width = 90,
                Height = 32
            };
            btnClose.Click += (_, __) => Close();

            Controls.Add(lbl);
            Controls.Add(info);
            Controls.Add(btnClose);
        }
    }
}
