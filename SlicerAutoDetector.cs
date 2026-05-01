using System.Collections.Generic;
using System.IO; // Required for File.Exists and Path
using System; // Required for Environment.GetFolderPath
using System.Diagnostics; // Added for Debug.WriteLine
using System.Drawing; // Added for Icon and Bitmap

namespace PickASlice
{
    public class SlicerAutoDetector
    {
        public Dictionary<string, string> LocateSlicers()
        {
            Dictionary<string, string> detectedPaths = new Dictionary<string, string>();
            // Get common program file directories
            string programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            string programFilesX86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            string appDataLocal = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            // Helper function to try and find a slicer executable
            void TryAddSlicerPath(string settingKey, string[] potentialBasePaths, string[] executableNames)
            {
                foreach (string basePath in potentialBasePaths)
                {
                    if (Directory.Exists(basePath))
                    {
                        foreach (string exeName in executableNames)
                        {
                            string fullPath = Path.Combine(basePath, exeName);
                            if (File.Exists(fullPath))
                            {
                                detectedPaths[settingKey] = fullPath;
                                Debug.WriteLine($"[SlicerAutoDetector] Detected {settingKey}: {fullPath}"); // Log detected path
                                return; // Found, move to next slicer
                            }
                        }
                    }
                }
                Debug.WriteLine($"[SlicerAutoDetector] {settingKey} not found in configured paths after checking."); // Log if not found
            }

            // OrcaSlicer
            TryAddSlicerPath("OrcaPath", new[]
            {
                Path.Combine(programFiles, "OrcaSlicer"),
                Path.Combine(programFilesX86, "OrcaSlicer"),
                Path.Combine(appDataLocal, "OrcaSlicer") // Sometimes installed here
            }, new[] { "Orca Slicer.exe", "orca-slicer.exe" });

            // Bambu Studio
            TryAddSlicerPath("BambuPath", new[]
            {
                Path.Combine(programFiles, "Bambu Studio"),
                Path.Combine(programFilesX86, "Bambu Studio"),
                Path.Combine(appDataLocal, "BambuStudio"), // Sometimes installed here
                Path.Combine("D:\\", "Program Files", "Bambu Studio"), // Check D drive Program Files
                Path.Combine("D:\\", "Bambu Studio") // Check D drive root
            }, new[] { "bambu-studio.exe" });

            // Creality Print (with version wildcard support)
            string[] crealityBasePaths = new[]
            {
                programFiles,
                programFilesX86,
                Path.Combine(programFiles, "Creality"),
                Path.Combine(programFilesX86, "Creality")
            };

            foreach (string basePath in crealityBasePaths)
            {
                if (Directory.Exists(basePath))
                {
                    // Search for directories matching "Creality Print*"
                    string[] matchingDirs = Directory.GetDirectories(basePath, "Creality Print*");
                    foreach (string dir in matchingDirs)
                    {
                        // Creality names executables with version suffixes in some releases.
                        string[] executablePatterns =
                        {
                            "CrealityPrint.exe",
                            "CrealityPrint*.exe",
                            "Creality*Print*.exe"
                        };

                        foreach (string executablePattern in executablePatterns)
                        {
                            string[] exeMatches = Directory.GetFiles(dir, executablePattern, SearchOption.TopDirectoryOnly);
                            if (exeMatches.Length > 0)
                            {
                                // Prefer the most recently updated executable if multiple matches exist.
                                string selectedExe = exeMatches[0];
                                DateTime newestWriteTime = File.GetLastWriteTimeUtc(selectedExe);
                                for (int i = 1; i < exeMatches.Length; i++)
                                {
                                    DateTime candidateWriteTime = File.GetLastWriteTimeUtc(exeMatches[i]);
                                    if (candidateWriteTime > newestWriteTime)
                                    {
                                        selectedExe = exeMatches[i];
                                        newestWriteTime = candidateWriteTime;
                                    }
                                }

                                detectedPaths["CrealityPath"] = selectedExe;
                                Debug.WriteLine($"[SlicerAutoDetector] Detected CrealityPath: {selectedExe}");
                                goto CrealityFound; // Exit all loops once found
                            }
                        }
                    }
                }
            }
            Debug.WriteLine($"[SlicerAutoDetector] CrealityPath not found in configured paths after checking.");
            CrealityFound:

            // Elegoo Slicer
            TryAddSlicerPath("ElegooPath", new[]
            {
                Path.Combine(programFiles, "Elegoo-Slicer"),
                Path.Combine(programFiles, "ElegooSlicer"),
                Path.Combine(programFilesX86, "Elegoo-Slicer"),
                Path.Combine(programFilesX86, "ElegooSlicer")
            }, new[] { "Elegoo-Slicer.exe" });

            // Qidi Studio
            TryAddSlicerPath("QidiPath", new[]
            {
                Path.Combine(programFiles, "QidiStudio"),
                Path.Combine(programFilesX86, "Qidi Studio"),
            }, new[] { "Qidi-Studio.exe" });

            // Anycubic Photon Workshop / SlicerNext
            TryAddSlicerPath("AnycubicPath", new[]
            {
                Path.Combine(programFiles, "ANYCUBIC Photon workshop"),
                Path.Combine(programFilesX86, "ANYCUBIC Photon workshop"),
                Path.Combine(programFiles, "AnycubicSlicerNext"), // For Slicer Next
                Path.Combine(programFilesX86, "AnycubicSlicerNext") // For Slicer Next
            }, new[] { "ANYCUBIC Photon workshop.exe", "AnycubicSlicerNext.exe" });
            
            return detectedPaths;
        }

        // Add this static method to extract the icon from an executable
        public static Bitmap? GetExecutableIcon(string exePath)
        {
            if (string.IsNullOrEmpty(exePath) || !System.IO.File.Exists(exePath))
                return null;

            try
            {
                Icon? icon = Icon.ExtractAssociatedIcon(exePath);
                return icon?.ToBitmap();
            }
            catch
            {
                return null;
            }
        }
    }
}