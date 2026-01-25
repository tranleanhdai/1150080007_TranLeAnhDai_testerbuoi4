using System;
using System.Windows.Forms;

namespace OrganizationManagement
{
    /// <summary>
    /// Màn hình chính đơn giản để thỏa yêu cầu Back (quay về màn trước).
    /// </summary>
    public class MainForm : Form
    {
        private Button _btnNewOrg;

        public MainForm()
        {
            Text = "Organization Management";
            Width = 420;
            Height = 220;
            StartPosition = FormStartPosition.CenterScreen;

            var lbl = new Label
            {
                Text = "Demo bài tự làm: Organization",
                AutoSize = true,
                Left = 20,
                Top = 20
            };

            _btnNewOrg = new Button
            {
                Text = "Add / Edit Organization",
                Left = 20,
                Top = 60,
                Width = 200,
                Height = 32
            };
            _btnNewOrg.Click += (s, e) =>
            {
                using (var f = new OrganizationForm())
                {
                    f.ShowDialog(this);
                }
            };

            Controls.Add(lbl);
            Controls.Add(_btnNewOrg);
        }
    }
}
