using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OrganizationManagement
{
    /// <summary>
    /// Form quản lý Organization theo đề:
    /// - OrgName (*) + Address + Phone + Email
    /// - Save, Back, Director (disable ban đầu)
    /// - Validate và hiển thị lỗi
    /// - Sau khi Save OK thì enable Director và truyền Organization sang DirectorForm
    /// </summary>
    public class OrganizationForm : Form
    {
        private readonly OrganizationService _service;
        private TextBox txtOrgName;
        private TextBox txtAddress;
        private TextBox txtPhone;
        private TextBox txtEmail;

        private Button btnSave;
        private Button btnBack;
        private Button btnDirector;

        private ErrorProvider ep;
        private Organization _savedOrg;

        public OrganizationForm() : this(new OrganizationService()) { }

        public OrganizationForm(OrganizationService service)
        {
            _service = service;

            Text = "Organization";
            Width = 540;
            Height = 320;
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            ep = new ErrorProvider { BlinkStyle = ErrorBlinkStyle.NeverBlink };

            BuildUi();

            // Director disable ban đầu
            btnDirector.Enabled = false;
        }

        private void BuildUi()
        {
            int leftLabel = 20;
            int leftInput = 160;
            int top = 20;
            int rowH = 34;
            int inputW = 320;

            Controls.Add(new Label { Text = "OrgName (*)", Left = leftLabel, Top = top + 6, AutoSize = true });
            txtOrgName = new TextBox { Left = leftInput, Top = top, Width = inputW };
            Controls.Add(txtOrgName);

            top += rowH;
            Controls.Add(new Label { Text = "Address", Left = leftLabel, Top = top + 6, AutoSize = true });
            txtAddress = new TextBox { Left = leftInput, Top = top, Width = inputW };
            Controls.Add(txtAddress);

            top += rowH;
            Controls.Add(new Label { Text = "Phone", Left = leftLabel, Top = top + 6, AutoSize = true });
            txtPhone = new TextBox { Left = leftInput, Top = top, Width = inputW };
            Controls.Add(txtPhone);

            top += rowH;
            Controls.Add(new Label { Text = "Email", Left = leftLabel, Top = top + 6, AutoSize = true });
            txtEmail = new TextBox { Left = leftInput, Top = top, Width = inputW };
            Controls.Add(txtEmail);

            // Buttons
            top += rowH + 10;
            btnSave = new Button { Text = "Save", Left = leftInput, Top = top, Width = 90, Height = 32 };
            btnBack = new Button { Text = "Back", Left = leftInput + 100, Top = top, Width = 90, Height = 32 };
            btnDirector = new Button { Text = "Director", Left = leftInput + 200, Top = top, Width = 110, Height = 32 };

            btnSave.Click += (_, __) => OnSave();
            btnBack.Click += (_, __) => Close();
            btnDirector.Click += (_, __) => OnDirector();

            Controls.Add(btnSave);
            Controls.Add(btnBack);
            Controls.Add(btnDirector);
        }

        private void ClearErrors()
        {
            ep.SetError(txtOrgName, "");
            ep.SetError(txtPhone, "");
            ep.SetError(txtEmail, "");
        }

        private void ApplyErrors(Dictionary<string, string> fieldErrors)
        {
            foreach (var kv in fieldErrors)
            {
                var key = kv.Key;
                var msg = kv.Value;
                if (string.Equals(key, "OrgName", StringComparison.OrdinalIgnoreCase)) ep.SetError(txtOrgName, msg);
                if (string.Equals(key, "Phone", StringComparison.OrdinalIgnoreCase)) ep.SetError(txtPhone, msg);
                if (string.Equals(key, "Email", StringComparison.OrdinalIgnoreCase)) ep.SetError(txtEmail, msg);
            }
        }

        private void OnSave()
        {
            ClearErrors();

            var r = _service.Save(
                txtOrgName.Text,
                txtAddress.Text,
                txtPhone.Text,
                txtEmail.Text
            );

            if (!r.Success)
            {
                // Duplicate name -> message chuẩn theo đề
                if (r.Message == "Organization Name already exists")
                {
                    MessageBox.Show(r.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validation errors -> show per field
                ApplyErrors(r.FieldErrors);
                MessageBox.Show("Please correct the highlighted fields.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _savedOrg = r.SavedOrganization;
            btnDirector.Enabled = true; // enable Director sau Save OK
            MessageBox.Show(r.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void OnDirector()
        {
            if (_savedOrg == null)
            {
                // an toàn: dù button đã disable nhưng vẫn check
                MessageBox.Show("Please save organization first.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var f = new DirectorForm(_savedOrg))
            {
                f.ShowDialog(this);
            }
        }
    }
}
