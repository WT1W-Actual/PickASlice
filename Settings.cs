using System.Diagnostics;

namespace PickASlice
{
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void btnOrca_Click(object sender, EventArgs e)
        {
            // 1. Read the saved path from settings
            string slicerPath = Properties.Settings.Default.OrcaPath;

            // 2. Check if the path exists and is not empty
            if (!string.IsNullOrEmpty(slicerPath) && System.IO.File.Exists(slicerPath))
            {
                try
                {
                    // 3. Launch the application
                    System.Diagnostics.Process.Start(slicerPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not start the slicer. Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // 4. If path is bad, tell the user to set it
                MessageBox.Show("Slicer path is not set or is invalid. Please use the Settings button to configure it.", "Path Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCreality_Click(object sender, EventArgs e)
        {
            // 1. Read the saved path from settings
            string slicerPath = Properties.Settings.Default.CrealityPath;

            // 2. Check if the path exists and is not empty
            if (!string.IsNullOrEmpty(slicerPath) && System.IO.File.Exists(slicerPath))
            {
                try
                {
                    // 3. Launch the application
                    System.Diagnostics.Process.Start(slicerPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not start the slicer. Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // 4. If path is bad, tell the user to set it
                MessageBox.Show("Slicer path is not set or is invalid. Please use the Settings button to configure it.", "Path Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnElegoo_Click(object sender, EventArgs e)
        {
            // 1. Read the saved path from settings
            string slicerPath = Properties.Settings.Default.ElegooPath;

            // 2. Check if the path exists and is not empty
            if (!string.IsNullOrEmpty(slicerPath) && System.IO.File.Exists(slicerPath))
            {
                try
                {
                    // 3. Launch the application
                    System.Diagnostics.Process.Start(slicerPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not start the slicer. Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // 4. If path is bad, tell the user to set it
                MessageBox.Show("Slicer path is not set or is invalid. Please use the Settings button to configure it.", "Path Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAnycubic_Click(object sender, EventArgs e)
        {
            // 1. Read the saved path from settings
            string slicerPath = Properties.Settings.Default.AnycubicPath;

            // 2. Check if the path exists and is not empty
            if (!string.IsNullOrEmpty(slicerPath) && System.IO.File.Exists(slicerPath))
            {
                try
                {
                    // 3. Launch the application
                    System.Diagnostics.Process.Start(slicerPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not start the slicer. Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // 4. If path is bad, tell the user to set it
                MessageBox.Show("Slicer path is not set or is invalid. Please use the Settings button to configure it.", "Path Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnQidi_Click(object sender, EventArgs e)
        {
            // 1. Read the saved path from settings
            string slicerPath = Properties.Settings.Default.QidiPath;

            // 2. Check if the path exists and is not empty
            if (!string.IsNullOrEmpty(slicerPath) && System.IO.File.Exists(slicerPath))
            {
                try
                {
                    // 3. Launch the application
                    System.Diagnostics.Process.Start(slicerPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not start the slicer. Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // 4. If path is bad, tell the user to set it
                MessageBox.Show("Slicer path is not set or is invalid. Please use the Settings button to configure it.", "Path Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnBambuClick(object sender, EventArgs e)
        {
            // 1. Read the saved path from settings
            string slicerPath = Properties.Settings.Default.BambuPath;

            // 2. Check if the path exists and is not empty
            if (!string.IsNullOrEmpty(slicerPath) && System.IO.File.Exists(slicerPath))
            {
                try
                {
                    // 3. Launch the application
                    System.Diagnostics.Process.Start(slicerPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not start the slicer. Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // 4. If path is bad, tell the user to set it
                MessageBox.Show("Slicer path is not set or is invalid. Please use the Settings button to configure it.", "Path Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnSettings_Click(object sender, EventArgs e)
        {
            // Use an OpenFileDialog to let the user pick the .exe file
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Select OrcaSlicer";
                dialog.Filter = "Executable files (*.exe)|*.exe|All files (*.*)|*.*";
                // If the user selects a file and clicks OK
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    // Save the chosen path to the OrcaPath setting
                    Properties.Settings.Default.OrcaPath = dialog.FileName;
                    MessageBox.Show("OrcaSlicer path has been saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                dialog.Title = "Select Bambu Studio";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default.BambuPath = dialog.FileName;
                    MessageBox.Show("Bambu Studio path has been saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                dialog.Title = "Select Elegoo Slicer";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default.ElegooPath = dialog.FileName;
                    MessageBox.Show("Elegoo Slicer path has been saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                dialog.Title = "Select Qidi Studio Executable";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default.QidiPath = dialog.FileName;
                    MessageBox.Show("Qidi Studio has been saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                dialog.Title = "Select Creality PrintExecutable";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default.CrealityPath = dialog.FileName;
                    MessageBox.Show("Creality Print path has been saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                dialog.Title = "Select AnyCubic Slicer Next Executable";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default.AnycubicPath = dialog.FileName;
                    MessageBox.Show("AnyCubic Slicer Next path has been saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                // IMPORTANT: You must call Save() for the changes to be permanent!
                Properties.Settings.Default.Save();
            }
        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }
}
