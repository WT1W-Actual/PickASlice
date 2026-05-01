using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection; // Added for FileVersionInfo
using System.Windows.Forms;
using PickASlice;
using System.Drawing; // Added for Bitmap, Icon

namespace PickASlice
{
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public partial class Settings : Form
    {
        public SlicerAutoDetector SlicerAutoDetector { get; private set; } = new SlicerAutoDetector();

        public Settings()
        {
            InitializeComponent();
            // Hide the pre-defined static slicer buttons if you intend to only use auto-detected ones dynamically.
            this.btnOrca.Visible = false;
            this.btnCreality.Visible = false;
            this.btnElegoo.Visible = false;
            this.btnAnycubic.Visible = false;
            this.btnQidi.Visible = false;
            this.btnBambu.Visible = false;
            this.btnSettings.Visible = false; // Hide manual settings button if auto-detect is primary

            // *** IMPORTANT: Call auto-detection on form load to populate buttons immediately ***
            btnAutoDetectSlicers_Click(this, EventArgs.Empty); 
        }

        // Helper method to reduce code duplication for launching slicers
        private void LaunchSlicer(string slicerPath, string slicerName)
        {
            if (!string.IsNullOrEmpty(slicerPath) && File.Exists(slicerPath))
            {
                try
                {
                    Process.Start(new ProcessStartInfo(slicerPath) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Could not start {slicerName}. Error: {ex.Message}", "Error Starting Slicer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show($"{slicerName} path is not set or is invalid. Please use the 'Auto-detect' button to configure it.", "Path Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // The following individual slicer click handlers are now for the static buttons,
        // which are currently set to be hidden. If you wish to re-enable them, ensure 
        // their Visible property is true in the constructor or designer.
        private void btnOrca_Click(object sender, EventArgs e)
        {
            string slicerPath = Properties.Settings.Default.OrcaPath;
            LaunchSlicer(slicerPath, "OrcaSlicer");
        }

        private void btnCreality_Click(object sender, EventArgs e)
        {
            string slicerPath = Properties.Settings.Default.CrealityPath;
            LaunchSlicer(slicerPath, "Creality Print");
        }

        private void btnElegoo_Click(object sender, EventArgs e)
        {
            string slicerPath = Properties.Settings.Default.ElegooPath;
            LaunchSlicer(slicerPath, "Elegoo Slicer");
        }

        private void btnAnycubic_Click(object sender, EventArgs e)
        {
            string slicerPath = Properties.Settings.Default.AnycubicPath;
            LaunchSlicer(slicerPath, "Anycubic Slicer Next");
        }

        private void btnQidi_Click(object sender, EventArgs e)
        {
            string slicerPath = Properties.Settings.Default.QidiPath;
            LaunchSlicer(slicerPath, "Qidi Studio");
        }

        private void btnBambuClick(object sender, EventArgs e)
        {
            string slicerPath = Properties.Settings.Default.BambuPath;
            LaunchSlicer(slicerPath, "Bambu Studio");
        }

        // This method handles the manual path setting for all slicers sequentially.
        // It is currently linked to btnSettings, which is now hidden.
        private void btnSettings_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Executable files (*.exe)|*.exe|All files (*.*)|*.*";
                
                dialog.Title = "Select OrcaSlicer Executable";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default.OrcaPath = dialog.FileName;
                    MessageBox.Show("OrcaSlicer path has been saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
                dialog.Title = "Select Bambu Studio Executable";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default.BambuPath = dialog.FileName;
                    MessageBox.Show("Bambu Studio path has been saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
                dialog.Title = "Select Elegoo Slicer Executable";
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

                dialog.Title = "Select Creality Print Executable";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default.CrealityPath = dialog.FileName;
                    MessageBox.Show("Creality Print path has been saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                dialog.Title = "Select AnyCubic SlicerNext Executable";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default.AnycubicPath = dialog.FileName;
                    MessageBox.Show("AnyCubic SlicerNext path has been saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                Properties.Settings.Default.Save();
            }
        }

        // --- UPDATED METHOD FOR AUTO-DETECTION AND DYNAMIC BUTTON CREATION ---
        private void btnAutoDetectSlicers_Click(object sender, EventArgs e)
        {
            // Clear any previously added dynamic slicer buttons
            if (this.flowLayoutPanelSlicers != null)
            {
                this.flowLayoutPanelSlicers.Controls.Clear();
            }
            else
            {
                Debug.WriteLine("[Settings] flowLayoutPanelSlicers is null or not initialized. Cannot create dynamic buttons.");
                MessageBox.Show("Layout panel for slicers is not available. Please check the form designer.", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SlicerAutoDetector detector = new SlicerAutoDetector();
            Dictionary<string, string> detectedPaths = detector.LocateSlicers();
            int pathsUpdated = 0;

            // Define button styling parameters
            Size buttonSize = new Size(120, 90);
            Color backColor = Color.LightSteelBlue;
            Color foreColor = Color.DarkBlue;
            Font buttonFont = new Font("Segoe UI", 8.5F, FontStyle.Bold); // Slightly smaller font
            int iconSize = 48;

            foreach (var entry in detectedPaths)
            {
                string slicerPath = entry.Value;
                string settingKey = entry.Key;

                // Update the application settings with detected paths
                var settingProperty = Properties.Settings.Default.Properties[settingKey];
                if (settingProperty != null && !settingProperty.IsReadOnly)
                {
                    Properties.Settings.Default[settingKey] = slicerPath;
                    pathsUpdated++;
                }

                // Create a new button for each detected slicer
                Button slicerButton = new Button();
                slicerButton.Size = buttonSize;
                slicerButton.Margin = new Padding(5);
                slicerButton.BackColor = backColor;
                slicerButton.ForeColor = foreColor;
                slicerButton.Font = buttonFont;
                slicerButton.FlatStyle = FlatStyle.Flat;
                slicerButton.FlatAppearance.BorderSize = 0;
                slicerButton.Cursor = Cursors.Hand; // Indicate it's clickable


                // Extract and assign the icon
                Bitmap? iconBitmap = SlicerAutoDetector.GetExecutableIcon(slicerPath);
                if (iconBitmap != null)
                {
                    // Scale Icon to the desired size. Use InterpolationMode for better quality if scaling down significantly.
                    Bitmap scaledIcon = new Bitmap(iconSize, iconSize);
                    using (Graphics g = Graphics.FromImage(scaledIcon))
                    {
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        g.DrawImage(iconBitmap, 0, 0, iconSize, iconSize);
                    }
                    slicerButton.Image = scaledIcon;
                    slicerButton.ImageAlign = ContentAlignment.TopCenter;
                    slicerButton.TextImageRelation = TextImageRelation.ImageAboveText;
                    iconBitmap.Dispose(); // Dispose original bitmap after scaling
                }
                else
                {
                    slicerButton.ImageAlign = ContentAlignment.MiddleCenter; 
                    // Optional: Set a default placeholder image or explicitly keep text centered
                    slicerButton.Text = Path.GetFileNameWithoutExtension(slicerPath); // Fallback to filename as text
                }

                // Get the application title for the button text
                string appTitle = Path.GetFileNameWithoutExtension(slicerPath).Replace("Slicer", "").Trim();
                try
                {
                    FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(slicerPath);
                    if (!string.IsNullOrEmpty(versionInfo.ProductName))
                    {
                        appTitle = versionInfo.ProductName;
                    }
                    else if (!string.IsNullOrEmpty(versionInfo.FileDescription))
                    {
                        appTitle = versionInfo.FileDescription;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[Settings] Error getting version info for {slicerPath}: {ex.Message}");
                }

                // If an icon was found, combine the acquired app title with button text, otherwise use only app title.
                // The earlier else block already handles Text if no icon is found.
                if (slicerButton.Image != null)
                {
                     slicerButton.Text = appTitle;
                }
                // Ensure text is truncated/ellipsized if too long for the button
                if (TextRenderer.MeasureText(appTitle, buttonFont).Width > buttonSize.Width - 10) // Basic check
                {
                     slicerButton.Text = appTitle.Substring(0, Math.Min(appTitle.Length, 10)) + "..."; // Truncate and add ellipsis
                }


                slicerButton.TextAlign = ContentAlignment.BottomCenter;
                slicerButton.UseVisualStyleBackColor = false;


                // Store the full path in the Tag property for use in the click event
                slicerButton.Tag = slicerPath;

                // Assign a common click event handler
                slicerButton.Click += SlicerButton_Click;

                // Add the button to the FlowLayoutPanel
                this.flowLayoutPanelSlicers.Controls.Add(slicerButton);
            }

            // Save detected paths to user settings
            if (pathsUpdated > 0)
            {
                Properties.Settings.Default.Save();
                MessageBox.Show($"{pathsUpdated} slicer paths were automatically detected and saved.", "Auto-Detection Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No new slicer paths were detected automatically.", "Auto-Detection Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Common click event handler for dynamically created slicer buttons
        private void SlicerButton_Click(object? sender, EventArgs e)
        {
            Button? clickedButton = sender as Button;
            if (clickedButton != null && clickedButton.Tag is string slicerPath)
            {
                string slicerName = clickedButton.Text; 
                LaunchSlicer(slicerPath, slicerName);
            }
        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }
}
